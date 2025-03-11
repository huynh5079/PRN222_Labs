"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    li.textContent = `${user}: ${message}`;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.start().then(() => {
    console.log("Connected to SignalR");
}).catch(err => console.error(err));

function sendMessage(roomId, message) {
    connection.invoke("SendMessage", roomId, "Me", message).catch(err => console.error(err));
}

connection.on("ReceiveMessage", (user, message) => {
    const chatBox = document.getElementById("chatMessages");
    chatBox.innerHTML += `<p><strong>${user}:</strong> ${message}</p>`;
});
