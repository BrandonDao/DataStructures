#pragma once
#include <string>
#include "Operator.h"

template <typename operand>
struct AddOperator : public Operator<2, operand>
{
	OpTypes getOpType() override { OpTypes::Binary; }
	int getPrecedence() override { return 1; }
	std::string getValue() override { return "+"; }

	operand execute(operand operands[]) override { return operands[0] + operands[1]; }
};