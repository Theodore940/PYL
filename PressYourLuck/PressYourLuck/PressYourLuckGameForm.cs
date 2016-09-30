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
        //private DataStructureClass dataStructureClass = new DataStructureClass();
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        PictureBox[] picBox = new PictureBox[18];
        Image [] images = new Image[22];
        public PressYourLuckGameForm()
        {

            
            InitializeComponent();

            wplayer.URL = "Game Show Music.mp3";
            wplayer.controls.play();
            for (int i =0; i<22 ;i++)
                images[i]=Image.FromFile(Directory.GetCurrentDirectory() + "\\Pictures\\Big Board\\" + (i+1) + ".png");
           
        }
        public void DisplayBoard(PictureBox [] picBox)
        {
            picBox = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, 
            pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, 
            pictureBox10, pictureBox11,pictureBox12,pictureBox13,pictureBox14,pictureBox15,
            pictureBox16,pictureBox17,pictureBox18};
            
            for (int i = 0; i < 18; i++)
            {
                int face = 1 + randomNumber.Next(22);
                picBox[i].Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Pictures\\Big Board\\" + face + ".png");

            }
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
            DataStructureClass dataStructureClass = new DataStructureClass(player);
            dataStructureClass.setPlayerName(1,Name1.Text);
            question.Text=dataStructureClass.getQuestion(1);
            Answer.Text = dataStructureClass.getPlayerName(1);

        }

        private void twoPlayer_CheckedChanged(object sender, EventArgs e)
        {
            playerNameText3.Enabled = false;
            groupBox5.Visible = false;
            player = 2;
        }

        private void threePlayer_CheckedChanged(object sender, EventArgs e)
        {
            playerNameText3.Enabled = true;
            groupBox5.Visible = true;
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
            wplayer.URL = "PYL Board.mp3";
            wplayer.controls.play();
            player1spin.Visible = false;
            player1stop.Visible = true;
        }

        private void player1stop_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            player1stop.Visible = false;
            player1spin.Visible = true;
            int face = 1 + randomNumber.Next(22);
            pictureBox19.Image = images[face];
        }

        private void player1pass_Click(object sender, EventArgs e)
        {

        }

        private void player2spin_Click(object sender, EventArgs e)
        {
            wplayer.URL = "PYL Board.mp3";
            wplayer.controls.play();
            player2spin.Visible = false;
            player2stop.Visible = true;
        }

        private void player2stop_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            player2stop.Visible = false;
            player2spin.Visible = true;
        }

        private void player2pass_Click(object sender, EventArgs e)
        {

        }

        private void player3spin_Click(object sender, EventArgs e)
        {
            wplayer.URL = "PYL Board.mp3";
            wplayer.controls.play();
            player3spin.Visible = false;
            player3stop.Visible = true;
        }

        private void player3stop_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            player3stop.Visible = false;
            player3spin.Visible = true;
        }

        private void player3pass_Click(object sender, EventArgs e)
        {

        }
       
    }
}
