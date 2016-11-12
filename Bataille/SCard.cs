using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; //servant à la méthode GetDescription
using System.Reflection; //servant à la méthode GetDescription

namespace Bataille
{
    struct SCard
    {
        private int m_Symbol;
        private int m_Number;

        public int Number
        {
            get { return m_Number; }
        }
        public int Symbol
        {
            get { return m_Symbol; }
        }

        public SCard(int s, int n)
        {
            this.m_Symbol = s;
            this.m_Number = n;
        }
        //format string to SN such as ♥7 so index 0 will always be S, rest of it will be N
        public override string ToString() 
        {
            string Str,S,N;
            S = GetEnumDescription((TCardSymbol)m_Symbol);
            N = GetEnumDescription((TCardNumber)m_Number);
            Str = string.Format(S+N);
            return Str;
        }

        private static string GetEnumDescription(Enum enumValeur)
        {
            FieldInfo fi = enumValeur.GetType().GetField(enumValeur.ToString());
            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return null;
        }
    }
}
