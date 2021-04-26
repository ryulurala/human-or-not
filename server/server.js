{
  const ws = require("ws");

  const wss = new ws.Server({ port: 9536 }, () => {
    console.log("Server Started...");
  });

  wss.on("connection", (socket) => {
    if (initSocket(socket) === false) {
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
const { PACKET_ID } = require("./Packet/packet");

const clientIDs = new Set();

function initSocket(socket) {
  // Create CLient ID
  if (createClientID(socket) === false) return false;

  console.log(`current clients: ${clientIDs.size}`);
  return true;
}

function clearSocket(socket) {
  deleteClientID(socket);

  console.log(`Clear socket: ${socket.id}`);
}

function createClientID(socket) {
  for (let i = 0; i < 1000; i++) {
    const id = makeID("123456789", 4);
    if (clientIDs.has(id) === false) {
      clientIDs.add(id);
      socket.id = id;
      return true;
    }
  }

  return false;
}

function deleteClientID(socket) {
  if (clientIDs.has(socket.id)) clientIDs.delete(socket.id);
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
