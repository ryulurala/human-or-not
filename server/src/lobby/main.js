const sessionManager = require("../session");
const { lobbyPacketManager: packetManager } = require("./packet");

const SESSION_LIMIT = 9999;

const init = (socket, callback) => {
  if (sessionManager.totalSession > SESSION_LIMIT) {
    socket.close(1013, "Try Again Later");
    console.log(`Exceeded maximum session count`);
  } else if (!sessionManager.generate(socket)) {
    socket.close(1013, "Try Again Later");
    console.log(`Session generation failed`);
  } else {
    callback();
    console.log(`Current number of sessions: ${sessionManager.totalSession}`);
  }
};

const clear = (socket, callback) => {
  sessionManager.destroy(socket, callback);
  if (callback) {
    callback();
  }
  console.log(`Current number of sessions: ${sessionManager.totalSession}`);
};

const handle = (socket, data) => {
  if (!data.includes("Protocol")) {
    return false;
  } else {
    const session = sessionManager.find(socket.id);
    if (!session) return false;

    // handle packet
    const json = JSON.parse(data);
    packetManager.handle(session, json);

    return true;
  }
};

module.exports = {
  init,
  clear,
  handle,
};
