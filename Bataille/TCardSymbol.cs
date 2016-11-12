using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; //servant à la méthode GetDescription sur CProduit

namespace Bataille
{
    enum TCardSymbol
    {
        [Description("♥")]
        HeartCoeur = 0,
        [Description("♦")]
        DiamondCarreau,
        [Description("♣")]
        ClubTrefle,
        [Description("♠")]
        SpadePique
    }
}
