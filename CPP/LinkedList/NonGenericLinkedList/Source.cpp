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


	return 0;
}