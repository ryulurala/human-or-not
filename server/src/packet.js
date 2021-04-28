const PACKET_ID = {
  C_CreateRoom: 1,
  C_EnterRoom: 2,
  S_PlayerOrder: 3,
  S_BroadcastEnterRoom: 4,
};

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
  constructor(roomId) {
    super(PACKET_ID.C_EnterRoom);
    this.roomId = roomId;
  }
}

class S_PlayerOrder extends Packet {
  constructor(playerId, order) {
    super(PACKET_ID.S_PlayerOrder);

    this.playerId = playerId;
    this.order = order;
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
  S_PlayerOrder,
  C_CreateRoom,
  C_EnterRoom,
  S_BroadcastEnterRoom,
};
