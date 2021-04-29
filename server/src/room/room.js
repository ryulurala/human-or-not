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

module.exports = Room;
