using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOP10COVID19CASESFranciscoHuit.Models
{
    public class CasesDeathsRegion
    {
        public string region { get; set; }
        public int cases { get; set; }
        public int deaths { get; set; }
    }

    public class CasesDeathsProvince
    {
        public string province { get; set; }
        public int cases { get; set; }
        public int deaths { get; set; }
    }
}