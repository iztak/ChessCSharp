using System;
using System.Windows;

using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChessCSharp
{
    class Board :Tile
    {
        public int size = 8;

        public Board(List<Tile> tiles)
        {
            Tile[,] board = new Tile[size, size];

            for(int i = 0; i < size ; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        board[i,j] = new Tile(Brushes.SaddleBrown, i, j);
                        tiles.Add(board[i, j]);
                    }
                    else
                    {
                        board[i, j] = new Tile(Brushes.Beige, i, j);
                        tiles.Add(board[i, j]);
                    }
                }
            }
        }

    }

    public class Tile : TextBlock
    {
        public Point currentPos;
        public bool isEmpty = true;
        public pieceColor colorOfPiece = pieceColor.empty;
 
        public Rectangle square = new Rectangle()
        {
            Width = 70,
            Height = 70,
        };

        //default constructor
        public Tile() { }


        //constructor
        public Tile (SolidColorBrush color, int i, int j)
        {
            SetValue(Grid.ColumnProperty, i);
            SetValue(Grid.RowProperty, j);
            Background = color;
            currentPos.X = j;
            currentPos.Y = i;
        }


    }
    public enum pieceColor
    {
        empty = 0,
        white = 1,
        black = -1
    }

    abstract public class Piece : Tile
    {
        public bool check = false;
        public bool checkMate = false;
        public pieceColor color;
        public Point nextPos;
        public bool firstMove = true;

        abstract public int IsValidMove(Piece p1, Tile destination, List<Piece> piece);

        public static bool CheckForCheck(Piece p1, Tile king, List<Piece> piece)
        {

           p1.nextPos = king.currentPos;

           if (p1.IsValidMove(p1, king, piece) == 2)
           {
               return true;
           }

            return false;
        }

        public static bool CheckForCheckKing(pieceColor color, Tile king, List<Piece> piece)
        {
            for(int i = 0; i < piece.Count; i++)
            {
                Piece p1;
                p1 = piece[i];
                
                if (p1.color != color)
                {
                    p1.nextPos = king.currentPos;

                    if (p1.IsValidMove(p1, king, piece) == 2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool CheckForCheckmate(pieceColor color, Point kingPos, List<Tile> tiles, List<Piece> piece)
        {
            List<Tile> kingTiles = new List<Tile>();
            int checks = 0;

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {

                Point kingPoint = new Point(kingPos.X -x, kingPos.Y -y);

                    for(int i = 0; i < tiles.Count; i++)
                    {
                        Tile tile = tiles[i];

                        if (tile.currentPos == kingPoint && tile.isEmpty==true)
                        {
                            kingTiles.Add(tile);
                        }

                    }

                }
            }

            for (int i = 0; i < kingTiles.Count; i++) 
            {
                Tile tile = kingTiles[i];

                if (CheckForCheckKing(color, tile, piece))
                {
                    checks++;
                }

                else
                {
                    break;
                }
            }


            return checks== kingTiles.Count;
        }


        public static bool CheckForPiece(Piece p1, List<Piece> piece)
        {
            int dirx = 1;
            if (p1.currentPos.X > p1.nextPos.X)
            { dirx = -1; }
            else if (p1.currentPos.X == p1.nextPos.X)
            { dirx = 0; }

            int diry = 1;
            if (p1.currentPos.Y > p1.nextPos.Y)
            { diry = -1; }
            else if (p1.currentPos.Y == p1.nextPos.Y)
            { diry = 0; }


            for (int i = 1; ((p1.currentPos.X + (dirx * i)) != p1.nextPos.X) || ((p1.currentPos.Y + (diry * i)) != p1.nextPos.Y); i++)
            {

                Point p;
                p.X = p1.currentPos.X + (dirx * i);
                p.Y = p1.currentPos.Y + (diry * i);

                foreach (var p3 in piece)
                    if (p3.currentPos == p)
                    {
                        return true;
                    }
            }

            return false;

        }

        public class Pawn : Piece
        {
            public new bool firstMove = true;

            public override int IsValidMove(Piece p1, Tile destination, List<Piece> piece)
            {
                if ((p1.nextPos.X - p1.currentPos.X) == (int)p1.color * 1 && Math.Abs(p1.nextPos.Y - p1.currentPos.Y) == 0 && destination.isEmpty == true)
                {
                    return 1;
                }

                else if ((p1.nextPos.X - p1.currentPos.X) == (int)p1.color * 2 && Math.Abs(p1.nextPos.Y - p1.currentPos.Y) == 0 && destination.isEmpty == true && p1.firstMove == true)
                {
                    return 1;
                }

                else if (Math.Abs(p1.nextPos.X - p1.currentPos.X) ==  1 && Math.Abs(p1.nextPos.Y - p1.currentPos.Y) == 1 && destination.isEmpty == false)
                {
                    return 2;
                }

                else return 0;

            }

            public static Pawn CreateNewPawn(string text,pieceColor color, int i, int j)
            {
                Piece.Pawn pawn = new Piece.Pawn();
                pawn.Text = text;
                pawn.TextAlignment = TextAlignment.Center;
                pawn.FontSize = (color == pieceColor.black ? 50 : 60);
                pawn.SetValue(Grid.ColumnProperty, i);
                pawn.SetValue(Grid.RowProperty, j);
                pawn.color = color;
                pawn.currentPos.Y = i;
                pawn.currentPos.X = j;
                pawn.isEmpty = false;
                pawn.firstMove = true;
                pawn.colorOfPiece = color;

                return pawn;
            }

        };
        public class Rook : Piece
        {

            public override int IsValidMove(Piece p1, Tile destination, List<Piece> piece)
            {
                if (CheckForPiece(p1, piece) == false)
                {

                    if ((Math.Abs(p1.nextPos.X - p1.currentPos.X) != 0 && (p1.nextPos.Y - p1.currentPos.Y) == 0) || Math.Abs(p1.nextPos.Y - p1.currentPos.Y) != 0 && (p1.nextPos.X - p1.currentPos.X) == 0)
                        
                    {
                        if (destination.isEmpty == true)
                        {

                            return 1;
                        }

                        else
                        {
                            return 2;
                        }
                    }


                }
                return 0;

            }
            public static Rook CreateNewRook(string text, pieceColor color, int i, int j)
            {
                Piece.Rook rook = new Piece.Rook();
                rook.Text = text;
                rook.TextAlignment = TextAlignment.Center;
                rook.FontSize = 60;
                rook.SetValue(Grid.ColumnProperty, i);
                rook.SetValue(Grid.RowProperty, j);
                rook.color = color;
                rook.currentPos.Y = i;
                rook.currentPos.X = j;
                rook.isEmpty = false;
                rook.colorOfPiece = color;

                return rook;
            }

        };
        public class Knight : Piece
        {

            public override int IsValidMove(Piece p1, Tile destination, List<Piece> piece)
            {

                if (Math.Abs(p1.nextPos.X - p1.currentPos.X) == 2 &&  Math.Abs(p1.nextPos.Y - p1.currentPos.Y) == 1)
                {
                    if (destination.isEmpty == true)
                    {

                        return 1;
                    }

                    else
                    {

                        return 2;
                    }
                }
                else if (Math.Abs(p1.nextPos.X - p1.currentPos.X) == 1 && Math.Abs(p1.nextPos.Y - p1.currentPos.Y) == 2)
                {
                    if (destination.isEmpty == true)
                    {

                        return 1;
                    }

                    else
                    {
                        return 2;
                    }
                }

                return 0;

            }
            public static Knight CreateNewKnight(string text, pieceColor color, int i, int j)
            {
                Piece.Knight knight = new Piece.Knight();
                knight.Text = text;
                knight.TextAlignment = TextAlignment.Center;
                knight.FontSize = 60;
                knight.SetValue(Grid.ColumnProperty, i);
                knight.color = color;
                knight.SetValue(Grid.RowProperty, j);
                knight.currentPos.X = j;
                knight.currentPos.Y = i;
                knight.isEmpty = false;
                knight.colorOfPiece = color;

                return knight;
            }


        };
        public class Bishop : Piece
        {

            public override int IsValidMove(Piece p1, Tile destination, List<Piece> piece)
            {
                if (CheckForPiece(p1, piece) == false)
                {

                    if (Math.Abs(p1.nextPos.X - p1.currentPos.X) == Math.Abs(p1.nextPos.Y - p1.currentPos.Y))
                    {
                        if (destination.isEmpty == true)
                        {

                            return 1;
                        }

                        else
                        {
                            return 2;
                        }
                    }

                }
                return 0;

            }
            public static Bishop CreateNewBishop(string text, pieceColor color, int i, int j)
            {
                Piece.Bishop bishop = new Piece.Bishop();
                bishop.Text = text;
                bishop.TextAlignment = TextAlignment.Center;
                bishop.FontSize = 60;
                bishop.SetValue(Grid.ColumnProperty, i);
                bishop.color = color;
                bishop.SetValue(Grid.RowProperty, j);
                bishop.currentPos.X = j;
                bishop.currentPos.Y = i;
                bishop.isEmpty = false;
                bishop.colorOfPiece = color;

                return bishop;
            }


        };
        public class Queen : Piece
        {
            public override int IsValidMove(Piece p1, Tile destination, List<Piece> piece)
            {
                if (CheckForPiece(p1, piece) == false)
                {

                    if ((Math.Abs(p1.nextPos.X - p1.currentPos.X) != 0 && (p1.nextPos.Y - p1.currentPos.Y) == 0) || (Math.Abs(p1.nextPos.Y - p1.currentPos.Y) != 0 && (p1.nextPos.X - p1.currentPos.X) == 0))
                    {
                        if (destination.isEmpty == true)
                        {

                            return 1;
                        }

                        else
                        {
                            return 2;
                        }
                    }
                    else if (Math.Abs(p1.nextPos.X - p1.currentPos.X) ==  Math.Abs(p1.nextPos.Y - p1.currentPos.Y) )
                    {
                        if (destination.isEmpty == true)
                        {

                            return 1;
                        }

                        else
                        {

                            return 2;
                        }
                    }


                }
                return 0;

            }
            public static Queen CreateNewQueen(string text, pieceColor color, int i, int j)
            {
                Piece.Queen queen = new Piece.Queen();
                queen.Text = text;
                queen.TextAlignment = TextAlignment.Center;
                queen.FontSize = 60;
                queen.SetValue(Grid.ColumnProperty, i);
                queen.color = color;
                queen.SetValue(Grid.RowProperty, j);
                queen.currentPos.X = j;
                queen.currentPos.Y = i;
                queen.isEmpty = false;
                queen.colorOfPiece = color;

                return queen;
            }

        };
        public class King : Piece
        {
            public static King whiteKing;
            public static King blackKing;

            public override int IsValidMove(Piece p1, Tile destination, List<Piece> piece)
            {


                    if (Math.Abs(p1.nextPos.X - p1.currentPos.X) <= 1 && Math.Abs(p1.nextPos.Y - p1.currentPos.Y) <= 1 && p1.currentPos != p1.nextPos)
                    {
                        if (destination.isEmpty == true)
                        {

                            return 1;
                        }

                        else
                        {

                            return 2;
                        }
                    }


                return 0;

            }
            public static King CreateNewKing(string text, pieceColor color, int i, int j)
            {
                Piece.King king = new Piece.King();
                king.Text = text;
                king.TextAlignment = TextAlignment.Center;
                king.FontSize = 60;
                king.SetValue(Grid.ColumnProperty, i);
                king.color = color;
                king.SetValue(Grid.RowProperty, j);
                king.currentPos.X = j;
                king.currentPos.Y = i;
                king.isEmpty = false;
                king.colorOfPiece = color;

                return king;
            }


        };


    }

}
