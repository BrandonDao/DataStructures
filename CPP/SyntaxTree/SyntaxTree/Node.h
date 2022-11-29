#pragma once
#include <string>

struct Node
{
	virtual std::string getValue() = 0; // "pure virtual" aka abstract
	virtual int evaluate(std::unordered_map<std::string, int> variableToValue) = 0;
};