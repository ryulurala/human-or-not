const ws = require("ws");
const { initSocket, clearSocket, handlePacket } = require("./server");

const wss = new ws.Server({ port: 9536 }, () => {
  console.log("Server Started...");
});

wss.on("connection", (socket) => {
  if (!initSocket(socket)) {
    socket.close(1013, "Try Again Later");
    return;
  }
  console.log(`connected client: ${socket.id}`);

  socket.onclose = () => {
    // 연결 종료 시
    clearSocket(socket);

    console.log("Closed");
  };

  socket.onerror = (err) => {
    // 에러날 경우
    clearSocket(socket);
    socket.close(1011, "Internal Server Error");

    console.error(err);
  };

  socket.onmessage = (message) => {
    // 패킷 받을 경우
    const data = message.data;
    if (data.includes("Protocol")) {
      const json = JSON.parse(data);
      handlePacket(socket, json);
    } else {
      clearSocket(socket);
      socket.close(1002, "Bad Request");

      console.log(`Bad Request data: ${data}`);
    }
  };
});
