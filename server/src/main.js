const { PACKET_ID, S_BroadcastEnterRoom } = require("./packet");
const { roomManager } = require("./room");
const { sessionManager } = require("./session");

function initSocket(socket) {
  // Create CLient ID
  if (!sessionManager.addSession(socket)) {
    return false;
  }

  console.log(`Current number of sessions: ${sessionManager.sessions.size}`);
  return true;
}

function clearSocket(socket) {
  sessionManager.removeSession(socket);

  console.log(`Current number of sessions: ${sessionManager.sessions.size}`);
}

function handlePacket(socket, data) {
  if (!sessionManager.sessions.has(socket.id)) return;

  const session = sessionManager.sessions.get(socket.id);

  switch (data.Protocol) {
    case PACKET_ID.C_CreateRoom:
      {
        roomManager.createRoom(session);
        console.log(`Current number of rooms: ${roomManager.rooms.size}`);
      }
      break;
    case PACKET_ID.C_EnterRoom:
      {
        if (roomManager.rooms.has(data.roomId)) {
          // 있으면
        } else {
          // 없으면
        }
        console.log(
          `EnterRoom: sessionId(${session.socket.id}), ${data.roomId}`
        );
      }
      break;
  }
}

module.exports = {
  initSocket,
  clearSocket,
  handlePacket,
};
