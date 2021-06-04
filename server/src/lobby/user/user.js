class User {
  session;
  info;
  room;

  constructor(id) {
    this.info = { id: id };
  }
}

module.exports = User;
