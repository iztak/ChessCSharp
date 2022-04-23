using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChessCSharp
{
    /// <summary>
    /// Dialog.xaml の相互作用ロジック
    /// </summary>
    public partial class Dialog : Window
    {
        public Dialog()
        {
            InitializeComponent();
        }

        private bool RookIsClicked = false;
        private bool KnightIsClicked = false;
        private bool BishopIsClicked = false;
        private bool QueenIsClicked = false;

        private void OnRookClick(object sender, RoutedEventArgs e)
        {
            RookIsClicked = !RookIsClicked;
            this.Hide();

        }


        public Piece isClicked(pieceColor color , int i, int j)
        {
            if(RookIsClicked)
            {
                if(color == pieceColor.white)
                {
                    Piece.Rook rook = Piece.Rook.CreateNewRook("♖", color, i, j);
                    return rook;
                }
                else
                {
                    Piece.Rook rook = Piece.Rook.CreateNewRook("♜", color, i, j);
                    return rook;
                }
               
            }

            if (KnightIsClicked)
            {
                if(color == pieceColor.white)
                {
                    Piece.Knight knight = Piece.Knight.CreateNewKnight("♘", color, i, j);
                    return knight;
                }
                else
                {
                    Piece.Knight knight = Piece.Knight.CreateNewKnight("♞", color, i, j);
                    return knight;
                }
               
            }

            if (BishopIsClicked)
            {
                if(color == pieceColor.white)
                {
                    Piece.Bishop bishop = Piece.Bishop.CreateNewBishop("♗",color, i, j);
                    return bishop;
                }
                else
                {
                    Piece.Bishop bishop = Piece.Bishop.CreateNewBishop("♝", color, i, j);
                    return bishop;
                }

            }

            if (QueenIsClicked)
            {
                if(color ==pieceColor.white)
                {
                    Piece.Queen queen = Piece.Queen.CreateNewQueen("♕",color, i, j);
                    return queen;
                }
                else
                {
                    Piece.Queen queen = Piece.Queen.CreateNewQueen("♛", color, i, j);
                    return queen;
                }
 
            }

            return null;
        }

        private void OnKnightClick(object sender, RoutedEventArgs e)
        {
            KnightIsClicked = !KnightIsClicked;
            this.Hide();
        }

        private void OnBishopClick(object sender, RoutedEventArgs e)
        {
            BishopIsClicked = !BishopIsClicked;
            this.Hide();
        }

        private void OnQueenClick(object sender, RoutedEventArgs e)
        {
            QueenIsClicked = !QueenIsClicked;
            this.Hide();
        }
    }
}
