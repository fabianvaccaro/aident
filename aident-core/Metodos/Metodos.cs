using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using LibreriaObjetos;


namespace MainCore
{
    [DataContract]
    [Serializable]
    public class Metodos
    {
        public Metodos()
        {

        }

        /// <summary>
        /// Añade a la BD un nuevo elemento TestFood
        /// </summary>
        /// <param name="tf">datos del elemento</param>
        /// <returns></returns>
        public Boolean AddTestFood(N_TestFood tf)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                TestFood Dbtf = new TestFood();

                //Popula el objeto DbPac
                //Dbtf.Id = tf.id;
                Dbtf.nombre = tf.nombre;
                Dbtf.descripcion = tf.descripcion;
                Dbtf.caracteristicaMonitorzadas = tf.caracteristicasMonitorizadas;
                Dbtf.IdTipo = tf.tipo;

                //Guardar el objeto Dbtf en el Context
                try
                {
                    Context.TestFoodSet.Add(Dbtf);
                    Context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        /// <summary>
        /// Elimina un TestFood de la base de datos en caso de existir. Si no existe devuelve false.
        /// </summary>
        /// <param name="tf">TestFood a eliminar</param>
        /// <returns> true si elimina. false si no elimina o si no existe.</returns>
        public Boolean DeleteTestFood(Int32 tfId)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de TestFood por su nombre
                var xdf = (from arecord in Context.TestFoodSet
                           where arecord.Id == tfId
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                try
                {
                    //Comprueba si el resultado es vacio
                    if (xdf.arecord != null)
                    {
                        //Borra el registro
                        Context.TestFoodSet.Remove(xdf.arecord);
                        Context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }

        }

        /// <summary>
        /// Comprueba la existencia del elemento TestFood en la BD.
        /// </summary>
        /// <param name="tf">TestFood a buscar</param>
        /// <returns>True si elemento existe. False si elemento no existe</returns>
        public Boolean ExisteTestFood(N_TestFood tf) {
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de paciente por su DNI
                var xdf = (from arecord in Context.TestFoodSet
                           where arecord.nombre.CompareTo(tf.nombre) == 0
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                try
                {
                    //Comprueba si el resultado es vacio
                    if (xdf.arecord != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        /// <summary>
        /// Genera una lista de TestFood.
        /// </summary>
        /// <returns> Devuelve una lista de TestFood.</returns>
        public List<N_TestFood> TestFoodToList()
        {
            using (Model1Container1 Context = new Model1Container1())
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
                            //crear instancia de objeto N_TestFood
                            N_TestFood tf = new N_TestFood();
                            tf.id = registro.arecord.Id;
                            tf.nombre = registro.arecord.nombre;
                            tf.tipo = registro.arecord.TipoTestFood.Id;
                            tf.descripcion = registro.arecord.descripcion;
                            tf.caracteristicasMonitorizadas = registro.arecord.caracteristicaMonitorzadas;

                            //añadir tf a la lista
                            lista.Add(tf);
                        }
                        return lista;
                    }
                    else
                    {
                        return lista;
                    }


                    
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return lista;
                }
            }
        }

        public List<N_TipoTestFood> TipoTestFoodToList()
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                List<N_TipoTestFood> lista = new List<N_TipoTestFood>();

                //Selecciona un registro de tipoTestFood por su Id
                var xdf = (from arecord in Context.TipoTestFoodSet
                           select new
                           {
                               arecord
                           }).ToList();
                try
                {

                    //Verifica que existan los registros
                    if (xdf != null)
                    {
                        foreach (var registro in xdf)
                        {
                            //crear instancia de objeto N_TipoTestFood
                            N_TipoTestFood tf = new N_TipoTestFood();
                            tf.id = registro.arecord.Id;
                            tf.nombre = registro.arecord.nombre;
                            tf.descripcion = registro.arecord.descripcion;
                            
                            //añadir tf a la lista
                            lista.Add(tf);
                        }
                        return lista;
                    }
                    else
                    {
                        return lista;
                    }



                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return lista;
                }
            }
        }

