const PACKET_ID = require("./pakcet-list.json");

class Packet {
  constructor(protocol) {
    this.Protocol = protocol;
  }
}

class C_CreateRoom extends Packet {
  constructor() {
    super(PACKET_ID.C_CreateRoom);
  }
}

class C_EnterRoom extends Packet {
  roomId;

  constructor() {
    super(PACKET_ID.C_EnterRoom);
  }
}

class S_EnterRoom extends Packet {
  user;

  constructor() {
    super(PACKET_ID.S_EnterRoom);
  }
}

class S_LeaveRoom extends Packet {
  constructor() {
    super(PACKET_ID.S_LeaveRoom);
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
  S_EnterRoom,
  S_LeaveRoom,
  S_Spawn,
  S_Despawn,
};
