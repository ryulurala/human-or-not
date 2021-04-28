const PACKET_ID = {
  S_Connected: 1,
  C_CreateRoom: 2,
  C_EnterRoom: 3,
  S_BroadcastEnterRoom: 4,
};

class Packet {
  constructor(protocol) {
    this.Protocol = protocol;
  }
}

class S_Connected extends Packet {
  constructor(playerId) {
    super(PACKET_ID.S_Connected);

    this.playerId = playerId;
  }
}

class C_CreateRoom extends Packet {
  constructor() {
    super(PACKET_ID.C_CreateRoom);
  }
}

class C_EnterRoom extends Packet {
  constructor(roomId) {
    super(PACKET_ID.C_EnterRoom);
    this.roomId = roomId;
  }
}

class S_BroadcastEnterRoom extends Packet {
  constructor(playerId) {
    super(PACKET_ID.S_BroadcastEnterRoom);
    this.playerId = playerId;
  }
}

module.exports = {
  PACKET_ID,
  S_Connected,
  C_CreateRoom,
  C_EnterRoom,
  S_BroadcastEnterRoom,
};
