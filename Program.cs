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
            string[] ops = { "+", "-" };
            List<Question> questions = new List<Question>();
            
            while (questions.Count < totalQuestions)
            {
                int num1 = random.Next(1, 11);
                int num2 = random.Next(1, 11);
                string op = ops[random.Next(ops.Length)];

                Question newQuestion = new Question(num1, num2, op);

                if (!questions.Any(q => q.IsSimilar(newQuestion)))
                {
                    questions.Add(newQuestion);
                }
            }

            Form1 gameForm = new Form1(questions.ToArray(), totalQuestions);
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
        public bool IsSimilar(Question other)
        {
            return this.op == other.op &&
                   ((this.num1 == other.num1 && this.num2 == other.num2) ||
                    (this.num1 == other.num2 && this.num2 == other.num1 && this.op == "+"));
        }
        public int getNum1() { return this.num1; }
        public int getNum2() { return this.num2; }
        public int getCorrectAnswer() { return this.correctAnswer; }
        public string toString() { return $"{this.num1}" +op+ $"{this.num2} = ?"; }
    }
}
