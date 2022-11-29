#pragma once
#include "Operator.h"
#include <memory>

struct NegateOperator : public Operator<OpTypes::Unary>
{
	OpTypes getOpType() override { return OpTypes::Unary; }
	std::string getValue() override { return "-"; }

	NegateOperator() {}
	NegateOperator(std::unique_ptr<Node> child)
	{
		children[0] = std::move(child);
	}

	int evaluate(std::unordered_map<std::string, int> variableToValue) override
	{
		return -(children[0]->evaluate(variableToValue));
	}
};