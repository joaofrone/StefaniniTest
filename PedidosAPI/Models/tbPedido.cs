namespace PedidosAPI.Models
{
    public class tbPedido
    {
        public int Id { get; set; }

        public string NomeCliente { get; set; }

        public string EmailCliente { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool Pago { get; set; }

    }
}
