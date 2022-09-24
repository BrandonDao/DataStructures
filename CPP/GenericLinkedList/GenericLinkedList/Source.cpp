#include "LinkedList.h"

int main() {

	LinkedList<int*> list{};

	auto one = new int(1);

	list.add(new int(1));
	list.add(one);
	list.add(new int(2));
	list.add(new int(3));
	
	list.remove(one);

	return 0;
};