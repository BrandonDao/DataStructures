#include "IntLinkedList.h"
#include "LinkedList.h"

#include <iostream>
#include <time.h>
using namespace std;


void leakRAM()
{
	IntLinkedList list{};

	for (int i = 0; i < 10; i++)
	{
		list.add(rand() % 1000);
	}
}

int main()
{
	//srand(time(NULL));

	//for (int i = 0; i < 1000000; i++)
	//{
	//	leakRAM();
	//}

	IntLinkedList one{};
	one.add(1);
	one.add(2);
	one.add(3);

	IntLinkedList two{};
	two.add(4);
	two.add(5);
	two.add(6);

	auto three = one + two;

	return 0;
}