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
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        private string ConnectionString = "server=localhost;user=root;database=platform_database;port=3306;password=admin123";

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<Cliente> lstClientes = new List<Cliente>();

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "select idCliente, nomeCliente, senhaCliente, emailCliente, ufCliente from clientes";

                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente()
                            {
                                idCliente = reader["idCliente"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idCliente"]),
                                nomeCliente = reader["nomeCliente"] == DBNull.Value ? string.Empty : reader["nomeCliente"].ToString(),
                                senhaCliente = reader["senhaCliente"] == DBNull.Value ? string.Empty : reader["senhaCliente"].ToString(),
                                emailCliente = reader["emailCliente"] == DBNull.Value ? string.Empty : reader["emailCliente"].ToString(),
                                ufCliente = reader["ufCliente"] == DBNull.Value ? string.Empty : reader["ufCliente"].ToString()
                            };

                            lstClientes.Add(cliente);
                        }
                    }

                    conn.Close();
                }

                return Request.CreateResponse(HttpStatusCode.OK, lstClientes.ToArray());
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
                Cliente cliente = null;

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "select idCliente, nomeCliente, senhaCliente, emailCliente, ufCliente from clientes where idCliente = @id";
                        command.Parameters.AddWithValue("id", id);

                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            cliente = new Cliente()
                            {
                                idCliente = reader["idCliente"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idCliente"]),
                                nomeCliente = reader["nomeCliente"] == DBNull.Value ? string.Empty : reader["nomeCliente"].ToString(),
                                senhaCliente = reader["senhaCliente"] == DBNull.Value ? string.Empty : reader["senhaCliente"].ToString(),
                                emailCliente = reader["emailCliente"] == DBNull.Value ? string.Empty : reader["emailCliente"].ToString(),
                                ufCliente = reader["ufCliente"] == DBNull.Value ? string.Empty : reader["ufCliente"].ToString()
                            };
                        }
                    }

                    conn.Close();
                }

                return Request.CreateResponse(HttpStatusCode.OK, cliente);
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
                        command.CommandText = "delete from clientes where idCliente = @id";
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
        public HttpResponseMessage Post(Cliente cliente)
        {
            try
            {                
                bool result = false;

                if (cliente == null) throw new ArgumentNullException("clientes");

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        
                        command.CommandText = "insert into clientes(nomeCliente, senhaCliente, emailCliente, ufCliente) values(@nomeCliente, @senhaCliente, @emailCliente, @ufCliente)";
                            
                        command.Parameters.AddWithValue("nomeCliente", cliente.nomeCliente);
                        command.Parameters.AddWithValue("senhaCliente", cliente.senhaCliente);
                        command.Parameters.AddWithValue("emailCliente", cliente.emailCliente);
                        command.Parameters.AddWithValue("ufCliente", cliente.ufCliente);

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
