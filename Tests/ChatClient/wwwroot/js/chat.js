﻿//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
var roomId;

function buildConnection(gameId) {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("http://" + host + "/hubs/chat", { accessTokenFactory: () => TOKEN })
        .build();

    roomId = gameId;

    connection.on("ReceiveMessage", function (messageDto) {
        var encodedMsg = messageDto.time + " " + messageDto.alias + ": " + messageDto.body;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.on("FillChatOnLoad", fillChatOnLoad);

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
        connection.invoke("JoinGameChat", gameId);
        connection.invoke("GetMessages", gameId, null, 20);
        
    }).catch(err => console.error(err.toString()));

    document.getElementById("sendButton").addEventListener("click", function (event) {
        var message = document.getElementById("messageInput").value;
        var dto = { gameId: roomId, body: message };

        connection.invoke("SendMessage", dto).catch(function (err) {
            console.error(err.toString());
        });
        event.preventDefault();
    });
}

function showChatForm() {
    document.getElementById("chatform").style.display = "block";
}

function hideChatForm() {
    document.getElementById("chatform").style.display = "none";
}

function fillChatOnLoad(listOfMessages) {
    var s = listOfMessages;

    for (var i = 0; i < listOfMessages.length; i++) {
        var encodedMsg = listOfMessages[i].time + " " + listOfMessages[i].alias + ": " + listOfMessages[i].body;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    }
}