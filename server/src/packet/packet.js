const PACKET_ID = require("./pakcet-list.json");

class Packet {
  constructor(protocol) {
    this.Protocol = protocol;
  }
}

module.exports = {
  PACKET_ID,
  Packet,
};
