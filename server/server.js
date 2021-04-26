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
        handlePacket(socket, json["Protocol"]);
      } else {
        clearSocket(socket);
        socket.close(1002, "Bad Request");

        console.log(`Bad Request data: ${data}`);
      }
    };
  });
}

// ------------------Handling--------------------
const { makeID } = require("./utils");
const { PACKET_ID, S_BroadcastEnterRoom } = require("./Packet/packet");
const { Room } = require("./room");
const { Session } = require("./session");

const clients = new Map(); // Session Manager??
const rooms = new Map(); // room Manager??

function initSocket(socket) {
  // Create CLient ID
  if (!addClient(socket)) return false;

  console.log(`current clients: ${clients.size}`);
  return true;
}

function clearSocket(socket) {
  removeClient(socket);

  console.log(`Clear socket: ${socket.id}`);
}

function addClient(socket) {
  for (let i = 0; i < 1000; i++) {
    // Try 1000 times
    const id = makeID("123456789", 4);
    if (!clients.has(id)) {
      socket.id = id;
      clients.set(id, new Session(socket, id));
      return true;
    }
  }

  return false;
}

function removeClient(socket) {
  if (clients.has(socket.id)) clients.delete(socket.id);
}

function handlePacket(socket, protocol) {
  switch (protocol) {
    case PACKET_ID.C_CreateRoom:
      console.log(`CreateRoom: clientID(${socket.id})`);
      break;
    case PACKET_ID.C_EnterRoom:
      console.log(`EnterRoom: clientID(${socket.id})`);
      break;
  }
}
