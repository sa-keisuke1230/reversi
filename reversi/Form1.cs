using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reversi
{
    public partial class Form1 : Form
    {

        private reversiManeger manager = new reversiManeger();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            board1.CellClick = CellClick;

            // reversiManager
            manager.InitilizeBoard();

            // updateBoard
            board1.UpdateBoard(manager.cellStatusList);
        }

        public void CellClick(Point p)
        {
            manager.ClickedCell(p);

            // update Borad
            board1.UpdateBoard(manager.cellStatusList);
        }
    }
}
