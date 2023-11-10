using Microsoft.AspNetCore.Mvc;
using PedidosWeb.Interface;
using PedidosWeb.Models;
using PedidosWeb.Services;
using System.Diagnostics;

namespace PedidosWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly iPedidosApi _pedidoAPI;

        public HomeController(ILogger<HomeController> logger, iPedidosApi pedidoAPI)
        {
            _logger = logger;
            _pedidoAPI = pedidoAPI;
        }

        public async Task<IActionResult> Index()
        {
            List<objPedido> lstPedidos = new List<objPedido>();

            lstPedidos = await _pedidoAPI.GetAll();

            return View(lstPedidos);
        }

        public async Task<IActionResult> Deletar(int id)
        {
            await _pedidoAPI.Deletar(id);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}