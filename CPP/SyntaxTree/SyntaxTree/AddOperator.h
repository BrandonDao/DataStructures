#pragma once
#include <string>
#include "Operator.h"

template <typename T>
concept Addable = requires(T x)
{
	{x + x} -> std::same_as<T>;
};

template <Addable T>
struct AddOperator : public Operator<OpTypes::Binary, T>
{
	OpTypes getOpType() override { return OpTypes::Binary; }
	std::string getValue() override { return "+"; }

	AddOperator(std::unique_ptr<Node<T>> lhs)
	{
		Operator<OpTypes::Binary, T>::children[0] = std::move(lhs);
	}

	T evaluate(std::unordered_map<std::string, T> variableToValue) override
	{
		return Operator<OpTypes::Binary, T>::children[0]->evaluate(variableToValue) + 
			   Operator<OpTypes::Binary, T>::children[1]->evaluate(variableToValue);
	}
};