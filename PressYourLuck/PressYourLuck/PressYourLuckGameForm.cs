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
        private DataStructureClass dataStructureClass = new DataStructureClass();
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        PictureBox[] picBox = new PictureBox[18];
        public PressYourLuckGameForm()
        {

            
            InitializeComponent();

            //PictureBox[] picBox = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, 
            //pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, 
            //pictureBox10, pictureBox11,pictureBox12,pictureBox13,pictureBox14,pictureBox15,
            //pictureBox16,pictureBox17,pictureBox18};

            //DisplayBoard(picBox);
            wplayer.URL = "Game Show Music.mp3";
            wplayer.controls.play();
           
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
            button1.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
            wplayer.controls.stop();
            Name1.Text = playerNameText1.Text.ToUpper();
            Name2.Text = playerNameText2.Text.ToUpper();
            Name3.Text = playerNameText3.Text.ToUpper();
            twoPlayer.Enabled = false;
            threePlayer.Enabled = false;
            startGame.Enabled = false;
        }

        private void twoPlayer_CheckedChanged(object sender, EventArgs e)
        {
            playerNameText3.Enabled = false;
            groupBox5.Visible = false;

        }

        private void threePlayer_CheckedChanged(object sender, EventArgs e)
        {
            playerNameText3.Enabled = true;
            groupBox5.Visible = true;
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            const string message = "Do you want to restart? ";
            const string caption = "Restart Game";
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

        private void button1_Click(object sender, EventArgs e)
        {
            wplayer.URL = "PYL Board.mp3";
            wplayer.controls.play();
            button1.Visible = false;
            button7.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            button7.Visible = false;
            button1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            wplayer.URL = "PYL Board.mp3";
            wplayer.controls.play();
            button3.Visible = false;
            button8.Visible = true;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            button8.Visible = false;
            button3.Visible = true;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            wplayer.URL = "PYL Board.mp3";
            wplayer.controls.play();
            button5.Visible = false;
            button9.Visible = true;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            wplayer.controls.stop();
            button9.Visible = false;
            button5.Visible = true;

        }



    }
}
