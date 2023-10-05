using modeloParcial.Datos.Interfaz;
using modeloParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modeloParcial.Datos.Implementaciones
{
    public class OrdenDAO : IOrdenDAO
    {
        public bool Crear(Orden oOrden)
        {
            bool resultado = true;

            SqlConnection conexion = DBHelper.ObtenerInstancia().ObtenerConexion();

            SqlTransaction t = null;

            try
            {

                conexion.Open();

                t = conexion.BeginTransaction();

                SqlCommand comando = new SqlCommand();

                comando.Connection = conexion;
                comando.Transaction = t;

                comando.CommandType = CommandType.StoredProcedure;

                comando.CommandText = "SP_INSERTAR_ORDEN";

                comando.Parameters.AddWithValue("@responsable", oOrden.Responsable);

                SqlParameter parametro = new SqlParameter();

                parametro.ParameterName = "@nro";

                parametro.SqlDbType = SqlDbType.Int;

                parametro.Direction = ParameterDirection.Output;

                comando.Parameters.Add(parametro);

                comando.ExecuteNonQuery();

                int ordenNro = (int)parametro.Value;

                int detalleNro = 1;

                SqlCommand cmdDetalle;

                foreach (DetalleOrden dp in oOrden.Detalles)
                {
                    //LE PASO EL SP Y LA CONEXION POR PARAMETROS y tambien le paso que sea por transaccion
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLES", conexion, t);

                    cmdDetalle.CommandType = CommandType.StoredProcedure;

                    cmdDetalle.Parameters.AddWithValue("@nro_orden", ordenNro);
                    cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@codigo", dp.Material.CodMaterial);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", dp.Cantidad);

                    cmdDetalle.ExecuteNonQuery();

                    detalleNro++;
                }

                t.Commit();

            }
            catch
            {
                t.Rollback();
                resultado = false;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return resultado;
        }

        public List<Material> TraerMateriales()
        {
            List<Material> lMateriales = new List<Material>();

            DataTable tabla = DBHelper.ObtenerInstancia().ConsultarTabla("SP_CONSULTAR_MATERIALES");

            foreach (DataRow fila in tabla.Rows)
            {
                int nro = int.Parse(fila[0].ToString());
                string nom = fila[1].ToString();
                int stock = Convert.ToInt32(fila[2].ToString());

                Material p = new Material(nro, nom, stock);

                lMateriales.Add(p);
            }

            return lMateriales;
        }
    }
}
