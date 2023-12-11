"use strict";

var userRole = document.getElementById("userInput").value;

function formatDate(date) {
    const options = { hour: '2-digit', minute: '2-digit', day: '2-digit', month: '2-digit' };
    return new Date(date).toLocaleDateString("en-US", options);
}

var connection = new signalR.HubConnectionBuilder().withUrl("/volHub").build();

// Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, userRole, sentAt) {
    var li = document.createElement("li");
    li.textContent = `${userRole} à ${formatDate(sentAt)} a envoyé: ${message}`;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = userRole;
    var message = document.getElementById("messageInput").value;

    connection.invoke("EnvoyerMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("messageInput").value = ""; // Clear the input
    event.preventDefault();
});
