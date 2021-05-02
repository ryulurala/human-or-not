class PacketManager {
  #handlers;

  constructor() {
    this.#handlers = new Map();
  }

  Register(protocol, handler) {
    this.#handlers.set(protocol, handler);
  }

  handle(session, packet) {
    const handler = this.#handlers.get(packet.Protocol);
    if (handler) handler(session, packet);
  }
}

module.exports = PacketManager;
