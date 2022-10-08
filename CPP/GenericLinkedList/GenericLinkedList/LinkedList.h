#pragma once
#include <stdexcept>
#include <memory>

template <typename T>
struct Node {
	T value{};
	std::shared_ptr<Node<T>> next{};

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

	// Constructors
	LinkedList() {}
	LinkedList(const LinkedList<T>& copyMe) {
		copy(copyMe);
	}
	// Destructor
	~LinkedList() {
		auto curr = std::move(head);

		while (curr->next != nullptr) {
			auto temp = curr->next;
			curr->next = curr->next->next;
			temp.reset();
		}

		tail.reset();
	}

	// Operators
	LinkedList<T>& operator =(const LinkedList<T>& copyMe) {
		if (head == copyMe.head) return *this;

		this->~LinkedList();
		copy(copyMe);

		return *this;
	}

	// Adding
	void add(T value) {
		auto newNode = std::make_shared<Node<T>>(value);

		if (head == nullptr) {
			head = std::move(newNode);
			tail = head;
		}
		else {
			tail->next = std::move(newNode);
			tail = tail->next;
		}

		count++;
	}

	// Removing
	bool remove(T value) {
		if (head->value == value) {
			head = head->next;
		}
		else if (tail->value == value) {
			auto curr = head;
			for (; curr->next != tail; curr = curr->next) {}

			tail = curr;
			curr->next = nullptr;
		}
		else {
			auto curr = head;

			for (; curr->next->value != value; curr = curr->next) {
				if (curr->next == nullptr) throw new std::invalid_argument("value is not contained in the list");
			}

			curr->next = curr->next->next;
		}

		count--;
		return true;
	}

	struct iterator {
		// don't know what this is for but maybe for compiler optimizations
		using difference_type = T;
		using value_type = T;
		using pointer = const T*;
		using reference = const T&;
		using iterator_catagory = std::forward_iterator_tag;

		// Constructor
		iterator(std::shared_ptr<Node<T>> current) {
			this->current = current;
		}

		// Operators
		const T& operator * () const {
			return current->value;
		}
		iterator& operator ++() { // increment, return
			current = current->next;
			return *this;
		}
		iterator& operator ++(T) { // return, increment (might not work)
			auto temp = *this;
			current = current->next;
			return *this;
		}
		bool operator ==(iterator other) {
			return current == other.current;
		}
		bool operator !=(iterator other) {
			return current != other.current;
		}

	private:
		std::shared_ptr<Node<T>> current;
	};

	iterator begin() { // iterator pointing to first val
		return iterator{ head };
	}
	iterator end() { // iterator pointing to last val's next
		return iterator{ nullptr };
	}

private:
	// Helper for assignment operator + copy constructor
	void copy(const LinkedList<T>& copyMe) {
		for (auto curr = copyMe.head; curr != nullptr; curr = curr->next) {
			add(curr->value);
		}
	}
};