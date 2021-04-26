const PACKET_ID = require("./packet-list.json");

class S_BroadcastEnterRoom {
  constructor(playerId) {
    this.Protocol = PACKET_ID.S_BroadcastEnterRoom;
    this.playerId = playerId;
  }
}

module.exports = {
  PACKET_ID,
  S_BroadcastEnterRoom,
};
