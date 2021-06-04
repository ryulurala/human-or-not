class Room {
  constructor(id) {
    this.id = id;
    this.users = [];
  }

  addUser(newUser, callback) {
    if (!newUser) return;

    // 방에 플레이어 추가
    this.users.push(newUser);
    newUser.room = this;

    if (callback) callback(this.users);
  }

  removeUser(userId, callback) {
    // Find user
    const index = this.users.findIndex((user) => user.info.id === userId);
    if (index < 0) return;

    // remove
    const leaveUser = this.users[index];
    this.users.splice(index, 1);
    leaveUser.room = null;

    if (callback) callback(this.users);

    // // 본인에게 정보 전송
    // {
    //   // S_LeaveRoom Packet
    //   const leavePacket = new Packet.S_LeaveRoom();
    //   leaveUser.session.send(leavePacket);
    // }

    // // 타인에게 정보 전송
    // {
    //   // S_Despawn Packet
    //   const despawnPacket = new Packet.S_Despawn();
    //   despawnPacket.userIds.push(leaveUser.session.id);

    //   // Send
    //   for (const user of this.users) {
    //     if (user !== leaveUser) {
    //       user.session.send(despawnPacket);
    //     }
    //   }
    // }
  }
}

module.exports = Room;
