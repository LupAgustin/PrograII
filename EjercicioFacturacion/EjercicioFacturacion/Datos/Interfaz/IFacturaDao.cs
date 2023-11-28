using EjercicioFacturacion.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioFacturacion.Datos.Interfaz
{
    public interface IFacturaDao
    {
        List<Articulo> GetArticulos();
        DataTable GetFormasPago();
        bool SaveFactura(Factura factura);

    }
}
