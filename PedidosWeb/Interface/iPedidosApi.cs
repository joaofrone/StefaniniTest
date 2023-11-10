using PedidosWeb.Models;

namespace PedidosWeb.Interface
{
    public interface iPedidosApi
    {
        Task<List<objPedido>> GetAll();

        Task<objPedido> GetId(int id);

        Task<bool> Update(objPedido oPedido);

        Task<bool> Deletar(int id);
    }
}
