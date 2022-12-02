#pragma once
#include "Operator.h"
#include <memory>

template <typename T>
concept Negatable = requires(T x)
{
	{-x} -> std::same_as<T>; // -x has to compile and return type has to equal T
};

template <Negatable T>
struct NegateOperator : public Operator<OpTypes::Unary, T>
{
	OpTypes getOpType() override { return OpTypes::Unary; }
	std::string getValue() override { return "-"; }

	NegateOperator() {}
	NegateOperator(std::unique_ptr<Node<T>> child)
	{
		Operator<OpTypes::Unary, T>::children[0] = std::move(child);
	}

	T evaluate(std::unordered_map<std::string, T> variableToValue) override
	{
		return -(Operator<OpTypes::Unary, T>::children[0]->evaluate(variableToValue));
	}
};