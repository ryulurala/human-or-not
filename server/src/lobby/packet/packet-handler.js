const Packet = require("./packet");

const C_CreateRoomHandler = (session, packet) => {
  // room id, player list 보내주기.

  const playerList = new Packet.S_PlayerList();
  playerList.players.push(session.id);

  session.send(playerList);
};

const C_EnterRoomHandler = (session, packet) => {
  const room = session.room;
};

module.exports = {
  C_CreateRoomHandler,
  C_EnterRoomHandler,
};
