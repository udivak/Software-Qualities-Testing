using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathGame
{
    public partial class Instructions : Form
    {
        Question[] questions;
        int totalQuestions;
        public Instructions(Question[] questions, int totalQuestions)
        {
            InitializeComponent();
            label1.Location = new Point((this.ClientSize.Width - label1.Width)/2 - 20, 30);
            textBox1.Location = new Point((this.ClientSize.Width - textBox1.Width) / 2, 100);
            textBox1.Text = "Welcome to the Math Game!" + Environment.NewLine +
                            "1. You will be presented with 10 add/sub questions." + Environment.NewLine +
                            "2. For each question, click on the correct answer from the four options." + Environment.NewLine +
                            "3. If you choose correctly, the button will turn green." + Environment.NewLine +
                            "4. If you choose incorrectly, the button will turn red." + Environment.NewLine +
                            "5. Try to answer as many questions correctly as you can!" + Environment.NewLine +
                               "Good luck and have fun!";
            button1.BackColor = Color.Aqua;     //#//
            this.questions = questions;
            this.totalQuestions = totalQuestions;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Game gameForm = new Game(questions, totalQuestions);
            gameForm.nextQuestion();
            gameForm.setButtons();
            gameForm.Show();
            this.Hide();
        }

    }
}
