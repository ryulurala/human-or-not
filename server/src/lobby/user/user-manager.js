const User = require("./user");
const { util } = require("../../utils");

class UserManager {
  // private
  #users;

  constructor() {
    this.#users = new Map();
  }

  get totalCount() {
    return this.#users.size;
  }

  createUser(session) {
    for (let i = 0; i < 1000; i++) {
      const userId = util.makeId("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", 6);

      if (!this.#users.has(userId)) {
        const user = new User(session, userId);
        this.#users.set(session.id, user);

        return user;
      }
    }

    return null;
  }

  removeUser(sessionId) {
    if (!this.#users.has(sessionId)) return;

    this.#users.delete(sessionId);
  }

  findUser(sessionId) {
    const user = this.#users.get(sessionId);
    if (!user) return null;

    return user;
  }
}

module.exports = new UserManager();
