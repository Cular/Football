//var host = 'localhost:5000';
var host = 'https://football-cular-api.herokuapp.com';
var TOKEN;

document.getElementById("authButton").addEventListener("click", function (event) {
    var username = document.getElementById("login").value;
    var password = document.getElementById("password").value;

    var xhr = new XMLHttpRequest();
    xhr.open('POST', host + '/token', true);

    xhr.setRequestHeader('Accept', 'application/json,text/plain,*/*');
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.setRequestHeader('Cache-Control', 'no-cache');

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var token = JSON.parse(xhr.response).access_token;
            //document.cookie = "footballToken=" + token + "; path=/;";
            TOKEN = token;

            hideAuth();
            getRooms();
            showRoomsForm();
        }
    };

    xhr.send('grant_type=password' + '&username=' + username + '&password=' + password);

    event.preventDefault();
});

function hideAuth() {
    document.getElementById("authform").style.display = "none";
}