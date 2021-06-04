const roomManager = require("../room");
const userManager = require("../user");
const Packet = require("./packet");

const C_CreateRoomHandler = (session, packet) => {
  // room 만들기
  const room = roomManager.createRoom();
  if (!room) return;

  // user 만들기
  const newUser = userManager.createUser(session.id);
  if (!newUser) {
    roomManager.removeRoom(room.id);
    return;
  }

  newUser.session = session;
  newUser.info.name = packet.userName;

  // room에 추가: 순환 참조로 인해 callback으로 넘김
  room.addUser(newUser, (users) => {
    // 본인에게 정보 전송(info, roomId)

    // S_CreateRoom Packet
    const createPacket = new Packet.S_CreateRoom();
    createPacket.user = newUser.info;
    createPacket.roomId = room.id;

    newUser.session.send(createPacket);
  });
};

const C_EnterRoomHandler = (session, packet) => {
  // roomId 검색
  const room = roomManager.findRoom(packet.roomId);
  if (!room) return;

  // user 만들기
  const newUser = userManager.createUser(session.id);
  if (!newUser) return;

  newUser.session = session;
  newUser.info.name = packet.userName;

  // room에 추가: 순환 참조로 인해 callback으로 넘김
  room.addUser(newUser, (users) => {
    // 본인에게 정보 전송
    {
      // S_EnterRoom Packet
      const enterPacket = new Packet.S_EnterRoom();
      enterPacket.user = newUser.info;

      newUser.session.send(enterPacket);

      // S_Spawn Packet
      const spawnPacket = new Packet.S_Spawn();
      for (const user of users) {
        if (user !== newUser) {
          spawnPacket.users.push(user.info);
        }
      }

      // Send
      newUser.session.send(spawnPacket);
    }

    // 타인에게 정보 전송
    {
      // S_Spawn Packet
      const spawnPacket = new Packet.S_Spawn();
      spawnPacket.users.push(newUser.info); // 새로운 유저 정보만

      // Send
      for (const user of users) {
        if (user !== newUser) {
          user.session.send(spawnPacket);
        }
      }
    }
  });
};

const C_LeaveRoomHandler = (session, packet) => {};

module.exports = {
  C_CreateRoomHandler,
  C_EnterRoomHandler,
  C_LeaveRoomHandler,
};
