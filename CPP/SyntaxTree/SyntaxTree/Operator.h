#pragma once
#include "Node.h"
#include <memory>

enum OpTypes
{
	Unary = 1,
	Binary,
	Ternary
};

template <size_t size, typename T>
struct Operator : public Node<T>
{
	std::unique_ptr<Node<T>> children[size];

	virtual OpTypes getOpType() = 0;
};