using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
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
        Image [] images = new Image[18];
        int[] score = new int[18];
        int numOfRounds = 3;
        int playerCount = 3;
        int playerIDnum = 0;
        private int randomNum;
        public PressYourLuckGameForm()
        {

            
            InitializeComponent();

            wplayer.URL = "Game Show Music.mp3";
            wplayer.controls.play();
            for (int i =0; i<18 ;i++)
                images[i]=Image.FromFile(Directory.GetCurrentDirectory() + "\\Pictures\\Big Board\\" + (i+1) + ".png");
            score[6] = 500;
            score[7] = 1250;
            score[8] = 1750;
            score[9] = 2250;
        }
        public void DisplayBoard(PictureBox [] picBox)
        {
            picBox = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, 
            pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, 
            pictureBox10, pictureBox11,pictureBox12,pictureBox13,pictureBox14,pictureBox15,
            pictureBox16,pictureBox17,pictureBox18};
            
            for (int i = 0; i < 18; i++)
            {
                int face = 1 + randomNumber.Next(17);
                picBox[i].Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Pictures\\Big Board\\" + face + ".png");

            }
        }
        private void gameLogic()
        {
            randomNum = randomNumber.Next(22);
            question.Text = dataStructureClass.getQuestion(randomNum);
            MessageBox.Show("Player 1 turn to answer the question.","Round 1",MessageBoxButtons.OK ,MessageBoxIcon.Information);
        }
        private void startGame_Click(object sender, EventArgs e)
        {
            DisplayBoard(picBox);
            player1spin.Enabled = true;
            player2spin.Enabled = true;
            player3spin.Enabled = true;
            wplayer.controls.stop();
            Name1.Text = playerNameText1.Text.ToUpper();
            Name2.Text = playerNameText2.Text.ToUpper();
            Name3.Text = playerNameText3.Text.ToUpper();

            twoPlayer.Enabled = false;
            threePlayer.Enabled = false;
            startGame.Enabled = false;
            var results = MessageBox.Show("Would you like to learn how to play before you begin?", 
                "Welcome to Press Your Luck", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (results == DialogResult.Yes)
            {
                string text = System.IO.File.ReadAllText("Game FAQ.txt");
                MessageBox.Show(text,"Game FAQ");
            }
            else if (results == DialogResult.Yes)
            {
                this.Close();
            }
            dataStructureClass = new DataStructureClass(player);
            dataStructureClass.setPlayerName(1,Name1.Text);
            dataStructureClass.setPlayerName(2, Name2.Text);

            if(player==3)
                dataStructureClass.setPlayerName(3, Name3.Text);

            dataStructureClass.addPlayerSpins(1, 3);
            dataStructureClass.addPlayerSpins(2, 1);
            gameLogic();
           
        }

        private void twoPlayer_CheckedChanged(object sender, EventArgs e)
        {
            playerNameText1.Enabled = true;
            playerNameText2.Enabled = true;
            playerNameText3.Enabled = false;
            groupBox5.Visible = false;
            submitAnswer3.Visible = false;
            submitAnswer2.Visible = true;
            playerCount = 2;
            player = 2;
        }

        private void threePlayer_CheckedChanged(object sender, EventArgs e)
        {
            playerNameText1.Enabled = true;
            playerNameText2.Enabled = true;
            playerNameText3.Enabled = true;
            groupBox5.Visible = true;
            submitAnswer2.Visible = false;
            submitAnswer3.Visible = true;
            playerCount = 3;
            player = 3;
        }

        private void Restart_Click(object sender, EventArgs e)
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

        private void player1spin_Click(object sender, EventArgs e)
        {
            if (dataStructureClass.getPlayerSpins(1) > 0)
            {
                wplayer.URL = "PYL Board.mp3";
                wplayer.controls.play();
                player1spin.Visible = false;
                player1stop.Visible = true;
                dataStructureClass.addPlayerSpins(1, -1);
                Earned1.Text = dataStructureClass.getPlayerSpins(1).ToString();

            }
        }

        private void player1stop_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            player1stop.Visible = false;
            player1spin.Visible = true;
            int face = 7 + randomNumber.Next(3);
            pictureBox19.Image = images[face];
            dataStructureClass.addPlayerScore(1, score[face]);
            Score1.Text = dataStructureClass.getPlayerScore(1).ToString();
        }

        private void player1pass_Click(object sender, EventArgs e)
        {

        }

        private void player2spin_Click(object sender, EventArgs e)
        {
            if (dataStructureClass.getPlayerSpins(2) > 0)
            {
                wplayer.URL = "PYL Board.mp3";
                wplayer.controls.play();
                player2spin.Visible = false;
                player2stop.Visible = true;
                dataStructureClass.addPlayerSpins(2, -1);
                Earned2.Text = dataStructureClass.getPlayerSpins(2).ToString();
            }
        }

        private void player2stop_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            player2stop.Visible = false;
            player2spin.Visible = true;
            int face = 7 + randomNumber.Next(3);
            pictureBox19.Image = images[face];
            dataStructureClass.addPlayerScore(2, score[face]);
            Score1.Text = dataStructureClass.getPlayerScore(2).ToString();
        }

        private void player2pass_Click(object sender, EventArgs e)
        {

        }

        private void player3spin_Click(object sender, EventArgs e)
        {
            if (dataStructureClass.getPlayerSpins(3) > 0)
            {
                wplayer.URL = "PYL Board.mp3";
                wplayer.controls.play();
                player3spin.Visible = false;
                player3stop.Visible = true;
                dataStructureClass.addPlayerSpins(3, -1);
                Earned3.Text = dataStructureClass.getPlayerSpins(3).ToString();
            }
        }

        private void player3stop_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            player3stop.Visible = false;
            player3spin.Visible = true;
            int face = 7 + randomNumber.Next(3);
            pictureBox19.Image = images[face];
            dataStructureClass.addPlayerScore(3, score[face]);
            Score1.Text = dataStructureClass.getPlayerScore(3).ToString();
        }

        private void player3pass_Click(object sender, EventArgs e)
        {

        }

        private void quitGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void submitAnswer3_Click(object sender, EventArgs e)
        {
            Answer.Text = "Correct Answer: " + dataStructureClass.getAnswer(randomNum);
            showPlayerAnswers.Text = String.Format(" Player 1: {0}\n Player 2: {1}\n Player 3: {2} "
                , dataStructureClass.getPlayerAns(1), dataStructureClass.getPlayerAns(2), dataStructureClass.getPlayerAns(3));
            numOfRounds--;
            Earned1.Text = dataStructureClass.getPlayerSpins(1).ToString();
            Earned2.Text = dataStructureClass.getPlayerSpins(2).ToString();
            Earned3.Text = dataStructureClass.getPlayerSpins(3).ToString();
        }
        private void submitAnswer2_Click(object sender, EventArgs e)
        {
            Answer.Text = "Correct Answer: " + dataStructureClass.getAnswer(randomNum);
            showPlayerAnswers.Text = String.Format(" Player 1: {0}\n Player 2: {1}"
                , dataStructureClass.getPlayerAns(1), dataStructureClass.getPlayerAns(2));
            numOfRounds--;
            Earned1.Text = dataStructureClass.getPlayerSpins(1).ToString();
            Earned2.Text = dataStructureClass.getPlayerSpins(2).ToString();
        }
        private void playerSubmit_Click(object sender, EventArgs e)
        {
            playerIDnum++;
            dataStructureClass.setPlayerAnswer(playerIDnum, playerAnswers.Text.ToUpper());
            playerAnswers.Text = string.Empty;
            if (playerIDnum == 1)
                MessageBox.Show("Player 2 turn to answer the question.", "Round 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (playerIDnum == 2)
                MessageBox.Show("Player 3 turn to answer the question.", "Round 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            playerCount--;
            if (playerCount == 0)
                playerSubmit.Enabled = false;
        }






    }
}
