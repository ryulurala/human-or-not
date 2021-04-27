const { makeID } = require("./utils");

class SessionManager {
  constructor() {
    this.sessions = new Map();
  }

  addSession(socket) {
    for (let i = 0; i < 1000; i++) {
      // try 1000 times
      const sessionID = makeID("123456789", 4);
      if (!this.sessions.has(sessionID)) {
        socket.id = sessionID;
        const session = new Session(socket, sessionID);
        this.sessions.set(sessionID, session);
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
  constructor(socket, clientID) {
    this.socket = socket;
    this.clientID = clientID;
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
