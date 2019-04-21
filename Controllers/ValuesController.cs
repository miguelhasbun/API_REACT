using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

using APIREACT.Models;

namespace APIREACT.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        string connectionString = "SERVER=localhost;DATABASE=react;UID=root;PASSWORD=1234;";

        public IEnumerable<Lado_Fuerza> Get()
        {
            //READ
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlDataReader reader;
            MySqlCommand cmd = new MySqlCommand("select * from lado_fuerza", conn);
            reader = cmd.ExecuteReader();
            List<Lado_Fuerza> jedi = new List<Lado_Fuerza>();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string nombre = reader.GetString(1);
                string sable = reader.GetString(2);
                jedi.Add(new Lado_Fuerza { id = id, nombre = nombre, sable = sable });
            }
            conn.Close();
            return jedi;
        }

        // GET api/values/5
        public Lado_Fuerza Get(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand("select * from lado_fuerza where id=@0", conn);
                cmd.Parameters.AddWithValue("0", id);
                reader = cmd.ExecuteReader();
                reader.Read();
                int idx = reader.GetInt32(0);
                string nombre = reader.GetString(1);
                string sable = reader.GetString(2);
                conn.Close();
                return new Lado_Fuerza {id=idx, nombre= nombre, sable= sable};
            }
            catch(MySqlException ex)
            {
                ex.ToString();
                conn.Close();
            }
            conn.Close();
            return null;
        }

        // POST api/values
        
        public void Post(Lado_Fuerza f)
        {
            //create
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = "insert into lado_fuerza values(@id,@nombre,@sable)";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", f.id);
                cmd.Parameters.AddWithValue("@nombre", f.nombre);
                cmd.Parameters.AddWithValue("@sable", f.sable);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(MySqlException e)
            {
                e.ToString();
                conn.Close();
            }
        }

        // PUT api/values/5
        public void Put(Lado_Fuerza f)
        {
            //Update
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = "update lado_fuerza " +
                    "set nombre=@nombre, " + 
                    "lado_fuerza.sable=@sable " + 
                    "where id=@id;";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@nombre", f.nombre);
                cmd.Parameters.AddWithValue("@sable", f.sable);
                cmd.Parameters.AddWithValue("@id", f.id);
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch(MySqlException e)
            {
                e.ToString();
                conn.Close();
            }

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            //Delete
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = "delete from lado_fuerza where id = @id";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (MySqlException e)
            {
                e.ToString();
                conn.Close();
            }
        }
    }
}
