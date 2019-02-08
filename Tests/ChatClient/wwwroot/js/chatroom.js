﻿function getRooms() {

    var xhr = new XMLHttpRequest();
    xhr.open('GET', 'http://' + host + '/api/games', true);
    
    xhr.setRequestHeader('Accept', 'application/json,text/plain,*/*');
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.setRequestHeader('Cache-Control', 'no-cache');
    xhr.setRequestHeader('Authorization', "Bearer " + TOKEN);

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var gameList = JSON.parse(xhr.response);

            for (var i = 0; i < gameList.length; i++) {

                var li = document.createElement("li");
                li.textContent = gameList[i].id;
                li.addEventListener("click", onGameRoomClick);

                document.getElementById("roomsList").appendChild(li);
            }
            
        }
    };
    xhr.send();
}

function showRoomsForm() {
    document.getElementById("chatroomform").style.display = "block";
}

function hideRoomsForm() {
    document.getElementById("chatroomform").style.display = "none";
}

function onGameRoomClick(event) {
    event.preventDefault();

    var gameId = event.currentTarget.textContent;
    buildConnection(gameId);

    hideRoomsForm();
    showChatForm();
}