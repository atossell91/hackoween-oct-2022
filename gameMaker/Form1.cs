using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using hackoween_oct_2022;

namespace gameMaker
{
    public partial class Form1 : Form
    {
        List<UserFrame> AllFrames;
        private int currentFrame = 0;
        enum EnumEntryMode { Frame, Action};
        EnumEntryMode EntryMode = EnumEntryMode.Frame;
        private bool flag = false; //  True if computer entry, otherwise false
        private Object selectedControl;
        public Form1()
        {
            bool temp = flag;
            flag = true;
            InitializeComponent();
            AllFrames = new List<UserFrame>();
            AllFrames.Add(new UserFrame());
            cb_type.SelectedIndex = 0;
            cb_actionName.SelectedIndex = 0;
            flag = temp;
        }

        void UpdateList(string outcome)
        {
            foreach (Object o in listBox1.Items)
            {
                TalliedName tn = (TalliedName)o;
                if (tn.Name == outcome)
                {
                    ++tn.Count;
                    return;
                }
            }
            listBox1.Items.Add(new TalliedName()
            {
                Name = outcome
            });
            listBox1.Refresh();
        }
        void UpdateMode(EnumEntryMode m)
        {
            EntryMode = m;
            if (m == EnumEntryMode.Frame)
            {
                tb_imgPath.Enabled = true;
                tb_text.Enabled = true;

                tb_odds.Enabled = false;
                tb_param.Enabled = false;
                cb_actionName.Enabled = false;
            }
            else
            {
                tb_imgPath.Enabled = false;
                tb_imgPath.Text = "";
                tb_text.Enabled = false;
                tb_text.Text = "";

                tb_odds.Enabled = true;
                tb_odds.Text = "1";
                tb_param.Enabled = true;
                tb_param.Text = "";
                cb_actionName.Enabled = true;
                cb_actionName.SelectedIndex = 0;
            }
        }
        private void b_Next_Click(object sender, EventArgs e)
        {
            UpdateList(AllFrames[currentFrame].OutcomeA);
            UpdateList(AllFrames[currentFrame].OutcomeB);
            int tempIndex = currentFrame + 1;
            if (tempIndex >= AllFrames.Count)
            {
                AllFrames.Add(new UserFrame());
            }
            DisplayFields(tempIndex);
            currentFrame = tempIndex;
        }
        void DisplayFields(int index)
        {
            bool temp = flag;
            flag = true;
            UserFrame f = AllFrames[index];
            cb_type.Text = f.Type;
            tb_name.Text = f.Name;
            tb_choiceA.Text = f.ChoiceA;
            tb_outcomeA.Text = f.OutcomeA;
            tb_choiceB.Text = f.ChoiceB;
            tb_outcomeB.Text = f.OutcomeB;
            tb_text.Text = f.Text;
            tb_imgPath.Text = f.ImagePath;
            cb_actionName.Text = f.ActionName;
            if (f.Parameter == null || String.IsNullOrEmpty(f.Parameter.ToString()))
            {
                tb_param.Text = "";
            }
            else
            {
                tb_param.Text = f.Parameter.ToString();
            }
            tb_odds.Text = f.Odds.ToString();
            flag = temp;
            this.Refresh();
        }

        private void b_prev_Click(object sender, EventArgs e)
        {
            int tempIndex = currentFrame - 1;
            if (tempIndex < 0)
            {
                return;
            }
            DisplayFields(tempIndex);
            currentFrame = tempIndex;
        }

        private void cb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int i = cb.SelectedIndex;
            UpdateMode((EnumEntryMode)i);
            if (flag) return;
            UserFrame f = AllFrames[currentFrame];
            f.Type = cb.Text;
        }

        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            if (flag) return;
            TextBox tb = (TextBox)sender;
            UserFrame f = AllFrames[currentFrame];
            f.Name = tb.Text;
        }

        private void tb_choiceA_TextChanged(object sender, EventArgs e)
        {
            if (flag) return;
            TextBox tb = (TextBox)sender;
            UserFrame f = AllFrames[currentFrame];
            f.ChoiceA = tb.Text;
        }

        private void tb_outcomeA_TextChanged(object sender, EventArgs e)
        {
            if (flag) return;
            TextBox tb = (TextBox)sender;
            UserFrame f = AllFrames[currentFrame];
            f.OutcomeA = tb.Text;
        }

        private void tb_choiceB_TextChanged(object sender, EventArgs e)
        {
            if (flag) return;
            TextBox tb = (TextBox)sender;
            UserFrame f = AllFrames[currentFrame];
            f.ChoiceB = tb.Text;
        }

        private void tb_outcomeB_TextChanged(object sender, EventArgs e)
        {
            if (flag) return;
            TextBox tb = (TextBox)sender;
            UserFrame f = AllFrames[currentFrame];
            f.OutcomeB = tb.Text;
        }

        private void tb_text_TextChanged(object sender, EventArgs e)
        {
            if (flag) return;
            TextBox tb = (TextBox)sender;
            UserFrame f = AllFrames[currentFrame];
            f.Text = tb.Text;
        }

        private void tb_imgPath_TextChanged(object sender, EventArgs e)
        {
            if (flag) return;
            TextBox tb = (TextBox)sender;
            UserFrame f = AllFrames[currentFrame];
            f.ImagePath = tb.Text;
        }

        private void cb_actionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag) return;
            UserFrame f = AllFrames[currentFrame];
            ComboBox cb = (ComboBox)sender;
            f.ActionName = cb.Text;
        }

        private void tb_param_TextChanged(object sender, EventArgs e)
        {
            if (flag) return;
            TextBox tb = (TextBox)sender;
            UserFrame f = AllFrames[currentFrame];
            f.Parameter = tb.Text;
        }

        private void tb_odds_TextChanged(object sender, EventArgs e)
        {
            if (flag) return;
            TextBox tb = (TextBox)sender;
            UserFrame f = AllFrames[currentFrame];
            f.Odds = Double.Parse(tb.Text);
        }
        private void set_selected(object sender, EventArgs e)
        {
            selectedControl = sender;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int sel = lb.SelectedIndex;
            if (sel < 0 || sel >= lb.Items.Count) return;
            if (selectedControl == tb_name)
            {
                tb_name.Text = lb.Items[sel].ToString();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string json = JsonSerializer.Serialize(AllFrames, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, json);
            }
        }
    }
}
