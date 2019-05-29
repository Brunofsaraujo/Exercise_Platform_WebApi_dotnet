using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Platform_WebApi.Models;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Platform_WebApi.Controllers
{
    [RoutePrefix("api/PedVendaCabecalho")]
    public class PedVendaCabecalhoController : ApiController
    {
        private string ConnectionString = "server=localhost;user=root;database=platform_database;port=3306;password=admin123";

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<PedVendaCabecalho> lstPedVendaCabecalho = new List<PedVendaCabecalho>();

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "select idPedCabecalho, idCliente, dtPedido, dtEntrega, desconto, valTotal from pedvenda_cabecalho";

                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            PedVendaCabecalho pedVendaCabecalho = new PedVendaCabecalho()
                            {
                                idPedCabecalho = reader["idPedCabecalho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idPedCabecalho"]),
                                idCliente = reader["idCliente"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idCliente"]),
                                dtPedido = reader["dtPedido"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["dtPedido"]),
                                dtEntrega = reader["dtEntrega"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["dtEntrega"]),
                                desconto = reader["desconto"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["desconto"]),
                                valTotal = reader["valTotal"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["valTotal"])
                            };

                            lstPedVendaCabecalho.Add(pedVendaCabecalho);
                        }
                    }

                    conn.Close();
                }

                return Request.CreateResponse(HttpStatusCode.OK, lstPedVendaCabecalho.ToArray());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                PedVendaCabecalho pedVendaCabecalho = null;

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "select idPedCabecalho, idCliente, dtPedido, dtEntrega, desconto, valTotal from pedvenda_cabecalho where idPedCabecalho = @id";
                        command.Parameters.AddWithValue("id", id);

                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            pedVendaCabecalho = new PedVendaCabecalho()
                            {
                                idPedCabecalho = reader["idPedCabecalho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idPedCabecalho"]),
                                idCliente = reader["idCliente"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idCliente"]),
                                dtPedido = reader["dtPedido"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["dtPedido"]),
                                dtEntrega = reader["dtEntrega"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["dtEntrega"]),
                                desconto = reader["desconto"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["desconto"]),
                                valTotal = reader["valTotal"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["valTotal"])
                            };
                        }
                    }

                    conn.Close();
                }

                return Request.CreateResponse(HttpStatusCode.OK, pedVendaCabecalho);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public HttpResponseMessage DeleteById(int id)
        {
            try
            {
                bool result = false;

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "delete from pedvenda_cabecalho where idPedCabecalho = @id";
                        command.Parameters.AddWithValue("id", id);

                        int i = command.ExecuteNonQuery();
                        result = i > 0;
                    }

                    conn.Close();
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("cadastro")]
        public HttpResponseMessage Post(PedVendaCabecalho pedVendaCabecalho)
        {
            try
            {
                bool result = false;

                if (pedVendaCabecalho == null) throw new ArgumentNullException("pedVendaCabecalho");

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "insert into pedvenda_cabecalho(idCliente, dtPedido, dtEntrega, desconto, valTotal) values(@idCliente, @dtPedido, @dtEntrega, @desconto, @valTotal)";

                        command.Parameters.AddWithValue("idCliente", pedVendaCabecalho.idCliente);
                        command.Parameters.AddWithValue("dtPedido", pedVendaCabecalho.dtPedido);
                        command.Parameters.AddWithValue("dtEntrega", pedVendaCabecalho.dtEntrega);
                        command.Parameters.AddWithValue("desconto", pedVendaCabecalho.desconto);
                        command.Parameters.AddWithValue("valTotal", pedVendaCabecalho.valTotal);

                        int i = command.ExecuteNonQuery();
                        result = i > 0;
                    }

                    conn.Close();
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
