using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainCore
{
    public class N_TipoTestFood
    {
        public Int32 id { set; get; }
        public String nombre { set; get; }
        public String descripcion { set; get; }

        public N_TipoTestFood()
        {
            id = 0;
            nombre = String.Empty;
            descripcion = String.Empty;
        }
    }
}
