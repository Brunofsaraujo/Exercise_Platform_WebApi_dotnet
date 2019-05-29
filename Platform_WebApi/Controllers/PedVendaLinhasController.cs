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
    [RoutePrefix("api/PedVendaLinhas")]
    public class PedVendaLinhasController : ApiController
    {
        private string ConnectionString = "server=localhost;user=root;database=platform_database;port=3306;password=admin123";

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<PedVendaLinhas> lstPedVendaLinhas = new List<PedVendaLinhas>();

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "select idPedLinhas, idPedCabecalho, idItem, valUnit, pedQtde from pedvenda_linhas";

                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            PedVendaLinhas pedVendaLinhas = new PedVendaLinhas()
                            {
                                idPedLinhas = reader["idPedLinhas"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idPedLinhas"]),
                                idPedCabecalho = reader["idPedCabecalho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idPedCabecalho"]),
                                idItem = reader["idItem"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idItem"]),
                                valUnit = reader["valUnit"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["valUnit"]),
                                pedQtde = reader["pedQtde"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pedQtde"])
                            };

                            lstPedVendaLinhas.Add(pedVendaLinhas);
                        }
                    }

                    conn.Close();
                }

                return Request.CreateResponse(HttpStatusCode.OK, lstPedVendaLinhas.ToArray());
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
                PedVendaLinhas pedVendaLinhas = null;

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "select idPedLinhas, idPedCabecalho, idItem, valUnit, pedQtde from pedvenda_linhas where idPedLinhas = @id";
                        command.Parameters.AddWithValue("id", id);

                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            pedVendaLinhas = new PedVendaLinhas()
                            {
                                idPedLinhas = reader["idPedLinhas"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idPedLinhas"]),
                                idPedCabecalho = reader["idPedCabecalho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idPedCabecalho"]),
                                idItem = reader["idItem"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idItem"]),
                                valUnit = reader["valUnit"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["valUnit"]),
                                pedQtde = reader["pedQtde"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pedQtde"])
                            };
                        }
                    }

                    conn.Close();
                }

                return Request.CreateResponse(HttpStatusCode.OK, pedVendaLinhas);
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
                        command.CommandText = "delete from pedvenda_linhas where idPedLinhas = @id";
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
        public HttpResponseMessage Post(PedVendaLinhas pedVendaLinhas)
        {
            try
            {
                bool result = false;

                if (pedVendaLinhas == null) throw new ArgumentNullException("pedvenda_linhas");

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "insert into pedvenda_linhas(idPedLinhas, idPedCabecalho, idItem, valUnit, pedQtde) values(@idPedLinhas, @idPedCabecalho, @idItem, @valUnit, @pedQtde)";

                        command.Parameters.AddWithValue("idPedLinhas", pedVendaLinhas.idPedLinhas);
                        command.Parameters.AddWithValue("idPedCabecalho", pedVendaLinhas.idPedCabecalho);
                        command.Parameters.AddWithValue("idItem", pedVendaLinhas.idItem);
                        command.Parameters.AddWithValue("valUnit", pedVendaLinhas.valUnit);
                        command.Parameters.AddWithValue("pedQtde", pedVendaLinhas.pedQtde);

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
