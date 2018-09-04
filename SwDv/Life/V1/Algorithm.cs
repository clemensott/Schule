using System;
using System.Collections.Generic;
using System.Text;

namespace V1
{
    partial class LifeForm
    {
        // aOld Matrix mit der n-ten Generation
        // aNew Matrix mit der n+1-ten Generation
        void CalcNextGeneration(bool[,] aOld, bool[,] aNew)
        {
            int columns = m_CC.GetLength(0);
            int rows = m_CC.GetLength(1);

            m_CC = aOld; // sicherstellen, daﬂ m_CC=aOld ist
            ClearCells(aNew);

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int aliveNeigbours = GetNeighbourCount(i, j);

                    if (aliveNeigbours == 3 || (aliveNeigbours == 2 && m_CC[i, j]))
                    {
                        aNew[i, j] = true;
                    }
                }
            }
        }

        void ClearCells(bool[,] aCells)
        {
            int columns = m_CC.GetLength(0);
            int rows = m_CC.GetLength(1);

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    aCells[i, j] = false;
                }
            }
        }

        // cells of m_CC
        void TurnCellOnOff(int aX, int aY)
        {
            int i = (aX - aX % CELL_SIZE) / CELL_SIZE;
            int j = (aY - aY % CELL_SIZE) / CELL_SIZE;

            int columns = m_CC.GetLength(0);
            int rows = m_CC.GetLength(1);

            if (i < 0 || i >= columns || j < 0 || j >= rows) return;

            m_CC[i, j] = !m_CC[i, j];
        }

        // cells of m_CC
        int GetNeighbourCount(int i, int j)
        {
            // wieviele lebende Nachbarn hat Cell(i,j)
            int aliveCellsCount = ValOf(i, j) ? -1 : 0;      // Beginnt mit -1 wenn Cell lebt, da sie sp‰ter dazu gez‰hlt wird

            for (int k = 0; k < 3; k++)
            {
                for (int l = 0; l < 3; l++)
                {
                    aliveCellsCount += ValOf(i - 1 + k, j - 1 + l) ? 1 : 0;
                }
            }

            return aliveCellsCount;
        }

        // Ist Cell(i,j) von m_CC on oder off ?
        // mit richtiger Behandlung von i,j<0 und i,j>=MAX_CELLS
        bool ValOf(int i, int j)
        {
            int columns = m_CC.GetLength(0);
            int rows = m_CC.GetLength(1);

            i = (i + columns) % columns;
            j = (j + rows) % rows;

            return m_CC[i, j];
        }

    }
}
