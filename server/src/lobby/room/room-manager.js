const Room = require("./room");
const { util } = require("../../utils");

class RoomManager {
  constructor() {
    this.rooms = new Map();
  }

  createRoom() {
    // 1000번만 시도
    for (let i = 0; i < 1000; i++) {
      const roomId = util.makeId("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", 5);

      if (!this.rooms.has(roomId)) {
        const room = new Room(roomId);
        this.rooms.set(roomId, room);

        return room;
      }
    }

    return null;
  }

  removeRoom(roomId) {
    if (!this.rooms.has(roomId)) return;

    this.rooms.delete(roomId);
  }

  findRoom(roomId) {
    const room = this.rooms.get(roomId);
    if (!room) return null;

    return room;
  }
}

// Singleton
module.exports = new RoomManager();
