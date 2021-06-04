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

class C_LeaveRoom extends Packet {
  userId;

  constructor(id) {
    super(PACKET_ID.C_LeaveRoom);
    this.userId = id;
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

class S_Spawn extends Packet {
  users = [];

  constructor() {
    super(PACKET_ID.S_Spawn);
  }
}

class S_Despawn extends Packet {
  userIds = [];

  constructor() {
    super(PACKET_ID.S_Despawn);
  }
}

module.exports = {
  ID: PACKET_ID,
  C_CreateRoom,
  C_EnterRoom,
  C_LeaveRoom,
  S_CreateRoom,
  S_EnterRoom,
  S_Spawn,
  S_Despawn,
};
