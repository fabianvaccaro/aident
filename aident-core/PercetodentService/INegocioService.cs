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
        List<N_TipoTestFood> tipoTestFoodToList();

        [OperationContract]
        Boolean AddTipoTestFood(N_TipoTestFood tff);
        
        [OperationContract]
        Boolean AddTestFood(N_TestFood tf);

        [OperationContract]
        List<N_TestFood> testFoodToList();

        [OperationContract]
        Boolean DeleteTestFood(Int32 id);

        [OperationContract]
        void validaMPAT(String nombre, String procedimiento, Int32 idTestFood, Int32 ciclosMasticatorios, Int32 ciclosValidacion);

        [OperationContract]
        void validaExperimento(String codigoExperimento, Int32 idMpat, Int32 numeroPacientes);

        [OperationContract]
        List<N_Mpat> mpatToList();
        
    }
}
