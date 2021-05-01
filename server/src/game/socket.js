const ws = require("ws");
const { init, clear, handle } = require("./main");

module.exports = (server) => {
  const wss = new ws.Server({ server });

  wss.on("connection", (socket) => {
    // 연결 성공할 경우
    if (!init(socket)) {
      socket.close(1013, "Try Again Later");
      console.log("Initialization failed!");
      return;
    }

    socket.onclose = () => {
      // 연결 종료할 경우
      clear(socket);
      console.log("Closed client");
    };

    socket.onerror = (err) => {
      // 에러날 경우
      clear(socket, () => socket.close(1011, "Internal Server Error"));
      console.error(err);
    };

    socket.onmessage = (message) => {
      // 패킷 받을 경우
      if (handle(socket, message.data)) {
        console.log(`OnMessage: ${message.data}`);
      } else {
        clear(socket, () => socket.close(1002, "Bad Request"));
        console.log(`Bad Request data: ${message.data}`);
      }
    };
  });
};
