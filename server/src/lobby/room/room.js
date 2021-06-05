const {
  S_CreateRoom,
  S_EnterRoom,
  S_UserList,
  S_LeaveRoom,
} = require("../packet/packet");

class Room {
  // private
  #id;
  #users;

  constructor(id) {
    this.#id = id;
    this.#users = [];
  }

  get id() {
    return this.#id;
  }

  get userCount() {
    return this.#users.length;
  }

  addUser(newUser) {
    if (!newUser) return;

    // 방에 플레이어 추가
    this.#users.push(newUser);
    newUser.room = this;
    const newUserInfo = { id: newUser.id, name: newUser.name };

    if (this.#users.length > 1) {
      // 방원으로 접속
      {
        // 본인에게 정보 전송
        const enterPacket = new S_EnterRoom();
        enterPacket.user = newUserInfo;

        // Send
        newUser.session.send(enterPacket);

        const userListPacket = new S_UserList();
        for (const user of this.#users) {
          if (user !== newUser) {
            userListPacket.users.push(newUserInfo);
          }
        }

        // Send
        newUser.session.send(userListPacket);
      }

      {
        // 타인에게 정보 전송
        const userListPacket = new S_UserList();
        userListPacket.users.push(newUserInfo); // 새로운 유저 정보만

        // Send
        for (const user of this.#users) {
          if (user !== newUser) {
            user.session.send(userListPacket);
          }
        }
      }
    } else {
      // 방장으로 접속
      {
        // 본인에게 정보 전송(info, roomId)
        const createPacket = new S_CreateRoom();
        createPacket.user = newUserInfo;
        createPacket.roomId = this.#id;

        newUser.session.send(createPacket);
      }
    }
  }

  removeUser(userId) {
    // Find user
    const index = this.#users.findIndex((user) => user.id === userId);
    if (index < 0) return;

    // remove
    const leaveUser = this.#users[index];
    this.#users.splice(index, 1);
    leaveUser.room = null;

    {
      // 타인에게 정보 전송
      const despawnPacket = new S_LeaveRoom();
      despawnPacket.user = { id: leaveUser.id, name: leaveUser.name };

      // Send
      for (const user of this.#users) {
        if (user !== leaveUser) {
          user.session.send(despawnPacket);
        }
      }
    }
  }
}

module.exports = Room;
