#pragma once
#include "Operator.h"
#include <memory>

struct Parenthesis : public Operator<OpTypes::Unary>
{
	OpTypes getOpType() override { return OpTypes::Unary; }
	std::string getValue() override { return val; }
	
	Parenthesis(char val) { this->val = val; }
	Parenthesis(char val, std::unique_ptr<Node> child)
	{
		this->val = val;
		children[0] = std::move(child);
	}

	int evaluate(std::unordered_map<std::string, int> variableToValue) override
	{
		return children[0]->evaluate(variableToValue);
	}

private:
	std::string val{};
};