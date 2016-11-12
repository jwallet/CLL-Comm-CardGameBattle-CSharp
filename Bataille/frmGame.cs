using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;//--added
using System.IO;//--added
using System.Threading;

namespace Bataille
{
    public partial class frmGame : Form
    {
        private CDeck m_Deck;
        private CData m_Data;
        private CPort m_Port;
        private CScore m_Score;
        //animation
        private Point m_LocalDraw, m_OppDraw;
        private bool m_Bataille;
        private string m_MessageOnScreen;
        private Point m_LocalDrawPoint = new Point(152, 388);
        private Point m_OppDrawPoint = new Point(234, -144);
        private bool m_CardVisibleFalse;
        //delegate
        internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        private delegate void SetTextCallback(string text);
        private delegate void SetBoolCallback(bool valeur);
        private delegate void SetTimerCallback();
        
        public frmGame()
        {
            InitializeComponent();            
            m_Deck = new CDeck();            
            m_Data = new CData();
            m_Port = new CPort();
            m_Score = new CScore();
            m_Bataille = false;
            m_MessageOnScreen = null;
            m_Port.ON();
            m_Data.DataReceived = null;
            CheckForIllegalCrossThreadCalls = true;
            m_Port.SP.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(SP_DataReceived);
            InvalidateCardOpp();
            InvalidateCardLocal();
            if (m_Port.SelectedPort != "COM") { m_Data.LT0_ONLINE = 1; m_Data.LT1_ORDER_STATUS = 0; DataBuildTasks(); } //send waiting
        }
        private void SP_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(200);
            string SaveDataOnError;
            m_Data.DataReceived = m_Port.SP.ReadExisting();
            SaveDataOnError = m_Data.DataReceived;
            while (m_Data.DataReceived != "")
            {
                m_Data.DataReceived_Analyze();
                if (m_Data.OT0_ONLINE == 1)
                {
                    m_Data.LT0_ONLINE = 1;
                    if (m_Data.OT1_ORDER_STATUS >= 0 && m_Data.OT1_ORDER_STATUS <= 2)
                        GetItStarted();
                    else if (m_Data.OT1_ORDER_STATUS == 3)
                        DataReadTasks();
                }
                else if (m_Data.OT0_ONLINE == 2)
                    DataReadTasks();
                else if(m_Data.OT0_ONLINE == 0)
                    this.BeginInvoke(new SetTextCallback(MessageOnScreen), new object[] { "ERROR" });                                    
            }
       }
        private void MessageOnScreen(string text) { lblMessage.Text = text; m_MessageOnScreen = text; }
        private void EnableMove(bool valeur) { pnlLocalDraw.Enabled = valeur; }
        private void VisibleMessage(bool valeur) { lblMessage.Visible = valeur; }
        private void StartOpponentMove() { tmrAnimationOpponent.Start(); }
        private void MessageHoldPoints(string text) { lblTitleHoldingPoints.Text = text; }
        private void MessageMyPointsPlus(string text) { lblMyPointsPlus.Text = text; }
        private void MessageHisPointsPlus(string text) { lblHisPointsPlus.Text = text; }
        private void MessageMyPoints(string text) { lblMyPoints.Text = text; }
        private void MessageHisPoints(string text) { lblHisPoints.Text = text; }
        private void VisibleMyCard(bool valeur) { pnlLocalCard.Visible = valeur; }
        private void VisibleHisCard(bool valeur) { pnlOpponentCard.Visible = valeur; }
        private void label1(string text) { lblaffichage.Text = text; }
        private void GetItStarted()
        {
            int ret=-1;
            if (m_Data.OT1_ORDER_STATUS == 0)//il attend joueur
            {
                m_Data.LT1_ORDER_STATUS = 1; //connection made,status roll the dice = 1
                this.BeginInvoke(new SetTextCallback(MessageOnScreen), new object[] { "PLAYER FOUND" });                
                DataBuildTasks();
                return;
            }
            else if (m_Data.OT1_ORDER_STATUS == 1 || m_Data.OT1_ORDER_STATUS == 2)//si rollthedice ou pret a jouer
            {
                if(m_Data.LT1_ORDER_STATUS==0)
                {
                    m_Data.LT1_ORDER_STATUS = 1;
                    this.BeginInvoke(new SetTextCallback(MessageOnScreen), new object[] { "PLAYER FOUND" });   
                    DataBuildTasks();
                    return;
                }
                else if (m_Data.LT1_ORDER_STATUS == 1||m_Data.LT1_ORDER_STATUS==2)//rollthedice
                {
                    if (m_Data.LT1_ORDER_STATUS == 2 && m_Data.OT1_ORDER_STATUS == 2)
                    {
                        DataToSend();
                        m_Data.LT1_ORDER_STATUS = 3;
                        m_Data.OT1_ORDER_STATUS = 3;
                        if(m_Data.LT2_PLAYER_SWITCHER==1)
                        {
                            this.BeginInvoke(new SetTextCallback(MessageOnScreen), new object[] { "PLAY" });
                            Thread.Sleep(800); //affichage Play
                            this.BeginInvoke(new SetBoolCallback(VisibleMessage), new object[] { false });
                            this.BeginInvoke(new SetBoolCallback(EnableMove), new object[] { true });
                        }
                        else
                        {
                            this.BeginInvoke(new SetTextCallback(MessageOnScreen), new object[] { "WAIT" });
                        }
                        return;
                    }
                    else
                    {
                        ret = m_Deck.AnalyseRolledNumber_0Lose_1Win_2Tied(m_Data.OT6_ROLLED_NUMBER_TO_START);
                        if (ret == 2)
                        {
                            m_Deck.RollTheDice();
                            m_Data.LT1_ORDER_STATUS = 1;
                            DataBuildTasks();
                            return;
                        }
                        else if (ret == 1)
                        {
                            m_Data.PlayerSwitcher = true;
                            m_Data.LT1_ORDER_STATUS = 2;
                            DataToSend();                          
                            return;
                        }
                        else
                        {
                            m_Data.PlayerSwitcher = false;
                            m_Data.LT1_ORDER_STATUS = 2;
                            DataToSend();
                            return;
                        }
                    }
                }
            }
        }
        private void DataReadTasks()
        {
            if (m_Data.OT0_ONLINE == 2)//offline ou game over
            {
                m_Data.Disconnect = true;
                this.BeginInvoke(new SetBoolCallback(VisibleMessage), new object[] { true });
                this.BeginInvoke(new SetBoolCallback(EnableMove), new object[] { false });
                this.BeginInvoke(new SetTextCallback(MessageOnScreen), new object[] { "PLAYER LEFT" });
                Thread.Sleep(1000);
                this.BeginInvoke(new SetTextCallback(MessageOnScreen), new object[] { "YOU WIN" });
                m_Port.OFF();
            }
            else if (m_Data.OT0_ONLINE == 1) //online, keep playing
            {
                if (m_Data.OT1_ORDER_STATUS == 3)//make sure it is set on playing
                {
                    if (m_Data.OT2_PLAYER_SWITCHER != m_Data.LT2_PLAYER_SWITCHER) //opponent turn to flip his card
                    {
                        m_Deck.OpponentCard = new SCard(m_Data.OT4_CARD_SUIT, m_Data.OT5_CARD_NUMBER);
                        this.BeginInvoke(new SetTimerCallback(StartOpponentMove));
                    }
                    
                }
            }
        }
        private void ValidationWinOrLose()
        {
            m_Deck.NumberCardsPlayed++;
            this.BeginInvoke(new SetTextCallback(label1), new object[] { m_Deck.NumberCardsPlayed.ToString() });
            string Result;

            if (m_Deck.NumberCardsPlayed % 2 == 0 && m_Deck.NumberCardsPlayed>1)
            {

                Result = m_Deck.UpdateScoreByComparingMyCardTo(m_Deck.OpponentCard);
                this.BeginInvoke(new SetTextCallback(MessageHoldPoints), new object[] { "BATTLE ZONE" });
                if (Result == "Hold")
                {
                    m_Score.Hold();
                    //memoire la carte bataille
                    //bool mode bataille
                    this.BeginInvoke(new SetTextCallback(MessageHisPointsPlus), new object[] { "" });
                    this.BeginInvoke(new SetTextCallback(MessageMyPointsPlus), new object[] { "" });
                    this.BeginInvoke(new SetTextCallback(MessageHoldPoints), new object[] { "HOLD ON " + m_Score.HoldPoints + " PTS" });
                    m_CardVisibleFalse = false;
                }
                else
                {
                    m_CardVisibleFalse = true;
                    //this.BeginInvoke(new SetBoolCallback(VisibleMyCard), new object[] { false });
                    //this.BeginInvoke(new SetBoolCallback(VisibleHisCard), new object[] { false });
                    if (Result == "Win")
                    {
                        m_Score.Win();
                        m_Deck.BattleCardToBeat = -1;
                        this.BeginInvoke(new SetTextCallback(MessageMyPointsPlus), new object[] { (m_Score.Plus2 + m_Score.HoldPoints).ToString() });
                        this.BeginInvoke(new SetTextCallback(MessageMyPoints), new object[] { m_Score.Local.ToString() });
                        this.BeginInvoke(new SetTextCallback(MessageHisPointsPlus), new object[] { "" });
                    }
                    else if (Result == "Lose")
                    {
                        m_Score.Lose();
                        m_Deck.BattleCardToBeat = -1;
                        this.BeginInvoke(new SetTextCallback(MessageHisPointsPlus), new object[] { (m_Score.Plus2 + m_Score.HoldPoints).ToString() });
                        this.BeginInvoke(new SetTextCallback(MessageHisPoints), new object[] { m_Score.Opponent.ToString() });
                        this.BeginInvoke(new SetTextCallback(MessageMyPointsPlus), new object[] { "" });
                    }
                }
                
            }
        }

