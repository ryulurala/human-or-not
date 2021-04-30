class Node {
  #data;
  #next;
  constructor(data) {
    this.#data = data;
    this.#next = null;
  }

  get data() {
    return this.#data;
  }

  get next() {
    return this.#next;
  }

  set next(node) {
    this.#next = node;
  }
}

class Queue {
  #front;
  #back;
  #size;
  constructor() {
    this.#front = null;
    this.#back = null;
    this.#size = 0;
  }

  get count() {
    return this.#size;
  }

  get empty() {
    if (this.#size === 0) {
      return true;
    } else {
      return false;
    }
  }

  get front() {
    return this.#front;
  }

  get back() {
    return this.#back;
  }

  enqueue(data) {
    const node = new Node(data);
    if (this.#size === 0) {
      this.#front = node;
    } else {
      this.#back.next = node;
    }
    this.#back = node;
    this.#size++;
  }

  dequeue() {
    if (this.#size === 0) {
      return null;
    }
    const data = this.#front.data;
    this.#front = this.#front.next;
    this.#size--;
    return data;
  }
}

module.exports = {
  Queue,
};
