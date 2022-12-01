#pragma once
#include "Operator.h"
#include <memory>

template <typename T>
struct Parenthesis : public Operator<OpTypes::Unary, T>
{
	OpTypes getOpType() override { return OpTypes::Unary; }
	std::string getValue() override { return val; }
	
	Parenthesis(char val) { this->val = val; }
	Parenthesis(char val, std::unique_ptr<Node<T>> child)
	{
		this->val = val;
		children[0] = std::move(child);
	}

	T evaluate(std::unordered_map<std::string, T> variableToValue) override
	{
		return children[0]->evaluate(variableToValue);
	}

private:
	std::string val{};
};