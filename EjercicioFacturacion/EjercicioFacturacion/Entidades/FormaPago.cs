using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioFacturacion.Entidades
{
    public class FormaPago
    {
        public int NroForma {  get; set; }
        public string Nombre { get; set; }

        public FormaPago()
        {
            NroForma = 0;
            Nombre = string.Empty;
        }

        public FormaPago(int nro, string nom)
        {
            this.NroForma = nro;
            this.Nombre = nom;
        }

        public static implicit operator FormaPago(int v)
        {
            throw new NotImplementedException();
        }
    }
}
