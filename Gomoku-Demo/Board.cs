using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gomoku_Demo
{
    class Board
    {
        private static readonly int OFFSET = 75;
        private static readonly int NODE_RADIUS = 5;
        private static readonly int NODE_DISTANCE = 75;
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);
        public static readonly int NODE_COUNT=9;
        private Piece[,] pieces = new Piece[NODE_COUNT, NODE_COUNT];
        private Point lastPlacedNode = NO_MATCH_NODE;
        public Point LastPlaceNode { get { return lastPlacedNode; } }
        public PieceType GetPieceType(int nodeIdX,int nodeIdY)
        {
            if (pieces[nodeIdX, nodeIdY] == null)
                return PieceType.NONE;
            else
                return pieces[nodeIdX, nodeIdY].GetPieceType();
        }
        public bool CanBePlace(int x, int y)
        {
            //TODO:找出最近節點
            Point nodeId = FindTheCloseNode(x, y);
            //TODO:如果沒有，回傳false
            if (nodeId == NO_MATCH_NODE)
                return false;

            
            //TODO:如果有，檢查是否有旗子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
            {
                return false;
            }
            return true;
        }
        public Piece PlaceAPiece(int x,int y,PieceType type)
        {
            //TODO:找出最近節點
            Point nodeId = FindTheCloseNode(x, y);
            //TODO:如果沒有，回傳false
            if (nodeId == NO_MATCH_NODE)
                return null;

            //TODO:如果有，檢查是否有旗子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
            {
                return null;
            }
            //TODO:根據type產生對應旗子
            Point formPos = convertToFormPosition(nodeId);
            if (type == PieceType.BLACK)
                pieces[nodeId.X, nodeId.Y] = new BlackPiece(formPos.X,formPos.Y);
            else if(type==PieceType.WHITE)
                pieces[nodeId.X, nodeId.Y] = new WhitePiece(formPos.X, formPos.Y);
            //紀錄最後下子位置
            lastPlacedNode = nodeId;

            return pieces[nodeId.X, nodeId.Y];
        }

        private Point convertToFormPosition(Point nodId)
        {
            Point formPostion = new Point();
            formPostion.X = nodId.X * NODE_DISTANCE + OFFSET;
            formPostion.Y = nodId.Y * NODE_DISTANCE + OFFSET;
            return formPostion;
        }
        private Point FindTheCloseNode(int x,int y)
        {
            int nodeIdX = FindTheClosetNode(x);
            if (nodeIdX == -1 || nodeIdX>=NODE_COUNT)
                return NO_MATCH_NODE;
            int nodeIdY = FindTheClosetNode(y);
            if (nodeIdY == -1||nodeIdY>=NODE_COUNT)
                return NO_MATCH_NODE;
            return new Point(nodeIdX, nodeIdY);
        }
        private int FindTheClosetNode(int pos)
        {
            pos -= OFFSET;
            int quotient = pos / NODE_DISTANCE;
            int remainder = pos % NODE_DISTANCE;
            if (remainder <= NODE_RADIUS)
                return quotient;
            else if (remainder >= NODE_DISTANCE - NODE_RADIUS)
                return quotient + 1;
            else
                return -1;
        }
    }
}
