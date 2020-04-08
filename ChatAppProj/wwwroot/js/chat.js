function ToggleInput() {
    var x = document.getElementById("inputsendmessage");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}
var joinChannel = function () {
    var url = '/Chat/JoinChannel/' + _connectionId + '/@Model.Id'
    axios.post(url, null)
        .then(res => {
            console.log("Room Joined!", res);
        })
        .catch(err => {
            console.log("Failed to join room!", err);
        })
}


    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();

    connection.start()
        .then(function () {
            connection.invoke('getConnectionId')
                .then(function (connectionId) {
                    _connectionId = connectionId
                    ToggleInput();
                    joinChannel();
                })

        })
        .catch(function (err) {
            console.log(err)
        })



var _connectionId = '';

connection.on("ReceiveMessage", function (data) {
    console.log(data);

    var message = document.createElement("div")
    message.classList.add('message')

    var header = document.createElement("header")
    header.appendChild(document.createTextNode(data.name))

    var p = document.createElement("p")
    p.appendChild(document.createTextNode(data.text))

    var footer = document.createElement("footer")
    footer.appendChild(document.createTextNode(data))

    message.appendChild(header);
    message.appendChild(p);
    message.appendChild(footer);

    document.querySelector('.chat-body').append(message);


    var sendMessage = function (event) {
        event.preventDefault();

        var data = new FormData(event.target);
        document.getElementById('message-input').value = '';
        axios.post('/Chat/SendMessage', data)
            .then(res => {
                console.log("Message Sent!")

            })
            .catch(err => {
                console.log("Failed to send message")

            })

    });




  