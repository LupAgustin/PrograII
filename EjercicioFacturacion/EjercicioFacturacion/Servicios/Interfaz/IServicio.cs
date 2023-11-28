using EjercicioFacturacion.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioFacturacion.Servicios.Interfaz
{
    public interface IServicio
    {
        bool GuardarFactura(Factura facturanueva);
        List<Articulo> ObtenerArticulos();
        DataTable ObtenerFormas();
    }
}
