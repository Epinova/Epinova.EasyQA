using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epinova.EasyQA.Core.Entities;

namespace Epinova.EasyQA.Utilities
{
    public static class QaHelper
    {
        public static int GetScoreOutOfSix(QaInstance qa)
        {
            if (qa.GetScore() >= 90)
                return 6;

            double scoreOutOfSix = qa.GetScore() / 16.6;
            return (int)Math.Round(scoreOutOfSix, MidpointRounding.ToEven);
        }
    }
}