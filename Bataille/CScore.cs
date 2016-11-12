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

        public int Local
        {
            get { return Local; }
        }

        public int Opponent
        {
            get { return Opponent; }
        }

        public CScore()
        {
            m_Local = 0;
            m_Opponent = 0;
        }

        public void Win()
        {
            m_Local += m_HoldPoints + 2;
        }

        public void Lose()
        {
            m_Opponent += m_HoldPoints + 2;
        }

        public void Hold()
        {
            m_HoldPoints += 2;
        }
    }
}
