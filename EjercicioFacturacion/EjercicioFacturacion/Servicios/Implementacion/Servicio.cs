using EjercicioFacturacion.Datos.Implementacion;
using EjercicioFacturacion.Entidades;
using EjercicioFacturacion.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioFacturacion.Servicios.Implementacion
{
    public class Servicio: IServicio
    {
        private FacturaDao facturaDao;
        public Servicio() 
        {
            facturaDao = new FacturaDao();
        }

        public bool GuardarFactura(Factura facturanueva)
        {
            return facturaDao.SaveFactura(facturanueva);
        }

        public List<Articulo> ObtenerArticulos()
        {
            return facturaDao.GetArticulos();
        }

        public DataTable ObtenerFormas()
        {
            return facturaDao.GetFormasPago();
        }
    }
}
