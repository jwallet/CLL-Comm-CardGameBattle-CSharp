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
        private Point m_LocalDrawPoint = new Point(152, 388);
        private Point m_OppDrawPoint = new Point(234, -144);
        //delegate
        internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        internal delegate void SerialPinChangedEventHandlerDelegate(object sender, SerialPinChangedEventArgs e);
        private delegate void SetTextCallback(string texte);
        private delegate void SetBoolCallback(bool valeur);  
        
        public frmGame()
        {
            InitializeComponent();            
            m_Deck = new CDeck();            
            m_Data = new CData();
            m_Port = new CPort();
            m_Score = new CScore();
            m_Bataille = false;
            //m_Deck.Random();
            m_Port.ON();
            if(m_Port.SelectedPort!="COM")
            { tmrWaiting4Player.Start(); }
            m_Data.DataReceived = null;
            CheckForIllegalCrossThreadCalls = true;
            m_Port.SP.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(SP_DataReceived);                    
            InvalidateCardOpp();
            InvalidateCardLocal();
        }
        private void SP_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(50);
            m_Data.DataReceived = m_Port.SP.ReadExisting();
            if (m_Data.DataReceived != "")
            {
               //**************************************************** this.BeginInvoke(new SetTextCallback(SetText), new object[] { m_Data.DataReceived });
                //Lancer methode analisation de la trame recu
                //Lancer methode ANALYSE DES INDEX pour executer les ordres et interprete les. (ex: dessiner une carte)
                m_Data.DataReceived_Analyze();                    
                if (tmrWaiting4Player.Enabled == false && m_Data.OT0_ONLINE==1)
                {
                    m_Data.LT0_ONLINE = 1;
                    if (m_Data.OT1_ORDER_STATUS == 0 || m_Data.OT1_ORDER_STATUS == 1)
                        GetItStarted();
                    else if (m_Data.OT1_ORDER_STATUS == 2)
                        DataReadTasks();
                }                    
            }
        }
        private void MessageStarting(string text){lblMessage.Text = text;}
        private void MessageWait(string text) { lblMessage.Text = text; }
        private void MessageClickCard(string text) { lblMessage.Text = text; }
        private void EnableMove(bool valeur) { pnlLocalDraw.Enabled = valeur; }
        private void GetItStarted()
        {
            int ret;
            if (m_Data.LT1_ORDER_STATUS == 1) //si rollthedice
            {
                {
                    m_Deck.RollTheDice();
                    DataBuildTasks();
                    m_Data.LT1_ORDER_STATUS = 2;
                    return;
                }
            }

            if (m_Data.OT1_ORDER_STATUS == 0)//il attend joueur
            {
                //connection made, now roll the dice
                m_Data.LT1_ORDER_STATUS = 1; //status roll the dice = 1
                //m_Data.DataReceived = ""; //clear les requetes de connexion du timer ?BAAAAAA:B? demander qui surcharge datareceive
                //puisque la connexion est etablie, continuer avec une nouvelle requete sans timer
                this.BeginInvoke(new SetTextCallback(MessageStarting), new object[] { "STARTING" });                
                DataBuildTasks();
                return;
            }
            else if (m_Data.OT1_ORDER_STATUS == 1)//si rollthedice
            {
                ret = m_Deck.AnalyseRolledNumber_0Lose_1Win_2Tied(m_Data.OT6_ROLLED_NUMBER_TO_START);
                if (ret == 2)
                {
                    m_Data.LT1_ORDER_STATUS = 1;
                }
                else if (ret == 1)
                {
                    this.BeginInvoke(new SetTextCallback(MessageClickCard), new object[] { "CLICK CARD" });
                    //Win = pile
                    this.BeginInvoke(new SetBoolCallback(EnableMove), new object[] { true });
                    m_Data.LT1_ORDER_STATUS = 2;
                    m_Data.PlayerSwitcher = true;
                    DataBuildTasks();
                    return;
                }
                else
                {
                    this.BeginInvoke(new SetTextCallback(MessageWait), new object[] { "WAIT" });
                    //Lose = face
                    m_Data.LT1_ORDER_STATUS = 2;
                    m_Data.PlayerSwitcher = false;
                    DataBuildTasks();
                    return;
                }
            }
        }
        private void DataReadTasks()
        {
            //chaque etape doit construire une trame (DATA.Builder) et puis l'envoyer une trame (PORT.SP.WRITE(datasent))
            //1er: verifier si empty = tout a 0 = erreur, terminer
            //2eme : verifier si online 1:yes 2:no
            //3eme : verifier si le De est actif, si oui juste prend index[7] pour le #
            //4eme : verifier si index[1] = 4, continuer, partie en jeux, voir les autres index[] pour la carte
            
            if(m_Data.OT0_ONLINE==2)//offline
            {
                //Quoi faire si deconnecte
                m_Data.Disconnect = true;
                //game over
                
            }
            else if (m_Data.OT0_ONLINE == 1) //online, keep playing
            {
                //do this
                if(m_Data.OT1_ORDER_STATUS==2)//make sure it is set on playing
                {
                    if(m_Data.OT2_PLAYER_SWITCHER != m_Data.LT2_PLAYER_SWITCHER) //my turn
                    {
                        tmrAnimationOpponent.Start();
                    }
                }

            }
        }

        //METHODE QUI construit une trame
        private void DataBuildTasks()
        {
            if(m_Data.LT0_ONLINE==2) //offline
            {
                DataToSend();
            }
            else if(m_Data.LT0_ONLINE==1) //online
            {
                if(m_Data.LT1_ORDER_STATUS==2)//jouer
                {
                    m_Data.LT3_NUMBER_CARDS_PLAYED = m_Deck.NumberCardsPlayed;
                    //m_Data.LT3_NUMBER_CARDS_ON_TABLE = 0; //surement inutile
                    m_Data.LT4_CARD_SUIT = m_Deck.LocalCard.Symbol;
                    m_Data.LT5_CARD_NUMBER = m_Deck.LocalCard.Number;
                    DataToSend();
                }
                else if(m_Data.LT1_ORDER_STATUS==1)//roll the dice
                {
                    m_Data.LT6_ROLLED_NUMBER_TO_START = m_Deck.RolledNumber;
                    DataToSend();
                }
                else if(m_Data.LT1_ORDER_STATUS==0)//attente
                {
                    DataToSend();//waiting for players
                }
                
            }
            //else = 0 = error
            
        }
        //methode qui envoie une trame
        private void DataToSend()
        {
            byte[] DataBuffer = new byte[11];
            //m_Data.DataReceived = "";
            m_Data.DataToSend_Builder();
            //m_Port.SP.Write(m_Data.DataToSend);
            for(int i=0;i<m_Data.DataToSend.Length;i++)
            {
                DataBuffer[i] = (byte)m_Data.DataToSend[i];
            }
            m_Port.SP.Write(DataBuffer, 0, 11);
        }

        private void NewAction()
        {
            m_Deck.Random(); //genere la carte
            m_Data.LT0_ONLINE = 1;
            m_Data.LT1_ORDER_STATUS = 2;
//peut etre va falloir le mettre une variable bool pour quand l'autre recoit son datareceived y check si c'est son tour
            m_Data.PlayerSwitcher = (!(m_Data.PlayerSwitcher));
            m_Data.TotalCardsDrawn = 1;
            m_Data.LT4_CARD_SUIT = m_Deck.LocalCard.Symbol;
            m_Data.LT5_CARD_NUMBER = m_Deck.LocalCard.Number;
            DataBuildTasks();
        }
        private void frmGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(m_Port.SP.IsOpen)
            {
                m_Data.Disconnect = true;
                m_Data.LT0_ONLINE = 2;
                DataBuildTasks();
                //Lancer methode ANALYSE DES INDEX
            }            
        }
        

        private void pnlLocalDraw_MouseClick(object sender, MouseEventArgs e)
        {
            lblMessage.Visible = false;
            if (m_Bataille == false)
            {
                pnlOpponentCard.Visible = false;
                pnlLocalCard.Visible = false;
            }
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
                tmrWaiting4Player.Start(); 
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
            m_Deck.OpponentCard = new SCard(m_Data.OT4_CARD_SUIT, m_Data.OT5_CARD_NUMBER);
            GraphicOpponent = e.Graphics;
            m_Deck.Draw(GraphicOpponent, m_Deck.OpponentCard, pnlOpponentCard.Width, pnlOpponentCard.Height);
        }

        private void tmrAnimationOpponent_Tick(object sender, EventArgs e)
        {
            m_OppDraw.Y += 10;
            pnlOpponentDraw.Location = m_OppDraw;
            pnlOpponentDraw.Invalidate();
            if (pnlOpponentDraw.Location.Y == pnlOpponentCard.Location.Y)
            {
                Thread.Sleep(300);
                pnlOpponentDraw.Visible = false;
                InvalidateCardOpp();
                pnlOpponentCard.Visible = true;
                pnlOpponentDraw.Visible = true;
                pnlLocalDraw.Enabled = true;
                tmrAnimationOpponent.Stop();                
            }
        }

        private void tmrAnimationLocal_Tick(object sender, EventArgs e)
        {
            m_LocalDraw.Y -= 10;
            pnlLocalDraw.Location = m_LocalDraw;
            pnlLocalDraw.Invalidate();
            if (pnlLocalDraw.Location.Y == pnlLocalCard.Location.Y)
            {                
                Thread.Sleep(300);           
                pnlLocalDraw.Visible = false;
                InvalidateCardLocal();
                pnlLocalCard.Visible = true;
                pnlLocalDraw.Visible = true;
                tmrAnimationLocal.Stop();
            }            
        }


        private void frmGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 032)
            {
                lblMessage.Text = "CLICK CARD";
                lblMessage.Visible = true;
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
            }
        }

        private void pnlLocalDraw_MouseLeave(object sender, EventArgs e)
        {
            pnlLocalDraw.Location = m_LocalDrawPoint;
        }

        private void tmrWaiting4Player_Tick(object sender, EventArgs e)
        {
            if (m_Data.DataReceived == null)
            {
                if (m_Port.SelectedPort != "COM")
                {
                    m_Data.LT0_ONLINE = 1;
                    if (m_Data.LT1_ORDER_STATUS == 0)
                        DataBuildTasks(); //send im waiting for connection...every 1sec
                }
            }
            else
            {
                m_Data.LT1_ORDER_STATUS = 1;
                DataBuildTasks();//******************************************************
                tmrWaiting4Player.Stop();
            }

                
        }
    }
}
