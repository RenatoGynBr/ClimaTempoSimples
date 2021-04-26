using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClimaTempo_MVC.Models
{
    public class ResultViewModel
    {
        public List<PrevisaoClima> PrevisaoMax { get; set; }
        public List<PrevisaoClima> PrevisaoMin { get; set; }
        public List<PrevisaoClima> PrevisaoProximosDias { get; set; }

        //public SelectListItem[] ListaCidades { get; set; }
        //public string SelectedCity { get; set; }
        //public string SelectedCityText { get; set; }

        //public int? SelectedId { set; get; }
    }
}