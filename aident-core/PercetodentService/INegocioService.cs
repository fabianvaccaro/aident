using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LibreriaObjetos;
using MainCore;

namespace PercetodentService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INegocioService" in both code and config file together.
    [ServiceContract]
    public interface INegocioService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<N_TipoTestFood> TipoTestFoodToList();

        [OperationContract]
        Boolean AddTipoTestFood(N_TipoTestFood tff);
        
        [OperationContract]
        Boolean AddTestFood(N_TestFood tf);

        [OperationContract]
        List<N_TestFood> TestFoodToList();

        [OperationContract]
        Boolean DeleteTestFood(Int32 id);

        [OperationContract]
        Boolean AddMPAT(N_Mpat mpat, out Int32 idMpat);

        [OperationContract]
        Boolean AddExperimento(N_Experimento experimento);

        [OperationContract]
        List<N_Mpat> MpatToList();

        [OperationContract]
        List<N_Experimento> ExperimentoToList();

        [OperationContract]
        Boolean AddProcedimiento(N_ProcedimientoClinico pro);

        [OperationContract]
        Boolean AddCiclosEvaluacion(N_CiclosEvaluacion ciclos);        
    }
}
