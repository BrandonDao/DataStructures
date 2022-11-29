#pragma once
#include "Node.h"
#include <memory>

enum OpTypes
{
	Unary = 1,
	Binary,
	Ternary
};

template <size_t size>
struct Operator : public Node
{
	std::unique_ptr<Node> children[size];

	virtual OpTypes getOpType() = 0;
};