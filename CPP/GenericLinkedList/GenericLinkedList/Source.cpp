#include "LinkedList.h"
#include <iostream>
#include <vector>

template <typename T>
void swap1(std::_Vector_iterator<T> a, std::_Vector_iterator<T> b) {
	auto temp = *a;
	*a = *b;
	*b = temp;
}

//template <typename iterator>
//void swap2(iterator a, iterator b) {
//	auto temp = *a;
//	*a = *b;
//	*b = temp;
//}

template <typename T>
void infiniteLoop(std::_Vector_iterator<T> begin, std::_Vector_iterator<T> end) {
	for(auto a = begin; a != end; a++) {
		for (auto b = begin; b != end; b++) {
			if (*a > *b) {
				std::swap(a, b);
			}
		}
	}
}

template <typename T>
void bestSort(std::_Vector_iterator<T> begin, std::_Vector_iterator<T> end) {
	for (auto a = begin; a != end; a++) {
		for (auto b = a + 1; b != end; b++) {
			if (*a > *b) {
				swap1(a, b);
			}
		}
	}
}

template <typename iterator>
void attempt3(iterator begin, iterator end) {

	for (auto a = begin; a != end; a++) {
		for (auto b = a; b != end; b++) {
			if (*a > *b) {
				//swap2(a, b);
			}
		}
	}
}

int main() {
		LinkedList<int> list{};


		std::vector<int> vcat{};
		vcat.emplace_back(5);
		vcat.emplace_back(1);
		vcat.emplace_back(4);
		vcat.emplace_back(2);
		vcat.emplace_back(3);

		bestSort(vcat.begin(), vcat.end());

		list.add(1);
		list.add(3);
		list.add(2);
		list.add(10);
		list.add(-1);
		list.add(100);

		//attempt3(list.begin(), list.end());

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