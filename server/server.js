{
  const ws = require("ws");

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
}

// ------------------Handling--------------------
const { PACKET_ID, S_BroadcastEnterRoom } = require("./Packet/packet");
const { RoomManager } = require("./room");
const { SessionManager } = require("./session");

const roomManager = new RoomManager();
const sessionManager = new SessionManager();

function initSocket(socket) {
  // Create CLient ID
  if (!sessionManager.addSession(socket)) {
    return false;
  }

  console.log(`current sessions: ${sessionManager.sessions.size}`);
  return true;
}

function clearSocket(socket) {
  sessionManager.removeSession(socket);

  console.log(`Clear socket: ${socket.id}`);
}

function handlePacket(socket, data) {
  switch (data["Protocol"]) {
    case PACKET_ID.C_CreateRoom:
      console.log(`CreateRoom: sessionID(${socket.id})`);
      break;
    case PACKET_ID.C_EnterRoom:
      console.log(`EnterRoom: sessionID(${socket.id})`);
      break;
  }
}
