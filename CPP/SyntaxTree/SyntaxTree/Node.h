#pragma once
#include <string>
#include <memory>

template <size_t size>
struct Node
{
	std::shared_ptr<Node> children[size];

	virtual std::string getValue() = 0; // "pure virtual" aka abstract
};