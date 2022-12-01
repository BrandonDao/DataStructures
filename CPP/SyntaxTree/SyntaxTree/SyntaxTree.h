#pragma once

#include <concepts>
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

template <typename T>
struct SyntaxTree
{
#pragma region Forward Declaration

	std::unique_ptr<Node<T>> parseExpression();

#pragma endregion

	std::string::const_iterator currI;
	std::string::const_iterator endI;

public:
	std::unique_ptr<Node<T>> root;
	std::unordered_map<std::string, int> variableToValue;

	SyntaxTree(std::string expression)
	{
		currI = expression.cbegin();
		endI = expression.cend();
		root = parseExpression();
	}
	SyntaxTree(std::string::const_iterator beginI, std::string::const_iterator endI)
	{
		currI = beginI;
		currI = endI;
		root = parseExpression();
	}

	T evaluateExpression()
	{
		return root->evaluate(variableToValue);
	}

private:
#pragma region isNodeType Functions
	bool isVariable(char c) { return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'); }
	bool isParenthesis(char c) { return c == '(' || c == ')'; }
	bool isUnaryOp(char c) { return c == '-'; }
	bool isBinaryOp(char c) { return c == '+'; }
#pragma endregion

	std::unique_ptr<Node<T>> parseExpression()
	{
		// can be variable, open parenthesis, unaryOp
		std::unique_ptr<Node<T>> node = readUnary();

		// can be close parenthesis, binaryOp
		if (isBinaryOp(*currI))
		{
			node = readBinaryOp(std::move(node));
		}

		return node;
	}

	std::unique_ptr<Operator<OpTypes::Unary, T>> readUnaryOp()
	{
		std::unique_ptr<Operator<OpTypes::Unary, T>> op;

		if (*currI == '-') op = std::make_unique<NegateOperator<T>>();
		else throw std::runtime_error(*currI + " could not be parsed!");

		currI++;
		op->children[0] = readUnary();
		return op;
	}

	std::unique_ptr<Operator<OpTypes::Binary, T>> eadBinaryOp(std::unique_ptr<Node<T>> lhs)
	{
		std::unique_ptr<Operator<OpTypes::Binary, T>> op;

		if (*currI == '+') op = std::make_unique<AddOperator<T>>(std::move(lhs));
		else throw std::runtime_error(*currI + " could not be parsed!");

		currI++;
		op->children[1] = readUnary();
		return op;
	}

	std::unique_ptr<Node<T>> readUnary()
	{
		if (isParenthesis(*currI))
		{
			auto temp = std::make_unique<Parenthesis<T>>(*currI);
			currI++;
			temp->children[0] = parseExpression();
			currI++;
			return temp;
		}
		else if (isVariable(*currI))
		{
			std::string::const_iterator newI = std::find_if_not(currI, endI, isVariable);
			auto temp = std::make_unique<Variable<T>>(currI++, newI);

			currI = newI;
			return temp;
		}
		else /*isOperator*/
		{
			return readUnaryOp();
		}
	}
};