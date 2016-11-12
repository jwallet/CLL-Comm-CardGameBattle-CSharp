using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bataille
{
    class CDeck
    {
        private SCard m_MyCard;
        private SCard m_OpponentCard;
        private bool[,] m_tDeck;//index 0:Heart 1:Diamond 2:Cubs 4:Spades
        private Random m_RollTheDice;
        private int m_NumberToStart;
        private int m_CountNumberOfCardsPlayed;
        private int m_BattleCardToBeat;

        public int BattleCardToBeat
        {
            get { return m_BattleCardToBeat; }
            set { value = m_BattleCardToBeat; }
        }
        public int RolledNumber
        {
            get { return m_NumberToStart; }
        }

        public int NumberCardsPlayed
        {
            get { return m_CountNumberOfCardsPlayed; }
            set { m_CountNumberOfCardsPlayed = value; }
        }

		public CDeck()
        {
            m_tDeck = new bool[4, 13];//les valeurs du deck seront à faux pour dire qu'ils n'ont pas été retirées
            m_RollTheDice = new Random();
            m_CountNumberOfCardsPlayed = 0;
            m_BattleCardToBeat = -1;
            RollTheDice();
		}

        public SCard LocalCard
        {
            get { return m_MyCard; }
            set { m_MyCard = value; }
        }

        public SCard OpponentCard
        {
            get { return m_OpponentCard; }
            set { m_OpponentCard = value; }
        }

        public void RollTheDice()
        {
            m_NumberToStart = m_RollTheDice.Next(1,5);
        }

        public int AnalyseRolledNumber_0Lose_1Win_2Tied(int OpponentRolledNumber)
        {
            if(m_NumberToStart==OpponentRolledNumber)
            {
                return 2;
            }
            else if (m_NumberToStart > OpponentRolledNumber)
            {
                return 1;
            }
            else
            { 
                return 0;
            }
       }

		public void Remove(SCard Card)
        {
            m_tDeck[Card.Symbol, Card.Number] = true;//carte retirée
            //m_CountNumberOfCardsPlayed++;
        }

        public void Draw(Graphics Graphic, SCard Card, float XMax, float YMax)
        {
            string CardText;
            string CardNumber;
            string CardSymbol;
            string SCard;
            int FontHalfSize, FontSize, SymbolPlus;
            int Number;
            float Xc, Yc;
            Font FontToUse;
            SolidBrush Brush;
            Bitmap BMP;
            Graphics BMPGraphics;
            StringFormat DrawFormat = new StringFormat();

            SymbolPlus = 0;
            DrawFormat.FormatFlags = StringFormatFlags.NoClip;
            SCard = Card.ToString();
            Number = Card.Number + 1;
            CardNumber = SCard.Remove(0,1);
            CardSymbol = SCard[0].ToString();
            CardText = CardNumber+CardSymbol;            
            Xc = (XMax/2)-2;
            Yc = (YMax/2)-2;
            Remove(Card);
            if(Card.Symbol==0 || Card.Symbol == 1)
                Brush = new SolidBrush(System.Drawing.Color.Red);
            else
                Brush = new SolidBrush(System.Drawing.Color.Black);
           
            FontToUse = new Font("Arial", 16);
            //text and upside down text
            //SizeOfUpsideDownText = Graphic.MeasureString(CardText, FontToUse, (int)((CardText).Length), DrawFormat);
            //BMP = new Bitmap((int)SizeOfUpsideDownText.Width, (int)SizeOfUpsideDownText.Height);
            BMP = new Bitmap(50,50);
            using (BMPGraphics = Graphics.FromImage(BMP))
            {
                BMPGraphics.DrawString(CardNumber, FontToUse, Brush, 0, 0, DrawFormat);
                BMPGraphics.DrawString(CardSymbol, FontToUse, Brush, 0, 20, DrawFormat);
            }
            Graphic.DrawImage(BMP, 0, 0);//normal
            BMP.RotateFlip(RotateFlipType.Rotate180FlipNone);
            Graphic.DrawImage(BMP, XMax-BMP.Width-2, YMax-BMP.Height-2);//upside-down
            //end text and upside down text

            //symbol
            //to align symbol on the card
            switch(Card.Symbol)
            {
                case 0:
                    {
                        SymbolPlus = 6;
                        break;
                    }                    
                case 1:
                    {
                        SymbolPlus = 2;
                        break;
                    }
                case 2:
                    {
                        SymbolPlus = 8;
                        break;
                    }
                case 3:
                    {
                        SymbolPlus = 4;
                        break;
                    }
            }   
            // if ace, jack, queen, king, show symbol or letter in the middle
            if (Number > 10 || Number == 1)
            {
                FontSize = 60;
                FontHalfSize = (FontSize / 2)+5;
                FontToUse = new Font("Arial", FontSize);
                FontSize -= 10;
                if (Number == 1)
                {
                    Graphic.DrawString(CardSymbol, FontToUse, Brush, Xc - FontHalfSize, Yc - FontSize, DrawFormat);
                }
                else
                {
                    if(Number==12)
                        Graphic.DrawString(CardNumber, FontToUse, Brush, Xc - FontHalfSize-6, Yc - FontSize, DrawFormat);
                    else
                        Graphic.DrawString(CardNumber, FontToUse, Brush, Xc - FontHalfSize, Yc - FontSize, DrawFormat);
                }
            }
                //otherwise show symbols at the right place and with the right amount.
            else
            {
                FontSize = 24;
                BMP = new Bitmap(FontSize + SymbolPlus, FontSize + 5);
                FontHalfSize = (FontSize / 2);
                FontToUse = new Font("Arial", FontSize);
                FontSize -= 10;
                using (BMPGraphics = Graphics.FromImage(BMP))
                {
                    BMPGraphics.DrawString(CardSymbol, FontToUse, Brush, 0, 0, DrawFormat);
                }
                if (Number >= 2 && Number <= 3)
                {                 
                    Graphic.DrawImage(BMP, Xc - FontHalfSize, 0);//centre haut
                    BMP.RotateFlip(RotateFlipType.Rotate180FlipNone); //DOWN
                    Graphic.DrawImage(BMP, Xc - FontHalfSize, YMax - BMP.Height-2);//centre bas
                    BMP.RotateFlip(RotateFlipType.Rotate180FlipNone); //UP
                }
                if(Number == 3 || Number == 5 || Number==9)
                {                    
                    Graphic.DrawImage(BMP, Xc - FontHalfSize, Yc - 18);//centre centre
                }
                if (Number >= 4 && Number <= 10)
                {
                    Graphic.DrawImage(BMP, Xc - (FontHalfSize * 3), 0);//gauche haut
                    Graphic.DrawImage(BMP, Xc + (FontHalfSize), 0);//droit haut
                    BMP.RotateFlip(RotateFlipType.Rotate180FlipNone);//DOWN
                    Graphic.DrawImage(BMP, Xc - (FontHalfSize * 3), YMax - BMP.Height-2);//gauche bas
                    Graphic.DrawImage(BMP, Xc + (FontHalfSize), YMax - BMP.Height-2);//droit bas
                    BMP.RotateFlip(RotateFlipType.Rotate180FlipNone);//UP

                    if (Number >= 6 && Number <= 8)
                    {
                        Graphic.DrawImage(BMP, Xc - (FontHalfSize * 3), Yc - 18);//centre gauche
                        Graphic.DrawImage(BMP, Xc + FontHalfSize, Yc - 18);//centre droit
                        if (Number == 7 || Number == 8)
                        {
                            Graphic.DrawImage(BMP, Xc - FontHalfSize, Yc - 46);//centre demi-haut
                            if (Number == 8)
                            {
                                BMP.RotateFlip(RotateFlipType.Rotate180FlipNone);//DOWN
                                Graphic.DrawImage(BMP, Xc - FontHalfSize, Yc + 24);//centre demi-haut
                            }
                        }
                    }
                    if (Number >= 9 && Number <=10)
                    {
                        Graphic.DrawImage(BMP, Xc - (FontHalfSize * 3), Yc - 36);//demi-centre gauche
                        Graphic.DrawImage(BMP, Xc + FontHalfSize, Yc - 36);//demi-centre droit
                        BMP.RotateFlip(RotateFlipType.Rotate180FlipNone);//DOWN
                        Graphic.DrawImage(BMP, Xc - (FontHalfSize * 3), Yc + 14);//demi-centre gauche
                        Graphic.DrawImage(BMP, Xc + FontHalfSize, Yc + 14);//demi-centre droit                        
                        if(Number==10)
                        {
                            Graphic.DrawImage(BMP, Xc - FontHalfSize, Yc + 32);//centre demi-haut
                            BMP.RotateFlip(RotateFlipType.Rotate180FlipNone);//UP
                            Graphic.DrawImage(BMP, Xc - FontHalfSize, Yc - 54);//centre demi-haut
                        }
                    }
                }
            }
            //end symbol
            
            FontToUse.Dispose();
            Brush.Dispose();
            Graphic.Dispose();
        }
        public void Random()
        {
            Random Rnd = new Random();
            int Valeur = Rnd.Next(1800);
            int S = Valeur %4;
            int N = Valeur %13;
            int i = 0;
            while (m_tDeck[S, N] == true)
            {
                Valeur = Rnd.Next(1800);
                S = Valeur % 4;
                N = Valeur % 13;
                i++;
            }
            m_MyCard = new SCard(S, N);
        }

        public bool this[int S, int N ]
        {
            get
            {
                return m_tDeck[S,N];//retourne vrai si retiré, faux si dans le deck
            }
            set
            {
                if ((N > 0 && N < 14) && (S > 0 && S < 5))
                {
                    m_tDeck[S,N] = value;//set la valeur dans le deck à true ou false
                }
            }
        }

        public SCard this[SCard Card]
        {
            get
            {
                return m_MyCard;
            }
        }

        public string UpdateScoreByComparingMyCardTo(SCard OpponentCard)
        {
            if(m_tDeck[m_OpponentCard.Symbol,m_OpponentCard.Number]==false)
            {
                m_tDeck[m_OpponentCard.Symbol, m_OpponentCard.Number] = true;
                //Remove(OpponentCard); //meme chose mais compte les cartes a 52, pas a 26
            }
            if(m_MyCard.Number>m_OpponentCard.Number||m_MyCard.Number==m_BattleCardToBeat)
            {
                return "Win";
            }
            else
                if(m_MyCard.Number<m_OpponentCard.Number||m_OpponentCard.Number==m_BattleCardToBeat)
                {

                    return "Lose";
                }
                else
                {
                    m_BattleCardToBeat = m_MyCard.Number;
                    return "Hold";
                }
        }
    }
}
