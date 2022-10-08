#include "LinkedList.h"
#include <iostream>

int main() {
		LinkedList<int> list{};

		list.add(1);
		list.add(3);
		list.add(2);
		list.add(10);
		list.add(-1);
		list.add(100);

		// For memory leak testing
		//auto betterList = list;
		//list = list;
		//list = betterList;
		//betterList.add(42);
		//auto test(betterList);

		for (auto val : list) {
			std::cout << val << std::endl;
		}

	return 0;
};