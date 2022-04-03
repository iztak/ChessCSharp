using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ChessCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
        public partial class MainWindow : Window
    {
        List<Piece> piece = new List<Piece>();
        bool initializePiece;
        Piece pieceToMove;
        Point currentPoint;
        Point nextPoint;
        pieceColor pieceToMoveColor;



        public MainWindow()
        {

            InitializeComponent();
            InitializeBoard();

        }
        private void GameControlIsClicked(object sender, RoutedEventArgs e)
        {

            InitializeBoard();
        }

        private void InitializeBoard()
        {
            Score.Text = "";
            piece.Clear();
            pieceToMove = null;
            initializePiece = false;


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        Tile t1 = Tile.newTile(Brushes.SaddleBrown,i, j);
                        Grid.Children.Add(t1);
                    }
                    else
                    {
                        Tile t2 = Tile.newTile(Brushes.Beige, i, j);
                        Grid.Children.Add(t2);
                    }
                }
            }



            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0)
                    {
                        if (i == 0 || i == 7)
                        {
                            Piece.Rook whiteRook = Piece.Rook.createNewRook("♖", pieceColor.white, i, j);
                            Grid.Children.Add(whiteRook);
                            piece.Add(whiteRook);
                        }

                        if (i == 1 || i == 6)
                        {
                            Piece.Knight whiteKnight = Piece.Knight.createNewKnight("♘",pieceColor.white, i, j);
                            Grid.Children.Add(whiteKnight);
                            piece.Add(whiteKnight);
                        }

                        if (i == 2 || i == 5)
                        {
                            Piece.Bishop whiteBishop = Piece.Bishop.createNewBishop("♗", pieceColor.white, i, j);
                            Grid.Children.Add(whiteBishop);
                            piece.Add(whiteBishop);

                        }
                        if (i == 3)
                        {
                            Piece.Queen whiteQueen = Piece.Queen.createNewQueen("♕",pieceColor.white, i, j);
                            Grid.Children.Add(whiteQueen);
                            piece.Add(whiteQueen);

                        }
                        if (i == 4)
                        {
                            Piece.King whiteKing = Piece.King.createNewKing("♔", pieceColor.white, i, j);
                            Piece.King.whiteKingPos = whiteKing.gridPosition;
                            Grid.Children.Add(whiteKing);
                            piece.Add(whiteKing);
                        }
                    }

                    if (j == 1)
                    {
                        Piece.Pawn whitePawn = Piece.Pawn.createNewPawn("♙", pieceColor.white, i, j);
                        Grid.Children.Add(whitePawn);
                        piece.Add(whitePawn);
                    }

                    else if (j == 6)
                    {
                        Piece.Pawn blackPawn = Piece.Pawn.createNewPawn("♟", pieceColor.black, i, j); 
                        Grid.Children.Add(blackPawn);
                        piece.Add(blackPawn);
                    }

                    if (j == 7)
                    {
                        if (i == 0 || i == 7)
                        {
                            Piece.Rook blackRook = Piece.Rook.createNewRook("♜",pieceColor.black, i, j);
                            Grid.Children.Add(blackRook);
                            piece.Add(blackRook);

                        }

                        if (i == 1 || i == 6)
                        {
                            Piece.Knight blackKnight = Piece.Knight.createNewKnight("♞", pieceColor.black, i, j);
                            Grid.Children.Add(blackKnight);
                            piece.Add(blackKnight);
                        }


                        if (i == 2 || i == 5)
                        {
                            Piece.Bishop blackBishop = Piece.Bishop.createNewBishop("♝",pieceColor.black, i, j);
                            Grid.Children.Add(blackBishop);
                            piece.Add(blackBishop);
                        }
                        if (i == 3)
                        {
                            Piece.Queen blackQueen = Piece.Queen.createNewQueen("♛",pieceColor.black, i, j);
                            Grid.Children.Add(blackQueen);
                            piece.Add(blackQueen);
                        }
                        if (i == 4)
                        {
                            Piece.King blackKing = Piece.King.createNewKing("♚",pieceColor.black, i, j);
                            Piece.King.blackKingPos = blackKing.gridPosition;
                            Grid.Children.Add(blackKing);
                            piece.Add(blackKing);

                        }
                    }
                }
            }
        }


        private void WindowIsClicked(object sender, MouseButtonEventArgs e)
        {
            if (piece.Count() != 0)
            {

                if (e.OriginalSource is Piece && initializePiece == false)
                {
                    Score.Text = "";
                    Piece p1 = (Piece)e.OriginalSource;
                    if(p1.color != pieceToMoveColor)
                    {
                        currentPoint = p1.gridPosition;
                        pieceToMove = p1;
                        initializePiece = true;
                        pieceToMove.Foreground = Brushes.Red;
                    }
                    else { Score.Text = $"Not {pieceToMoveColor} turn."; }

                }

                else if (initializePiece == true && e.OriginalSource is Tile)
                {
                    Tile t1 = (Tile)e.OriginalSource;
                    nextPoint = t1.gridPosition;
                    MovePiece(currentPoint, nextPoint, pieceToMove, t1);
                }


                else
                {
                    pieceToMove = null;
                }
            }

        }

        private void MovePiece( Point currentPoint, Point nextPoint, Piece pieceToMove, Tile destination)
        {
            int direction = (int)pieceToMove.color;
            int result = 0;
            bool check = false;
            
            if(destination.color == pieceToMove.color)
            {
                result = 0;
            }
            else 
            {
                if(pieceToMove.color == pieceColor.black)
                {
                    check = (Piece.checkforCheck(pieceToMove, nextPoint, Piece.King.whiteKingPos, direction, piece));
                }
                else
                {
                    check = (Piece.checkforCheck(pieceToMove, nextPoint, Piece.King.blackKingPos, direction, piece));
                }
                   
                result = pieceToMove.IsValidMove(currentPoint, nextPoint, destination.isEmpty, direction, pieceToMove.initial, piece);
                if(pieceToMove.GetType() == typeof(Piece.Pawn))
                {
                    pawnPromotion(pieceToMove, nextPoint, piece);
                }
               

            }


            if(check == true)
            {
                Score.Text = "Check";
            }

            moveResult(result, nextPoint, pieceToMove, destination);

            initializePiece = false;
            pieceToMove.Foreground = Brushes.Black;
            pieceToMove = null;

        }

        private void moveResult(int result, Point nextPoint, Piece pieceToMove, Tile destination)
        {
            if (result >= 1)
            {
                pieceToMove.SetValue(Grid.ColumnProperty, (Int32)nextPoint.Y);
                pieceToMove.SetValue(Grid.RowProperty, (Int32)nextPoint.X);
                pieceToMove.gridPosition = nextPoint;
                pieceToMoveColor = pieceToMove.color;
                pieceToMove.initial = false;

                if (result == 2)
                {
                    foreach (Piece p1 in piece)
                    {
                        if (p1.GetValue(Grid.ColumnProperty) == destination.GetValue(Grid.ColumnProperty) && p1.GetValue(Grid.RowProperty) == destination.GetValue(Grid.RowProperty))
                        {
                            Grid.Children.Remove(destination);
                            piece.Remove(p1);
                            if (p1.GetType() == typeof(Piece.King))
                            {
                                Score.Text = "Game over";
                                piece.Clear();
                            }
                            break;
                        }
                    }

                }
            }

            else
            {
                Score.Text = "Not a valid move.";

            }

 
        }

        private void pawnPromotion(Piece pieceToMove, Point nextPoint, List<Piece> piece)
        {
            if ((pieceToMove.color == pieceColor.white && nextPoint.X == 7) || (pieceToMove.color == pieceColor.black && nextPoint.X == 0))
            {
                pieceToMove.SetValue(Grid.ColumnProperty, (Int32)nextPoint.Y);
                pieceToMove.SetValue(Grid.RowProperty, (Int32)nextPoint.X);

                Dialog d1 = new Dialog();
                d1.ShowDialog();

                Piece p1 = d1.isClicked(pieceToMove.color, (Int32)nextPoint.Y, (Int32)nextPoint.X);
                Grid.Children.Add(p1);
                piece.Add(p1);

                Grid.Children.Remove(pieceToMove);
                piece.Remove(pieceToMove);

            }
        }

    }

}
