#pragma once
#include <string>
#include "Operator.h"

struct AddOperator : public Operator<OpTypes::Binary>
{
	OpTypes getOpType() override { return OpTypes::Binary; }
	std::string getValue() override { return "+"; }

	AddOperator(std::unique_ptr<Node> lhs)
	{
		children[0] = std::move(lhs);
	}

	int evaluate(std::unordered_map<std::string, int> variableToValue) override
	{
		return children[0]->evaluate(variableToValue) + children[1]->evaluate(variableToValue);
	}
};