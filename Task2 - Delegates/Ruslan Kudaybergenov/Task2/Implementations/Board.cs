using System;
using Task2.Abstractions;

namespace Task2.Implementations
{
    public class Board : IBoard
    {
        public int BoardSizeX
        {
            get
            {
                return boardSizeX;
            }
            set
            {
                boardSizeX = value;
            }
        }
        int boardSizeX;
        public int BoardSizeY
        {
            get
            {
                return boardSizeY;
            }
            set
            {
                boardSizeY = value;
            }
        }
        int boardSizeY;
        public int BoardPositionX
        {
            get
            {
                return boardPositionX;
            }
            set
            {
                boardPositionX = value;
            }
        }
        int boardPositionX;
        public int BoardPositionY
        { 
            get
            {
                return boardPositionY;
            }
            set
            {
                boardPositionY = value;
            }
        }
        int boardPositionY;
        public string BoardLineMarkerX
        {
            get
            {
                return boardLineMarkerX;
            }
            set
            {
                boardLineMarkerX = value;
            }
        }
        string boardLineMarkerX;
        public string BoardLineMarkerY
        {
            get
            {
                return boardLineMarkerY;
            }
            set
            {
                boardLineMarkerY = value;
            }
        }
        string boardLineMarkerY;

        public Board()
        {
            this.boardSizeX = 10;
            this.boardSizeY = 10;
            this.BoardPositionX = Console.CursorLeft;
            this.boardPositionY = Console.CursorTop;
            this.boardLineMarkerX = "-";
            this.boardLineMarkerY = "|";
        }
        public Board(int sizeX,int sizeY)
        {
            this.boardSizeX = sizeX;
            this.boardSizeY = sizeY;
        }
    }
}
