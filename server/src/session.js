const { makeId } = require("./utils");

class SessionManager {
  constructor() {
    this.sessions = new Map();
  }

  addSession(socket) {
    for (let i = 0; i < 1000; i++) {
      // try 1000 times
      const id = makeId("123456789", 4);
      if (!this.sessions.has(id)) {
        socket.id = id;
        const session = new Session(socket);
        this.sessions.set(id, session);
        return true;
      }
    }

    return false;
  }

  removeSession(socket, callback) {
    if (this.sessions.has(socket.id)) {
      const session = this.sessions.get(socket.id);
      session.clear();
      this.sessions.delete(socket.id);

      if (callback) callback();
      return true;
    }

    return false;
  }
}

class Session {
  constructor(socket) {
    this.socket = socket;
    this.room = null;
  }

  send(packet) {
    const json = JSON.stringify(packet);
    this.socket.send(json);
  }

  clear() {
    if (this.room) {
      this.room.removePlayer(session);
    }
    this.socket = null;
    this.room = null;
  }

  // manager한테 부탁?
}

// Singleton
const sessionManager = new SessionManager();
Object.freeze(sessionManager);

module.exports = {
  sessionManager,
};
