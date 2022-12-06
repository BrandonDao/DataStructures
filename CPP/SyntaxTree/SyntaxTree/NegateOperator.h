#pragma once
#include "Operator.h"
#include <memory>

template <typename T>
concept Negatable = requires(T x)
{
	{-x} -> std::same_as<T>; // -x has to compile and return type has to equal T
};

template <typename T>
struct NegateOperator : public Operator<OpTypes::Unary, T>
{
	OpTypes getOpType() override { return OpTypes::Unary; }
	std::string getValue() override { return "-"; }

	NegateOperator()
	{
		// evaluated at compile time.  If false, is not checked by the compiler
		if constexpr (!Negatable<T>) throw std::runtime_error("Type is not negatable!");
	}

	T evaluate(std::unordered_map<std::string, T> variableToValue) override
	{
		if constexpr (Negatable<T>)
		{
			return -(Operator<OpTypes::Unary, T>::children[0]->evaluate(variableToValue));
		}
		throw std::runtime_error("Type is not negatable!");
	}
};