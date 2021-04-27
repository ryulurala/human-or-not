const { PACKET_ID } = require("./packet");
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
  switch (data.Protocol) {
    case PACKET_ID.C_CreateRoom:
      {
        roomManager.createRoom(socket);
        console.log(`CreateRoom: sessionId(${socket.id})`);
      }
      break;
    case PACKET_ID.C_EnterRoom:
      {
        console.log(`EnterRoom: sessionId(${socket.id}), ${data.roomId}`);
      }
      break;
  }
}

module.exports = {
  initSocket,
  clearSocket,
  handlePacket,
};
