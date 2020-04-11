using CidadeWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;


namespace CidadeWeb.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        public ActionResult Index()
        {

            if (Session["usuario"] != null)
            {
                using (CidadeModel model = new CidadeModel())
                {
                    List<Cidade> lista = model.Read();
                    return View(lista);
                }
            }
            else
            {
                return RedirectToAction("Login");
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
            cidade.Codigo = int.Parse(form["Codigo"]);
            cidade.Nome = form["Nome"];
            cidade.Uf = form["Uf"];

            using (CidadeModel model = new CidadeModel())
            {
                model.Create(cidade);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Update(int id)
        {
            using (CidadeModel model = new CidadeModel())
            {
                return View(model.Read(id));
            }
        }

        [HttpPost]
        public ActionResult Update(int id, Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                using (CidadeModel model = new CidadeModel())
                {
                    model.Update(cidade);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(cidade);
            }
        }

        public ActionResult Delete(int id)
        {
            using(CidadeModel model = new CidadeModel())
            {
                model.Delete(id);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            Usuario u = new Usuario();
            u.Login = form["Login"];
            u.Senha = form["Senha"];          
            
            using (UsuarioModel model = new UsuarioModel())
            {
                Usuario objUsuario = model.Consulta(u);
                if (objUsuario != null)
                {
                    if (objUsuario.Senha != u.Senha)
                    {
                        ViewData["Message"] = "Senha Incorreta";
                    }
                    else
                    {
                        Session["usuario"] = objUsuario.Login.ToString();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewData["Message"] = "Usuario Incorreto";
                }
  
                return View();
            }
   
        }

    }
}