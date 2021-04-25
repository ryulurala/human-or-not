const PacketId = require("./Packet/packet.json");
const util = require("./utils");
const ws = require("ws");
const { makePacket } = require("./Packet/packet");
const wss = new ws.Server({ port: 9536 }, () => {
  console.log("Server Started...");
});

const ClientIdSet = new Set();

wss.on("connection", (socket) => {
  if (InitClientId(socket) === false) {
    socket.close(1013, "Try Again Later");
  }
  console.log(`connected current clients: ${ClientIdSet.size}`);
  socket.send(makePacket(PacketId.S_ConnectedClient, socket));

  // id 받으면 superClient로 왔는지 client로 왔는지

  socket.onerror = (err) => {
    // Client와 Error났을 경우
    console.error(err);
  };

  socket.onopen = () => {
    // Client와 연결됐을 경우
    console.log("opened !");
  };

  socket.onmessage = (message) => {
    // Client로부터 메시지 수신 시
    // switch (msg.event) {
    //   case "hi":
    //     console.log(msg.data);
    //     break;
    // }
    console.log(`message received: ${message.data}`);
  };

  socket.onclose = () => {
    // 연결 종료 시
    console.log("closed !");
    ClientIdSet.delete(socket.id);
  };
});

const InitClientId = (socket) => {
  for (let i = 0; i < 1000; i++) {
    const id = util.makeId("123456789", 4);
    if (ClientIdSet.has(id) === false) {
      ClientIdSet.add(id);
      socket.id = id;
      return true;
    }
  }
  return false;
};
