const PACKET_ID = require("./packet-list.json");

class Packet {
  constructor(protocol) {
    this.Protocol = protocol;
  }
}

class S_BroadcastEnterRoom extends Packet {
  constructor(playerID) {
    super(PACKET_ID.S_BroadcastEnterRoom);
    this.playerID = playerID;
  }
}

module.exports = {
  PACKET_ID,
  S_BroadcastEnterRoom,
};
