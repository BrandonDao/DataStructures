#include "SyntaxTree.h"


int main()
{
	// expression must only add, negate, group, use single-character variables, and cannot have spaces
	SyntaxTree<std::string> tree("(Apple)+(Banana+-Coconut)");

	tree.variableToValue.insert(
	{
		{ "Apple", "Hel" },
		{ "Banana", "lo W" },
		{ "Coconut", "orld!" }
	});

	auto result = tree.evaluateExpression(); // -8

	return 007;
}