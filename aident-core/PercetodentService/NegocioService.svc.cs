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

        public List<N_TipoTestFood> TipoTestFoodToList()
        {
            Metodos metodo = new Metodos();
            return metodo.TipoTestFoodToList();
        }

        public Boolean AddTestFood(N_TestFood tf)
        {
            Metodos metodo = new Metodos();
            return metodo.AddTestFood(tf);
        }

        public List<N_TestFood> TestFoodToList()
        {
            Metodos metodo = new Metodos();
            return metodo.TestFoodToList();
        }

        public List<N_ProcedimientoClinico> ProcedimientoToList(Int32 idMpat)
        {
            Metodos metodo = new Metodos();
            return metodo.ProcedimientoToList(idMpat);

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

        public Boolean AddMPAT(N_Mpat mpat, out Int32 idMpat)
        {
            Metodos metodo = new Metodos();
            return metodo.AddMPAT(mpat, out idMpat);
        }

        public List<N_Experimento> ExperimentoToList()
        {
            Metodos metodo = new Metodos();
            return metodo.ExperimentoToList();
        }

        public List<N_CiclosEvaluacion> CiclosEvaluacionToList(Int32 idMpat)
        {
            Metodos metodo = new Metodos();
            return metodo.CiclosEvaluacionToList(idMpat);
        }

        public Boolean AddProcedimiento(N_ProcedimientoClinico pro)
        {
            Metodos metodo = new Metodos();
            return metodo.AddProcedimiento(pro);
        }

        public Boolean AddCiclosEvaluacion(N_CiclosEvaluacion ciclos)
        {
            Metodos metodo = new Metodos();
            return metodo.AddCiclosEvaluacion(ciclos);
        }

        public List<N_Mpat> MpatToList()
        {
            Metodos metodo = new Metodos();
            return metodo.MpatToList();
        }
    }
}
