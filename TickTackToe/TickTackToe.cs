using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace TickTackToe
{
    public partial class TickTackTie : Form
    {

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = 0x20000;
                return cp;
            }
        }

        protected static Point point = new Point(0, 0);

        public TickTackTie()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(LoadForms);
            EraseTheForm.MouseDown += new System.Windows.Forms.MouseEventHandler(ChangeMouseDown);
            PositionChange.MouseDown += new System.Windows.Forms.MouseEventHandler(ChangeMouseDown);
            PositionChange.MouseUp += new System.Windows.Forms.MouseEventHandler(ChangeMouseUp);
            PositionChange.MouseMove += new System.Windows.Forms.MouseEventHandler(MovePosition);
            PlayWithAI.Click += new System.EventHandler(PlayingWithFriend);
            PlayingWithFriends.Click += new System.EventHandler(PlayingWithFriend);
            CreateGame.Click += new System.EventHandler(CreateGameAndCancel);
            CancelCreate.Click += new System.EventHandler(CreateGameAndCancel);
            StartGame.Click += new System.EventHandler(StartAndPuseGame);
            PauseGame.Click += new System.EventHandler(StartAndPuseGame);
            SureMove.Click += new System.EventHandler(SureMoveUser);
            PlayAgainss.Click += (object control, EventArgs e) => PlayAgains(control, e);
            Exit.Click += (object control, EventArgs e) => Exits(control, e);
        }


        //...............................
        bool condition = false;
        private void ChangeMouseDown(object control, MouseEventArgs e) {

            Panel pan = null;
            Label label = null;

            if (control.GetType() == typeof(Panel))
            {
                pan = (Panel)control;
            }
            else
            {
                label = (Label)control;
            }

            if ((control.GetType() == typeof(Panel) ? pan.AccessibleName == "Position":label.AccessibleName == "Position"))
            {
                point = new Point(e.X, e.Y);
                condition = true;
            }
            else {
                this.Close();
            }
        }

        private void MovePosition(object control, System.Windows.Forms.MouseEventArgs e) {
            if (condition) {
                Point point2 = PointToScreen(e.Location);
                this.Location = new Point(point2.X-point.X, point2.Y - point.Y);
            }
        }

        private void ChangeMouseUp(object control, MouseEventArgs e) {
            condition = false;
        }
        //...............................







        //PLAYING WITH FRIENDS START..................................................
        //BUTTON OF PLAYING WITH FRIENDS AND PAYING WITH AI...............................................

        private void PlayingWithFriend(object control, EventArgs e) {
            Control con = (Control)control;
            if (con.GetType() == typeof(Panel)) {
                Panel pan = (Panel)con;
                if (pan.AccessibleName == "PlaywithF")
                {
                    JarOfPlayerSegment.Visible = true;
                }
                else {
                    MessageBox.Show("Do A.I");
                }
            }
        }

        //LOAD FORM..........................................................
        protected static string[] handleData = new string[2] { "", "" }, handleXandOA = new string[] { "", ""};
        protected static int[] handleScore = new int[] { 0, 0 };
        protected static int[,] handleScanning = new int[,] { { 1, 4, 7 }, { 3, 6, 9 }, { 1, 5, 9 }, { 3, 5, 7 }, { 2, 5, 8 },
        {1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        protected static string[,] st = { { "panel4", "panel7", "panel10" }, { "panel6", "panel9", "panel12" },
             { "panel4", "panel8", "panel12" }, { "panel6", "panel8", "panel10" }, { "panel5", "panel8", "panel11" },
            { "panel4", "panel5", "panel6" }, { "panel7", "panel8", "panel9" }, { "panel10", "panel11", "panel12" } };
        protected static System.Windows.Forms.Timer time = new System.Windows.Forms.Timer(), 
            timer2 = new System.Windows.Forms.Timer();
        protected static int numberTime = 60, numberCount = 0, numberHandlePan = 0, numberCountIntononeClick = 0;
        protected static string handleXandOS = "", handleNameOfPan = "", stringHandleCondition = "", conditionToNumberCount = "",
            conditionToAgain = "true", finalCondition = "true";
        protected static bool ConditionToNonClick = true;
        protected delegate void AgainF(string PlayAgain, string ShowWin);

        //TIMER.........................................................
        private void LoadForms(object control, EventArgs e) {
            time.Interval = 100;
            timer2.Interval = 200;
            time.Tick += (object TControls, EventArgs Te) => {
                if (numberTime >= 0)
                {
                    if (numberTime == 60)
                    {
                        labelTime.Text = "0" + (numberTime / 60) + ":" + (numberTime % 60) + "0";
                    }
                    else
                    {
                        if (numberTime >= 10)
                        {
                            labelTime.Text = "0" + (numberTime / 60) + ":" + numberTime;
                        }
                        else
                        {
                            labelTime.Text = "0" + (numberTime / 60) + ":" + "0" + numberTime;
                        }
                    }
                }
                else {
                    if (ConditionToNonClick != true)
                    {
                        if (handleNameOfPan == "")
                        {
                            numberTime = 60;
                            if (numberCount == 0)
                            {
                                numberCount++;
                                JarOfNameMove.Text = handleData[numberCount];
                                handleXandOS = handleXandOA[numberCount];
                            }
                            else
                            {
                                numberCount--;
                                JarOfNameMove.Text = handleData[numberCount];
                                handleXandOS = handleXandOA[numberCount];
                            }
                        }
                        else
                        {
                            time.Stop();
                            if (ConditionToNonClick != true)
                            {
                                conditionToNumberCount = "Stop";
                            }
                            else {
                                timer2.Start();
                            }
                        }
                    }
                    else {
                        time.Stop();
                        if (ConditionToNonClick != true)
                        {
                            conditionToNumberCount = "Stop";
                        }
                        else {
                            timer2.Start();
                        }
                    }
                }
                numberTime--;
            };


            timer2.Tick += (object controls, EventArgs es) =>
            {
                if (numberCountIntononeClick == 0)
                {
                    labelTime.Text = "";
                    numberCountIntononeClick++;
                }
                else {
                    labelTime.Text = "00:00";
                    numberCountIntononeClick--;
                }
            };



            //GIVE FUNCTION IN EVERY PANEL.............................................
            foreach (Panel panel in JarOfClickingBox.Controls) {
                panel.Click += new System.EventHandler(clickingBox);
            }
        }



        //CREATE GAME..................................................
        private void CreateGameAndCancel(object control, EventArgs e) {
            Button bttn = (Button)control;
            string FirstMoveHandle = textBox3.Text;
            int numberCount = 0;

            if (bttn.AccessibleName != "Cancel")
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    bool cond = true;
                    if (textBox1.Text == textBox3.Text || textBox2.Text == textBox3.Text)
                    {
                        cond = true;
                    }
                    else {
                        cond = false;
                    }
                    if (cond == true)
                    {
                        foreach (Control TextBoxCheck in JarOfPlayerSegment.Controls)
                        {
                            if (TextBoxCheck.GetType() == typeof(TextBox))
                            {
                                numberCount++;
                                if (TextBoxCheck.AccessibleName != "FirstMove")
                                {
                                    if (FirstMoveHandle == TextBoxCheck.Text)
                                    {
                                        handleData[0] = TextBoxCheck.Text;
                                        handleXandOA[0] = "X";
                                        handleXandOS = handleXandOA[0];
                                    }
                                }

                                if (numberCount == 3)
                                {
                                    foreach (Control TextBoxCheck2 in JarOfPlayerSegment.Controls)
                                    {
                                        if (TextBoxCheck2 is TextBox)
                                        {
                                            if (TextBoxCheck2.AccessibleName != "FirstMove")
                                            {
                                                if (FirstMoveHandle != TextBoxCheck2.Text)
                                                {
                                                    handleData[1] = TextBoxCheck2.Text;
                                                    handleXandOA[1] = "O";
                                                }
                                            }
                                        }
                                    }

                                    GamePanel.Visible = true;
                                    JarOfPlayerSegment.Visible = false;
                                    JarOfNameMove.Text = FirstMoveHandle;
                                }
                            }
                        }
                    }
                    else {
                        MessageBox.Show("Sorry.... Please select the first move player.............");
                    }
                }
                else
                {
                    MessageBox.Show("Sorry.... The textbox is empty.............");   
                }
            }
            else {
                JarOfPlayerSegment.Visible = false;
            }
        }




        //START AND GAME............................................................
        private void StartAndPuseGame(object control, EventArgs e) {
            Button bttn = (Button)control;
            if (bttn.AccessibleName == "Start") {
                conditionToAgain = "true";
                stringHandleCondition = "Start";

                if (!String.IsNullOrEmpty(handleNameOfPan))
                {
                    SureMove.Enabled = true;
                }
                else {
                    SureMove.Enabled = false;
                }

                time.Start();
            } else if (bttn.AccessibleName == "Pause") {
                stringHandleCondition = "";
                conditionToAgain = "false";
                SureMove.Enabled = false;
                time.Stop();
            }
        }



        //GAME CLICKING BOX.........................................................
        private void clickingBox(object control, EventArgs e)
        {
            if (conditionToAgain == "true") { 
            if (conditionToNumberCount != "Stop") {
                    if (stringHandleCondition == "Start")
                    {
                        Panel pan = (Panel)control;
                        if (handleNameOfPan == pan.Name || handleNameOfPan == "")
                        {
                            numberHandlePan = Convert.ToInt32(pan.Tag);
                            if (String.IsNullOrEmpty(pan.AccessibleDescription))
                            {
                                if (pan.AccessibleName.ToString() == "")
                                {
                                    foreach (Label lb in pan.Controls)
                                    {
                                        ConditionToNonClick = false;
                                        SureMove.Enabled = true;
                                        handleNameOfPan = pan.Name;
                                        lb.Text = handleXandOS;
                                        pan.AccessibleName = handleXandOS;
                                    }
                                }
                                else
                                {
                                    foreach (Label lb in pan.Controls)
                                    {
                                        ConditionToNonClick = true;
                                        SureMove.Enabled = false;
                                        handleNameOfPan = "";
                                        lb.Text = "";
                                        pan.AccessibleName = "";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        //SURE MOVE.....................................................................
        private void SureMoveUser(object control, EventArgs e)
        {
            for (int numberCountAS = 0; numberCountAS <= handleScanning.Length-1; numberCountAS++) {
                if (finalCondition == "true") {
                    int countCheck = 0, numberCountFinal = 0;
                    if (numberCountAS <= 7) {

                        foreach (Panel panel in JarOfClickingBox.Controls)
                        {
                            countCheck++;
                            if (handleScanning[numberCountAS, 0] == countCheck && st[numberCountAS, 0] == panel.Name)
                            {
                                if (panel.AccessibleName != "") {
                                    if (handleXandOS == panel.AccessibleName)
                                    {
                                        numberCountFinal++;
                                    }
                                }
                            }


                            if (handleScanning[numberCountAS, 1] == countCheck && st[numberCountAS, 1] == panel.Name) {
                                if (panel.AccessibleName != "")
                                {
                                    if (handleXandOS == panel.AccessibleName)
                                    {
                                        numberCountFinal++;
                                    }
                                }
                            }

                            if (handleScanning[numberCountAS, 2] == countCheck && st[numberCountAS, 2] == panel.Name) {
                                if (panel.AccessibleName != "")
                                {
                                    if (handleXandOS == panel.AccessibleName)
                                    {
                                        numberCountFinal++;
                                    }
                                }
                            }

                        }


                        //GAME ENDED......................................
                        if (numberCountFinal == 3)
                        {
                            AgainF delegateA = new AgainF(FAgain);
                            delegateA.Invoke("", "ShowWin");
                            break;
                        }

                        if (numberCountAS == 7) {

                            if (numberCountFinal != 3) {
                                int numCountF1 = 0, numCountF2 = 0;
                                foreach (Panel panS in JarOfClickingBox.Controls) {
                                    numCountF1++;
                                    if (String.IsNullOrEmpty(panS.AccessibleName)) {
                                        numCountF2 += 1;
                                    }
                                }
                                if (numCountF1 == 9) {
                                    if (numCountF2 != 0)
                                    {
                                        Panel pan = (Panel)(JarOfClickingBox.Controls["panel" + numberHandlePan]);
                                        pan.AccessibleDescription = "DoneSureMove";
                                        handleNameOfPan = "";
                                        conditionToNumberCount = "";
                                        SureMove.Enabled = false;
                                        ConditionToNonClick = true;
                                        numberTime = 60;
                                        time.Stop();
                                        if (numberCount == 0)
                                        {
                                            numberCount++;
                                            JarOfNameMove.Text = handleData[numberCount];
                                            handleXandOS = handleXandOA[numberCount];
                                        }
                                        else
                                        {
                                            numberCount--;
                                            JarOfNameMove.Text = handleData[numberCount];
                                            handleXandOS = handleXandOA[numberCount];
                                        }
                                        time.Start();
                                        timer2.Stop();
                                    }
                                    else {
                                        AgainF delegateA = new AgainF(FAgain);
                                        delegateA.Invoke("", "ShowWinsTie");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        //DELEGATE AGAIN.................................................
        protected void FAgain(string PlayAgain, string ShowWin) {
            if (PlayAgain == "Playagain")
            {
                numberCount = 0;
                numberTime = 60;
                handleXandOS = "";
                handleXandOS = "X";
                handleNameOfPan = "";
                finalCondition = "true";
                labelTime.Text = "01:00";
                ConditionToNonClick = true;
                SureMove.Enabled = false;
                GameEnded.Visible = false;
                conditionToAgain = "false";
                stringHandleCondition = "Start";
                foreach (Control con in JarOfClickingBox.Controls)
                {
                    if (con.GetType() == typeof(Panel))
                    {
                        foreach (Label lb in con.Controls)
                        {
                            con.AccessibleDescription = "";
                            con.AccessibleName = "";
                            lb.Text = "";
                        }
                    }
                }
            }
            else if(ShowWin == "ShowWin" || ShowWin == "ShowWinsTie")
            {
                time.Stop();
                finalCondition = "false";
                handleScore[numberCount] += (ShowWin == "ShowWin" ? 1:0);
                WinnerSee.Text = (ShowWin == "ShowWin" ? handleData[numberCount]:"Tie");
                for (int count1 = 0; count1 < handleData.Length; count1++)
                {
                    if (textBox1.Text == handleData[count1])
                    {
                        Player1.Text = handleData[count1];
                        Player1S.Text = handleScore[count1].ToString();
                    }
                    else
                    {
                        Player2.Text = handleData[count1];
                        Player2S.Text = handleScore[count1].ToString();
                    }
                }

                GameEnded.Visible = true;
            }
        }


        //PLAY AGAIN BUTTON.....................................................
        private void PlayAgains(object control, EventArgs e) {
            AgainF delegateA = new AgainF(FAgain);
            delegateA.Invoke("Playagain", "");
        }


        //EXIT GAME................................................................
        private void Exits(object control, EventArgs e) {

            handleData = new string[2] { "", "" };
            handleXandOA = new string[] { "", "" };
            handleScore = new int[] { 0, 0 };
            numberTime = 60;
            numberCount = 0;
            numberHandlePan = 0;
            handleXandOS = "";
            handleNameOfPan = "";
            finalCondition = "true";
            labelTime.Text = "01:00";
            conditionToAgain = "true";
            ConditionToNonClick = true;
            stringHandleCondition = "";
            conditionToNumberCount = "";


            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            GameEnded.Visible = false;
            GamePanel.Visible = false;
            SureMove.Enabled = false;
            JarOfPlayerSegment.Visible = false;

            foreach (Control con in JarOfClickingBox.Controls)
            {
                if (con.GetType() == typeof(Panel))
                {
                    foreach (Label lb in con.Controls)
                    {
                        con.AccessibleDescription = "";
                        con.AccessibleName = "";
                        lb.Text = "";
                    }
                }
            }

        }


        //PLAYING WITH FRIENDS END.....................................................................



        //PLAYING WITH A.I START...................................................................



    }
}
