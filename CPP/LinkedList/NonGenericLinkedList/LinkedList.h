#pragma once

template <class T>
struct LinkedListNode {
	int value;
	LinkedListNode* next;

	LinkedListNode(int value) {
		LinkedListNode::value = value;
		next = nullptr;
	}
};

template <class T>
struct LinkedList {
	int count;
	LinkedListNode<T>* first;
	LinkedListNode<T>* last;

	LinkedList() {
		count = 0;
		first = nullptr;
		last = nullptr;
	}
	~LinkedList() {
		LinkedListNode<T>* curr = first;

		while (curr->next != nullptr) {
			LinkedListNode<T>* temp = curr->next;
			curr->next = curr->next->next;
			delete temp;
		}
		delete curr;
	}

	LinkedList<T>& operator =(const LinkedList<T>& copyMe) {
		copy(copyMe);
	}
	LinkedList<T>(const LinkedList<T>& copyMe) {
		copy(copyMe);
	}

	void addFirst(int value) {
		LinkedListNode<T>* newNode = new LinkedListNode<T>(value);

		if (first == nullptr) {
			first = newNode;
			last = newNode;
			count++;
			return;
		}

		newNode->next = first->next;
		first->next = newNode;
		first = newNode;
		count++;
	}
	void addAfter(LinkedListNode<T>* node, int value) {
		if (node == nullptr) throw std::invalid_argument("node cannot be null!");

		LinkedListNode<T>* newNode = new LinkedListNode<T>(value);

		if (first == nullptr) {
			first = newNode;
			last = newNode;
			count++;
			return;
		}

		newNode->next = node->next;
		node->next = newNode;
		count++;
	}
	void addLast(int value) {
		LinkedListNode<T>* newNode = new LinkedListNode<T>(value);

		if (first == nullptr) {
			first = newNode;
			last = newNode;
			count++;
			return;
		}

		last->next = newNode;
		last = newNode;
		count++;
	}

	LinkedListNode<T>* find(int value) {
		for (LinkedListNode<T>* curr = first; curr->next != nullptr; curr = curr->next) {
			if (curr->value == value) return curr;
		}
		return nullptr;
	}
	bool contains(int value) {
		return find(value) != nullptr;
	}

	bool remove(int value) {
		if (first->value == value) {
			return removeFirst();
		}
		else if (last->value == value) {
			return removeLast();
		}

		LinkedListNode<T>* curr = first;

		for (; curr->next->value != value; curr = curr->next) {
			if (curr->next == nullptr) throw new std::invalid_argument("value is not contained in the list");
		}

		curr->next = curr->next->next;
		count--;
		return true;
	}
	bool removeFirst() {
		first = first->next;
		count--;
		return true;
	}
	bool removeLast() {
		LinkedListNode<T>* curr = first;
		for (; curr->next != last; curr = curr->next) {}

		last = curr;
		curr->next = nullptr;
		count--;
		return true;
	}


private:
	LinkedList<T> copy(const LinkedList<T>& copyMe) {
		LinkedList<T> list{};

		if (copyMe.first == nullptr) {
			return list;
		}

		for (auto curr = copyMe.first; curr->next != nullptr; curr = curr->next) {
			list.addLast(curr->value);
		}

		return list;
	}
};