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
    [RoutePrefix("api/item")]
    public class ItemController : ApiController
    {
        private string ConnectionString = "server=localhost;user=root;database=platform_database;port=3306;password=admin123";

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<Item> lstItems = new List<Item>();

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "select idItem, descricao, qtdeEstoque, valorUnit from item";

                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Item item = new Item()
                            {
                                idItem = reader["idItem"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idItem"]),
                                descricao = reader["descricao"] == DBNull.Value ? string.Empty : reader["descricao"].ToString(),
                                qtdeEstoque = reader["qtdeEstoque"] == DBNull.Value ? 0 : Convert.ToInt32(reader["qtdeEstoque"]),
                                valorUnit = reader["valorUnit"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["valorUnit"])
                            };

                            lstItems.Add(item);
                        }
                    }

                    conn.Close();
                }

                return Request.CreateResponse(HttpStatusCode.OK, lstItems.ToArray());
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
                Item item = null;

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "select idItem, descricao, qtdeEstoque, valorUnit from item where idItem = @id";
                        command.Parameters.AddWithValue("id", id);

                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Item()
                            {
                                idItem = reader["idItem"] == DBNull.Value ? 0 : Convert.ToInt32(reader["idItem"]),
                                descricao = reader["descricao"] == DBNull.Value ? string.Empty : reader["descricao"].ToString(),
                                qtdeEstoque = reader["qtdeEstoque"] == DBNull.Value ? 0 : Convert.ToInt32(reader["qtdeEstoque"]),
                                valorUnit = reader["valorUnit"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["valorUnit"])
                            };
                        }
                    }

                    conn.Close();
                }

                return Request.CreateResponse(HttpStatusCode.OK, item);
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
                        command.CommandText = "delete from item where idItem = @id";
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
        public HttpResponseMessage Post(Item item)
        {
            try
            {
                bool result = false;

                if (item == null) throw new ArgumentNullException("item");

                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText = "insert into item(descricao, qtdeEstoque, valorUnit) values(@descricao, @qtdeEstoque, @valorUnit)";

                        command.Parameters.AddWithValue("descricao", item.descricao);
                        command.Parameters.AddWithValue("qtdeEstoque", item.qtdeEstoque);
                        command.Parameters.AddWithValue("valorUnit", item.valorUnit);

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
