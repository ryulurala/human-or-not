const { Queue } = require("../utils");

class JobQueue {
  #jobQueue;
  constructor() {
    this.#jobQueue = new Queue();
  }

  push(job) {
    this.#jobQueue.push(job);

    this.#flush();
  }

  #pop() {
    if (this.#jobQueue.count === 0) {
      return null;
    } else {
      return this.#jobQueue.dequeue();
    }
  }

  #flush() {
    while (true) {
      const job = this.#pop();
      if (!job) break;

      job();
    }
  }
}

module.exports = JobQueue;
