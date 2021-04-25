const PacketId = require("./packet.json");

const makePacket = (packetId, data) => {
  switch (packetId) {
    case PacketId.S_ConnectedClient:
      return JSON.stringify(new S_ConnectedClientId(data.id));
    case PacketId.C_CreateRoom:
      break;
    case PacketId.C_EnterRoom:
      break;
    case PacketId.S_BroadcastEnterRoom:
      break;
  }
};

class S_ConnectedClientId {
  constructor(playerId) {
    this.Protocol = PacketId.S_ConnectedClient;
    this.playerId = playerId;
  }
}

module.exports = {
  makePacket,
};
