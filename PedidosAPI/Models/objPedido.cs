namespace PedidosAPI.Models
{
    public class objPedido
    {
        public int Id { get; set; }

        public string nomeCliente { get; set; }

        public string emailCliente { get; set; }

        public bool pago { get; set; }

        public decimal valorTotal { get; set; }

        public List<objItensPedido> itensPedido { get; set; }

    }

}
