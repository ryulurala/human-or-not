class SessionManager {
  // private
  #sessionId;
  #sessions;

  constructor() {
    this.#sessionId = 0;
    this.#sessions = new Map();
  }

  get sessionCount() {
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

class Session {
  // private
  #id;
  #socket;
  #room;

  constructor(id, socket) {
    this.#id = id;
    this.#socket = socket;
    this.#room = null;

    socket.id = id; // need to send on event
  }

  get id() {
    return this.#id;
  }

  get room() {
    return this.#room;
  }

  set room(room) {
    if (room) {
      this.#room = room;
    }
  }

  onConnected() {
    console.log(`Connected: ${this.#id}`);
  }

  onDisconnected() {
    if (this.#room) {
      // room에서 나가기
      this.#room.removePlayer(this);
      this.#room = null;
    }

    this.#id = null;
    this.#socket = null;
  }

  send(packet) {
    const str = JSON.stringify(packet);
    this.#socket.send(str);
    console.log(`On Send packet: ${str}`);
  }

  receive(packet, handleCallback) {
    const json = JSON.parse(packet);
    handleCallback(json);
    console.log(`On receive packet: ${packet}`);
  }
}

// Singleton
const sessionManager = new SessionManager();

module.exports = {
  sessionManager,
};
