class Room {
  constructor(roomID, playerID) {
    this.roomID = roomID;

    this.players = [];
    this.players.push(playerID);
  }

  addPlayer(playerID) {
    this.players.push(playerID);
  }

  removePlayer(playerID) {
    const idx = this.players.indexOf(playerID);
    this.players.splice(idx, 1);
  }
}

module.exports = {
  Room,
};
