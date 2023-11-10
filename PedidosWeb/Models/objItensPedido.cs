namespace PedidosWeb.Models
{
    public class objItensPedido
    {
        public int Id { get; set; }
        public int idProduto { get; set; }
        public string nomeProduto { get; set; }
        public decimal valorUnitario { get; set; }
        public int quantidade { get; set; }
    }
}
