using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace MathGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Random random = new Random();
            int totalQuestions = 10;
            string op = "";
            Question[] questions = new Question[10];
            for (int i=0; i<totalQuestions; i++)
            {
                int num1 = random.Next(1, 11);
                int num2 = random.Next(1, 11);
                int choice = random.Next(1, 3);
                if (choice == 1)
                    op = "+";
                if (choice == 2)
                    op = "-";
                questions[i] = new Question(num1, num2, op);
            }
            Form1 gameForm =new Form1(questions, totalQuestions);
            gameForm.nextQuestion();
            gameForm.setButtons();
            Application.Run(gameForm);
        }
    }
    public class Question
    {
        private int num1, num2, correctAnswer;
        private string op;
        public Question(int num1, int num2, string op)
        {
            this.num1 = num1;
            this.num2 = num2;
            this.op = op;
            if (op == "+")
                correctAnswer = num1 + num2;
            else if (op == "-")
            {
                this.num1 = Math.Max(num1, num2);
                this.num2 = Math.Min(num1, num2);
                this.correctAnswer = this.num1 - this.num2;
            }
                
            else
                throw new ArgumentException("empty operator");
        }
        public int getNum1() { return this.num1; }
        public int getNum2() { return this.num2; }
        public int getCorrectAnswer() { return this.correctAnswer; }
        public string toString() { return $"{this.num1}" +op+ $"{this.num2} = ?"; }
    }
}
