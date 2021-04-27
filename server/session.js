const { makeId } = require("./utils");

class SessionManager {
  constructor() {
    this.sessions = new Map();
  }

  addSession(socket) {
    for (let i = 0; i < 1000; i++) {
      // try 1000 times
      const sessionId = makeId("123456789", 4);
      if (!this.sessions.has(sessionId)) {
        socket.id = sessionId;
        const session = new Session(socket, sessionId);
        this.sessions.set(sessionId, session);
        return true;
      }
    }

    return false;
  }

  removeSession(socket) {
    if (this.sessions.has(socket.id)) {
      this.sessions.delete(socket.id);
    }
  }
}

class Session {
  constructor(socket, clientId) {
    this.socket = socket;
    this.clientId = clientId;
  }

  Send(packet) {
    const json = JSON.stringify(packet);
    this.socket.Send(json);
  }
}

module.exports = {
  SessionManager,
  Session,
};
