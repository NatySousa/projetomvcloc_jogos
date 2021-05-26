using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC_LocJogos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()//método que abre a página index
        {
            return View();
        }
    }
}
