function makeId(characters, length) {
  let result = "";
  for (let i = 0; i < length; i++) {
    const randIdx = Math.floor(Math.random() * characters.length);
    result += characters.charAt(randIdx);
  }

  return result;
}

module.exports = {
  makeId,
};
