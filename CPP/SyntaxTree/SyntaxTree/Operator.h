#pragma once
#include "Node.h"

enum OpTypes
{
	Unary,
	Binary,
	Ternary
};

template <size_t size, typename operand>
struct Operator : public Node<size>
{
	virtual OpTypes getOpType() = 0;
	virtual int getPrecedence() = 0;

	virtual operand execute(operand operands[]) = 0;
};