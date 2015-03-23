using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaObjetos;
using System.Runtime.Serialization;
using Metodos;

namespace MainCore
{
    [DataContract]
    [Serializable]
    class Metodos
    {
        public Metodos()
        {

        }
        public List<N_TestFood> testFoodToList()
        {
            using ( Model1Container Context = new Model1Container())
            {
                List<N_TestFood> lista = new List<N_TestFood>();
                
                //Selecciona un registro de paciente por su Id
                var xdf = (from arecord in Context.TestFoodSet
                           select new
                           {
                               arecord
                           }).ToList();
                try
                {
                    
                    //Verifica que existan los registros
                    if(xdf != null)
                    {
                        foreach (var registro in xdf)
                        {
                            //crear instancia de objeto N_Paciente
                            N_TestFood tf = new N_TestFood();
                            tf.id = registro.arecord.Id;
                            tf.nombre = registro.arecord.nombre;
                            tf.tipo = registro.arecord.tipo;
                            tf.descripcion = registro.arecord.descripcion;
                            tf.caracteristicasMonitorizadas = registro.arecord.caracteristicaMonitorzadas;

                            //añadir tf a la lista
                            lista.Add(tf);
                        }
                        return lista;
                    }
                    else
                    {
                        return false;
                    }


                    
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                }
            }
        }
    }
}
