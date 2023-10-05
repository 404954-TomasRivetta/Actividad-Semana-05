using modeloParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modeloParcial.Datos
{
    public class DBHelper
    {
        private SqlConnection conexion;

        private static DBHelper instancia;

        private DBHelper()
        {
            conexion = new SqlConnection(@"Data Source=PCCESAR;Initial Catalog=db_ordenes;Integrated Security=True");
        }

        public static DBHelper ObtenerInstancia() {

            if (instancia == null)
            {
                instancia = new DBHelper();
            }

            return instancia;
        }

        public SqlConnection ObtenerConexion() {
            return conexion;
        }

        public DataTable ConsultarTabla(string nombreSP)
        {

            conexion.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            //le paso el nombre del sp a ejecutar
            comando.CommandText = nombreSP;

            DataTable dt = new DataTable();

            dt.Load(comando.ExecuteReader());

            conexion.Close();

            return dt;

        }
    }
}
