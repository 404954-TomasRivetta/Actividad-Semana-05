using modeloParcial.Datos.Implementaciones;
using modeloParcial.Datos.Interfaz;
using modeloParcial.Entidades;
using modeloParcial.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modeloParcial.Servicios.Implementacion
{
    public class Servicio : IServicio
    {
        private IOrdenDAO dao;

        public Servicio()
        {
            dao = new OrdenDAO();
        }

        public List<Material> TraerMateriales()
        {
            return dao.TraerMateriales();
        }

        public bool GrabarOrden(Orden oOrden) {

            return dao.Crear(oOrden);
        }


    }
}
