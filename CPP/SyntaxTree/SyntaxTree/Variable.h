#pragma once
#include "Node.h"

template <typename T>
struct Variable : public Node<T>
{
	std::string getValue() { return value; }

	Variable(std::string::const_iterator beginI, std::string::const_iterator endI) { this->value = std::string(beginI, endI); }

	T evaluate(std::unordered_map<std::string, T> variableToValue) override
	{
		return variableToValue[value];
	}

private:
	std::string value;
};