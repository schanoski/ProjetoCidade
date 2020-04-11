using ProjetoCidade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoCidade.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade



        public ActionResult Index()
        {
            using (CidadeModel model = new CidadeModel())
            {
                List<Cidade> lista = model.Read();
                return View(lista);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Cidade cidade = new Cidade();
            
            // cidade.codigoMunicipio = Convert.ToInt32(form["codigoMunicipio"]);
             cidade.nomeMunicipio = form["nomeMunicipio"];
             cidade.ufMunicipio = form["ufMunicipio"];
             

            using (CidadeModel model = new CidadeModel())
            {
                model.Create(cidade);
                return RedirectToAction("Index");
            }
        }

    }
}