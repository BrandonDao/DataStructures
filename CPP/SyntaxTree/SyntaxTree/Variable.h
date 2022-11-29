#pragma once
#include "Node.h"

struct Variable : public Node
{
	std::string getValue() { return value; }

	Variable(std::string::const_iterator beginI, std::string::const_iterator endI) { this->value = std::string(beginI, endI); }

	int evaluate(std::unordered_map<std::string, int> variableToValue) override
	{
		return variableToValue[value];
	}

private:
	std::string value;
};