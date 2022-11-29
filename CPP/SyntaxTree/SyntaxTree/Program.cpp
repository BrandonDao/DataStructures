#include "SyntaxTree.h"

int main()
{
	// expression must only add, negate, group, use single-character variables, and cannot have spaces
	SyntaxTree tree("(Apple)+-(Banana+Coconut)");

	tree.variableToValue.insert(
	{
		{ "Apple", 5 },
		{ "Banana", 6 },
		{ "Coconut", 7 }
	});

	auto result = tree.evaluateExpression(); // -8

	return 007;
}