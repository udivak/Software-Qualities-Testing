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
    public partial class Game : Form
    {
        Question[] questions;
        List<Question> wrong_answers = new List<Question>();
        int totalQuestions, index, score;
        Random random = new Random();

        public Game(Question[] questions, int totalQuestions, int index = 0)
        {
            InitializeComponent();
            this.questions = questions;
            this.totalQuestions = totalQuestions;
            this.index = index;
            this.score = 0;

            progressBar1.Maximum = totalQuestions;
        }
        public void nextQuestion()
        {
            lblQuestion.Text = $"#{this.index+1}.  "+this.questions[this.index].toString();
        }
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
                this.wrong_answers.Add(this.questions[index]);
                clickedButton.BackColor = Color.Red;
            }
            // Disable all buttons to prevent multiple clicks
            SetButtonsEnabled(false);
            // Wait for 1 second
            await Task.Delay(1000);
            // Reset button colors and re-enable them
            ResetButtonColors();
            SetButtonsEnabled(true);
            progressBar1.Value = this.index + 1;
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
        //private bool gameOver()
        //{
        //    if (this.index == 10)
        //    {
        //        int grade = this.score * 10;
        //        MessageBox.Show($"Your score is {this.score}/{this.totalQuestions}" + Environment.NewLine +
        //                        $"Your Grade is {grade}!!!","Game Over");
        //        return true;
        //    }
        //    return false;
        //}
        private bool gameOver()
        {
            if (this.index == 10)
            {
                int grade = this.score * 10;
                string message = $"Your score is {this.score}/{this.totalQuestions}\nYour Grade is {grade}!!!\n\n";

                if (wrong_answers.Count > 0)
                {
                    message += "Questions you got wrong:\n\n";
                    for (int i = 0; i < wrong_answers.Count; i++)
                    {
                        Question q = wrong_answers[i];
                        message += $"{i + 1}. {q.toString()}\n   Correct answer: {q.getCorrectAnswer()}\n\n";
                    }
                }
                else
                {
                    message += "Congratulations! You got all questions right!";
                }

                MessageBox.Show(message, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        private bool checkAnswer(int btnANswer) { return btnANswer == this.questions[this.index].getCorrectAnswer(); }
        internal void setButtons()
        {
            Button[] buttons = { button1, button2, button3, button4 };
            int correctIndex = random.Next(0, 4);
            int correctAnswer = this.questions[index].getCorrectAnswer();

            HashSet<int> usedNumbers = new HashSet<int> { correctAnswer };

            for (int i = 0; i < 4; i++)
            {
                if (i == correctIndex)
                {
                    buttons[i].Text = $"{correctAnswer}";
                }
                else
                {
                    int wrongAnswer;
                    do
                    {
                        wrongAnswer = random.Next(1, 21);
                    } while (usedNumbers.Contains(wrongAnswer));

                    usedNumbers.Add(wrongAnswer);
                    buttons[i].Text = $"{wrongAnswer}";
                }
            }
        }
    }
}
