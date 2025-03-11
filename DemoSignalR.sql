-- Create database
CREATE DATABASE DemoSignalR;
GO

USE DemoSignalR;
GO

-- Users table
CREATE TABLE Users (
    UserId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- ChatRooms table
CREATE TABLE ChatRooms (
    RoomId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RoomName NVARCHAR(100) NOT NULL UNIQUE,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Messages table
CREATE TABLE Messages (
    MessageId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    RoomId UNIQUEIDENTIFIER NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    Timestamp DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Messages_Users FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    CONSTRAINT FK_Messages_ChatRooms FOREIGN KEY (RoomId) REFERENCES ChatRooms(RoomId) ON DELETE CASCADE
);

-- UserRooms table (Many-to-Many relationship between Users and ChatRooms)
CREATE TABLE UserRooms (
    UserId UNIQUEIDENTIFIER NOT NULL,
    RoomId UNIQUEIDENTIFIER NOT NULL,
    JoinedAt DATETIME DEFAULT GETDATE(),
    PRIMARY KEY (UserId, RoomId),
    CONSTRAINT FK_UserRooms_Users FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    CONSTRAINT FK_UserRooms_ChatRooms FOREIGN KEY (RoomId) REFERENCES ChatRooms(RoomId) ON DELETE CASCADE
);

-- Insert sample Users
INSERT INTO Users (UserId, Username, PasswordHash, Email) VALUES
	(NEWID(), 'user', '123456', 'user@example.com'),
    (NEWID(), 'user1', 'passwordhash1', 'user1@example.com'),
    (NEWID(), 'user2', 'passwordhash2', 'user2@example.com'),
    (NEWID(), 'user3', 'passwordhash3', 'user3@example.com');

-- Insert sample ChatRooms
INSERT INTO ChatRooms (RoomId, RoomName) VALUES
    (NEWID(), 'General'),
    (NEWID(), 'Tech Talk'),
    (NEWID(), 'Random');

INSERT INTO Messages (MessageId, SenderId, RoomId, Content, MessageType, SentAt, IsRead)
VALUES 
    (NEWID(), '1532238A-C6F6-4CCE-B339-455468CF7E7D', '3C82C142-9BF4-4720-AF9D-082828AE9E9F', 'Hello, how are you?', 'Text', GETDATE(), 0),
    (NEWID(), '25A9BA31-B0C1-4AAF-AD4A-36087FA39D55', '3C82C142-9BF4-4720-AF9D-082828AE9E9F', 'I am good, thanks!', 'Text', GETDATE(), 1),
    (NEWID(), '1532238A-C6F6-4CCE-B339-455468CF7E7D', 'A6A546AE-7176-4DA1-810E-70A18B7A7523', 'Did you check the update?', 'Text', GETDATE(), 0),
    (NEWID(), '4B0FC86A-63DA-47B2-BF4A-95DC147BE0E5', '5A3BA81B-522D-4AF1-BD07-A97B0151CFC7', 'Let’s meet at 5 PM.', 'Text', GETDATE(), 0),
    (NEWID(), 'BC619D98-0C26-4CA6-879F-B4B0303EB16A', 'A6A546AE-7176-4DA1-810E-70A18B7A7523', 'Yes, it looks great!', 'Text', GETDATE(), 1),
    (NEWID(), '1532238A-C6F6-4CCE-B339-455468CF7E7D', '5A3BA81B-522D-4AF1-BD07-A97B0151CFC7', 'Sharing an image', 'Image', GETDATE(), 0),
    (NEWID(), '25A9BA31-B0C1-4AAF-AD4A-36087FA39D55', '5A3BA81B-522D-4AF1-BD07-A97B0151CFC7', 'Here is the document.', 'File', GETDATE(), 1);

-- Insert sample UserRooms
INSERT INTO UserRooms (UserId, RoomId) 
SELECT U.UserId, C.RoomId 
FROM Users U CROSS JOIN ChatRooms C;


-- Update all user passwords to "123456" and compute the hash using SHA-256
UPDATE Users
SET PasswordHash = LOWER(CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '123456'), 2))
WHERE UserId IS NOT NULL;
