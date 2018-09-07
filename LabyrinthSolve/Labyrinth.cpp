#include "stdafx.h"
#include "Labyrinth.h"


Labyrinth::Labyrinth()
{
	Width = Lab_Width;
	Height = Lab_Height;

	horizontal = new int*[Lab_Width - 1];
	for (int i = 0; i < Lab_Width - 1; i++)
	{
		horizontal[i] = new int[Lab_Height];
	}

	vertical = new int*[Lab_Height - 1];
	for (int i = 0; i < Lab_Height - 1; i++)
	{
		horizontal[i] = new int[Lab_Width];
	}
}

int Labyrinth::GetRelationState(Block block1, Block block2)
{
	if (block1.Y == block2.Y)	
	// Überprüft ob die Blöcke horizontal zu einander liegen
	{
		if (block1.X < block2.X)	
		// Die Position des linken Blocks kann als Index verwendet werden.
		{
			return horizontal[block1.X][block1.Y];
		}
		else
		{
			return horizontal[block2.X][block2.Y];
		}
	}
	else if (block1.Y < block2.Y)
	// Die Position des oberen Blocks kann als Index verwendet werden.
	{
		return vertical[block1.X][block1.Y];
	}
	else
	{
		return vertical[block2.X][block2.Y];
	}
}

void Labyrinth::SetRelationState(Block block1, Block block2, int state)
{
	if (block1.Y == block2.Y)
	{
		if (block1.X < block2.X) horizontal[block1.X][block1.Y] = state;
		else horizontal[block1.X][block2.Y] = state;
	}
	else if (block1.Y < block2.Y) vertical[block1.X][block1.Y] = state;
	else vertical[block2.X][block1.Y] = state;
}
