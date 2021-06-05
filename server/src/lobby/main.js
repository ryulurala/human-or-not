const sessionManager = require("../session");
const packetManager = require("./packet");
const roomManager = require("./room");
const userManager = require("./user");

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
  // 나중에 고쳐야할 코드 -> 구조 변경
  sessionManager.destroy(socket, (session) => {
    const user = userManager.findUser(session.id);
    if (user) {
      // room 나가기
      if (user.room) {
        user.room.removeUser(user.id);
        if (user.room.userCount === 0) roomManager.removeRoom(user.room.id);
      }

      // user 삭제
      userManager.removeUser(session.id);
    }
  });

  if (callback) callback();

  console.log(`Current number of sessions: ${sessionManager.totalSession}`);
};

const handle = (socket, data) => {
  if (!data.includes("Protocol")) {
    return false;
  } else {
    const session = sessionManager.find(socket.id); // socket보다 socket.id로 찾는 것이 더 효율적?
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
