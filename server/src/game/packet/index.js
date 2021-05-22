const PACKET_ID = require("./pakcet-list.json");
const PacketManager = require("./packet-manager");
const PacketHandler = require("./packet-handler");

const initGamePacket = () => {
  const gamePacketManager = new PacketManager();

  return gamePacketManager;
};

const gamePacketManager = initGamePacket();

module.exports = gamePacketManager;
