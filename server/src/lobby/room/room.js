const { Packet } = require("../packet");
const { S_Spawn } = require("../packet/packet");

class Room {
  constructor(roomId) {
    this.roomId = roomId;
    this.users = [];
  }

  addUser(newUser) {
    if (!newUser) return;

    // 방에 플레이어 추가
    this.users.push(newUser);
    newUser.room = this;

    // 본인에게 정보 전송
    {
      // S_EnterRoom Packet
      const enterPacket = new Packet.S_EnterRoom();
      enterPacket.user = newUser.info;
      newUser.session.send(enterPacket);

      // S_Spawn Packet
      const spawnPacket = new Packet.S_Spawn();
      for (const user of this.users) {
        if (user !== newUser) {
          spawnPacket.users.push(user.info);
        }
      }
      newUser.session.send(spawnPacket);
    }

    // 타인에게 정보 전송
    {
      // S_Spawn Packet
      const spawnPacket = new Packet.S_Spawn();
      spawnPacket.users.push(newUser.info); // 새로운 유저 정보만
      for (const user of this.users) {
        if (user != newUser) {
          user.session.send(spawnPacket);
        }
      }
    }
  }

  removeUser(userId) {
    // Find user
    const index = this.users.findIndex((user) => user.session.id === userId);
    if (index < 0) return;

    // remove
    const leaveUser = this.users[index];
    this.users.splice(index, 1);
    leaveUser.room = null;

    // 본인에게 정보 전송
    {
      // S_LeaveRoom Packet
      const leavePacket = new Packet.S_LeaveRoom();
      leaveUser.session.send(leavePacket);
    }

    // 타인에게 정보 전송
    {
      // S_Despawn Packet
      const despawnPacket = new Packet.S_Despawn();
      despawnPacket.userIds.push(leaveUser.session.id);
      for (const user of this.users) {
        if (user !== leaveUser) {
          user.session.send(despawnPacket);
        }
      }
    }
  }

  sendBroadcast(data) {
    this.users.forEach((session) => {
      session.send(data);
    });
  }

  clear() {
    this.users.forEach((session) => {
      // session 다 나가기
      session.clear();
    });
    this.roomId = null;
    this.users = null;
  }
}

module.exports = Room;
