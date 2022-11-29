#include "BinarySearchTree.h"

/*
	int count{};
	std::shared_ptr<Node<T>> root;

public: 
	int getCount();

	BinarySearchTree();
	~BinarySearchTree();
	bool Contains(T value);
	void Insert();
	bool Delete();
	void Clear();
	std::shared_ptr<Node<T>> Minimum();
	std::shared_ptr<Node<T>> Maximum();

private:
	std::shared_ptr<Node<T>> Find(T value);
*/

template <typename T>
BinarySearchTree<T>::BinarySearchTree() { }

template <typename T>
BinarySearchTree<T>::~BinarySearchTree() { Clear(); }

template <typename T>
bool BinarySearchTree<T>::Contains(T value)
{

}

template <typename T>
void BinarySearchTree<T>::Insert(T value)
{
	if (root == nullptr)
	{
		root = std::make_shared<Node<T>>(value);
		return;
	}

	std::shared_ptr<Node<T>> curr = root;

	while (true)
	{
		if (curr->get()) {}
	}
}

template <typename T>
bool BinarySearchTree<T>::Delete(T value)
{

}
template <typename T>
void BinarySearchTree<T>::Clear()
{

}

template <typename T>
std::shared_ptr<Node<T>> BinarySearchTree<T>::Minimum()
{

}
template <typename T>
std::shared_ptr<Node<T>> BinarySearchTree<T>::Maximum()
{

}