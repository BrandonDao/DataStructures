#pragma once
#include <memory>

#include "Node.h"

template <typename T>
class BinarySearchTree
{
	int count{};
	std::shared_ptr<Node<T>> root{};

public:
	int getCount() { return count; }

	BinarySearchTree();
	~BinarySearchTree();
	bool Contains(T value);
	void Insert(T value);
	bool Delete(T value);
	void Clear();
	std::shared_ptr<Node<T>> Minimum();
	std::shared_ptr<Node<T>> Maximum();

private:
	void Insert(std::shared_ptr<Node<T>> curr, T value);
	std::shared_ptr<Node<T>> Find(T value);
};