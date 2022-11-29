#include "SyntaxTree.h"

bool SyntaxTree::isVariable(char c) { return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'); }
bool SyntaxTree::isParenthesis(char c) { return c == '(' || c == ')'; }
bool SyntaxTree::isUnaryOp(char c) { return c == '-'; }
bool SyntaxTree::isBinaryOp(char c) { return c == '+'; }


SyntaxTree::SyntaxTree(std::string expression)
{
	currI = expression.cbegin();
	endI = expression.cend();
	root = parseExpression();
}
SyntaxTree::SyntaxTree(std::string::const_iterator beginI, std::string::const_iterator endI)
{
	currI = beginI;
	currI = endI;
	root = parseExpression();
}

int SyntaxTree::evaluateExpression()
{
	return root->evaluate(variableToValue);
}

std::unique_ptr<Node> SyntaxTree::parseExpression()
{
	// can be variable, open parenthesis, unaryOp
	std::unique_ptr<Node> node = readUnary();

	// can be close parenthesis, binaryOp
	if (isBinaryOp(*currI))
	{
		node = readBinaryOp(std::move(node));
	}

	return node;
}

std::unique_ptr<Operator<OpTypes::Unary>> SyntaxTree::readUnaryOp()
{
	std::unique_ptr<Operator<OpTypes::Unary>> op;

	if (*currI == '-') op = std::make_unique<NegateOperator>();
	else throw std::runtime_error(*currI + " could not be parsed!");

	currI++;
	op->children[0] = readUnary();
	return op;
}

std::unique_ptr<Operator<OpTypes::Binary>> SyntaxTree::readBinaryOp(std::unique_ptr<Node> lhs)
{
	std::unique_ptr<Operator<OpTypes::Binary>> op;

	if (*currI == '+') op = std::make_unique<AddOperator>(std::move(lhs));
	else throw std::runtime_error(*currI + " could not be parsed!");

	currI++;
	op->children[1] = readUnary();
	return op;
}

std::unique_ptr<Node> SyntaxTree::readUnary()
{
	if (isParenthesis(*currI))
	{
		auto temp = std::make_unique<Parenthesis>(*currI);
		currI++;
		temp->children[0] = parseExpression();
		currI++;
		return temp;
	}
	else if (isVariable(*currI))
	{
		std::string::const_iterator newI = std::find_if_not(currI, endI, isVariable);
		auto temp = std::make_unique<Variable>(currI++, newI);

		currI = newI;
		return temp;
	}
	else /*isOperator*/
	{
		return readUnaryOp();
	}
}