using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioFacturacion.Entidades
{
    public class DetalleFactura
    {
        public Articulo Art {  get; set; }
        public double Cantidad { get; set; }

        public DetalleFactura()
        {
            Art = null;
            Cantidad = 0;
        }
        public DetalleFactura(Articulo art, double cant) 
        {
            this.Art = art;
            this.Cantidad = cant;
        }
        public double Total()
        {
            double total = 0;
            total = Art.Precio * Cantidad;
            return total;
        }
    }
}
