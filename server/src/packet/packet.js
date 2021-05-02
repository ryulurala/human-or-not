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

class S_BroadcastEnterRoom extends Packet {
  playerId;

  constructor(playerId) {
    super(PACKET_ID.S_BroadcastEnterRoom);
  }
}

class S_BroadcastLeaveRoom extends Packet {
  playerId;

  constructor() {
    super(PACKET_ID.S_BroadcastLeaveRoom);
  }
}

class S_PlayerList extends Packet {
  players;

  constructor() {
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
