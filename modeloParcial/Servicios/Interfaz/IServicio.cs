using modeloParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modeloParcial.Servicios.Interfaz
{
    public interface IServicio
    {
        List<Material> TraerMateriales();

        bool GrabarOrden(Orden oOrden);
    }
}
