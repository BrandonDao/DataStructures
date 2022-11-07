#include <memory>
#include <string>

template <size_t size>
struct Node
{
	std::shared_ptr<Node> children[size];
	virtual std::string getValue();

private:
	std::string value;
};

template <size_t size, typename operand>
struct Operator : public Node<size>
{
	enum OpTypes
	{
		Unary,
		Binary,
		Ternary
	};

	virtual OpTypes getOpType() = 0; // "pure virtual" aka abstract

	virtual operand execute(operand operands[]) = 0;
};

struct AddOperator : public Operator<2, int>
{
	OpTypes getOpType() override { return OpTypes::Binary; }
	std::string getValue() override { return "+"; }

	int execute(int operands[]) override { return operands[0] + operands[1]; }
};
struct NotOperator : public Operator<1, bool>
{
	OpTypes getOpType() override { return OpTypes::Unary; }
	std::string getValue() override { return "!"; }

	bool execute(bool operands[1]) override { return !operands[1]; }
};


struct SyntaxTree
{
	
};

int main()
{
	// unary binary and ternary operators
	// needs to parse !a ? b : c + d
	// https://discord.com/channels/876537798616817754/1007407792317009980/1038612384761253908

	//auto test = Operator<2>::opType::Binary;

	return 007;
}