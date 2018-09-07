#include "stdafx.h"
#include "Search.h"
#include "Target.h"


Search::Search(Labyrinth lab, Target tar)
{
	labyrinth = lab;
	target = tar;

	current = new Block[Lab_Width * Lab_Height];
	currentLength = 0;

	found = new Block[Lab_Width * Lab_Height];
	foundLength = -1;

	minDistances = new int*[Lab_Width];
	for (int i = 0; i < Lab_Width; i++)
	{
		minDistances[i] = new int[Lab_Height];

		for (int j = 0; j < Lab_Height; j++)
		{
			minDistances[i][j] = Lab_Width * Lab_Height;
			// Weist jedem Eintrag den maximal möglichen Wert zu.
		}
	}
}

void Search::Add(Block next)
{
	if (!TryAdd(next)) return;	// Überprüft ob der Block übersprungen werden kann.

	if (target.GetMinDistance(next) == 0)	
	// Überprüft ob die aktuelle Route am Target angekommen ist.
	// Diese Route ist kürzer als die bisher gefundene, 
	// da der Block sonst übersprungen worden wäre.
	{	for (int i = 0; i < currentLength; i++)
		{
			found[i] = current[i];
		}

		foundLength = currentLength;
		return;
	}

	int startLength = currentLength;		// Puffert die aktuelle Länge.

	Block neighbours[4];
	int neighboursCount = target.GetOrderedBlocks(next, labyrinth, neighbours);

	for (int i = 0; i < neighboursCount; i++)
	{
		Block neighbour = neighbours[i];

		if (labyrinth.GetRelationState(next, neighbour) != State_Open) continue;

		currentLength = startLength;
		// Kürzt die aktuelle Route auf die Länge die die Route bis hierhin hatte,
		// da durch das Aufufen von der Add-Methode mit eienm vorherigen Nachbarblocks
		// die Route verlängert worden sein könnte.

		Add(neighbour);
	}
}

bool Search::TryAdd(Block next)
{
	if (foundLength != -1)
	{
		int minLength = currentLength + 1 + target.GetMinDistance(next);

		if (foundLength <= minLength) return false;	// Maximalwertüberschreitung
	}

	if (minDistances[next.X][next.Y] <= currentLength + 1)
	{
		return false;		// Blockwerüberschreitung
	}

	if (foundLength != -1)				// Es gibt eine gefundene Route.
	{
		for (int i = 0; i < foundLength; i++)
		{
			Block block = found[i];

			if (block.X != next.X || block.Y != next.X) continue;

			// Es kann abgekürtzt werden, da der aktuelle Block ein Teil 
			// der bereits gefundenen Route ist und kürzer erreicht wurde,
			//ansonsten wäre diese Methode schon aufgrund einer 
			// Blockwertüberschreitung verlassen worden

			Block newFound[Lab_Width * Lab_Height];

			for (int j = 0; j < currentLength; j++)	// Kopiert die aktuelle Route
			{
				newFound[j] = current[j];
			}

			for (int j = 0; i + j < foundLength; j++)	
			// Kopiert den Rest der gefundenen Route.
			{
				Block foundBlock = found[i + j];
				newFound[currentLength + j] = foundBlock;

				minDistances[foundBlock.X][foundBlock.Y] = currentLength + j;
				// aktualisiert die miniml Distanz. Absolut notwendig,
				// da sonst an anderer Stelle Überprüfungen fehlen.
			}

			found = &newFound[0];
			foundLength = currentLength + foundLength - i;
			return false;
		}
	}

	current[currentLength++] = next;				
	// Fügt den aktuellen Block an die aktuelle Route an und erhöht die Länge.

	minDistances[next.X][next.Y] = currentLength;	
	// Aktualisiert die minimal Distanz zum aktuellen Block.

	return true;
}
