#include "LinkedList.h"
#include <iostream>
#include <vector>


template <typename cat>
void swap2(cat a, cat b) {
	auto temp = *a;
	*a = *b;
	*b = temp;
}

template <typename cat>
void attempt3(cat begin, cat end) {

	for (auto a = begin; a != end; a++) {
		for (auto b = a; b != end; b++) {
			if (*a > *b) {
				swap2(a, b);
			}
		}
	}
}

int main() {
		std::vector<int> vcat{};
		vcat.emplace_back(5);
		vcat.emplace_back(1);
		vcat.emplace_back(4);
		vcat.emplace_back(2);
		vcat.emplace_back(3);

		attempt3(vcat.begin(), vcat.end());


		LinkedList<int> list{};

		while (true) {

			list.add(1);									// 1
			auto test = list.insertAfter(list.begin(), 2);	// 1 -> 2
			test = list.insertAfter(test, 3);				// 1 -> 2 -> 3
			test = list.insertAfter(test, 4);				// 1 -> 2 -> 3 -> 4
			auto testtt = list.eraseAfter(list.begin());	// 1 -> 3 -> 4
			testtt = list.eraseAfter(testtt);				// 1 -> 4
			testtt = list.eraseAfter(testtt);				// 1
			list.remove(1);									// empty

			//attempt3(list.begin(), list.end());
		}


		// For memory leak testing
		//auto betterList = list;
		//list = list;
		//list = betterList;
		//betterList.add(42);
		//auto test(betterList);

		//for (auto val : list) {
		//	std::cout << val << std::endl;
		//}

	return 0xdead;
};