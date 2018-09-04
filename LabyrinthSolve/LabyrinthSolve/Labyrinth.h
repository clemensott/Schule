#include "Block.h"

#define State_Open 0
#define State_Close 1
#define State_Unkown 2

#define Lab_Width 10
#define Lab_Height 10

#pragma once
class Labyrinth		// Verwaltet die Statuse der Blockrelations
{
public:
	Labyrinth();

	int Width;			// Gibt die Breite des Objekts in Blöcken an.
	int Height;			// Gibt die Höhe des Objekts in Blöcken an.
	int **horizontal;	// Speichert den Status der horizontalen Blockrelations.
	int **vertical;		// Speichert den Status der vertikalen Blockrelations.

	int GetRelationState(Block block1, Block block2);	
	// Gibt den Status einer Blockrelation zwischen den angegeben Blöcken zurück.

	void SetRelationState(Block block1, Block block2, int state);
	// Setzt den Status einer Blockrelation zwischen den angegeben Blöcken.
};

