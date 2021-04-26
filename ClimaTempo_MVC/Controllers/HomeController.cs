using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClimaTempo_MVC.Models;
using Newtonsoft.Json;

namespace ClimaTempo_MVC.Controllers
{
    public class HomeController : Controller
    {
        private ClimaTempoSimplesDBEntities db = new ClimaTempoSimplesDBEntities();

        public ActionResult Index(int? id)
        {
            ResultViewModel Obj = new ResultViewModel();

            ViewBag.SelectedId = id;
            if (id != null)
                ViewBag.Descricao = getDescricao(id);
            else
                ViewBag.Descricao = "";

            Obj.PrevisaoMax = db.PrevisaoClima.Include(c => c.Cidade)
                .Where(x => x.DataPrevisao == DateTime.Today)
                .OrderByDescending(p => p.TemperaturaMaxima)
                .ThenBy(p => p.CidadeId)
                .ThenBy(p => p.Cidade.Estado.UF)
                .Take(3)
                .ToList();

            Obj.PrevisaoMin = db.PrevisaoClima.Include(c => c.Cidade)
                .Where(x => x.DataPrevisao == DateTime.Today)
                .OrderBy(p => p.TemperaturaMinima)
                .ThenBy(p => p.CidadeId)
                .ThenBy(p => p.Cidade.Estado.UF)
                .Take(3)
                .ToList();

            //Obj.PrevisaoProximosDias = new PrevisaoClima();
            Obj.PrevisaoProximosDias = db.PrevisaoClima.Include(c => c.Cidade)
                .Where(x => x.DataPrevisao >= DateTime.Today && x.CidadeId == id)
                .OrderBy(p => p.DataPrevisao)
                .Take(6)
                .ToList();

            return View(Obj);
        }

        //private SelectListItem[] getCities()
        //{
        //    return Cidade.Select(x => new {
        //        Value = x.Id,
        //        Text = x.Nome
        //    }).OrderBy(x => x.CityName).ToList();
        //}
        public ActionResult getCities()
        {
            //DatabaseEntities db = new DatabaseEntities();
            return Json(db.Cidade.Select(x => new {
                CityID = x.Id,
                CityName = x.Nome
            }).OrderBy(x => x.CityName).ToList(), JsonRequestBehavior.AllowGet);
        }

        private string getDescricao(int? id)
        {
            if (id == null)
            {
                return "";
            }
            var city = db.Cidade.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return "";
            }
            return "Clima hoje e para os próximos dias na cidade de " + city.Nome;
        }
    }
}