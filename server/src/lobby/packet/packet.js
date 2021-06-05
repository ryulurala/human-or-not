const PACKET_ID = require("./pakcet-list.json");

class Packet {
  constructor(protocol) {
    this.Protocol = protocol;
  }
}

class C_CreateRoom extends Packet {
  userName;

  constructor() {
    super(PACKET_ID.C_CreateRoom);
  }
}

class C_EnterRoom extends Packet {
  userName;
  roomId;

  constructor() {
    super(PACKET_ID.C_EnterRoom);
  }
}

class S_CreateRoom extends Packet {
  user;
  roomId;

  constructor() {
    super(PACKET_ID.S_CreateRoom);
  }
}

class S_EnterRoom extends Packet {
  user;
  roomId;

  constructor() {
    super(PACKET_ID.S_EnterRoom);
  }
}

class S_LeaveRoom extends Packet {
  user;

  constructor() {
    super(PACKET_ID.S_LeaveRoom);
  }
}

class S_UserList extends Packet {
  users = [];

  constructor() {
    super(PACKET_ID.S_UserList);
  }
}

module.exports = {
  ID: PACKET_ID,
  C_CreateRoom,
  C_EnterRoom,
  S_CreateRoom,
  S_EnterRoom,
  S_LeaveRoom,
  S_UserList,
};
