//****************************************************************
//                   Project #1 Press Your Luck
//                   Name: Nathan Ho & Ryan Luig
//                   CPL Date: 10/06/2016
//*****************************************************************
//      This is the Press Your Luck partial class game form that 
//      controls the GUI and the logic for the whole game.
//      It allows user to play two rounds of trivia & big board 
//      and displays the winner of the game with the highest score
//      at the end of the game.
//
//*****************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WMPLib;

namespace PressYourLuck
{
    public partial class PressYourLuckGameForm : Form
    {

        private Random randomNumber = new Random();
        private int player;
        private DataStructureClass dataStructureClass;
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        PictureBox[] picBox = new PictureBox[18];
        private int numOfRounds = 1;
        private int numOfQuestions = 1;
        private int playerCount;
        private int playerIDnum = 0;
        private bool wasClicked = false;
        private int randomNum;
        public PressYourLuckGameForm()
        {
            InitializeComponent();
            wplayer.URL = "Game Show Music.mp3";
            wplayer.controls.play();
        }
        //Randomizes the bigboard whened called
        public void DisplayBoard(PictureBox[] picBox)
        {
            picBox = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, 
            pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, 
            pictureBox10, pictureBox11,pictureBox12,pictureBox13,pictureBox14,pictureBox15,
            pictureBox16,pictureBox17,pictureBox18};

            for (int i = 0; i < 18; i++)
            {
                int face = 1 + randomNumber.Next(17);
                picBox[i].Image = dataStructureClass.images[face];
            }
        }
        //Display the next question for trivia round
        private void getQuestionFromStruct()
        {
            randomNum = randomNumber.Next(69);
            question.Text = dataStructureClass.getQuestion(randomNum);
            MessageBox.Show(dataStructureClass.getPlayerName(1) + "'s turn to answer the question."
                , "Round " + numOfRounds + " Question " + numOfQuestions, MessageBoxButtons.OK, MessageBoxIcon.Information);
            playerSubmit.Enabled = true;
        }
        // this starts the game and sets all the name parameters 
        // and ask if users would like to understand the rules before
        // playing.
        private void startGame_Click(object sender, EventArgs e)
        {
            if (wasClicked == true)
            {
                wasClicked = false;
                dataStructureClass = new DataStructureClass(player);
                DisplayBoard(picBox);
                wplayer.controls.stop();
                Name1.Text = playerNameText1.Text.ToUpper();
                Name2.Text = playerNameText2.Text.ToUpper();
                dataStructureClass.setPlayerName(1, Name1.Text);
                dataStructureClass.setPlayerName(2, Name2.Text);
                if (player == 3)
                {
                    Name3.Text = playerNameText3.Text.ToUpper();
                    dataStructureClass.setPlayerName(3, Name3.Text);
                }
                gameFAQ();
                twoPlayer.Enabled = false;
                threePlayer.Enabled = false;
                startGame.Enabled = false;
                newGame.Enabled = true;
                playerNameText1.Enabled = false;
                playerNameText2.Enabled = false;
                playerNameText3.Enabled = false;
                getQuestionFromStruct();
            }

        }

