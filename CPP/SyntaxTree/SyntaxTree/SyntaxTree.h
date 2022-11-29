#pragma once

#include <memory>
#include <string>
#include <algorithm>
#include <stdexcept>
#include <unordered_map>

#include "Operator.h"
#include "AddOperator.h"
#include "Variable.h"
#include "Parenthesis.h"
#include "NegateOperator.h"

class SyntaxTree
{
	std::string::const_iterator currI;
	std::string::const_iterator endI;

public:
	std::unique_ptr<Node> root;
	std::unordered_map<std::string, int> variableToValue;

	SyntaxTree(std::string expression);
	SyntaxTree(std::string::const_iterator beginI, std::string::const_iterator endI);

	int evaluateExpression();

private:
	std::unique_ptr<Node> parseExpression();
	std::unique_ptr<Node> readUnary();
	std::unique_ptr<Operator<OpTypes::Unary>> readUnaryOp();
	std::unique_ptr<Operator<OpTypes::Binary>> readBinaryOp(std::unique_ptr<Node> lhs);

	static bool isVariable(char c);
	static bool isParenthesis(char c);
	static bool isUnaryOp(char c);
	static bool isBinaryOp(char c);
};