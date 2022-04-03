using System;
using System.Windows;

using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChessCSharp
{
    class Board
    {

    }

    public class Tile : TextBlock
    {
        public Point gridPosition;
        public bool isEmpty = true;
        public SolidColorBrush fillColour;
        public Rectangle square = new Rectangle()
        {
            Width = 70,
            Height = 70,
        };
        public pieceColor color;

        public static Tile newTile(SolidColorBrush color, int i, int j)
        {
            Tile t1 = new Tile();
            t1.SetValue(Grid.ColumnProperty, i);
            t1.SetValue(Grid.RowProperty, j);
            t1.Background = color;
            t1.gridPosition.X = j;
            t1.gridPosition.Y = i;
            return t1;
        }


    }
    public enum pieceColor
    {
        white = 1,
        black = -1
    }

    abstract public class Piece : Tile
    {
        public bool check = false;
        public bool checkMate = false;
        public enum pieceType
        {
            pawn,
            rook,
            knight,
            bishop,
            queen,
            king

        }

        public bool initial = true;
        abstract public int IsValidMove(Point now, Point next, bool isEmpty, int dir, bool initial, List<Piece> piece);
 

        public static bool checkforCheck(Piece p1, Point next, Point king, int dir, List<Piece> piece)
        {
            if (p1.IsValidMove(next, king, false, dir, false, piece) == 2)
            {
                return true;
            }

            return false;
        }


        public static bool checkForPiece(Point now, Point next, List<Piece> piece)
        {
            int dirx = 1;
            if (now.X > next.X)
            { dirx = -1; }
            else if (now.X == next.X)
            { dirx = 0; }

            int diry = 1;
            if (now.Y > next.Y)
            { diry = -1; }
            else if (now.Y == next.Y)
            { diry = 0; }


            for (int i = 1; ((now.X + (dirx * i)) != next.X) || ((now.Y + (diry * i)) != next.Y); i++)
            {

                Point p;
                p.X = now.X + (dirx * i);
                p.Y = now.Y + (diry * i);

                foreach (var p1 in piece)
                    if (p1.gridPosition == p)
                    {
                        return true;
                    }
            }

            return false;

        }

        public class Pawn : Piece
        {
            public override int IsValidMove(Point now, Point next, bool isEmpty, int dir, bool initial, List<Piece> piece)
            {


                if ((next.X - now.X) == dir * 1 && Math.Abs(next.Y - now.Y) == 0 && isEmpty == true)
                {
                    return 1;
                }

                else if ((next.X - now.X) == dir * 2 && Math.Abs(next.Y - now.Y) == 0 && isEmpty == true && initial == true)
                {
                    return 1;
                }

                else if (Math.Abs(next.X - now.X) ==  1 && Math.Abs(next.Y - now.Y) == 1 && isEmpty == false)
                {
                    return 2;
                }

                else return 0;

            }

            public static Pawn createNewPawn(string text,pieceColor color, int i, int j)
            {
                Piece.Pawn pawn = new Piece.Pawn();
                pawn.Text = text;
                pawn.TextAlignment = TextAlignment.Center;
                pawn.FontSize = 60;
                pawn.SetValue(Grid.ColumnProperty, i);
                pawn.SetValue(Grid.RowProperty, j);
                pawn.color = color;
                pawn.gridPosition.Y = i;
                pawn.gridPosition.X = j;
                pawn.isEmpty = false;

                return pawn;
            }

        };
        public class Rook : Piece
        {

            public override int IsValidMove(Point now, Point next, bool isEmpty, int dir, bool initial, List<Piece> piece)
            {
                if (checkForPiece(now, next, piece) == false)
                {

                    if ((Math.Abs(next.X - now.X) != 0 && (next.Y - now.Y) == 0) || Math.Abs(next.Y - now.Y) != 0 && (next.X - now.X) == 0)
                        
                    {
                        if (isEmpty == true)
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
            public static Rook createNewRook(string text, pieceColor color, int i, int j)
            {
                Piece.Rook rook = new Piece.Rook();
                rook.Text = text;
                rook.TextAlignment = TextAlignment.Center;
                rook.FontSize = 60;
                rook.SetValue(Grid.ColumnProperty, i);
                rook.SetValue(Grid.RowProperty, j);
                rook.color = color;
                rook.gridPosition.Y = i;
                rook.gridPosition.X = j;
                rook.isEmpty = false;

                return rook;
            }

        };
        public class Knight : Piece
        {

            public override int IsValidMove(Point now, Point next, bool isEmpty, int dir, bool initial, List<Piece> piece)
            {

                if (Math.Abs(next.X - now.X) == 2 &&  Math.Abs(next.Y - now.Y) == 1)
                {
                    if (isEmpty == true)
                    {

                        return 1;
                    }

                    else
                    {

                        return 2;
                    }
                }
                else if (Math.Abs(next.X - now.X) == 1 && Math.Abs(next.Y - now.Y) == 2)
                {
                    if (isEmpty == true)
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
            public static Knight createNewKnight(string text, pieceColor color, int i, int j)
            {
                Piece.Knight knight = new Piece.Knight();
                knight.Text = text;
                knight.TextAlignment = TextAlignment.Center;
                knight.FontSize = 60;
                knight.SetValue(Grid.ColumnProperty, i);
                knight.color = color;
                knight.SetValue(Grid.RowProperty, j);
                knight.gridPosition.X = j;
                knight.gridPosition.Y = i;
                knight.isEmpty = false;

                return knight;
            }


        };
        public class Bishop : Piece
        {

            public override int IsValidMove(Point now, Point next, bool isEmpty, int dir, bool initial, List<Piece> piece)
            {
                if (checkForPiece(now, next, piece) == false)
                {

                    if (Math.Abs(next.X - now.X) == Math.Abs(next.Y - now.Y))
                    {
                        if (isEmpty == true)
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
            public static Bishop createNewBishop(string text, pieceColor color, int i, int j)
            {
                Piece.Bishop bishop = new Piece.Bishop();
                bishop.Text = text;
                bishop.TextAlignment = TextAlignment.Center;
                bishop.FontSize = 60;
                bishop.SetValue(Grid.ColumnProperty, i);
                bishop.color = color;
                bishop.SetValue(Grid.RowProperty, j);
                bishop.gridPosition.X = j;
                bishop.gridPosition.Y = i;
                bishop.isEmpty = false;

                return bishop;
            }


        };
        public class Queen : Piece
        {
            public override int IsValidMove(Point now, Point next, bool isEmpty, int dir, bool initial, List<Piece> piece)
            {
                if (checkForPiece(now, next, piece) == false)
                {

                    if ((Math.Abs(next.X - now.X) != 0 && (next.Y - now.Y) == 0) || (Math.Abs(next.Y - now.Y) != 0 && (next.X - now.X) == 0))
                    {
                        if (isEmpty == true)
                        {

                            return 1;
                        }

                        else
                        {
                            return 2;
                        }
                    }
                    else if (Math.Abs(next.X - now.X) ==  Math.Abs(next.Y - now.Y) )
                    {
                        if (isEmpty == true)
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
            public static Queen createNewQueen(string text, pieceColor color, int i, int j)
            {
                Piece.Queen queen = new Piece.Queen();
                queen.Text = text;
                queen.TextAlignment = TextAlignment.Center;
                queen.FontSize = 60;
                queen.SetValue(Grid.ColumnProperty, i);
                queen.color = color;
                queen.SetValue(Grid.RowProperty, j);
                queen.gridPosition.X = j;
                queen.gridPosition.Y = i;
                queen.isEmpty = false;

                return queen;
            }

        };
        public class King : Piece
        {
            public static Point whiteKingPos;
            public static Point blackKingPos;
            public override int IsValidMove(Point now, Point next, bool isEmpty, int dir, bool initial, List<Piece> piece)
            {


                    if (Math.Abs(next.X - now.X) <= 1 && Math.Abs(next.Y - now.Y) <= 1 && now != next)
                    {
                        if (isEmpty == true)
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
            public static King createNewKing(string text, pieceColor color, int i, int j)
            {
                Piece.King king = new Piece.King();
                king.Text = text;
                king.TextAlignment = TextAlignment.Center;
                king.FontSize = 60;
                king.SetValue(Grid.ColumnProperty, i);
                king.color = color;
                king.SetValue(Grid.RowProperty, j);
                king.gridPosition.X = j;
                king.gridPosition.Y = i;
                king.isEmpty = false;

                return king;
            }


        };


    }

}
