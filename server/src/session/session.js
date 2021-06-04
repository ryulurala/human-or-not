class Session {
  // private
  #id;
  #socket;

  constructor(id, socket) {
    this.#id = id;
    this.#socket = socket;

    socket.id = id; // need to send on event
  }

  get id() {
    return this.#id;
  }

  onConnected() {
    console.log(`Connected session id: ${this.#id}`);

    // TODO: DB에서 User 정보 가져오기
  }

  onDisconnected() {
    console.log(`Disconnected session id: ${this.#id}`);

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

module.exports = Session;
