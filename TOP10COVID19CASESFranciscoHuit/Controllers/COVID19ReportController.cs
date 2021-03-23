using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using TOP10COVID19CASESFranciscoHuit.Models;

namespace TOP10COVID19CASESFranciscoHuit.Controllers
{
    public class COVID19ReportController
    {
        DataTable tabla;
        public DataTable getRegions()
        {
            var client = new HttpClient();
            tabla = new DataTable();
            tabla.Columns.Add("name", typeof(string));
            tabla.Columns.Add("iso", typeof(string));

            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/regions"),
                    Headers =
                {
                    { "x-rapidapi-key", "873c4c31b5msh1bdfdf32e1c4e3dp12bc74jsn69b536e91d7e" },
                    { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                },
                };
                var response = client.SendAsync(request).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                Root regiones = JsonConvert.DeserializeObject<Root>(json);
                List<Region> listaRegion = regiones.data;

                foreach (var dat in listaRegion)
                {
                    DataRow row = tabla.NewRow();
                    row["name"] = dat.name;
                    row["iso"] = dat.iso;
                    tabla.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to get the information or regions : " + ex.Message);
            }                        
            return tabla;
        }

        public DataTable getReportForRegions()
        {
            var client = new HttpClient();
            tabla = new DataTable();
            tabla.Columns.Add("REGION", typeof(string));
            tabla.Columns.Add("CASES", typeof(int));
            tabla.Columns.Add("DEATHS", typeof(int));

            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/reports"),
                    Headers =
                {
                    { "x-rapidapi-key", "873c4c31b5msh1bdfdf32e1c4e3dp12bc74jsn69b536e91d7e" },
                    { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                },
                };
                var response = client.SendAsync(request).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                RootRegionsProvinces principal = JsonConvert.DeserializeObject<RootRegionsProvinces>(json);
                List<RegionsProvinces> listaxTodos = principal.data;
                List<CasesDeathsRegion> listaxRegion = new List<CasesDeathsRegion>();
                foreach (var regiones in listaxTodos)
                {
                    CasesDeathsRegion repRegion = new CasesDeathsRegion();
                    repRegion.region = regiones.region.name;
                    repRegion.cases = regiones.confirmed;
                    repRegion.deaths = regiones.deaths;
                    listaxRegion.Add(repRegion);
                }

                var tablaOrdenada = listaxRegion
                            .GroupBy(a => a.region)
                            .Select(a => new { Region = a.Key, Cases = a.Sum(b => b.cases), Deaths = a.Sum(b => b.deaths) })
                            .OrderByDescending(a => a.Cases)
                            ;
                foreach (var dat in tablaOrdenada.Take(10))
                {
                    DataRow row = tabla.NewRow();
                    row["REGION"] = dat.Region;
                    row["CASES"] = dat.Cases;
                    row["DEATHS"] = dat.Deaths;
                    tabla.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to get the information in the Regions report: " + ex.Message);
            }            
            return tabla;
        }

        public DataTable getReportForProvinces(String region)
        {
            var client = new HttpClient();
            tabla = new DataTable();
            tabla.Columns.Add("PROVINCE", typeof(string));
            tabla.Columns.Add("CASES", typeof(int));
            tabla.Columns.Add("DEATHS", typeof(int));
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/reports?region_name=" + region.Replace(" ", "%20")),
                    Headers =
                {
                    { "x-rapidapi-key", "873c4c31b5msh1bdfdf32e1c4e3dp12bc74jsn69b536e91d7e" },
                    { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                },
                };
                var response = client.SendAsync(request).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                RootRegionsProvinces principal = JsonConvert.DeserializeObject<RootRegionsProvinces>(json);
                List<RegionsProvinces> listaxTodos = principal.data;
                List<CasesDeathsProvince> listaxRegion = new List<CasesDeathsProvince>();
                foreach (var regiones in listaxTodos)
                {
                    CasesDeathsProvince repRegion = new CasesDeathsProvince();
                    repRegion.province = regiones.region.province;
                    repRegion.cases = regiones.confirmed;
                    repRegion.deaths = regiones.deaths;
                    listaxRegion.Add(repRegion);
                }

                var tablaOrdenada = listaxRegion
                            .GroupBy(a => a.province)
                            .Select(a => new { Province = a.Key, Cases = a.Sum(b => b.cases), Deaths = a.Sum(b => b.deaths) })
                            .OrderByDescending(a => a.Cases)
                            ;

                foreach (var dat in tablaOrdenada.Take(10))
                {
                    DataRow row = tabla.NewRow();
                    if (dat.Province.Equals(""))
                    {
                        row["PROVINCE"] = region;
                    }
                    else
                    {
                        row["PROVINCE"] = dat.Province;
                    }
                    row["CASES"] = dat.Cases;
                    row["DEATHS"] = dat.Deaths;
                    tabla.Rows.Add(row);
                }
            }catch(Exception ex)
            {
                throw new Exception("Error to get the information in the provinces report: " + ex.Message);
            }
            return tabla;
        }
    }
}