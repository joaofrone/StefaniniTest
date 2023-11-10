using Microsoft.AspNetCore.Mvc;
using PedidosWeb.Interface;
using PedidosWeb.Models;

namespace PedidosWeb.Controllers
{
    public class PedidoController : Controller
    {
        private readonly iPedidosApi _pedidoAPI;

        public PedidoController(iPedidosApi pedidoAPI)
        {
            _pedidoAPI = pedidoAPI;
        }

        public async Task<IActionResult> Index(int id)
        {
            objPedido oPedido = await _pedidoAPI.GetId(id);

            return View("Pedido", oPedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id, nomeCliente,emailCliente,pago")] objPedido oItem)
        {
            await _pedidoAPI.Update(oItem);

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Novo()
        {
            objPedido oPedido = new objPedido();
            oPedido.id = 0;
            oPedido.itensPedido = new List<objItensPedido>();

            return View("Pedido", oPedido);
        }
    }
}