        //METHODE QUI construit une trame
        private void DataBuildTasks()
        {
            if(m_Data.LT0_ONLINE==2) //offline ou gameover
                DataToSend();
            else if(m_Data.LT0_ONLINE==1) //online
            {
                if(m_Data.LT1_ORDER_STATUS==3)//jouer
                {
                    m_Data.LT3_NUMBER_CARDS_PLAYED = m_Deck.NumberCardsPlayed;
                    m_Data.LT4_CARD_SUIT = m_Deck.LocalCard.Symbol;
                    m_Data.LT5_CARD_NUMBER = m_Deck.LocalCard.Number;
                    DataToSend();
                }
                else if(m_Data.LT1_ORDER_STATUS==1)//roll the dice
                {
                    m_Data.LT6_ROLLED_NUMBER_TO_START = m_Deck.RolledNumber;
                    DataToSend();
                }
                else if(m_Data.LT1_ORDER_STATUS==0 || m_Data.LT1_ORDER_STATUS == 2)//attente ou pretajouer
                    DataToSend();//waiting for players
            }
        }
        //methode qui envoie une trame
        private void DataToSend()
        {
            byte[] DataBuffer = new byte[11];
            m_Data.DataToSend_Builder();
            for(int i=0;i<m_Data.DataToSend.Length;i++)
                DataBuffer[i] = (byte)m_Data.DataToSend[i];
            m_Port.SP.Write(DataBuffer, 0, 11);
        }