        // radio button if the users would like to play 2 people
        // and setting players to 2.
        private void twoPlayer_CheckedChanged(object sender, EventArgs e)
        {
            wasClicked = true;
            playerNameText1.Enabled = true;
            playerNameText2.Enabled = true;
            playerNameText3.Enabled = false;
            groupBox5.Visible = false;
            playerCount = player = 2;
        }
        // radio button if the users would like to play 3 people
        // and setting players to 3.
        private void threePlayer_CheckedChanged(object sender, EventArgs e)
        {
            wasClicked = true;
            playerNameText1.Enabled = true;
            playerNameText2.Enabled = true;
            playerNameText3.Enabled = true;
            groupBox5.Visible = true;
            playerCount = player = 3;
        }
        // Asks the user if they would like to restart the game to play a new one.
        private void newGame_Click(object sender, EventArgs e)
        {
            const string message = "Play Again? ";
            const string caption = "New Game";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
            else if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
        // These next few button event help the players 
        // control their spins/stop of the bigboard or 
        // if they want to pass their spins off to the 
        // next person
        private void player1spin_Click(object sender, EventArgs e)
        {
            handleSpin(1);

        }
        private void player1stop_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            player1stop.Visible = false;
            player1spin.Visible = true;
            handleScore(1);
            Score1.Text = "$" + dataStructureClass.getPlayerScore(1).ToString();
            roundTwo();
        }
        private void player1pass_Click(object sender, EventArgs e)
        {
            if (dataStructureClass.getPlayerPassedSpins(1) > 0)
                MessageBox.Show("You can't pass. You must use all passed spin!");
            else
            {
                if (dataStructureClass.getPlayerScore(2) >= dataStructureClass.getPlayerScore(3)
                    && dataStructureClass.getPlayerSpins(1) != 0) //player 2 has highest score
                {
                    dataStructureClass.addPlayerPassedSpins(2, dataStructureClass.getPlayerSpins(1));
                    Passed2.Text = dataStructureClass.getPlayerPassedSpins(2).ToString();
                    player2spin.Enabled = true;
                }
                else //player 3 has highest score
                {
                    dataStructureClass.addPlayerPassedSpins(3, dataStructureClass.getPlayerSpins(1));
                    Passed2.Text = dataStructureClass.getPlayerPassedSpins(3).ToString();
                    player3spin.Enabled = true;

                }
                dataStructureClass.addPlayerSpins(1, -(dataStructureClass.getPlayerSpins(1)));
                Earned1.Text = dataStructureClass.getPlayerSpins(1).ToString();
                player1spin.Enabled = false;
                player1pass.Enabled = false;
            }
        }
        private void player2spin_Click(object sender, EventArgs e)
        {
            handleSpin(2);
        }
        private void player2stop_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            player2stop.Visible = false;
            player2spin.Visible = true;
            handleScore(2);
            Score2.Text = "$" + dataStructureClass.getPlayerScore(2).ToString();
            roundTwo();
        }
        private void player2pass_Click(object sender, EventArgs e)
        {
            if (dataStructureClass.getPlayerPassedSpins(2) > 0)
                MessageBox.Show("You can't pass. You must use all passed spin!");
            else
            {
                if (dataStructureClass.getPlayerScore(1) > dataStructureClass.getPlayerScore(3)
                    && dataStructureClass.getPlayerSpins(2)!=0) //player 1 has highest score
                {
                    dataStructureClass.addPlayerPassedSpins(1, dataStructureClass.getPlayerSpins(2));
                    Passed1.Text = dataStructureClass.getPlayerPassedSpins(1).ToString();
                    player1spin.Enabled = true;
                }
                else //Player 3 has highest score
                {
                    dataStructureClass.addPlayerPassedSpins(3, dataStructureClass.getPlayerSpins(2));
                    Passed3.Text = dataStructureClass.getPlayerPassedSpins(3).ToString();
                    player3spin.Enabled = true;
                }
                dataStructureClass.addPlayerSpins(2, -(dataStructureClass.getPlayerSpins(2)));
                Earned2.Text = dataStructureClass.getPlayerSpins(2).ToString();
                player2spin.Enabled = false;
                player2pass.Enabled = false;
            }
        }
        private void player3spin_Click(object sender, EventArgs e)
        {
            handleSpin(3);

        }
        private void player3stop_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            player3stop.Visible = false;
            player3spin.Visible = true;
            handleScore(3);
            Score3.Text = "$"+dataStructureClass.getPlayerScore(3).ToString();
            roundTwo();
        }
        private void player3pass_Click(object sender, EventArgs e)
        {
            if (dataStructureClass.getPlayerPassedSpins(1) > 0)
                MessageBox.Show("You can't pass. You must use all passed spin!");
            else
            {
                if (dataStructureClass.getPlayerScore(2) > dataStructureClass.getPlayerScore(1)
                    && dataStructureClass.getPlayerSpins(3) != 0) //player 2 has highest score
                {
                    dataStructureClass.addPlayerPassedSpins(2, dataStructureClass.getPlayerSpins(3));
                    Passed2.Text = dataStructureClass.getPlayerPassedSpins(2).ToString();
                    player2spin.Enabled = true;
                }
                else //player 1 has highest score
                {
                    dataStructureClass.addPlayerPassedSpins(1, dataStructureClass.getPlayerSpins(3));
                    Passed1.Text = dataStructureClass.getPlayerPassedSpins(1).ToString();
                    player1spin.Enabled = true;
                }
                dataStructureClass.addPlayerSpins(3, -(dataStructureClass.getPlayerSpins(3)));
                Earned3.Text = dataStructureClass.getPlayerSpins(3).ToString();
                player3spin.Enabled = false;
                player3pass.Enabled = false;
            }
        }
        //button that quits the game
        private void quitGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //This button let players submit their answers to check if they got it correct or
        //incorrect to earn spins. Also controls the 4 question rounds.
        private void submitAnswer2_Click(object sender, EventArgs e)
        {
            if (numOfQuestions <= 4)
            {
                Answer.Text = "Correct Answer: " + dataStructureClass.getAnswer(randomNum);
                if (player == 2)
                {
                    showPlayerAnswers.Text = String.Format(" Player 1: {0}\n Player 2: {1}"
                    , dataStructureClass.getPlayerAns(1), dataStructureClass.getPlayerAns(2));
                    for (int i = 1; i < 3; i++)
                        dataStructureClass.checkAnswer(i, dataStructureClass.getAnswer(randomNum));
                    Earned1.Text = dataStructureClass.getPlayerSpins(1).ToString();
                    Earned2.Text = dataStructureClass.getPlayerSpins(2).ToString();
                }
                else
                {
                    showPlayerAnswers.Text = String.Format(" Player 1: {0}\n Player 2: {1}\n Player 3: {2} "
                    , dataStructureClass.getPlayerAns(1), dataStructureClass.getPlayerAns(2)
                    , dataStructureClass.getPlayerAns(3));
                    for (int i = 1; i < 4; i++)
                        dataStructureClass.checkAnswer(i, dataStructureClass.getAnswer(randomNum));
                    Earned1.Text = dataStructureClass.getPlayerSpins(1).ToString();
                    Earned2.Text = dataStructureClass.getPlayerSpins(2).ToString();
                    Earned3.Text = dataStructureClass.getPlayerSpins(3).ToString();
                }
                numOfQuestions++;
                if (numOfQuestions <= 4)
                {
                    getQuestionFromStruct();
                    playerSubmit.Enabled = true;
                    playerIDnum = 0;
                    if (player == 2)
                        playerCount = 2;
                    else
                        playerCount = 3;
                }
                else
                {
                    MessageBox.Show("Moving on to Big Board Round!","Trivia Round Over");
                    if (player == 2)
                    {
                        if (dataStructureClass.getLowestSpinsPlayer(2) == 1)
                            player1spin.Enabled = true;
                        if (dataStructureClass.getLowestSpinsPlayer(2) == 2)
                            player2spin.Enabled = true;
                    }
                    else
                    {
                        if (dataStructureClass.getLowestSpinsPlayer(3) == 1)
                            player1spin.Enabled = true;
                        if (dataStructureClass.getLowestSpinsPlayer(3) == 2)
                            player2spin.Enabled = true;
                        if (dataStructureClass.getLowestSpinsPlayer(3) == 3)
                            player3spin.Enabled = true;
                    }
                }
            }
            submitAnswer2.Enabled = false;
        }
        //lets each player submit and store their answers every round.
        private void playerSubmit_Click(object sender, EventArgs e)
        {
            playerIDnum++;
            playerCount--;
            dataStructureClass.setPlayerAnswer(playerIDnum, playerAnswers.Text.ToUpper());
            playerAnswers.Text = string.Empty;
            if (playerIDnum == 1)
                MessageBox.Show(dataStructureClass.getPlayerName(2) + "'s turn to answer the question.", "Round " + numOfRounds + " Question " + numOfQuestions, MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (playerCount == 1 && playerIDnum == 2)
                MessageBox.Show(dataStructureClass.getPlayerName(3) + "'s turn to answer the question.", "Round " + numOfRounds + " Question " + numOfQuestions, MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (playerCount == 0)
            {
                playerSubmit.Enabled = false;
                submitAnswer2.Enabled = true;
            }
        }
        // This method shows the player the game rules before playing if player
        // clicks on yes otherwise continue with the game.
        private void gameFAQ()
        {
            var results = MessageBox.Show("Would you like to learn how to play before you begin?",
                "Welcome to Press Your Luck",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (results == DialogResult.Yes)
            {
                string text = System.IO.File.ReadAllText("Game FAQ.txt");
                MessageBox.Show(text, "Game FAQ");
            }
            else if (results == DialogResult.Yes)
            {
                this.Close();
            }
        }
        // This method starts roundTwo when first big board is over
        private void roundTwo()
        {
            if (dataStructureClass.getSpins() == 0)
            {
                
                player1spin.Enabled = false;
                player2spin.Enabled = false;
                player3spin.Enabled = false;
                player1pass.Enabled = false;
                player2pass.Enabled = false;
                player3pass.Enabled = false;
                numOfRounds++;
                numOfQuestions = 1;
                if (numOfRounds < 3)
                {
                    MessageBox.Show("Trivia Round " + numOfRounds, "Round " + numOfRounds);
                    getQuestionFromStruct();
                    playerSubmit.Enabled = true;
                    playerIDnum = 0;
                    playerCount = player;
                }
                else
                {
                    MessageBox.Show(dataStructureClass.ToString(), "Game Over!");//return winner here
                    newGame.PerformClick();
                }
            }
        }
        // This method take in a playerID and handles the score
        // after the player has stop their spin adding in money
        // or subtracting all money due to whammies.
        private void handleScore(int player)
        {
            int face = randomNumber.Next(18);
            pictureBox19.Image = dataStructureClass.images[face];
            if (dataStructureClass.score[face] == -1)
            {
                wplayer.URL = "Whammy Sound.mp3";
                wplayer.controls.play();
                dataStructureClass.ResetScore(player);
            }
            else
            {
                if (numOfRounds == 2)
                    dataStructureClass.addPlayerScore(player, dataStructureClass.score[face] * 2);
                else
                    dataStructureClass.addPlayerScore(player, dataStructureClass.score[face]);
            }
        }
        // This method take in a playerID and handles each player 
        // spin button has been hit. It controls the bigboard randomize 
        // and display a value the player lands on.
        private void handleSpin(int player)
        {

            if (dataStructureClass.getPlayerSpins(player) > 0 ||
                dataStructureClass.getPlayerPassedSpins(player) > 0)
            {
                DisplayBoard(picBox);
                pictureBox19.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Pictures\\Press_Your_Luck.png");
                wplayer.URL = "PYL Board.mp3";
                wplayer.controls.play();
                if (player == 1)
                {
                    player1spin.Visible = false;
                    player1stop.Visible = true;
                    player1pass.Enabled = true;
                }
                else if (player == 2)
                {
                    player2spin.Visible = false;
                    player2stop.Visible = true;
                    player2pass.Enabled = true;
                }
                else
                {
                    player3spin.Visible = false;
                    player3stop.Visible = true;
                    player3pass.Enabled = true;
                }
                if (dataStructureClass.getPlayerPassedSpins(player) > 0)
                {
                    dataStructureClass.addPlayerPassedSpins(player, -1);
                }
                else
                {
                    dataStructureClass.addPlayerSpins(player, -1);
                }
                Earned1.Text = dataStructureClass.getPlayerSpins(1).ToString();
                Passed1.Text = dataStructureClass.getPlayerPassedSpins(1).ToString();
                Earned2.Text = dataStructureClass.getPlayerSpins(2).ToString();
                Passed2.Text = dataStructureClass.getPlayerPassedSpins(2).ToString();
                Earned3.Text = dataStructureClass.getPlayerSpins(3).ToString();
                Passed3.Text = dataStructureClass.getPlayerPassedSpins(3).ToString();
            }
            if (dataStructureClass.getPlayerSpins(player) == 0 &&
                dataStructureClass.getPlayerPassedSpins(player) == 0)
            {
                if (player == 1)
                {
                    player1spin.Enabled = false; 
                    player1pass.Enabled = false;
                    if(dataStructureClass.getPlayerSpins(2) == 0 && dataStructureClass.getPlayerPassedSpins(2) == 0)
                    {
                        player3pass.Enabled = true;
                        player3spin.Enabled = true;
                    }
                    else
                    {
                        player2spin.Enabled = true;
                        player2pass.Enabled = true;        
                    }
                    

                }
                else if (player == 2)
                {
                    player2spin.Enabled = false;
                    player2pass.Enabled = false;
                    if (dataStructureClass.getPlayerSpins(3) == 0 && dataStructureClass.getPlayerPassedSpins(3) == 0)
                    {
                        player1pass.Enabled = true;
                        player1spin.Enabled = true;
                    }
                    else
                    {
                        player3spin.Enabled = true;
                        player3pass.Enabled = true;
                    }

                }
                else
                {
                    player3spin.Enabled = false;
                    player3pass.Enabled = false;
                    if (dataStructureClass.getPlayerSpins(1) == 0 && dataStructureClass.getPlayerPassedSpins(1) == 0)
                    {
                        player2pass.Enabled = true;
                        player2spin.Enabled = true;
                    }
                    else
                    {
                        player1spin.Enabled = true;
                        player1pass.Enabled = true;
                    }
    
                }



            }
        }
    }
}
