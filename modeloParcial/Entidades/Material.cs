using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modeloParcial.Entidades
{
    public class Material
    {
        public int CodMaterial { get; set; }

        public string NomMaterial { get; set; }

        public int Stock { get; set; }

        public Material(int codMaterial,string nomMaterial,int stock)
        {
            this.CodMaterial = codMaterial;
            this.NomMaterial = nomMaterial;
            this.Stock = stock;
        }

        public Material()
        {
            this.CodMaterial = 0;
            this.NomMaterial = string.Empty; 
            this.Stock = 0;
        }

        public override string ToString()
        {
            return NomMaterial;
        }
    }
}