        public List<N_Experimento> ExperimentoToList(){
            using (Model1Container1 Context = new Model1Container1())
            {
                List<N_Experimento> listaExp = new List<N_Experimento>();

                //Selecciona un registro de tipoTestFood por su Id
                var xdf = (from arecord in Context.ExperimentoSet
                           select new
                           {
                               arecord
                           }).ToList();
                try
                {

                    //Verifica que existan los registros
                    if (xdf != null)
                    {
                        foreach (var registro in xdf)
                        {
                            //crear instancia de objeto N_TipoTestFood
                            N_Experimento tf = new N_Experimento();
                            tf.id = registro.arecord.Id;
                            tf.idMpat = registro.arecord.idMpat;
                            tf.numeroPacientes = registro.arecord.NumeroPacientes;
                            tf.codigoExperimento = registro.arecord.Codigo;
                            tf.idPaciente = 0;

                            //añadir tf a la lista
                            listaExp.Add(tf);
                        }
                        return listaExp;
                    }
                    else
                    {
                        return listaExp;
                    }



                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return listaExp;
                }
            }
        }

        public List<N_Mpat> MpatToList()
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                List<N_Mpat> lista = new List<N_Mpat>();

                //Selecciona un registro de paciente por su Id
                var xdf = (from arecord in Context.MpatSet
                           select new
                           {
                               arecord
                           }).ToList();
                try
                {

                    //Verifica que existan los registros
                    if (xdf != null)
                    {
                        foreach (var registro in xdf)
                        {
                            //crear instancia de objeto N_Mpat
                            N_Mpat ob = new N_Mpat();
                            ob.id = registro.arecord.Id;
                            //ob.ListaProcedimientos = registro.arecord.procedimiento;
                            //ob.CiclosEvaluacion = registro.arecord.ciclosEvaluacion;
                            ob.CiclosMasticatorios = registro.arecord.ciclosMasticatorios;
                            ob.idTestFood = registro.arecord.idTestFood;
                            ob.idEstado = registro.arecord.Estado;
                            ob.nombre = registro.arecord.nombre;
                                                        
                            //añadir ob a la lista
                            lista.Add(ob);
                        }
                        return lista;
                    }
                    else{
                        return lista;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return lista;
                }
            }
        }

        public List<N_ProcedimientoClinico> ProcedimientoToList(Int32 idMpat)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                List<N_ProcedimientoClinico> lista = new List<N_ProcedimientoClinico>();

