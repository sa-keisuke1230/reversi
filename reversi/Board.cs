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
    public partial class Board : UserControl
    {
        // borad size
        public Size BOARD_SIZE = new Size(500, 500);

        private Bitmap STONE_WHITE = new Bitmap(30, 30);
        private Bitmap STONE_BLACK = new Bitmap(30, 30);
        private Bitmap STONE_LEGAL = new Bitmap(30, 30);
        private PictureBox[,] cell = new PictureBox[8, 8];

        public Action<Point> CellClick;

        public Board()
        {
            InitializeComponent();
        }

        public void setCallBack(Action<Point> cellclick)
        {
            CellClick = cellclick;
        }

        private void Board_Load(object sender, EventArgs e)
        {
            BoardInit();

            // black stone
            using (var g = Graphics.FromImage(STONE_WHITE))
            {
                g.FillEllipse(Brushes.White, new Rectangle(3, 3, 27, 27));
            }

            // white stone
            using (var g = Graphics.FromImage(STONE_BLACK))
            {
                g.FillEllipse(Brushes.Black, new Rectangle(3, 3, 27, 27));
            }

            // legal stone
            using (var g = Graphics.FromImage(STONE_LEGAL))
            {
                g.DrawEllipse(Pens.Red, new Rectangle(3, 3, 25, 25));
            }
        }

        public void UpdateBoard(cellStatus[,] cellStatusList)
        {
            for(int x = 0; x < 8; x++)
            {
                for(var y = 0; y < 8; y++)
                {
                    switch (cellStatusList[x, y])
                    {
                        case cellStatus.EMPTY:
                            cell[x, y].Image = null;
                            break;

                        case cellStatus.BLACK:
                            cell[x, y].Image = STONE_BLACK;
                            break;

                        case cellStatus.WHITE:
                            cell[x, y].Image = STONE_WHITE;
                            break;
                        case cellStatus.LEGAL:
                            cell[x, y].Image = STONE_LEGAL;
                            break;
                        default:
                            Console.WriteLine("UpdateBoard error");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Draw Board
        /// </summary>
        public void BoardInit()
        {
            Bitmap bpm = new Bitmap(BOARD_SIZE.Width, BOARD_SIZE.Height);

            // draw board
            using(Graphics g = Graphics.FromImage(bpm))
            {
                g.FillRectangle(Brushes.Green, 0, 0, BOARD_SIZE.Width, BOARD_SIZE.Height);

                // draw lines
                for(int i = 0; i < 8; i++)
                {
                    g.DrawLine(new Pen(Color.Black, 2), new Point(0, (i + 1) * 40), new Point(BOARD_SIZE.Width, (i + 1) * 40));
                    g.DrawLine(new Pen(Color.Black, 2), new Point((i + 1) * 40, 0), new Point((i + 1) * 40, BOARD_SIZE.Height));
                }
            }
            pictureBox_borad.Image = bpm;

            // picturebox
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var pb = new PictureBox
                    {
                        Name = $"{x}{y}",
                        Location = new Point(
                            (x * 40) + 2, 
                            (y * 40) + 2
                        ),
                        Size = new Size(35, 35),
                        BackColor = Color.Transparent,
                    };

                    pb.Parent = pictureBox_borad.Parent;
                    pb.Image = STONE_BLACK;
                    pb.Click += StoneClickHandler;
                    cell[x, y] = pb;

                    pictureBox_borad.Controls.Add(pb);
                }
            }
        }

        private void StoneClickHandler(object sender, System.EventArgs e)
        {
            var name = ((PictureBox)sender).Name;
            var p = new Point(int.Parse(name[0].ToString()), int.Parse(name[1].ToString()));
            CellClick(p);
        }
    }
}
