using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V1
{
    public partial class LifeForm : Form
    {
        #region CONSTANTS
        const int MAX_CELLS = 50;
        const int CELL_SIZE = 10;
        const int C_M = 2; // CellMargin
        #endregion

        // 2 Matritzen zur Verwaltung der n'ten und der n+1'ten Generation
        bool[,] m_CA = new bool[MAX_CELLS, MAX_CELLS];
        bool[,] m_CB = new bool[MAX_CELLS, MAX_CELLS];
        bool[,] m_CC; // current ( active ) CellArray n'te Generation

        public LifeForm()
        {
            InitializeComponent();
            timer1.Interval = 100;
            // zum Testen ein paar Zellen setzen
            // m_CA[3, 3] = true; m_CA[3, 4] = true; m_CA[3, 5] = true;
            // m_CA[4, 3] = true; m_CA[4, 4] = true; m_CA[4, 5] = true;
            m_CC = m_CA;
        }

        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);
            DrawCells(e.Graphics);
        }

        private void OnPanelMouseDown(object sender, MouseEventArgs e)
        {
            // der Cell Editor
            TurnCellOnOff(e.X, e.Y);
            m_panel.Invalidate();
        }

        // Cells des aktiven Arrays (m_CC) zeichnen
        void DrawCells(Graphics gr)
        {
            int columns = m_CC.GetLength(0);
            int rows = m_CC.GetLength(1);

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    DrawCell(gr, i, j);
                }
            }
        }

        private void DrawCell(Graphics gr, int i, int j)
        {
            if (!m_CC[i, j]) return;

            int xPixel = i * CELL_SIZE + C_M;
            int yPixel = j * CELL_SIZE + C_M;
            int widthHeightPixel = CELL_SIZE - 2 * C_M + 1;
            Rectangle rect = new Rectangle(xPixel, yPixel, widthHeightPixel, widthHeightPixel);

            gr.FillRectangle(Brushes.Black, rect);
        }

        void DrawGrid(Graphics gr)
        {
            int columns = m_CC.GetLength(0);
            int rows = m_CC.GetLength(1);

            for (int i = 0; i <= rows; i++)
            {
                gr.DrawLine(Pens.Red, 0, CELL_SIZE * i, columns * CELL_SIZE, CELL_SIZE * i);
            }

            for (int i = 0; i <= columns; i++)
            {
                gr.DrawLine(Pens.Red, CELL_SIZE * i, 0, CELL_SIZE * i, rows * CELL_SIZE);
            }
        }

        // Nächste Generation berechnen
        private void OnStepButton(object sender, EventArgs e)
        {
            if (m_CC == m_CA)
            {
                ClearCells(m_CB);
                CalcNextGeneration(m_CA, m_CB);
                m_CC = m_CB;
            }
            else
            {
                ClearCells(m_CA);
                CalcNextGeneration(m_CB, m_CA);
                m_CC = m_CA;
            }
            m_panel.Invalidate();
        }

        private void OnTimerChk(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                timer1.Enabled = false;
            else
                timer1.Enabled = true;
        }

        private void OnTimer(object sender, EventArgs e)
        {
            OnStepButton(null, null);
        }

        private void OnClearButton(object sender, EventArgs e)
        {
            ClearCells(m_CA); ClearCells(m_CB);
            m_panel.Invalidate();
        }

    }
}