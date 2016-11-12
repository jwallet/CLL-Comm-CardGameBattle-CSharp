using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; //servant à la méthode GetDescription sur CProduit

namespace Bataille
{
    enum TCardNumber
    {
        [Description("A")]
        Ace = 0,
        [Description("2")]
        Two,
        [Description("3")]
        Three,
        [Description("4")]
        Four,
        [Description("5")]
        Five,
        [Description("6")]
        Six,
        [Description("7")]
        Seven,
        [Description("8")]
        Eight,
        [Description("9")]
        Nine,
        [Description("10")]
        Ten,
        [Description("J")]
        JackValet,
        [Description("Q")]
        QueenDame,
        [Description("K")]
        KingRoi
    }
}
