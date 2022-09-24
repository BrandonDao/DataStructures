#pragma once
#include <stdexcept>

template <typename T>
struct Node {
	T value{};
	Node* next{};

	Node() { }
	Node(T value) {
		Node::value = value;
	}
};

template <typename T>
struct LinkedList {
	int count{};
	Node<T>* head{};
	Node<T>* tail{};

	LinkedList<T>() {}
	LinkedList<T>(const LinkedList& copyMe) {
		copy(copyMe);
	}
	~LinkedList() {
		Node<T>* curr = head;

		while (curr->next != nullptr) {
			Node<T>* temp = curr->next;
			curr->next = curr->next->next;
			delete temp;
		}
		delete curr;
	}

	LinkedList& operator =(const LinkedList& copyMe) {
		copy(copyMe);
	}

	void add(T value) {
		auto newNode = new Node<T>{ value };

		if (head == nullptr) {
			head = newNode;
			tail = newNode;
			count++;
			return;
		}

		tail->next = newNode;
		tail = newNode;
		count++;
	}
	bool remove(T value) {
		if (head->value == value) {
			head = head->next;
			count--;
			return true;
		}
		else if (tail->value == value) {
			Node<T>* curr = head;
			for (; curr->next != tail; curr = curr->next) {}

			tail = curr;
			curr->next = nullptr;
			count--;
			return true;
		}

		Node<T>* curr = head;

		for (; curr->next->value != value; curr = curr->next) {
			if (curr->next == nullptr) throw new std::invalid_argument("value is not contained in the list");
		}	

		curr->next = curr->next->next;
		count--;
		return true;
	}

private:
	void copy(const LinkedList& copyMe) {
		for (auto curr = copyMe.head; curr != nullptr; curr = curr->next) {
			add(curr->value);
		}
	}
};