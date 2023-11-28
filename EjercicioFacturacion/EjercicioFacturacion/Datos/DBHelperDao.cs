using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioFacturacion.Datos
{
    public class DBHelperDao
    {
        private SqlConnection cnn;
        private static DBHelperDao instance;

        private DBHelperDao() 
        {
            cnn = new SqlConnection(@"Data Source = AGUSTINLUPARIA\SQLEXPRESS; Initial Catalog = EjercicioFacturacion; Integrated Security = True");
        }
        public static DBHelperDao getInstance()
        {
            if (instance == null)
            {
                instance = new DBHelperDao();
            }
            return instance;
        }
        public SqlConnection ObtenerConexion()
        {
            return cnn;
        }

        public int ConsultarEscalar(string nombreSP, string nombreParamOut)
        {
            cnn.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = cnn;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombreParamOut;
            parametro.SqlDbType = SqlDbType.Int;
            parametro.Direction = ParameterDirection.Output;

            comando.Parameters.Add(parametro);
            comando.ExecuteNonQuery();

            cnn.Close();

            return (int)parametro.Value;
        }

        public DataTable Consultar(string nombreSP)
        {
            cnn.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = cnn;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            cnn.Close();
            return tabla;
        }

        public DataTable Consultar(string nombreSP, List<Parametro> lParametros)
        {
            cnn.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = cnn;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;

            foreach (Parametro p in lParametros)
            {
                if (p.Valor != "" && p.Valor != null)
                    comando.Parameters.AddWithValue(p.Nombre, p.Valor.ToString());
                else
                    comando.Parameters.AddWithValue(p.Nombre, DBNull.Value);
            }



            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            cnn.Close();
            return tabla;
        }

    }
}