                //Selecciona un registro de paciente por su Id
                var xdf = (from arecord in Context.ProcedimientoClinicoSet
                           where arecord.idMpat == idMpat
                           select new
                           {
                               arecord
                           }).ToList();
                try
                {
                    //Verifica que existan los registros
                    if (xdf != null)
                    {
                        foreach (var registro in xdf)
                        {
                            //crear instancia de objeto N_ProcedimientoClinico
                            N_ProcedimientoClinico ob = new N_ProcedimientoClinico();
                            ob.id = registro.arecord.Id;
                            ob.idMpat = registro.arecord.idMpat;
                            ob.orden = registro.arecord.orden;
                            ob.descripcion = registro.arecord.descripcion;

                            //añadir ob a la lista
                            lista.Add(ob);
                        }
                        return lista;
                    }
                    else
                    {
                        return lista;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return lista;
                }
            }
        }        
        
        public Boolean AddTipoTestFood(N_TipoTestFood ttf)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                TipoTestFood Dbttf = new TipoTestFood();

                //Popula el objeto DbPac
                //Dbtf.Id = tf.id;
                Dbttf.nombre = ttf.nombre;
                Dbttf.descripcion = ttf.descripcion;

                //Guardar el objeto Dbtf en el Context
                try
                {
                    Context.TipoTestFoodSet.Add(Dbttf);
                    Context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        public Boolean AddMPAT(N_Mpat mpat, out Int32 idMpat)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Mpat Db = new Mpat();
                idMpat = 0;

                //Popula el objeto Db
                //Dbtf.Id = tf.id;
                Db.idTestFood = mpat.idTestFood;
                //Db.procedimiento = mpat.ListaProcedimientos;
                Db.ciclosMasticatorios = mpat.CiclosMasticatorios;
                //Db.ciclosEvaluacion = mpat.CiclosEvaluacion;
                Db.nombre = mpat.nombre;


                //Guardar el objeto Dbtf en el Context
                try
                {
                    Context.MpatSet.Add(Db);
                    Context.SaveChanges();
                    idMpat = Db.Id;
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        public Boolean AddExperimento(N_Experimento exp)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Experimento Db = new Experimento();

                //Popula el objeto Db
                //Db.Id = exp.id;
                Db.idMpat = exp.idMpat;
                Db.NumeroPacientes = exp.numeroPacientes;
                Db.Codigo = exp.codigoExperimento;

                //Guardar el objeto Dbtf en el Context
                try
                {
                    Context.ExperimentoSet.Add(Db);
                    Context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        public Boolean AddProcedimiento(N_ProcedimientoClinico pro)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                ProcedimientoClinico Db = new ProcedimientoClinico();

                //Popula el objeto Db
                //Db.Id = exp.id;
                Db.idMpat = pro.idMpat;
                Db.descripcion = pro.descripcion;
                Db.orden = pro.orden;

                //Guardar el objeto Dbtf en el Context
                try
                {
                    Context.ProcedimientoClinicoSet.Add(Db);
                    Context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        public Boolean AddPacientesExperimento(List<N_Paciente> listPacientes, String codigoExperimento)
        {
            using (Model1Container1 Context = new Model1Container1())
            {

                Paciente Db = new Paciente();
                N_Paciente paciente = new N_Paciente();

                //Selecciona un registro de TestFood por su nombre
                var xdf = (from arecord in Context.ExperimentoSet
                           where arecord.Id.CompareTo(codigoExperimento) == 0
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                try
                {
                    //Insertamos la lista de Pacientes en la Base de Datos
                    if (xdf.arecord != null)
                    {
                        paciente = listPacientes.First();
                        while (paciente != null)
                        {
                            //Popula el objeto Db
                            //Db.Id = exp.id;
                            Db.DNI = paciente.identificacion;
                            Db.idExperimento = xdf.arecord.Id;
                            Db.idHistoriaClinica = paciente.idHistoriaClinica;
                            Db.Nombre = paciente.nombre;
                            Db.Sexo = paciente.sexo;
                            Db.Ubicacion = paciente.ubicacion;

                            Context.PacienteSet.Add(Db);
                            Context.SaveChanges();

                            paciente = listPacientes.First();
                        }
                        
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        public Boolean AddPaciente(N_Paciente paciente, out Int32 miid)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Paciente Db = new Paciente();
                miid = 0;
                //Popula el objeto Db
                //Dbtf.Id = tf.id;
                Db.DNI = paciente.identificacion;
                Db.idHistoriaClinica = paciente.idHistoriaClinica;
                Db.idPacienteExp = paciente.idPacienteExp.ToString();
                Db.Nombre = paciente.nombre;
                Db.Sexo = paciente.sexo;
                Db.Ubicacion = paciente.ubicacion;
                //Db.HistoriaClinica = paciente.idHistoriaClinica;
                //Db.Experimento = paciente.idPacienteExp;
                Db.idExperimento = paciente.idPacienteExp;

                //Guardar el objeto Dbtf en el Context
                try
                {
                    Context.PacienteSet.Add(Db);
                    Context.SaveChanges();
                    miid = Db.Id;
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        public Boolean AddHistoriaClinica(N_HistoriaClinica historiaClinica, out Int32 idHistoriaClinica)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                HistoriaClinica Db = new HistoriaClinica();
                idHistoriaClinica = 0;
                //Popula el objeto Db
                //Dbtf.Id = tf.id;
                Db.odontograma = historiaClinica.odontograma;
                Db.numeroCariados = historiaClinica.numeroCariados;
                Db.numeroDientesPerdidos = historiaClinica.numeroDientesPerdidos;
                Db.numeroDientesObturados = historiaClinica.numeroDientesObturados;
                Db.ortodoncia = historiaClinica.ortodoncia;
                Db.protesis = historiaClinica.protesis;
                Db.implantes = historiaClinica.implantes;
                Db.paresAntagPerdidos = historiaClinica.paresAntagPerdidos;
                Db.gradoEdentulismo = historiaClinica.gradoEdentulismo;
                Db.estadoSaludGeneral = historiaClinica.estadoSaludGeneral;
                Db.enfermedadCardioVascular = historiaClinica.enfermedadCardioVascular;
                Db.enfermedadRenal = historiaClinica.enfermedadRenal;
                Db.ICTUS = historiaClinica.ICTUS;
                Db.ACV = historiaClinica.ACV;
                Db.paralisisFacial = historiaClinica.paralisisFacial;
                Db.gradoDesnutricion = historiaClinica.gradoDesnutricion;
                //Guardar el objeto Dbtf en el Context
                try
                {
                    Context.HistoriaClinicaSet.Add(Db);
                    Context.SaveChanges();
                    idHistoriaClinica = Db.Id;
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        public Boolean AddCiclosEvaluacion(N_CiclosEvaluacion ciclos)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                CiclosEvaluacion Db = new CiclosEvaluacion();

                //Popula el objeto Db
                //Db.Id = ciclos.id;
                Db.idMpat = ciclos.idMpat;
                Db.numeroCiclos = ciclos.numeroCiclos; 

                //Guardar el objeto Dbtf en el Context
                try
                {
                    Context.CiclosEvaluacionSet.Add(Db);
                    Context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        public Boolean BuscaExperimento(Int32 miid, N_Experimento exp)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de paciente por su DNI
                var xdf = (from arecord in Context.ExperimentoSet
                           where arecord.Id == miid
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                try
                {
                    //Comprueba si el resultado es vacio
                    if (xdf.arecord != null)
                    {
                        exp.id = xdf.arecord.Id;
                        exp.codigoExperimento = xdf.arecord.Codigo;
                        exp.idMpat = xdf.arecord.idMpat;
                        exp.numeroPacientes = xdf.arecord.NumeroPacientes;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }
        public Boolean BuscaMpat(Int32 miid, N_Mpat mpat)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de paciente por su DNI
                var xdf = (from arecord in Context.MpatSet
                           where arecord.Id == miid
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                try
                {
                    //Comprueba si el resultado es vacio
                    if (xdf.arecord != null)
                    {
                        mpat.id = xdf.arecord.Id;
                        //mpat.CiclosEvaluacion = xdf.arecord.CiclosEvaluacion.ToList();
                        mpat.CiclosMasticatorios = xdf.arecord.ciclosMasticatorios;
                        mpat.idEstado = xdf.arecord.Estado;
                        mpat.idTestFood = xdf.arecord.idTestFood;
                        //mpat.ListaProcedimientos = xdf.arecord.Experimento;
                        mpat.nombre = xdf.arecord.nombre;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        //public void validaMPAT(String nombre, String procedimiento, Int32 idTestFood, Int32 ciclosMasticatorios, Int32 ciclosValidacion)
        //{
        //    // falta validacion de datos
        //    N_Mpat mpat = new N_Mpat(nombre, procedimiento, idTestFood, ciclosMasticatorios, ciclosValidacion);
        //    addMPAT(mpat);
        //}

        //public void validaExperimento(String codigoExperimento, Int32 idMpat, Int32 numeroPacientes)
        //{
        //    // falta validacion de datos

        //    N_Experimento experimento = new N_Experimento(codigoExperimento, idMpat, numeroPacientes);

        //    this.AddExperimento(experimento);
        //}


    }    
}