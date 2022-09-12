#pragma once
#include <stdexcept>

struct IntLinkedListNode {
	int value{};
	IntLinkedListNode* next{};

	IntLinkedListNode(int value) {
		IntLinkedListNode::value = value;
		next = nullptr;
	}
};

struct IntLinkedList {
	int count{};
	IntLinkedListNode* head{};
	IntLinkedListNode* tail{};

	IntLinkedList() {
	}
	~IntLinkedList() {
		IntLinkedListNode* curr = head;

		while (curr->next != nullptr) {
			IntLinkedListNode* temp = curr->next;
			curr->next = curr->next->next;
			delete temp;
		}
		delete curr;
	}

	IntLinkedList& operator =(const IntLinkedList& copyMe) {
		copy(copyMe);
	}
	IntLinkedList(const IntLinkedList& copyMe) {
		copy(copyMe);
	}

	void add(int value) {
		auto newNode = new IntLinkedListNode(value);

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
	bool remove(int value) {
		if (head->value == value) {
			head = head->next;
			count--;
			return true;
		}
		else if (tail->value == value) {
			IntLinkedListNode* curr = head;
			for (; curr->next != tail; curr = curr->next) {}

			tail = curr;
			curr->next = nullptr;
			count--;
			return true;
		}

		IntLinkedListNode* curr = head;

		for (; curr->next->value != value; curr = curr->next) {
			if (curr->next == nullptr) throw new std::invalid_argument("value is not contained in the list");
		}

		curr->next = curr->next->next;
		count--;
		return true;
	}

	IntLinkedList* operator +(const IntLinkedList& addMe) {
		auto list = this;

		list->tail->next = addMe.head;
		list->tail = addMe.tail;
		list->count += addMe.count;
		return list;
	}

private:
	void copy(const IntLinkedList& copyMe) {
		for (auto curr = copyMe.head; curr != nullptr; curr = curr->next) {
			add(curr->value);
		}
	}
};