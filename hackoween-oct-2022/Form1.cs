using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hackoween_oct_2022
{
    public partial class Form1 : Form
    {
        private Game game;
        public Form1()
        {
            game = new Game();
            game.UpdateDisplayEvent += GameUpdateHandler;
            game.FormClosedRequest += HandleCloseRequest;
            InitializeComponent();
            game.Init();
        }

        private void b_optionA_Click(object sender, EventArgs e)
        {
            game.ChooseA();
        }

        private void b_optionB_Click(object sender, EventArgs e)
        {
            game.ChooseB();
        }

        private void GameUpdateHandler(Object sender, GameEventArgs e)
        {
            if (File.Exists(e.Frame.ImagePath))
            {
                pb_viewPort.Image = Image.FromFile(e.Frame.ImagePath);
            }
            rtb_textArea.Text = e.Frame.Text;
            b_optionA.Text = e.Frame.ChoiceA;
            b_optionB.Text = e.Frame.ChoiceB;
            l_healthDisplay.Text = game.Health + "%";
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HandleCloseRequest(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
