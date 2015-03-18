using OnlineExamination.BL;
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
        public int currentIndex = -1;
        public int preIndex = 0;
        public int nextIndex = 0;
        private int secondsToWait = 20;
        private int totalQuestion = 0;
        private DateTime startTime;
        DataTable dtQuestions = new DataTable();
        AnswerBL _ansBL = new AnswerBL();
        public ExamQuestion()
        {
            InitializeComponent();
            ApplicationLookAndFeel.UseTheme(this);
            timer.Start(); 
            startTime = DateTime.Now;
            FillQuestions();
            GetNextQuestion();
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
                ques["Title"] = "This is Question NO:" +(i+1);
                dtQuestions.Rows.Add(ques);
            }
            totalQuestion = dtQuestions.Rows.Count;
        }

        //public void SetCurrentQuestion()
        //{
        //    if (totalQuestion > currentIndex)
        //    {
        //        var currentQuestion = dtQuestions.Rows[currentIndex-1];
        //        lblQuestion.Text = currentQuestion["Title"].ToString();
        //        preIndex = currentIndex;
        //        currentIndex++;
        //    }
        //    if (preIndex > 1)
        //    {
        //        btnPrevious.Visible = true;
        //    }
        //    else
        //        btnPrevious.Visible = false;
        //    if (currentIndex == totalQuestion)
        //        btnNext.Visible = false;
        //}

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
                label1.Text = String.Format("Time: " + minutes + ":" + seconds);
            }
            else
            MessageBox.Show("Thanks for giving Exam");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetNextQuestion();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetPreviousQuestion();
        }

        private void GetNextQuestion()
        {
            preIndex = currentIndex;
            currentIndex++;
            BindQuestion();
            MangaeNavigation();
            
        }
        private void GetPreviousQuestion()
        {
            currentIndex = preIndex;
            preIndex--;
            BindQuestion();
            MangaeNavigation();
        }

        private void MangaeNavigation()
        {
            if (preIndex > -1)  btnPrevious.Visible = true; else btnPrevious.Visible = false;
            if (currentIndex == totalQuestion-1) btnNext.Visible = false; else  btnNext.Visible = true;
        }

        private void BindQuestion()
        {
           if (totalQuestion > currentIndex)
            {
                var currentQuestion = dtQuestions.Rows[currentIndex];
                lblQuestion.Text = currentQuestion["Title"].ToString();
                BindAnswers(Convert.ToInt32(currentQuestion["QuestionId"].ToString()));
            }
        }

        private void BindAnswers(int questionId)
        {
            var answers=_ansBL.GetAnswerByQuestionId(questionId);
            pnlAnswer.Controls.Clear();
            List<RadioButton> rdbs = new List<RadioButton>();
            foreach(DataRow ans in answers.Rows)
            {
                RadioButton rdb = new RadioButton()
                rdb.Text = ans["AnswerOption"].ToString();
                rdb.Name = ans["AnswerId"].ToString();
                rdb.Tag = ans["QuestionId"].ToString();
                rdb.CheckedChanged += new System.EventHandler(this.rdbAnswer_CheckedChanged);
                rdbs.Add(rdb);
            }
                pnlAnswer.Controls.AddRange((WindF)rdbs);

        }

        private void rdbAnswer_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            int questionId = Convert.ToInt32(r.Tag.ToString());
            int userAns = Convert.ToInt32(r.Name.ToString());
        }
        }
 
}
