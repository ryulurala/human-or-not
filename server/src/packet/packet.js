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

class S_BroadcastLeaveRoom extends Packet {
  constructor(playerId) {
    super(PACKET_ID.S_BroadcastLeaveRoom);

    this.playerId = playerId;
  }
}

class S_PlayerList extends Packet {
  constructor(playerId) {
    super(PACKET_ID.S_PlayerList);

    this.players = [];
  }
}

module.exports = {
  C_CreateRoom,
  C_EnterRoom,
  S_BroadcastEnterRoom,
  S_BroadcastLeaveRoom,
  S_PlayerList,
};
