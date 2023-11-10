using Microsoft.Extensions.Configuration;
using PedidosWeb.Interface;
using PedidosWeb.Models;
using System.Data;
using System.Text;
using System.Text.Json;

namespace PedidosWeb.Services
{
    public class srvPedidosApi : iPedidosApi
    {
        public async Task<List<objPedido>> GetAll()
        {
            List<objPedido> oRetorno = new List<objPedido>();

            string url = "http://localhost:5101/api/Pedidos/";

            var httpClient = new HttpClient();

            try
            {
                HttpResponseMessage? response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string reponseBody = await response.Content.ReadAsStringAsync();

                oRetorno = JsonSerializer.Deserialize<List<objPedido>>(reponseBody);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oRetorno;
        }

        public async Task<objPedido> GetId(int id)
        {
            objPedido oRetorno = new objPedido();

            string url = "http://localhost:5101/api/Pedidos/" + id.ToString();

            var httpClient = new HttpClient();

            try
            {
                HttpResponseMessage? response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string reponseBody = await response.Content.ReadAsStringAsync();

                oRetorno = JsonSerializer.Deserialize<objPedido>(reponseBody);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oRetorno;
        }

        public async Task<bool> Deletar(int id)
        {
            bool retorno = false;

            string url = "http://localhost:5101/api/Pedidos/" + id.ToString();

            var httpClient = new HttpClient();

            try
            {
                HttpResponseMessage? response = await httpClient.DeleteAsync(url);

                response.EnsureSuccessStatusCode();

                string reponseBody = await response.Content.ReadAsStringAsync();

                retorno = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        public async Task<bool> Update(objPedido oPedido)
        {
            bool retorno = false;

            string url = "http://localhost:5101/api/Pedidos/";

            var httpClient = new HttpClient();

            try
            {
                objPedidoAPI oItemApi = new objPedidoAPI();

                oItemApi.Id = oPedido.id;

                oItemApi.NomeCliente = oPedido.nomeCliente;

                oItemApi.EmailCliente = oPedido.emailCliente;

                oItemApi.Pago = oPedido.pago;

                oItemApi.DataCriacao = DateTime.Now;

                var jSon = new StringContent(JsonSerializer.Serialize(oItemApi), Encoding.UTF8, "application/json");

                HttpResponseMessage? response;

                if (oPedido.id.Equals(0))
                {
                    //Insere
                    response = await httpClient.PostAsync(url, jSon);
                }
                else
                {
                    url += oPedido.id.ToString();

                    //Atualiza
                    response = await httpClient.PutAsync(url, jSon);
                }

                response.EnsureSuccessStatusCode();

                string reponseBody = await response.Content.ReadAsStringAsync();

                retorno = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

    }
}
