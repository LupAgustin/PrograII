using EjercicioFacturacion.Datos.Interfaz;
using EjercicioFacturacion.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioFacturacion.Datos.Implementacion
{
    public class FacturaDao : IFacturaDao
    {
        public List<Articulo> GetArticulos()
        {
            List<Articulo> articulos = new List<Articulo>();
            DataTable dt = DBHelperDao.getInstance().Consultar("SP_Consultar_Articulos");
            foreach(DataRow r in dt.Rows)
            {
                Articulo a = new Articulo();
                a.NroArt = Convert.ToInt32(r["id_articulo"]);
                a.Nombre = r["nom_articulo"].ToString();
                a.Precio = Convert.ToDouble(r["pre_unit"]);
                if (Convert.ToInt32(r["activo"]) == 1)
                    a.Activo = true;
                else
                    a.Activo= false;
                articulos.Add(a);
            }
            return articulos;
        }

        public DataTable GetFormasPago()
        {
            //List<FormaPago> formas = new List<FormaPago>();
            DataTable dt = DBHelperDao.getInstance().Consultar("SP_Consultar_FormasPago");
            //foreach (DataRow r in dt.Rows)
            //{
            //    FormaPago f = new FormaPago();
            //    f.NroForma = Convert.ToInt32(r["id_formapago"]);
            //    f.Nombre = r["nom_forma"].ToString();
            //    formas.Add(f);
            //}
            //return formas;
            return dt;
        }

        public bool SaveFactura(Factura factura)
        {
            bool resultado = true;
            SqlConnection cnn = DBHelperDao.getInstance().ObtenerConexion();
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_Insertar_Facturas", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@f_pago", factura.FPago.NroForma);
                cmd.Parameters.AddWithValue("@cliente", factura.Cliente);
                cmd.Parameters.AddWithValue("@total", factura.CalcularTotal());

                SqlParameter parameter = new SqlParameter("@nro_fac", SqlDbType.Int);
                parameter.Direction = ParameterDirection.Output;
                
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                int nro = (int)parameter.Value;
                int det = 1;
                SqlCommand cmd2;

                foreach (DetalleFactura d in factura.detalleFacturas)
                {
                    cmd2 = new SqlCommand("SP_Insertar_DetallesFacturas", cnn, t);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@factura", nro);
                    cmd2.Parameters.AddWithValue("@detalle", det);
                    cmd2.Parameters.AddWithValue("@articulo", d.Art.NroArt);
                    cmd2.Parameters.AddWithValue("@cantidad", d.Cantidad);
                    cmd2.ExecuteNonQuery();
                    det++;
                }
                t.Commit();


            }
            catch (Exception ex)
            {
                if (t == null)
                {
                    t.Rollback();
                }
                resultado = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }



            return resultado;
        }

        
    }
}
