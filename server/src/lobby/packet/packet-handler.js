const roomManager = require("../room");
const userManager = require("../user");

const C_CreateRoomHandler = (session, packet) => {
  // room 만들기
  const room = roomManager.createRoom();
  if (!room) return;

  // user 만들기
  const user = userManager.createUser(session);
  if (!user) {
    roomManager.removeRoom(room.id);
    return;
  }

  user.name = packet.userName;

  // room에 추가
  room.addUser(user);
};

const C_EnterRoomHandler = (session, packet) => {
  // roomId 검색
  const room = roomManager.findRoom(packet.roomId);
  if (!room) return;

  // user 만들기
  const user = userManager.createUser(session);
  if (!user) return;

  user.name = packet.userName;

  // room에 추가
  room.addUser(user);
};

module.exports = {
  C_CreateRoomHandler,
  C_EnterRoomHandler,
};
