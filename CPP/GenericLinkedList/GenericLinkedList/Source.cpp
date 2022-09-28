#include "LinkedList.h"

int main() {
	while(true)	{
		LinkedList<int> list{};

		list.add(1);
		list.add(2);
		list.add(3);

		list.remove(2);

		auto betterList = list;
		list = list;
		list = betterList;
		betterList.add(42);
		auto test(betterList);
	}

	return 0;
};