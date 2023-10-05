using modeloParcial.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modeloParcial.Servicios
{
    public abstract class FabricaServicio
    {
        public abstract IServicio CrearServicio();

    }
}
