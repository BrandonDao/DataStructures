#pragma once
#include <memory>

template <typename T>
class Node
{
	T value;

public:
	T getValue() { return value; }
	bool isLeaf() { return left == nullptr && right == nullptr; }
	std::shared_ptr<Node<T>> left{};
	std::shared_ptr<Node<T>> right{};

	Node(T value) { this->value = value; }
};