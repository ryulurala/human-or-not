const { sessionManager } = require("./session");
const { S_PlayerOrder } = require("./packet"); // temp

const SESSION_LIMIT = 9999;

function init(socket) {
  if (sessionManager.sessionCount > SESSION_LIMIT) {
    return false;
  } else if (!sessionManager.generate(socket)) {
    return false;
  } else {
    console.log(`Current number of sessions: ${sessionManager.sessionCount}`);
    return true;
  }
}

function clear(socket, callback) {
  sessionManager.destroy(socket, callback);
  if (callback) {
    callback();
  }
  console.log(`Current number of sessions: ${sessionManager.sessionCount}`);
}

function handle(socket, data) {
  if (!data.includes("Protocol") || !sessionManager.find(socket.id)) {
    return false;
  }

  const json = JSON.parse(data);
  console.log(json["Protocol"]);

  // temp---
  const session = sessionManager.find(socket.id);
  if (json.Protocol === 1) {
    session.send(new S_PlayerOrder(session.id, 0));
  } else if (json.Protocol === 2) {
    session.send(new S_PlayerOrder(session.id, 1));
  }
  // ---temp

  // JobQueue에 push

  return true;
}

module.exports = {
  init,
  clear,
  handle,
};
