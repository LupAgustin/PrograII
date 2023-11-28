using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioFacturacion.Entidades
{
    public class Articulo
    {
        public int NroArt {  get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public bool Activo { get; set; }

        public Articulo()
        {
            NroArt = 0;
            Nombre = string.Empty;
            Precio = 0;
            Activo = true;
        }

        public Articulo(int nro, string nom, double pre, bool act)
        {
            this.NroArt = nro;
            this.Nombre = nom;
            this.Precio = pre;
            this.Activo = act;
        }
    }
}
