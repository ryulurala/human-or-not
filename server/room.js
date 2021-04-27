const { makeID } = require("./utils");

class RoomManager {
  constructor() {
    this.rooms = new Map();
  }

  createRoom(socket) {
    for (let i = 0; i < 1000; i++) {
      const roomID = makeID("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", 5);
      if (!this.rooms.has(roomID)) {
        const room = new Room(roomID);
        room.addPlayer(socket.id);
        this.rooms.set(roomID, room);
        return true;
      }
    }

    return false;
  }

  removeRoom() {}
}

class Room {
  constructor(roomID) {
    this.roomID = roomID;
    this.players = [];
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
  RoomManager,
  Room,
};
