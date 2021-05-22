const Packet = require("./packet");
const PacketManager = require("./packet-manager");
const PacketHandler = require("./packet-handler");

const initLobbyPacket = () => {
  const lobbyPacketManager = new PacketManager();

  lobbyPacketManager.Register(
    Packet.ID.C_CreateRoom,
    PacketHandler.C_CreateRoomHandler
  );

  lobbyPacketManager.Register(
    Packet.ID.C_EnterRoom,
    PacketHandler.C_EnterRoomHandler
  );

  return lobbyPacketManager;
};

const lobbyPacketManager = initLobbyPacket();

module.exports = {
  lobbyPacketManager,
  Packet,
};
