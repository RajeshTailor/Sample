using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineExamination
{
    public partial class ExamQuestion : Form
    {
        public int CurrentQuestionNo = 0;
        public int TotalQuestion = 10;
        public int CurrentQuestionId = 0;
        public int startIndex = 0;
        public int endIndex = 0;
        public int currentIndex = 0;
        private int secondsToWait = 150;
        private DateTime startTime;
        DataTable dtQuestions = new DataTable();
        public ExamQuestion()
        {
            InitializeComponent();
            timer.Start(); 
            startTime = DateTime.Now; 
        }

        public void FillQuestions()
        {
            dtQuestions.Clear();
            dtQuestions.Columns.Add("QuestionId");
            dtQuestions.Columns.Add("Title");
            for (int i = 0; i < 4; i++)
            {
                DataRow ques = dtQuestions.NewRow();
                ques["QuestionId"] = 1;
                ques["Title"] = "This is first Question";
                dtQuestions.Rows.Add(ques);
            }
        }

        public void SetCurrentQuestion()
        {
            //var currentQuestion=dtQuestions.[currentIndex];

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int elapsedSeconds = (int)(DateTime.Now - startTime).TotalSeconds;
            int remainingSeconds = secondsToWait - elapsedSeconds;

            if (remainingSeconds <= 0)
            {
                // run your function
                timer.Stop();
            }
            if (remainingSeconds > 0)
            {
                int seconds = remainingSeconds % 60;
                int minutes = remainingSeconds / 60;
                string time = minutes + ":" + seconds;
                lblTime.Text = String.Format("Time: " + minutes + ":" + seconds);
            }
            else
                MessageBox.Show("Thanks for giving Exam");
        }

       
    }
}
