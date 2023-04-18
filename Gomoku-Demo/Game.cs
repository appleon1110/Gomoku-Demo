using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku_Demo
{
    class Game
    {
        private Board board = new Board();
        private PieceType currentPlayer = PieceType.BLACK;
        private PieceType winner = PieceType.NONE;
        private int countA = 0, countB = 0, countC = 0, countD = 0;


        public PieceType Winner { get { return winner; } }
        
        public bool CanBePlaced(int x,int y)
        {
            return board.CanBePlace(x, y);
        }
       
        public Piece PlaceAPiece(int x,int y)
            {
                Piece piece = board.PlaceAPiece(x,y, currentPlayer);
                if (piece != null)
                {
                    //檢查選手獲勝
                    CheckWinner();

                    //交換選手
                    if (currentPlayer == PieceType.BLACK)
                        currentPlayer = PieceType.WHITE;
                    else if (currentPlayer == PieceType.WHITE)
                        currentPlayer = PieceType.BLACK;
                    return piece;
                }
                return null;
            }
        private void CheckWinner()
        {
            int centerX = board.LastPlaceNode.X;
            int centerY = board.LastPlaceNode.Y;
            

            //檢查八個方向
            for (int xDir = -1; xDir <= 1; xDir++)
            {
                for(int yDir = -1; yDir <= 1; yDir++)
                {
                    //排除中間的情況
                    if (xDir == 0 && yDir == 0)
                        continue;
                    //紀錄現在看到幾顆相同棋子
                    int count = 1;
                   
                    while (count < 5)
                    {
                        //設定被檢查的目標
                        int targetX = centerX + count * xDir;
                        int targetY = centerY + count * yDir;


                        //超出棋盤 或是顏色與當前棋子不同色 如果不同顏色 就跳出 如果相同 count++
                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                            targetY < 0 || targetY >= Board.NODE_COUNT ||
                            board.GetPieceType(targetX, targetY) != currentPlayer)
                        //不論檢查有多少 都先記錄 分別為 (-1-1 11) (-11  1-1)   (-10 10) (01)(0-1)共四組
                        {
                            break;
                        }

                        
                        count++;
                    }
                    
                    if ((xDir < 0 && yDir < 0) || (xDir > 0 && yDir > 0))
                    {
                        countA+= count;
                    }
                    if ((xDir < 0 && yDir > 0) || (xDir > 0 && yDir < 0))
                    {
                        countB+= count;
                    }
                    if ((xDir < 0 && yDir == 0) || (xDir > 0 && yDir == 0))
                    {
                        countC+=count;
                    }
                    if ((xDir == 0 && yDir > 0) || (xDir == 0 && yDir < 0))
                    {
                        countD+=count;
                    }

                    //檢查是否看到五顆棋子
                    if (countA >= 6||countB >= 6 ||countC >= 6 ||countD >= 6)
                    {
                        winner = currentPlayer;
                    }
                    //if (count == 5)
                    //{
                    //    winner = currentPlayer;
                    //}

                   

                }
            }
            countA = 0;
            countB = 0;
            countC = 0;
            countD = 0;
        }
    }
}
