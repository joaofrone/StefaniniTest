namespace PedidosAPI.Models
{
    public class tbItensPedido
    {
        public int Id { get; set; }

        public int IdPedido { get; set; }

        public int IdProduto { get; set; }

        public int Quantidade { get; set; }
    }
}
