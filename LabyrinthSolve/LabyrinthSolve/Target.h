#pragma once
#include "Block.h"
#include "Labyrinth.h"

class Target
{
public:
	virtual int GetMinDistance(Block block);
	// Gibt den minimal Abstand eines Blocks zu dem Objekt an, in Blöcken.

	virtual int GetOrderedBlocks(Block block, Labyrinth labyrinth, Block neighbours[]);
	// Gibt die Anzahl der Nachbarblöcke des Blocks direkt zurück und
	// die Nachbarblöcke selbst in prioriesierter Reihenfolge im Array neighbours.
	// Der Block mit der kürzesten Luftlinie zum Objekt ist das erste im Array.
};

