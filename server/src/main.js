const { sessionManager } = require("./session");
const { S_Connected } = require("./packet");

const SESSION_LIMIT = 9999;

function init(socket) {
  if (sessionManager.sessionCount > SESSION_LIMIT) {
    return;
  }

  // session 생성
  const session = sessionManager.generate(socket);

  session.send(new S_Connected(session.id)); // temp

  // Job Queue에 push: Sending client id

  console.log(`Current number of sessions: ${sessionManager.sessionCount}`);
}

function clear(socket, callback) {
  sessionManager.destroy(socket, callback);
  if (callback) {
    callback();
  }
  console.log(`Current number of sessions: ${sessionManager.sessionCount}`);
}

function handle(socket, data) {
  if (data.includes("Protocol") || !sessionManager.find(socket.id)) {
    return;
  }

  console.log(data);

  // JobQueue에 push
}

module.exports = {
  init,
  clear,
  handle,
};
