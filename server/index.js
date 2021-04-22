const WebSocket = require("ws");
const wss = new WebSocket.Server({ port: 9536 }, () => {
  console.log("Server Started...");
});

wss.on("connection", (ws, req) => {
  console.log("connected !");

  ws.onerror = (err) => {
    // Client와 Error났을 경우
    console.error(err);
  };

  ws.onopen = () => {
    // Client와 연결됐을 경우
    console.log("opened !");
  };

  ws.onmessage = (msg) => {
    // Client로부터 메시지 수신 시
    switch (msg.event) {
      case "hi":
        console.log(msg.data);
        break;
    }
    // console.log(`message received: ${msg.data}`);
  };

  ws.onclose = () => {
    // 연결 종료 시
    console.log("closed !");
  };
});
