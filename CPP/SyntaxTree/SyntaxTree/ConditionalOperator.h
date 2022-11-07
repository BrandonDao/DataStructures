#pragma once
#include <string>
#include "Operator.h"

template <typename operand>
struct ConditionalOperator : public Operator<3, operand>
{
	OpTypes getOpType() override { return OpTypes::Ternary; }
	int getPrecedence() override { return 2; }
	std::string getValue() override { return "?:"; }

	operand execute(operand operands[3]) override { return operands[0] ? operands[1] : operands[2]; }
};