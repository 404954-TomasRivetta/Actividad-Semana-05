using modeloParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modeloParcial.Datos.Interfaz
{
    public interface IOrdenDAO
    {
        List<Material> TraerMateriales();

        bool Crear(Orden oOrden);
    }
}
