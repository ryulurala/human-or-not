/**
 * @TODO
 * 패킷 암호화
  const crypto = require("crypto");
 */

const cluster = require("cluster");
const express = require("express");
const { createServer } = require("http");

const lobbySocket = require("./src/lobby");
const gameSocket = require("./src/game");

const LOBBY_PORT = process.env.PORT || 80;
const GAME_PORT = process.env.GAME_PORT || 9536;

const app = express();
const server = createServer(app);

if (cluster.isMaster) {
  // 로비 서버
  lobbySocket(server);
  server.listen(LOBBY_PORT, () => {
    console.log(`Listening on ${LOBBY_PORT} Port..`);

    // cluster.fork();
    // cluster.on("exit", (worker, code, signal) => {
    //   // 무한 서버
    //   console.log(`closed worker: ${worker.id} and restart !`);
    //   cluster.fork();
    // });
  });
} else {
  // 게임 서버
  gameSocket(server);
  server.listen(GAME_PORT, () => {
    console.log(`Listening on ${GAME_PORT} Port..`);
  });
}
