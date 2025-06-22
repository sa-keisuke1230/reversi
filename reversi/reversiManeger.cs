using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reversi
{
    internal class reversiManeger
    {
        public cellStatus[,] cellStatusList;
        public Size FIELD_SIZE = new Size(8, 8);
        public cellStatus currentP;
        Random rnd = new Random();

        public void InitilizeBoard()
        {
            cellStatusList = new cellStatus[FIELD_SIZE.Width, FIELD_SIZE.Height];
            for (int x = 0; x < FIELD_SIZE.Width; x++)
            {
                for (int y = 0; y < FIELD_SIZE.Height; y++)
                {
                    cellStatusList[x, y] = cellStatus.EMPTY;
                }
            }

            for(int x = 0; x < FIELD_SIZE.Width; x++)
            {
                cellStatusList[rnd.Next(0, 7), rnd.Next(0, 7)] = cellStatus.BLACK;
            }

            for (int x = 0; x < FIELD_SIZE.Width; x++)
            {
                cellStatusList[rnd.Next(0, 7), rnd.Next(0, 7)] = cellStatus.WHITE;
            }

            cellStatusList[0, 0] = cellStatus.BLACK;
            cellStatusList[3, 4] = cellStatus.BLACK;
            cellStatusList[4, 3] = cellStatus.BLACK;
            cellStatusList[2, 5] = cellStatus.BLACK;

            cellStatusList[2, 4] = cellStatus.WHITE;
            cellStatusList[1, 0] = cellStatus.WHITE;
            cellStatusList[3, 3] = cellStatus.WHITE;
            cellStatusList[3, 2] = cellStatus.WHITE;
            cellStatusList[4, 4] = cellStatus.WHITE;
            cellStatusList[5, 4] = cellStatus.WHITE;
            cellStatusList[6, 4] = cellStatus.WHITE;
            cellStatusList[2, 5] = cellStatus.WHITE;
            cellStatusList[2, 4] = cellStatus.WHITE;
            cellStatusList[2, 6] = cellStatus.BLACK;

            cellStatusList[2, 3] = cellStatus.WHITE;
            cellStatusList[2, 2] = cellStatus.WHITE;

            currentP = cellStatus.BLACK;
        }

        public void ClickedCell(Point p)
        {
            getLegalMove(p);
            // cellStatusList[p.X, p.Y] = cellStatus.BLACK;
        }

        void printArray(int [,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int q = 0; q < arr.GetLength(1); q++)
                {
                    Console.Write($"{arr[q, i]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------");
        }

        void getLegalMove(Point p)
        {
            int[,] white_borad = new int[FIELD_SIZE.Width, FIELD_SIZE.Height];
            int[,] black_borad = new int[FIELD_SIZE.Width, FIELD_SIZE.Height];
            int[,] tmp_borad = new int[FIELD_SIZE.Width, FIELD_SIZE.Height];
            int[,] empty_borad = new int[FIELD_SIZE.Width, FIELD_SIZE.Height];
            int[,] legal_borad = new int[FIELD_SIZE.Width, FIELD_SIZE.Height];
            int[,] playerBorad = new int[FIELD_SIZE.Width, FIELD_SIZE.Height];
            int[,] enemyBorad = new int[FIELD_SIZE.Width, FIELD_SIZE.Height];

            // 盤面をbitに変換
            for (int x = 0; x < FIELD_SIZE.Width; x++)
            {
                for (int y = 0; y < FIELD_SIZE.Height; y++)
                {
                    white_borad[x, y] = (cellStatusList[x, y] == cellStatus.WHITE) ? 1 : 0;
                    black_borad[x, y] = (cellStatusList[x, y] == cellStatus.BLACK) ? 1 : 0;
                }
            }

            // 空きマスをマーク
            for (int x = 0; x < FIELD_SIZE.Width; x++)
            {
                for (int y = 0; y < FIELD_SIZE.Height; y++)
                {
                    empty_borad[x, y] = (white_borad[x, y] == 0 && black_borad[x, y] == 0) ? 1 : 0;
                }
            }

            // 右
            playerBorad = (int[,])black_borad.Clone();
            enemyBorad = (int[,])white_borad.Clone();
            for (int count = 0; count < FIELD_SIZE.Width - 3; count++)
            {
                for (int y = 0; y < FIELD_SIZE.Height; y++)
                {
                    // 右シフト
                    var last = playerBorad[FIELD_SIZE.Width - 1, y];
                    for (int x = FIELD_SIZE.Width - 1; x > 0; x--)
                    {
                        playerBorad[x, y] = playerBorad[x - 1, y];
                    }
                    playerBorad[0, y] = last;



                    // 自分の石が相手の石と同じだったら && それより右側は壁ではない
                    for (int x = 0; x < FIELD_SIZE.Width; x++)
                    {
                        if (tmp_borad[x, y] == 1) continue;
                        tmp_borad[x, y] = ((playerBorad[x, y] == 1 && enemyBorad[x, y] == 1) && x < FIELD_SIZE.Width - 2 && x > 0) ? 1 : 0;
                    }
                }
            }

            // 合法手
            for (int y = 0; y < FIELD_SIZE.Height; y++)
            {
                // 右シフト
                var last = tmp_borad[FIELD_SIZE.Width - 1, y];
                for (int x = FIELD_SIZE.Width - 1; x > 0; x--)
                {
                    tmp_borad[x, y] = tmp_borad[x - 1, y];
                }
                tmp_borad[0, y] = last;


                // ひっくり返せそうなマスが空マスか
                for (int x = 0; x < FIELD_SIZE.Width; x++)
                {
                    if (legal_borad[x, y] == 1) continue;
                    legal_borad[x, y] = (tmp_borad[x, y] == 1 && empty_borad[x, y] == 1) ? 1 : 0;
                }
            }

            
            // 左
            /*
            playerBorad = (int[,])black_borad.Clone();
            enemyBorad = (int[,])white_borad.Clone();
            tmp_borad = new int[FIELD_SIZE.Height, FIELD_SIZE.Width];
            for (int count = 0; count < FIELD_SIZE.Width - 2; count++)
            {
                for (int y = 0; y < FIELD_SIZE.Height; y++)
                {
                    // 左シフト
                    var last = playerBorad[0, y];
                    for (int x = 0; x < FIELD_SIZE.Width - 1; x++)
                    {
                        playerBorad[x, y] = playerBorad[x + 1, y];
                    }
                    playerBorad[FIELD_SIZE.Width - 1, y] = last;


                    // 自分の石が相手の石と同じだったら && それより左側は壁ではない
                    for (int x = 0; x < FIELD_SIZE.Width; x++)
                    {
                        if (tmp_borad[x, y] == 1) continue;
                        tmp_borad[x, y] = ((playerBorad[x, y] == 1 && enemyBorad[x, y] == 1) && x > 0) ? 1 : 0;
                    }
                }
            }

            
            // 左合法手
            for (int y = 0; y < FIELD_SIZE.Height; y++)
            {
                // 右シフト
                var last = tmp_borad[0, y];
                for (int x = 0; x < FIELD_SIZE.Width - 1; x++)
                {
                    tmp_borad[x, y] = tmp_borad[x + 1, y];
                }
                tmp_borad[FIELD_SIZE.Width - 1, y] = last;


                // ひっくり返せそうなマスが空マスか
                for (int x = 0; x < FIELD_SIZE.Width; x++)
                {
                    if (legal_borad[x, y] == 1) continue;
                    legal_borad[x, y] = (tmp_borad[x, y] == 1 && empty_borad[x, y] == 1) ? 1 : 0;
                }
            }*/
            

           
            // 上
            /*
            playerBorad = (int[,])black_borad.Clone();
            enemyBorad = (int[,])white_borad.Clone();
            tmp_borad = new int[FIELD_SIZE.Height, FIELD_SIZE.Width];
            for (int count = 0; count < FIELD_SIZE.Height - 2; count++)
            {
                for (int x = 0; x < FIELD_SIZE.Width; x++)
                {
                    // 上シフト
                    var last = playerBorad[x, 0];
                    for (int y = 0; y < FIELD_SIZE.Height - 1; y++)
                    {
                        playerBorad[x, y] = playerBorad[x, y + 1];
                    }
                    playerBorad[x, FIELD_SIZE.Height - 1] = last;


                    // 自分の石が相手の石と同じだったら && それより左側は壁ではない
                    for (int y = 0; y < FIELD_SIZE.Height; y++)
                    {
                        if (tmp_borad[x, y] == 1) continue;
                        tmp_borad[x, y] = ((playerBorad[x, y] == 1 && enemyBorad[x, y] == 1) && y > 0) ? 1 : 0;
                    }
                }
            }

            // 上合法手
            for (int x = 0; x < FIELD_SIZE.Width; x++)
            {
                // 上シフト
                var last = tmp_borad[x, 0];
                for (int y = 0; y < FIELD_SIZE.Height - 1; y++)
                {
                    tmp_borad[x, y] = tmp_borad[x, y + 1];
                }
                tmp_borad[x, FIELD_SIZE.Height - 1] = last;


                // ひっくり返せそうなマスが空マスか
                for (int y = 0; y < FIELD_SIZE.Height; y++)
                {
                    if (legal_borad[x, y] == 1) continue;
                    legal_borad[x, y] = (tmp_borad[x, y] == 1 && empty_borad[x, y] == 1) ? 1 : 0;
                }
            }*/

            // 下
            /*
            playerBorad = (int[,])black_borad.Clone();
            enemyBorad = (int[,])white_borad.Clone();
            tmp_borad = new int[FIELD_SIZE.Height, FIELD_SIZE.Width];
            for (int count = 0; count < FIELD_SIZE.Height - 2; count++)
            {
                for (int x = 0; x < FIELD_SIZE.Width; x++)
                {
                    // 下シフト
                    var last = playerBorad[x, FIELD_SIZE.Height - 1];
                    for (int y = FIELD_SIZE.Height - 1; y > 0; y--)
                    {
                        playerBorad[x, y] = playerBorad[x, y - 1];
                    }
                    playerBorad[x, 0] = last;

                    // 自分の石が相手の石と同じだったら && それより左側は壁ではない
                    for (int y = 0; y < FIELD_SIZE.Height; y++)
                    {
                        if (tmp_borad[x, y] == 1) continue;
                        tmp_borad[x, y] = ((playerBorad[x, y] == 1 && enemyBorad[x, y] == 1) && y > 0) ? 1 : 0;
                    }
                }
            }

            // 下合法手
            for (int x = 0; x < FIELD_SIZE.Width; x++)
            {
                // 下シフト
                var last = tmp_borad[x, 0];
                for (int y = FIELD_SIZE.Height - 1; y > 0; y--)
                {
                    tmp_borad[x, y] = tmp_borad[x, y - 1];
                }
                tmp_borad[x, 0] = last;

                // ひっくり返せそうなマスが空マスか
                for (int y = 0; y < FIELD_SIZE.Height; y++)
                {
                    if (legal_borad[x, y] == 1) continue;
                    legal_borad[x, y] = (tmp_borad[x, y] == 1 && empty_borad[x, y] == 1) ? 1 : 0;
                }
            }*/


            // 配置可能マスに丸を配置
            for (int y = 0; y < FIELD_SIZE.Height; y++)
            {
                for (int x = 0; x < FIELD_SIZE.Width; x++)
                {
                    if(legal_borad[x, y] == 1)
                    {
                        cellStatusList[x, y] = cellStatus.LEGAL;
                    }
                }
            }
        }

    }
}
