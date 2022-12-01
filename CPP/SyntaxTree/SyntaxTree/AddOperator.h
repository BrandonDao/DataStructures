#pragma once
#include <string>
#include "Operator.h"

template <typename T>
struct AddOperator : public Operator<OpTypes::Binary, T>
{
	OpTypes getOpType() override { return OpTypes::Binary; }
	std::string getValue() override { return "+"; }

	AddOperator(std::unique_ptr<Node<T>> lhs)
	{
		children[0] = std::move(lhs);
	}

	T evaluate(std::unordered_map<std::string, T> variableToValue) override
	{
		return children[0]->evaluate(variableToValue) + children[1]->evaluate(variableToValue);
	}
};