const Session = require("./session");

class SessionManager {
  // private
  #sessionId;
  #sessions;

  constructor() {
    this.#sessionId = 0;
    this.#sessions = new Map();
  }

  get totalSession() {
    return this.#sessions.size;
  }

  generate(socket) {
    const sessionId = ++this.#sessionId; // 무한 서버 [X]
    const session = new Session(sessionId, socket);
    this.#sessions.set(sessionId, session);
    session.onConnected();

    return session;
  }

  destroy(socket) {
    const session = this.#sessions.get(socket.id);
    if (session) {
      this.#sessions.delete(socket.id);
      session.onDisconnected();
    }
  }

  find(id) {
    if (this.#sessions.has(id)) {
      return this.#sessions.get(id);
    } else {
      return null;
    }
  }
}

// Singleton
module.exports = new SessionManager();
