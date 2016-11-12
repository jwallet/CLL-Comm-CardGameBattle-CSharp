using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    class CScore
    {
        private int m_Local;//my score
        private int m_Opponent;//opponent score
        private int m_HoldPoints;//points to hold, waiting to be gained
        private int m_Plus2;

        public int Plus2
        {
            get { return m_Plus2; }
        }
        public int Local
        {
            get { return m_Local; }
        }

        public int Opponent
        {
            get { return m_Opponent; }
        }
        public int HoldPoints
        {
            get { return m_HoldPoints; }
        }

        public CScore()
        {
            m_Local = 0;
            m_Opponent = 0;
            m_HoldPoints = 0;
            m_Plus2 = 2;
        }

        public void Win()
        {
            m_Local += m_HoldPoints + m_Plus2;
            m_HoldPoints = 0;
        }

        public void Lose()
        {
            m_Opponent += m_HoldPoints + m_Plus2;
            m_HoldPoints = 0;
        }

        public void Hold()
        {
            m_HoldPoints += m_Plus2;
        }
    }
}
