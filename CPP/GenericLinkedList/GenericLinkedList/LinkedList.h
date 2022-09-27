#pragma once
#include <stdexcept>
#include <memory>

// std::shared_ptr
// make_shared


template <typename T>
struct Node {

	using Node_ptr = std::shared_ptr<Node<T>>;

	T value{};
	Node_ptr next{};
	Node<T>* next{};

	Node() { }
	Node(T value) {
		Node<T>::value = value;
	}
};

template <typename T>
struct LinkedList {
	int count{};
	std::shared_ptr<Node<T>> head{};
	std::shared_ptr<Node<T>> tail{};

	LinkedList() {}
	LinkedList(const LinkedList<T>& copyMe) {
		copy(copyMe);
	}
	~LinkedList() {
		auto curr = head;

		while (curr->next != nullptr) {
			Node<T>* temp = curr->next;
			curr->next = curr->next->next;
			delete temp;
		}
		delete curr;
	}

	LinkedList<T>& operator =(const LinkedList<T>& copyMe) {
		copy(copyMe);
	}

	void add(T value) {
		auto newNode = std::make_shared<Node<T>>(value); //new Node<T>{ value };

		if (head == nullptr) {
			head = std::move(newNode);
			tail = head;
			count++;
			return;
		}

		tail->next = std::move(newNode);
		tail = tail->next;
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
	void copy(const LinkedList<T>& copyMe) {
		for (auto curr = copyMe.head; curr != nullptr; curr = curr->next) {
			add(curr->value);
		}
	}
};