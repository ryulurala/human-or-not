const PACKET_ID = require("./pakcet-list.json");
const PacketManager = require("./packet-manager");
const PacketHandler = require("./packet-handler");

const initLobbyPacket = () => {
  const lobbyPacketManager = new PacketManager();

  lobbyPacketManager.Register(
    PACKET_ID.C_CreateRoom,
    PacketHandler.C_CreateRoomHandler
  );

  lobbyPacketManager.Register(
    PACKET_ID.C_EnterRoom,
    PacketHandler.C_EnterRoomHandler
  );

  return lobbyPacketManager;
};

const initGamePacket = () => {
  const gamePacketManager = new PacketManager();

  return gamePacketManager;
};

const lobbyPacketManager = initLobbyPacket();
const gamePacketManager = initGamePacket();

module.exports = {
  lobbyPacketManager,
  gamePacketManager,
};
