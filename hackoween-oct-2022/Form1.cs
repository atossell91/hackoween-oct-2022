using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            InitializeComponent();
        }

        private void b_optionA_Click(object sender, EventArgs e)
        {
            game.ChooseA();
        }

        private void b_optionB_Click(object sender, EventArgs e)
        {
            game.ChooseB();
        }
    }
}
