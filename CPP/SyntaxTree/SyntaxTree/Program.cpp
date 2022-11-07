#include <memory>
#include <string>
#include <stack>
#include <queue>

#include "Operator.h"
#include "NotOperator.h"
#include "AddOperator.h"
#include "ConditionalOperator.h"

// shunting yard algorithm:
// add all variables to output
// if operator:
//     if stack.top is lower/same precedence:
//         pop from opStack to output
//     push to opStack
// when out of tokens: pop all opStack to output
// https://en.wikipedia.org/wiki/Shunting_yard_algorithm
void helper(char c, std::string& output, std::stack<char>& opStack)
{
	if (!opStack.empty() && c > opStack.top())
	{
		output += opStack.top();
		opStack.pop();
	}
	opStack.push(c);
}

std::string convertToRPN(std::string input)
{
	std::stack<char> opStack{};
	std::string output;

	// map probably ideal

	for (char& token : input)
	{
		// ASCII	 63				 43				 33
		// technically could get precedence by doing (val - 3) / 20
		//   63/43/33 -> 60/40/30 -> 3/2/1
		if (token == '!' || token == '+' || token == '?')
		{
			helper(token, output, opStack);
		}
		else if (token == ' ' || token == ':') {/* do nothing */}
		else
		{
			output += token;
		}
	}
	while (!opStack.empty())
	{
		output += opStack.top();
		opStack.pop();
	}

	return output;
}


int main()
{
	// unary binary and ternary operators
	// needs to parse !a ? b : c + d
	// https://media.discordapp.net/attachments/1007407792317009980/1038612384438304818/image.png

	//auto test = Operator<2>::opType::Binary;
	
	convertToRPN("!a?b:c+d");

	return 007;
}