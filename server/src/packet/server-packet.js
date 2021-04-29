const { PACKET_ID, Packet } = require("./packet");

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
  S_PlayerOrder,
  S_BroadcastEnterRoom,
};