        private void NewAction()
        {
            m_Deck.Random(); //genere la carte
            m_Data.LT0_ONLINE = 1;//online
            m_Data.LT1_ORDER_STATUS = 3;//playing a card
            m_Data.TotalCardsDrawn = 1;//accumulate played cards
            m_Data.LT4_CARD_SUIT = m_Deck.LocalCard.Symbol; //card symbol
            m_Data.LT5_CARD_NUMBER = m_Deck.LocalCard.Number; //card number
            DataBuildTasks();
        }
        private void frmGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(m_Port.SP.IsOpen)
            {
                m_Data.Disconnect = true;
                DataBuildTasks();
            }            
        }
        

        private void pnlLocalDraw_MouseClick(object sender, MouseEventArgs e)
        {
            lblMessage.Visible = false;
            lblArrow.Visible = false;
            pnlLocalDraw.Enabled = false;
            pnlLocalDraw.Location = m_LocalDrawPoint;
            
            tmrAnimationLocal.Start();
            NewAction();
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            m_Port.ShowSettings();
            if (m_Port.SelectedPort != "COM")
            {
                m_Port.ON();
                m_Data.LT0_ONLINE = 1; 
                m_Data.LT1_ORDER_STATUS = 0; 
                DataBuildTasks(); //send waiting
            }
        }
        private void pnlLocalCard_Paint(object sender, PaintEventArgs e)
        {
            Graphics GraphicLocal;
            GraphicLocal = e.Graphics;
            m_Deck.Draw(GraphicLocal, m_Deck.LocalCard, pnlLocalCard.Width, pnlLocalCard.Height);
        }

        private void pnlOpponentCard_Paint(object sender, PaintEventArgs e)
        {
            Graphics GraphicOpponent;
            GraphicOpponent = e.Graphics;
            m_Deck.Draw(GraphicOpponent, m_Deck.OpponentCard, pnlOpponentCard.Width, pnlOpponentCard.Height);
        }

        private void tmrAnimationOpponent_Tick(object sender, EventArgs e)
        {
            m_OppDraw.Y += 20;
            pnlOpponentDraw.Location = m_OppDraw;
            pnlOpponentDraw.Invalidate();
            if (m_CardVisibleFalse == false && m_Deck.NumberCardsPlayed % 2 == 0 && m_Deck.NumberCardsPlayed > 1)
            {
                pnlLocalCard.Visible = true;
                pnlOpponentCard.Visible = true;
            }
            else if (m_CardVisibleFalse == true && m_Deck.NumberCardsPlayed % 2 == 0 && m_Deck.NumberCardsPlayed > 1)
            {
                pnlLocalCard.Visible = false;
                pnlOpponentCard.Visible = false;
            }
            if (pnlOpponentDraw.Location.Y == pnlOpponentCard.Location.Y)
            {
                Thread.Sleep(300);
                pnlOpponentDraw.Visible = false;
                InvalidateCardOpp();
                pnlOpponentCard.Visible = true;
                pnlOpponentDraw.Visible = true;
                pnlLocalDraw.Enabled = true;
                lblMessage.Visible = false;
                tmrAnimationOpponent.Stop();
                ValidationWinOrLose();
                //if(m_Data.PlayerSwitcher==true)
                //{
                //    if (m_Bataille == false)
                //    {
                //        pnlOpponentCard.Visible = false;
                //        pnlLocalCard.Visible = false;
                //    }
                //}
            }
        }

        private void tmrAnimationLocal_Tick(object sender, EventArgs e)
        {
            m_LocalDraw.Y -= 20;
            pnlLocalDraw.Location = m_LocalDraw;
            pnlLocalDraw.Invalidate();
            if (m_CardVisibleFalse == false && m_Deck.NumberCardsPlayed % 2 == 0 && m_Deck.NumberCardsPlayed > 1)
            {
                pnlLocalCard.Visible = true;
                pnlOpponentCard.Visible = true;
            }
            else if (m_CardVisibleFalse == true && m_Deck.NumberCardsPlayed % 2 == 0 && m_Deck.NumberCardsPlayed > 1)
            {
                pnlLocalCard.Visible = false;
                pnlOpponentCard.Visible = false;
            }
            if (pnlLocalDraw.Location.Y == pnlLocalCard.Location.Y)
            {                
                Thread.Sleep(300);           
                pnlLocalDraw.Visible = false;
                InvalidateCardLocal();
                pnlLocalCard.Visible = true;
                pnlLocalDraw.Visible = true;
                lblMessage.Text = "WAIT";
                lblMessage.Visible = true;
                tmrAnimationLocal.Stop();
                ValidationWinOrLose();
            }            
        }


        private void frmGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 032 && lblMessage.Visible == false)
            {
                lblMessage.Text = "CLICK HERE";
                lblMessage.Visible = true;
                lblArrow.Visible = true;                
            }
        }

        private void InvalidateCardOpp()
        {
            m_OppDraw = m_OppDrawPoint;
            pnlOpponentDraw.Location = m_OppDraw;
        }

        private void InvalidateCardLocal()
        {
            m_LocalDraw = m_LocalDrawPoint;
            pnlLocalDraw.Location = m_LocalDraw;
        }

        private void pnlLocalDraw_MouseHover(object sender, EventArgs e)
        {
            if (pnlLocalDraw.Location == m_LocalDrawPoint)
            {
                pnlLocalDraw.Location = new Point(152, 368);
                if (lblArrow.Visible == true)
                    lblArrow.Location = new Point(190, 325);
            }
        }

        private void pnlLocalDraw_MouseLeave(object sender, EventArgs e)
        {
            pnlLocalDraw.Location = m_LocalDrawPoint;
            lblArrow.Location = new Point(190, 345);
        }
    }
}
