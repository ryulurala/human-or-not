class SessionManager {
  constructor() {
    this.sessionId = 0;
    this.sessions = new Map();
  }

  generate(socket) {
    const sessionId = ++this.sessionId; // 무한 서버 [X]
    const session = new Session(sessionId, socket);
    this.sessions.set(sessionId, session);

    console.log(`Generate session: ${sessionId}`);

    return session;
  }

  remove(socket, closeCallback) {
    this.sessions.delete(socket.id);
    if (closeCallback) closeCallback();
  }
}

class Session {
  constructor(id, socket) {
    this.id = id;
    this.socket = socket;
    this.room = null;

    socket.id = id; // need to send on event
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

  callbackClear() {
    sessionManager.removeSession(this.socket);
  }
}

// Singleton
const sessionManager = new SessionManager();

module.exports = {
  sessionManager,
};
