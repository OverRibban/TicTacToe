using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        static List<String> gameBoard = new List<String>(); //Initialize list as 3x3 board in order to right and down.
        static int currentPlayer = 0; // Initialize variable for deciding starting player
        public Form1()
        {
            InitializeComponent();
            // Randomly select integer 1 or 2. 
            Random rand = new Random();
            currentPlayer = rand.Next(0, 2);
            ClearBoard();
        }
        private void ClearBoard()
        {
            // Clear whole list in case of restart
            gameBoard.Clear();
            // Fill all indexes with numbers from 1 to 9 in ascending order, to make every index unlike each other and match their button's tag.
            for (int i = 0; i < 10; i++)
            {
                gameBoard.Add((i.ToString()));
                // Null every form that has a tag to reset board buttons
                foreach (Control Y in this.Controls)
                {
                    Console.WriteLine(Y.Tag);
                    if (Y.Tag != null)
                    {
                        Y.BackgroundImage = null;
                    }
                }
            }
            // Change current player image to their corresponding player
            if (currentPlayer % 2 != 0)
            {
                CurrentPlayerIcon.Image = Properties.Resources.Xred;
            }
            else
            {
                CurrentPlayerIcon.Image = Properties.Resources.Oblue;
            }
        }
        private void MatchResultInput(string CurrentWinner)
        {
            // Display winner and restart game if either player clicks on "OK" in the end message.
            if (MessageBox.Show(CurrentWinner + " WON!" + "\n" + "Start Over?", "Game Over!", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ClearBoard();
            }
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            // Use button as object in further functions
            Button b = (sender as Button);
            // Get the button's tag and store it in a variable
            int buttonTag = int.Parse(b.Tag.ToString());
            DecidePlayerTurn(buttonTag, b);
        }
        private void DecidePlayerTurn(int tag, Button b)
        {
            // If button's matching index in list has not been changed
            if (gameBoard[tag] == tag.ToString()) {
                // Switch player every turn and use random int to choose starting player in 1st game
                // Change current player image to their corresponding player
                if (currentPlayer % 2 == 0)
                {
                    CheckBoard("O", tag, b);
                    CurrentPlayerIcon.Image = Properties.Resources.Xred;
                }
                else
                {
                    CheckBoard("X", tag, b);
                    CurrentPlayerIcon.Image = Properties.Resources.Oblue;
                }
            }
            else
            {
                MessageBox.Show("Taken!");
            }
        }
        private void CheckBoard(string v, int tag, Button b)
        {
            // Change button's index in board array to the current player
            gameBoard[tag] = v;
            // If player is O or X
            if (v == "O")
            {
                b.BackgroundImageLayout = ImageLayout.Stretch;
                b.BackgroundImage = Properties.Resources.Oblue;
            }
            else
            {
                b.BackgroundImageLayout = ImageLayout.Stretch;
                b.BackgroundImage = Properties.Resources.Xred;
            }
            // Check various combinations horizontally and vertically to see if a player achived 3 in a row
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[0 + (3 * i)] == gameBoard[1 + (3 * i)] && gameBoard[1 + (3 * i)] == gameBoard[2 + (3 * i)])
                {
                    MatchResultInput(gameBoard[0 + (3 * i)]);
                }
                if (gameBoard[0 + (1 * i)] == gameBoard[3 + (1 * i)] && gameBoard[3 + (1 * i)] == gameBoard[6 + (1 * i)])
                {
                    MatchResultInput(gameBoard[3 + (3 * i)]);
                }
            }
            // Check both diagonal combinations to see if a player achived 3 in a row
            if (gameBoard[0] == gameBoard[4] && gameBoard[4] == gameBoard[8])
            {
                MatchResultInput(gameBoard[0]);
            }
            if (gameBoard[2] == gameBoard[4] && gameBoard[4] == gameBoard[6])
            {
                MatchResultInput(gameBoard[2]);
            }
            // Up the variable by one to change player next turn
            currentPlayer++;
            // Check if all slots are taken
            if (
                    (gameBoard[0] == "X" || gameBoard[0] == "O")
                && (gameBoard[1] == "X" || gameBoard[1] == "O")
                && (gameBoard[2] == "X" || gameBoard[2] == "O")
                && (gameBoard[3] == "X" || gameBoard[3] == "O")
                && (gameBoard[4] == "X" || gameBoard[4] == "O")
                && (gameBoard[5] == "X" || gameBoard[5] == "O")
                && (gameBoard[6] == "X" || gameBoard[6] == "O")
                && (gameBoard[7] == "X" || gameBoard[7] == "O")
                && (gameBoard[8] == "X" || gameBoard[8] == "O")
                )
            {
                // Restarts game if either player clicks on "OK" in end message
                if (MessageBox.Show(" DRAW!" + "\n" + "Start Over?", "Game Over!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    ClearBoard();
                }
            }
        }
        // Display manual for the player
        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rules\n\n1. The board/playing area is a grid that is 3 squares wide and 3 squares tall. \n2. There are two players, 'X' or 'O', and you will need to assign yourself to one of them. \n3. The player who is first to get 3 of their own marks in a row in a diagional, horizontal or vertical order will win. \n4. If all the slots are taken on the board then the game will result in a draw. \n\nHow to win? \n\nYour goal is to get 3 marks in a row, when you first set out your first mark you will then need to look ahead and make decisions that will benefit you in the future. This would for example be that if you as a player places a mark across the board that might give the incentive of the other player to block your path. The trick is to have the player not knowing what your next motive will be after you have placed your first mark.");

        }
        // Restarts the game if the player manually clicks on the restart button
        private void button11_Click(object sender, EventArgs e)
        {
            ClearBoard();
        }
    }
}
