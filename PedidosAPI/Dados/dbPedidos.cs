using Microsoft.EntityFrameworkCore;
using PedidosAPI.Models;

namespace PedidosAPI.Dados
{
    public class dbPedidos : DbContext
    {
        public dbPedidos(DbContextOptions<dbPedidos> options) : base(options)
        {
        }

        public DbSet<tbPedido> Pedido { get; set; }
        public DbSet<tbProduto> Produto { get; set; }
        public DbSet<tbItensPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbPedido>().ToTable("tbPedido");
            modelBuilder.Entity<tbProduto>().ToTable("tbProduto");
            modelBuilder.Entity<tbItensPedido>().ToTable("tbItensPedido");
        }
    }
}
