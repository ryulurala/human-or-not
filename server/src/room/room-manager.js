const { makeId } = require("../utils");
const Room = require("./room");

class RoomManager {
  constructor() {
    this.rooms = new Map();
  }

  createRoom(session) {
    for (let i = 0; i < 1000; i++) {
      const roomId = makeId("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", 5);
      if (!this.rooms.has(roomId)) {
        const room = new Room(roomId);
        room.addPlayer(session);
        this.rooms.set(roomId, room);
        return true;
      }
    }

    return false;
  }

  removeRoom(roomId) {
    if (this.rooms.has(roomId)) {
      const room = this.rooms.get(roomId);
      room.clear();
      this.rooms.delete(roomId);
      return true;
    }

    return false;
  }
}

// Singleton
module.exports = new RoomManager();
