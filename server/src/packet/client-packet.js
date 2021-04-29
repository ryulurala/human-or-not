const { PACKET_ID, Packet } = require("./packet");

class C_CreateRoom extends Packet {
  constructor() {
    super(PACKET_ID.C_CreateRoom);
  }
}

class C_EnterRoom extends Packet {
  constructor(roomId) {
    super(PACKET_ID.C_EnterRoom);
    this.roomId = roomId;
  }
}

module.exports = {
  C_CreateRoom,
  C_EnterRoom,
};
