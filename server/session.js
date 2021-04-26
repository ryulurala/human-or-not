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
  Session,
};
