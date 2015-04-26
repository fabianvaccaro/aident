using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MainCore;
using LibreriaObjetos;

namespace PercetodentService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NegocioService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select NegocioService.svc or NegocioService.svc.cs at the Solution Explorer and start debugging.
    public class NegocioService : INegocioService
    {
        public void DoWork()
        {
        }

        public Boolean AddTipoTestFood(N_TipoTestFood tff)
        {
            Metodos metodo = new Metodos();
            return metodo.AddTipoTestFood(tff);
        }

        public List<N_TipoTestFood> tipoTestFoodToList()
        {
            Metodos metodo = new Metodos();
            return metodo.tipoTestFoodToList();
        }
        public Boolean AddTestFood(N_TestFood tf)
        {
            Metodos metodo = new Metodos();
            return metodo.AddTestFood(tf);
        }

        public List<N_TestFood> testFoodToList()
        {
            Metodos metodo = new Metodos();
            return metodo.testFoodToList();
        }
        public Boolean DeleteTestFood(Int32 id)
        {
            Metodos metodo = new Metodos();
            return metodo.DeleteTestFood(id);
        }
        public Boolean AddExperimento(N_Experimento experimento) 
        {
            Metodos metodo = new Metodos();
            return metodo.AddExperimento(experimento);
        }
        public Boolean AddMPAT(N_Mpat mpat)
        {
            Metodos metodo = new Metodos();
            return metodo.AddMPAT(mpat);
        }
        public List<N_Mpat> mpatToList()
        {
            Metodos metodo = new Metodos();
            return metodo.mpatToList();
        }

        public List<N_Experimento> experimentoToList()
        {
            Metodos metodo = new Metodos();
            return metodo.experimentoToList();
        }
    }
}
