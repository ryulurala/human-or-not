const sessionManager = require("../session");
const { Packet } = require("../packet"); // temp

const SESSION_LIMIT = 9999;

const init = (socket) => {
  if (sessionManager.totalSession > SESSION_LIMIT) {
    return false;
  } else if (!sessionManager.generate(socket)) {
    return false;
  } else {
    console.log(`Current number of sessions: ${sessionManager.totalSession}`);
    return true;
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
  if (!data.includes("Protocol") || !sessionManager.find(socket.id)) {
    return false;
  }

  const json = JSON.parse(data);
  console.log(json["Protocol"]);

  /**
   * @TODO
   * 로비에 대한 패킷을 핸들링.
   */

  return true;
};

module.exports = {
  init,
  clear,
  handle,
};
