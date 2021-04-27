const { makeId } = require("./utils");

class RoomManager {
  constructor() {
    this.rooms = new Map();
  }

  createRoom(socket) {
    for (let i = 0; i < 1000; i++) {
      const roomId = makeId("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", 5);
      if (!this.rooms.has(roomId)) {
        const room = new Room(roomId);
        room.addPlayer(socket.id);
        this.rooms.set(roomId, room);
        return true;
      }
    }

    return false;
  }

  removeRoom() {}
}

class Room {
  constructor(roomId) {
    this.roomId = roomId;
    this.players = [];
  }

  addPlayer(playerId) {
    this.players.push(playerId);
  }

  removePlayer(playerId) {
    const idx = this.players.indexOf(playerId);
    this.players.splice(idx, 1);
  }
}

module.exports = {
  RoomManager,
  Room,
};
