
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/messagehub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

connection.on("Message", message => {
    const div = document.createElement("div");
    div.textContent = message;
    document.getElementById("messages").appendChild(div);
});

// Start the connection.
start();
