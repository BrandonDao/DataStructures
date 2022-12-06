#pragma once

#include <concepts>
#include <functional>
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
class SyntaxTree
{
	std::string::const_iterator currI;
	std::string::const_iterator endI;

public:
	std::unique_ptr<Node<T>> root;
	std::unordered_map<std::string, T> variableToValue;

	SyntaxTree(std::string expression);
	SyntaxTree(std::string::const_iterator beginI, std::string::const_iterator endI);

	T evaluateExpression();

private:
	static bool isVariable(const char c) { return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'); }
	bool isParenthesis() const;
	bool isUnaryOp() const;
	bool isBinaryOp() const;

	std::unique_ptr<Node<T>> parseExpression();
	std::unique_ptr<Node<T>> readUnary();
	std::unique_ptr<Operator<OpTypes::Unary, T>> readUnaryOp();
	std::unique_ptr<Operator<OpTypes::Binary, T>> readBinaryOp(std::unique_ptr<Node<T>> lhs);
};

//template <typename T>
//static bool SyntaxTree<T>::isVariable(const char c)
template <typename T>
bool SyntaxTree<T>::isParenthesis() const  { char c = *currI; return c == '(' || c == ')'; }
template <typename T>
bool SyntaxTree<T>::isUnaryOp() const { char c = *currI; return c == '-'; }
template <typename T>
bool SyntaxTree<T>::isBinaryOp() const { char c = *currI; return c == '+'; }

// Constructors
template <typename T>
SyntaxTree<T>::SyntaxTree(std::string expression)
{
	currI = expression.cbegin();
	endI = expression.cend();
	root = parseExpression();
}
template <typename T>
SyntaxTree<T>::SyntaxTree(std::string::const_iterator beginI, std::string::const_iterator endI)
{
	currI = beginI;
	currI = endI;
	root = parseExpression();
}

// Solve
template <typename T>
T SyntaxTree<T>::evaluateExpression()
{
	return root->evaluate(variableToValue);
}

// Parse
template <typename T>
std::unique_ptr<Node<T>> SyntaxTree<T>::parseExpression()
{
	// can be variable, open parenthesis, unaryOp
	std::unique_ptr<Node<T>> node = readUnary();

	// can be close parenthesis, binaryOp
	if (isBinaryOp())
	{
		node = readBinaryOp(std::move(node));
	}

	return node;
}

template <typename T>
std::unique_ptr<Node<T>> SyntaxTree<T>::readUnary()
{
	if (isParenthesis())
	{
		auto temp = std::make_unique<Parenthesis<T>>(*currI);
		currI++;
		temp->children[0] = parseExpression();
		currI++;
		return temp;
	}
	else if (SyntaxTree<T>::isVariable(*currI))
	{
		std::string::const_iterator newI = std::find_if_not(currI, endI, &SyntaxTree::isVariable);
		auto temp = std::make_unique<Variable<T>>(currI++, newI);

		currI = newI;
		return temp;
	}
	else /*isOperator*/
	{
		return readUnaryOp();
	}
}

template <typename T>
std::unique_ptr<Operator<OpTypes::Unary, T>> SyntaxTree<T>::readUnaryOp()
{
	std::unique_ptr<Operator<OpTypes::Unary, T>> op;

	if (*currI == '-') op = std::make_unique<NegateOperator<T>>();
	else throw std::runtime_error(*currI + " could not be parsed!");

	currI++;
	op->children[0] = readUnary();
	return op;
}

template <typename T>
std::unique_ptr<Operator<OpTypes::Binary, T>> SyntaxTree<T>::readBinaryOp(std::unique_ptr<Node<T>> lhs)
{
	std::unique_ptr<Operator<OpTypes::Binary, T>> op;

	if (*currI == '+') op = std::make_unique<AddOperator<T>>(std::move(lhs));
	else throw std::runtime_error(*currI + " could not be parsed!");

	currI++;
	op->children[1] = readUnary();
	return op;
}