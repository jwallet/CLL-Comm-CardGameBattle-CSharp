using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class CData
    {
        /*logique du jeu
         * disable tout sauf message ATTENTE DE JOUEUR until timer=off
         * timer = on
         * envoie en continu trame IM ONLINE 1-0-0-0-0-0-0-0 (BAAAAAAA)
         * si recoit la meme LES DEUX SONT ONLINE
         * roule le D, envoie trame 1-1-0-0-0-0-0-#ROULER=100 (BBAAAAAd)
         * attend la trame de l'autre joueur, pile ou face roule tant que pas fini ou pareille
         * lance methode interne sans trame cdeck.AnalyseRolledNumber_0Lose_1Win_2Tied
         * si =2 doit renvoie une trame 1-1-0-0-0-0-0-#ROULER=100 (BBAAAAAd) et reattendre la trame de l'ennemi
         * sinon stop timer (TIMER=off) interne, affiche pile si gagner, face si perdu
         * si 1er je lance ma carte et ma trame (1-2-1-...), enable mouse clic, lancer animation de la carte
         * si 2eme jattend le data received avec index[1] = 2;, garde disable le mouse clic
         * si 1er a envoye sa carte, disable mouse clic et attend 2eme joueur a jouer, contraire pour joueur 2, et etc.
         * */
        private bool m_Disconnect; //if activated, warn the opponent that his opponent has disconnected
        private string m_DataReceived;
        private int[] m_tDataCodeOrdersReceived;//10 orders and task, takes numeral 0 to 26 //0 3 0 0 0 0 143 0
        private int[] m_tDataCodeOrdersSent;//10 orders and task, takes numeral 0 to 26 //0 3 0 0 0 0 143 0
        private string m_DataToSend;//AABCDAAA
        private bool m_PlayerSwitcher;
        private int m_TotalCardsDrawn;

        public int TotalCardsDrawn
        {
            get
            {
                return m_TotalCardsDrawn;
            }
            set
            {
                if (value == 1)
                {
                    LT3_NUMBER_CARDS_PLAYED++;
                    m_TotalCardsDrawn += value;
                }
            }
        }

        public bool PlayerSwitcher
        {
            get
            {
                return m_PlayerSwitcher;
            }
            set
            {
                if(value==true)
                {
                    LT2_PLAYER_SWITCHER = 1;
                }
                else if(value==false)
                {
                    LT2_PLAYER_SWITCHER = 0;
                }
                value = m_PlayerSwitcher;
            }
        }

        public string DataReceived
        {
            get { return m_DataReceived; }
            set { m_DataReceived = value; }
        }

        public string DataToSend
        {
            get { return m_DataToSend; }
            //set { m_DataToSend = value; }
        }

        public bool Disconnect
        {
            get { return m_Disconnect; }
            set { m_Disconnect = value; }
        }

        //DATA - MY ORDERS TO SEND
        public int LT0_ONLINE //0: Error 1:YES 2:Non
        {
            get { return m_tDataCodeOrdersSent[0]; }
            set { m_tDataCodeOrdersSent[0] = value; }
        }
        public int LT1_ORDER_STATUS //0:ATTENTE 1:ROULE_LE_D 2:JOUER
        {
            get { return m_tDataCodeOrdersSent[1]; }
            set { m_tDataCodeOrdersSent[1] = value; }
        }
        public int LT2_PLAYER_SWITCHER //0 - 1;
        {
            get { return m_tDataCodeOrdersSent[2]; }
            set { m_tDataCodeOrdersSent[2] = value; }
        }
        public int LT3_NUMBER_CARDS_PLAYED //0:ZERO 1:ONE 2:TWO @ 53:... (agrementer localement apres envoi de trame)
        {
            get { return m_tDataCodeOrdersSent[3]; }
            set { m_tDataCodeOrdersSent[3] = value; }
        }
        public int LT4_CARD_SUIT //SORTE index 0:Heart 1:Diamond 2:Club 3:Spade
        {
            get { return m_tDataCodeOrdersSent[4]; }
            set { m_tDataCodeOrdersSent[4] = value; }
        }
        public int LT5_CARD_NUMBER //index 0:ACE, 1:2, @ 12:KING
        {
            get { return m_tDataCodeOrdersSent[5]; }
            set { m_tDataCodeOrdersSent[5] = value; }
        }
        public int LT6_ROLLED_NUMBER_TO_START //0:EMPTY - others:ROLLED NUMBER
        {
            get { return m_tDataCodeOrdersSent[6]; }
            set { m_tDataCodeOrdersSent[6] = value; }
        }

        //DATA - HIS TASKS TO EXECUTE
        public int OT0_ONLINE //0: Error 1: yes 2:no
        {
            get { return m_tDataCodeOrdersReceived[0]; }
            set { m_tDataCodeOrdersReceived[0] = value; }
        }
        public int OT1_ORDER_STATUS //0:ATTENTE 1:ROULE_LE_D 2:JOUER
        {
            get { return m_tDataCodeOrdersReceived[1]; }
            set { m_tDataCodeOrdersReceived[1] = value; }
        }
        public int OT2_PLAYER_SWITCHER //0:Aucune 1:One 2:Two (agrementer localement apres envoi de trame)        
        {
            get { return m_tDataCodeOrdersReceived[2]; }
            set { m_tDataCodeOrdersReceived[2] = value; }
        }
        public int OT3_NUMBER_CARDS_PLAYED //0:ZERO 1:ONE 2:TWO @ 53:... (agrementer localement apres envoi de trame)
        {
            get { return m_tDataCodeOrdersReceived[3]; }
            set { m_tDataCodeOrdersReceived[3] = value; }
        }
        public int OT4_CARD_SUIT //SORTE index 0:Heart 1:Diamond 2:Club 3:Spade
        {
            get { return m_tDataCodeOrdersReceived[4]; }
            set { m_tDataCodeOrdersReceived[4] = value; }
        }
        public int OT5_CARD_NUMBER //index 0:ACE, 1:2, @ 12:KING
        {
            get { return m_tDataCodeOrdersReceived[5]; }
            set { m_tDataCodeOrdersReceived[5] = value; }
        }
        public int OT6_ROLLED_NUMBER_TO_START //0:EMPTY - others:ROLLED NUMBER
        {
            get { return m_tDataCodeOrdersReceived[6]; }
            set { m_tDataCodeOrdersReceived[6] = value; }
        }
        ////MANIPULER MON TABLEAU D'ORDRE
        //public int this[int i] //pour clearer plus rapidement le tableau de tasks, acceder par m_Data[i]
        //{
        //    get
        //    {
        //        if (i >= 0 && i < m_tDataCodeOrders.Length)
        //        {
        //            return m_tDataCodeOrders[i];
        //        }
        //        else
        //        {
        //            return -1;
        //        }
        //    }
        //    set
        //    {
        //        if (value >= 0 && value <= 255)
        //        {
        //            m_tDataCodeOrders[i] = value;
        //        }
        //    }
        //}
        ////MANIPULER TABLEAU DES TACHES DE L'OPPOSANT
        //public int this[int i] //pour clearer plus rapidement le tableau de tasks, acceder par m_Data[i]
        //{
        //    get
        //    {
        //        if (i >= 0 && i < m_tDataCodeTasks.Length)
        //        {
        //            return m_tDataCodeTasks[i];
        //        }
        //        else
        //        {
        //            return -1;
        //        }
        //    }
        //    set
        //    {
        //        if(value>=0&&value<=255)
        //        {
        //            m_tDataCodeTasks[i] = value;
        //        }
        //    }
        //}
       

        public CData()
        {
            m_tDataCodeOrdersReceived = new int[7];//means max of 10 orders and tasks.
            m_tDataCodeOrdersSent = new int[7];
            m_Disconnect = false;
            m_PlayerSwitcher = false;
            m_TotalCardsDrawn = 0;
        }

        public void DataToSend_Builder()
        {
            int i, CheckSum = 0;
            m_DataToSend = "?";
            for (i = 0; i < m_tDataCodeOrdersSent.Length; i++)
            {
                m_DataToSend += (char)(m_tDataCodeOrdersSent[i] + 65);
                //m_tDataCodeOrdersSent[i] = 0; //reset value
                CheckSum += m_tDataCodeOrdersSent[i];
            }
            m_DataToSend += ":";
            m_DataToSend += (char)(CheckSum + 65);
            m_DataToSend += "?";
        }

        public void DataReceived_Analyze()
        {
            int i, j, CheckSum_Calculated = 0, CheckSum_Found = 0;
            i = 0;
            //bool DataToSend_OnProgress = false;
            ////verifier si tous sont a zero, sinon m_tDATACODEINDEXTASKSANDORDERS n'a pas ete sender encore
            //for (i = 0; i < m_tDataCodeOrdersReceived.Length && DataToSend_OnProgress == true; i++)
            //{
            //    if (m_tDataCodeOrdersReceived[i] != 0)
            //        DataToSend_OnProgress = true;
            //}
            //if (DataToSend_OnProgress == true)
            //{
            //    DataToSend_Builder();
            //    //send data
            //    return false;
            //}
            //else //go for it, nothing is waiting
            //{
            if (DataReceived != "")
            {
                if ((OT0_ONLINE==0)||(OT0_ONLINE==1&&DataReceived[2]>'A'))
                {
                    if (m_DataReceived[i] == '?')//DataReceived = ?DAALAEAZAB:CH?
                    {
                        for (i = i + 1; m_DataReceived[i] != ':'; i++)
                        {
                            if (m_DataReceived[i] >= 65 && m_DataReceived[i] <= 90)
                            {
                                m_tDataCodeOrdersReceived[i - 1] = m_DataReceived[i] - 65; //DataReceived = DAALAEAZAB, convert to 3,0,0,11,0,4,0,26,0,1
                                CheckSum_Calculated += m_DataReceived[i] - 65;
                            }
                            else
                            {
                                m_tDataCodeOrdersReceived[i - 1] = 0;//can't convert letter to numeral order, must be between A and Z. 0 and 26.
                            }
                        }
                        for (j = i + 1; m_DataReceived[j] != '?'; j++)
                        {
                            CheckSum_Found += m_DataReceived[j] - 65;
                        }
                    }
                    //else
                    //{
                    //    return false;//return false if '?' was not found at index[0]
                    //}
                    if (CheckSum_Calculated != CheckSum_Found)
                    {
                        for (int clear = 0; clear < m_tDataCodeOrdersReceived.Length; clear++)
                        {
                            m_tDataCodeOrdersReceived[clear] = 0;//clear content of m_tDataCode
                        }
                        //return false;//return false if both checksum are not equal
                    }
                    //else
                    //{
                    //    return true;//otherwise return true and we can use m_tDataCode..
                    //}

                }
                else
                {
                    while(DataReceived.Contains("?BAAAAAA:B?"))
                        DataReceived = DataReceived.Remove(0,11);
                }
                //return false;
            }
            //return false;
        }
    }
}
