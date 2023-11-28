using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioFacturacion.Entidades
{
    public class Factura
    {
        public int nroFactura {  get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FPago { get; set; }
        public string Cliente { get; set; }

        public List<DetalleFactura> detalleFacturas { get; set; }

        public Factura() 
        {
            detalleFacturas = new List<DetalleFactura>();
            FPago = new FormaPago();
        }
        public void AgregarDetalle(DetalleFactura d)
        {
            detalleFacturas.Add(d);
        }
        public void QuitarDetalle(int pos)
        {
            detalleFacturas.RemoveAt(pos);
        }
        public double CalcularTotal()
        {
            double total = 0;
            foreach (DetalleFactura d in detalleFacturas)
            {
                total += d.Total();
            }
            
            return total;
        }
        
    }
}
