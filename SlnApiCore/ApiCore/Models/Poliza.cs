using Microsoft.VisualBasic;

namespace ApiCore.Models
{
    public class Poliza
    {
        public int IdPoliza { get; set; }

        public string Placa { get; set; }

        public int  IdCiudad { get; set; }

        public string Nombre_Tomador { get; set; }

        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Fin { get; set; }

        public DateTime Fecha_Vencimiento { get; set; }

      

    }
}
