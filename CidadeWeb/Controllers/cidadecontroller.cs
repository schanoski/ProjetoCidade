using CidadeWeb.DAO;
using CidadeWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;


namespace CidadeWeb.Controllers
{
    [HandleError]
    public class CidadeController : Controller
    {
        // GET: Cidade
        public ActionResult Index()
        {
            if (Session["usuario"] != null)
            {
                using (CidadeDAO model = new CidadeDAO())
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
            if (Session["usuario"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Cidade cidade = new Cidade();
            cidade.Codigo = int.Parse(form["Codigo"]);
            cidade.Nome = form["Nome"];
            cidade.Uf = form["Uf"];

            using (CidadeDAO model = new CidadeDAO())
            {
                model.Create(cidade);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Update(int id)
        {
            if (Session["usuario"] != null)
            {
                using (CidadeDAO model = new CidadeDAO())
                {
                    return View(model.Read(id));
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        [HttpPost]
        public ActionResult Update(int id, Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                using (CidadeDAO model = new CidadeDAO())
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
            if (Session["usuario"] != null)
            {
                using (CidadeDAO model = new CidadeDAO())
                {
                    model.Delete(id);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login");
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
            
            using (UsuarioDAO model = new UsuarioDAO())
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