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
        public PressYourLuckGameForm()
        {

            
            InitializeComponent();
            PictureBox[] picBox = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10 };
            for (int i = 0; i < 10; i++)
            {
                int face = 1 + randomNumber.Next(9);
                picBox[i].Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Pictures\\Big Board\\Whammy"+face+".png");

            }
            wplayer.URL = "Game Show Music.mp3";
            wplayer.controls.play();
            
        }
        public void DisplayBoard(PictureBox pictureBox)
        {
            
            //int face = 1 + randomNumber.Next(18);
            pictureBox.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Pictures\\Whammy.gif");
        }
        private void startGame_Click(object sender, EventArgs e)
        {
            //DisplayBoard(pictureBox1);
            wplayer.controls.stop();
            Name1.Text = playerNameText1.Text.ToUpper();
            Name2.Text = playerNameText2.Text.ToUpper();
            Name3.Text = playerNameText3.Text.ToUpper();
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



    }
}
