#pragma once
#include <string>

template <typename T>
struct Node
{
	virtual std::string getValue() = 0; // "pure virtual" aka abstract
	virtual T evaluate(std::unordered_map<std::string, T> variableToValue) = 0;
};