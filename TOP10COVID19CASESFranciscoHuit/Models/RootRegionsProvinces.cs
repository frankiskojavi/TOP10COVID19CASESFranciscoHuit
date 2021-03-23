using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOP10COVID19CASESFranciscoHuit.Models
{

    public class Provinces
    {
        public string iso { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
        public List<object> cities { get; set; }
    }

    public class RegionsProvinces
    {
        public string date { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
        public int confirmed_diff { get; set; }
        public int deaths_diff { get; set; }
        public int recovered_diff { get; set; }
        public string last_update { get; set; }
        public int active { get; set; }
        public int active_diff { get; set; }
        public double fatality_rate { get; set; }
        public Provinces region { get; set; }
    }


    public class RootRegionsProvinces
    {
        public List<RegionsProvinces> data { get; set; }
    }
}