#include "LinkedList.h"
#include <iostream>
#include <vector>

template<typename T>
void notStdSort() {

}

int main() {
		LinkedList<int> list{};

		std::vector<int> vcat{};
		vcat.emplace_back(1);
		vcat.emplace_back(5);
		vcat.emplace_back(2);
		vcat.emplace_back(3);
		vcat.emplace_back(4);

		//list.add(1);
		//list.add(3);
		//list.add(2);
		//list.add(10);
		//list.add(-1);
		//list.add(100);

		// For memory leak testing
		//auto betterList = list;
		//list = list;
		//list = betterList;
		//betterList.add(42);
		//auto test(betterList);

		//for (auto val : list) {
		//	std::cout << val << std::endl;
		//}

	return 0xcafebabe;
};