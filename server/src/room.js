const { makeId } = require("./utils");

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

class Room {
  constructor(roomId) {
    this.roomId = roomId;
    this.sessions = [];
  }

  addPlayer(session) {
    if (this.sessions.includes(session)) return false;

    this.sessions.push(session);
    return true;
  }

  removePlayer(session) {
    const idx = this.sessions.indexOf(session);
    if (idx < 0) return false;

    this.sessions.splice(idx, 1);
    return true;
  }

  sendBroadcast(data) {
    this.sessions.forEach((session) => {
      session.send(data);
    });
  }

  clear() {
    this.sessions.forEach((session) => {
      // session 다 나가기
      session.clear();
    });
    this.roomId = null;
    this.sessions = null;
  }
}

// Singleton
const roomManager = new RoomManager();
Object.freeze(roomManager);

module.exports = {
  roomManager,
};
