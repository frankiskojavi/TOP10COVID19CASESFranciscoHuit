using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOP10COVID19CASESFranciscoHuit.Models
{
    public class Region
    {
        public string iso { get; set; }
        public string name { get; set; }
    }

    public class Root
    {
        public List<Region> data { get; set; }
    }

}