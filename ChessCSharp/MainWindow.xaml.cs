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
        public List<Piece> piece = new List<Piece>();
        public List<Piece> whitePiece = new List<Piece>();
        public List<Piece> blackPiece = new List<Piece>();
        bool initializePiece;
        Piece pieceToMove;
        pieceColor pieceToMoveColor;
        public List<Tile> tiles = new List<Tile>();


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
            pieceToMoveColor = pieceColor.empty;

            Board myBoard = new Board(tiles);
            PrintBoard(myBoard);


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0)
                    {
                        if (i == 0 || i == 7)
                        {
                            Piece.Rook whiteRook = Piece.Rook.CreateNewRook("♖", pieceColor.white, i, j);
                            Grid.Children.Add(whiteRook);
                            whitePiece.Add(whiteRook);
                            piece.Add(whiteRook);
                        }

                        if (i == 1 || i == 6)
                        {
                            Piece.Knight whiteKnight = Piece.Knight.CreateNewKnight("♘",pieceColor.white, i, j);
                            Grid.Children.Add(whiteKnight);
                            whitePiece.Add(whiteKnight);
                            piece.Add(whiteKnight);
                        }

                        if (i == 2 || i == 5)
                        {
                            Piece.Bishop whiteBishop = Piece.Bishop.CreateNewBishop("♗", pieceColor.white, i, j);
                            Grid.Children.Add(whiteBishop);
                            whitePiece.Add(whiteBishop);
                            piece.Add(whiteBishop);
                        }
                        if (i == 3)
                        {
                            Piece.Queen whiteQueen = Piece.Queen.CreateNewQueen("♕",pieceColor.white, i, j);
                            Grid.Children.Add(whiteQueen);
                            whitePiece.Add(whiteQueen);
                            piece.Add(whiteQueen);
                        }
                        if (i == 4)
                        {
                            Piece.King.whiteKing = Piece.King.CreateNewKing("♔", pieceColor.white, i, j);
                            Grid.Children.Add(Piece.King.whiteKing);
                            whitePiece.Add(Piece.King.whiteKing);
                            piece.Add(Piece.King.whiteKing);
                        }
                    }

                    if (j == 1)
                    {
                        Piece.Pawn whitePawn = Piece.Pawn.CreateNewPawn("♙", pieceColor.white, i, j);
                        Grid.Children.Add(whitePawn);
                        whitePiece.Add(whitePawn);
                        piece.Add(whitePawn);
                    }

                    else if (j == 6)
                    {
                        Piece.Pawn blackPawn = Piece.Pawn.CreateNewPawn("♟", pieceColor.black, i, j); 
                        Grid.Children.Add(blackPawn);
                        blackPiece.Add(blackPawn);
                        piece.Add(blackPawn);
                    }

                    if (j == 7)
                    {
                        if (i == 0 || i == 7)
                        {
                            Piece.Rook blackRook = Piece.Rook.CreateNewRook("♜",pieceColor.black, i, j);
                            Grid.Children.Add(blackRook);
                            blackPiece.Add(blackRook);
                            piece.Add(blackRook);
                        }

                        if (i == 1 || i == 6)
                        {
                            Piece.Knight blackKnight = Piece.Knight.CreateNewKnight("♞", pieceColor.black, i, j);
                            Grid.Children.Add(blackKnight);
                            blackPiece.Add(blackKnight);
                            piece.Add(blackKnight);
                        }


                        if (i == 2 || i == 5)
                        {
                            Piece.Bishop blackBishop = Piece.Bishop.CreateNewBishop("♝",pieceColor.black, i, j);
                            Grid.Children.Add(blackBishop);
                            blackPiece.Add(blackBishop);
                            piece.Add(blackBishop);
                        }
                        if (i == 3)
                        {
                            Piece.Queen blackQueen = Piece.Queen.CreateNewQueen("♛",pieceColor.black, i, j);
                            Grid.Children.Add(blackQueen);
                            blackPiece.Add(blackQueen);
                            piece.Add(blackQueen);
                        }
                        if (i == 4)
                        {
                            Piece.King.blackKing = Piece.King.CreateNewKing("♚",pieceColor.black, i, j);
                            Grid.Children.Add(Piece.King.blackKing);
                            blackPiece.Add(Piece.King.blackKing);
                            piece.Add(Piece.King.blackKing);
                        }
                    }
                }
            }
        }


        private void WindowIsClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Piece && initializePiece == false)
            {
                Score.Text = "";
                Piece p1 = (Piece)e.OriginalSource;
                if(p1.color != pieceToMoveColor)
                {
                    pieceToMove = p1;
                    initializePiece = true;
                    pieceToMove.Foreground = Brushes.Red;
                }
                else { Score.Text = $"Not {pieceToMoveColor} turn."; }

            }

            else if (initializePiece == true && e.OriginalSource is Tile)
            {
                Tile t1 = (Tile)e.OriginalSource;
                MovePiece(t1.currentPos, pieceToMove, t1);
            }


            else
            {
                pieceToMove = null;
            }
        

        }

        private void MovePiece(Point nextPoint, Piece pieceToMove, Tile destination)
        {
            int result = 0;
            
            pieceToMove.nextPos = nextPoint;

            if (pieceToMove.color != destination.colorOfPiece)
            { result = pieceToMove.IsValidMove(pieceToMove, destination, piece); }

            if(pieceToMove.GetType() == typeof(Piece.Pawn))
            {
                PawnPromotion(pieceToMove, nextPoint, piece);
            }
               
            MoveResult(result, nextPoint, pieceToMove, destination);

            CheckForCheckmateAndCheck(pieceToMove);


            initializePiece = false;
            pieceToMove.Foreground = Brushes.Black;
            pieceToMove = null;

        }

        private void MoveResult(int result, Point nextPoint, Piece pieceToMove, Tile destination)
        {
            if (result >= 1)
            {
                pieceToMove.SetValue(Grid.ColumnProperty, (Int32)nextPoint.Y);
                pieceToMove.SetValue(Grid.RowProperty, (Int32)nextPoint.X);
                pieceToMove.currentPos = nextPoint;
                pieceToMove.firstMove = false;
                pieceToMoveColor = pieceToMove.color;
                destination.colorOfPiece = pieceToMoveColor;

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

        private void PawnPromotion(Piece pieceToMove, Point nextPoint, List<Piece> piece)
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

        private void PrintBoard (Board b1)
        {
            for (int i = 0; i < b1.size; i++)
            {
                for (int j = 0; j < b1.size; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        Tile t1 = new Tile(Brushes.SaddleBrown, i, j);
                        Grid.Children.Add(t1);
                    }
                    else
                    {
                        Tile t2 = new Tile(Brushes.Beige, i, j);
                        Grid.Children.Add(t2);
                    }
                }
            }
        }

        private void CheckForCheckmateAndCheck(Piece pieceToMove)
        {
            bool check = false;
            bool checkmate = false;

            if (pieceToMove.color == pieceColor.black)
            {
                if(pieceToMove == Piece.King.blackKing)
                {
                    check = Piece.CheckForCheckKing(pieceToMove.color, Piece.King.blackKing, whitePiece);
                }

                else
                {
                    check = Piece.CheckForCheck(pieceToMove, Piece.King.whiteKing, blackPiece); 
                }
                
            }
            else 
            {
                if (pieceToMove == Piece.King.whiteKing)
                {
                    check = Piece.CheckForCheckKing(pieceToMove.color, Piece.King.whiteKing, blackPiece);
                }
                else
                {
                    check = Piece.CheckForCheck(pieceToMove, Piece.King.blackKing, whitePiece);
                }

            }

            if (check)
            {
                Score.Text = "Check";

                if (pieceToMove.color == pieceColor.black)
                {
                    checkmate = Piece.CheckForCheckmate(pieceToMove.color, Piece.King.whiteKing.currentPos, tiles, blackPiece);
                }
                else
                {
                    checkmate = Piece.CheckForCheckmate(pieceToMove.color, Piece.King.blackKing.currentPos, tiles, whitePiece);
                }
            }
            if (checkmate)
            {
                Score.Text = "Checkmate";
            }
        }
    }

}
