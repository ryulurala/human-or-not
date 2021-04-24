const makeId = (length) => {
  let result = "";
  const characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
  for (let i = 0; i < length; i++) {
    const randIdx = Math.floor(Math.random() * characters.length);
    result += characters.charAt(randIdx);
  }

  return result;
};

module.exports = {
  makeId,
};
