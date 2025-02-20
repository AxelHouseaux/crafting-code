using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tax.Simulator
{
    public enum SituationFamiliale
    {
        INCONNUE = 0,

        [EnumMember(Value = "Marié/Pacsé")]
        MARIE_PACSE,

        [EnumMember(Value = "Célibataire")]
        CELIBATAIRE,
    }

    public static class SituationFamilialeUtils
    {
        public static SituationFamiliale GetFromString(string name)
        {
            SituationFamiliale result;

            switch (name)
            {
                case "Marié/Pacsé":
                    result = SituationFamiliale.MARIE_PACSE;
                    break;
                case "Célibataire":
                    result = SituationFamiliale.CELIBATAIRE;
                    break;
                default:
                    result = SituationFamiliale.INCONNUE;
                    break;
            }

            return result;
        }
    }
}
