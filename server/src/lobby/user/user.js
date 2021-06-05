class User {
  #session;
  #room;
  #info;

  constructor(session, id) {
    this.#session = session;
    this.#info = { id: id, name: null };
  }

  get session() {
    return this.#session;
  }

  get room() {
    return this.#room;
  }

  set room(value) {
    if (value) this.#room = value;
  }

  get id() {
    return this.#info.id;
  }

  get name() {
    return this.#info.name;
  }

  set name(value) {
    this.#info.name = value;
  }
}

module.exports = User;
