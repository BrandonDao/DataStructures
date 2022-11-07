#pragma once
#include <string>
#include "Operator.h"

template <typename operand>
struct NotOperator : public Operator<1, operand>
{
	OpTypes getOpType() override { return OpTypes::Unary; }
	int getPrecedence() override { return 0; }
	std::string getValue() override { return "!"; }

	operand execute(operand operands[1]) override { return !operands[0]; }
};