using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace MathGame
{
    public partial class Form1 : Form
    {
        Question[] questions;
        int totalQuestions, index, score;
        Random random = new Random();

        public Form1(Question[] questions, int totalQuestions, int index = 0)
        {
            InitializeComponent();
            this.questions = questions;
            this.totalQuestions = totalQuestions;
            this.index = index;
            this.score = 0;
        }
        public void nextQuestion()
        {
            lblQuestion.Text = $"#{this.index+1}.  "+this.questions[this.index].toString();
        }

        //private void checkAnswerEvent(object sender, EventArgs e)
        //{
        //    int btnAnswer = int.Parse(((Button)sender).Text);
        //    int btnTag = Convert.ToInt32(((Button)sender).Tag);         //for coloring;

        //    if (checkAnswer(btnAnswer))
        //    {
        //        this.score++;
        //    }

        //    this.index++;

        //    if (gameOver())
        //    {
        //        this.Close();
        //        Application.Exit();
        //    }
        //    else
        //    {
        //        setButtons();
        //        nextQuestion();
        //    }

        //}

        private async void checkAnswerEvent(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int btnAnswer = int.Parse(clickedButton.Text);

            if (checkAnswer(btnAnswer))
            {
                this.score++;
                clickedButton.BackColor = Color.Green;
            }
            else
            {
                clickedButton.BackColor = Color.Red;
            }

            // Disable all buttons to prevent multiple clicks
            SetButtonsEnabled(false);

            // Wait for 1.5 seconds
            await Task.Delay(1000);

            // Reset button colors and re-enable them
            ResetButtonColors();
            SetButtonsEnabled(true);

            this.index++;
            if (gameOver())
            {
                this.Close();
                Application.Exit();
            }
            else
            {
                setButtons();
                nextQuestion();
            }
        }

        private void ResetButtonColors()
        {
            button1.BackColor = SystemColors.Control;
            button2.BackColor = SystemColors.Control;
            button3.BackColor = SystemColors.Control;
            button4.BackColor = SystemColors.Control;
        }
        private void SetButtonsEnabled(bool enabled)
        {
            button1.Enabled = enabled;
            button2.Enabled = enabled;
            button3.Enabled = enabled;
            button4.Enabled = enabled;
        }
    private bool gameOver()
        {
            if (this.index == 10)
            {
                int grade = this.score * 10;
                MessageBox.Show($"Your score is {this.score}/{this.totalQuestions}" + Environment.NewLine +
                                $"Your Grade is {grade}!","Game Over");
                return true;
            }
            return false;
        }
        private bool checkAnswer(int btnANswer) { return btnANswer == this.questions[this.index].getCorrectAnswer(); }
        internal void setButtons()
        {
            Button[] buttons = { button1, button2, button3, button4 };
            int correctIndex = random.Next(0, 4);
            buttons[correctIndex].Text = $"{this.questions[index].getCorrectAnswer()}";
            for (int i=0; i<4; i++)
            {
                if (i == correctIndex)
                    continue;
                buttons[i].Text = $"{random.Next(1, 21)}";
            }
        }
    }
}
