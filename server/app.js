const crypto = require("crypto");
const express = require("express");
const { createServer } = require("http");
const webSocket = require("./src/socket");

const PORT = process.env.PORT || 80;
const app = express();
const server = createServer(app);

webSocket(server);

server.listen(PORT, () => {
  console.log("Listening on http://localhost:80");
});
