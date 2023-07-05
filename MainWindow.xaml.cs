using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the current of cell in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player 1`s turn (X) or player 2`s turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if game has ended
        /// </summary>
        private bool mGameEnded;
        
        #endregion
        #region Costructor

        /// <summary>
        ///  Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        /// <summary>
        /// Statrs a new game and clears all values to the start
        /// </summary>
        private void NewGame()
        {
            mResults = new MarkType[9];
            for (int i = 0; i < mResults.Length; i++)
                mResults[i] =MarkType.Free;
            
            //MAke sure Player 1 starts the game
            mPlayer1Turn = true;
            //Iterate every button
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Red;
            });

            mGameEnded = false;



        }
        
        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //start a new game on click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;

            //Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (mResults[index] != MarkType.Free)
                return;

            //Set the call value based on which turn it is
            if (mPlayer1Turn)
                mResults[index] = MarkType.Cross;
            else
                mResults[index] = MarkType.Nought;
            button.Content = mPlayer1Turn ? "X" : "O";
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Blue;

            //Toggle players
            if (mPlayer1Turn)
                mPlayer1Turn = false;
            else
                mPlayer1Turn = true;

            //Check Winner
            CheckWin();
        }
        /// <summary>
        /// Color win button
        /// </summary>
                
        private void HighlightWinningCells(Button[,] winningCells)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Button button = winningCells[row, col];
                    if (button != null)
                    {
                        button.Background = Brushes.GreenYellow;
                    }
                }
            }
        }
        /// <summary>
        /// Check if there winner of round
        /// </summary>
        
        private void CheckWin()
        {
            // check for wins
            Button[,] winningCells = new Button[3, 3];

            // Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // ENDGAME
                mGameEnded = true;
                // Save wins
                winningCells[0, 0] = Button0;
                winningCells[0, 1] = Button1;
                winningCells[0, 2] = Button2;
                //Highlight winning cells green 
                HighlightWinningCells(winningCells);
            }
            // Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // ENDGAME
                mGameEnded = true;
                // Save wins
                winningCells[1, 0] = Button3;
                winningCells[1, 1] = Button4;
                winningCells[1, 2] = Button5;
                //Highlight winning cells green 
                HighlightWinningCells(winningCells);
            }
            // Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // ENDGAME
                mGameEnded = true;
                // Save wins
                winningCells[2, 0] = Button6;
                winningCells[2, 1] = Button7;
                winningCells[2, 2] = Button8;
                //Highlight winning cells green 
                HighlightWinningCells(winningCells);
            }
            // Col 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // ENDGAME
                mGameEnded = true;
                // Save wins
                winningCells[0, 0] = Button0;
                winningCells[1, 0] = Button3;
                winningCells[2, 0] = Button6;
                //Highlight winning cells green 
                HighlightWinningCells(winningCells);
            }
            // Col 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // ENDGAME
                mGameEnded = true;
                // Save wins
                winningCells[0, 1] = Button1;
                winningCells[1, 1] = Button4;
                winningCells[2, 1] = Button7;
                //Highlight winning cells green 
                HighlightWinningCells(winningCells);
            }
            // Col 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // ENDGAME
                mGameEnded = true;
                // Save wins
                winningCells[0, 2] = Button2;
                winningCells[1, 2] = Button5;
                winningCells[2, 2] = Button8;
                 
                //Highlight winning cells green 
                HighlightWinningCells(winningCells);
            }
            //Diagonals
            // Diag 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // ENDGAME
                mGameEnded = true;
                // Save wins
                winningCells[0, 0] = Button0;
                winningCells[1, 1] = Button4;
                winningCells[2, 2] = Button8;
                HighlightWinningCells(winningCells);
            }
            // Diag 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // ENDGAME
                mGameEnded = true;
                // Save wins
                winningCells[0, 2] = Button2;
                winningCells[1, 1] = Button4;
                winningCells[2, 0] = Button6;
                HighlightWinningCells(winningCells);
            }
            //win when all cells not free
            if ( )
            {

            }
            //no winner
            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;

                //Turn all cells red
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.LightCoral;
                });
            }
        }
    }
}
