﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using LibreriaObjetos;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Runtime.InteropServices;
using System.IO;
using ImProcessing;


namespace MainCore
{
    [DataContract]
    [Serializable]
    public class Metodos
    {
        List<S_Muestra> listaMuestrasAEnviar = new List<S_Muestra>();
        
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
        
        //--------------- PROCEDIMIENTOS A LISTAS
        /// <summary>
        /// Genera una lista con todos los TestFood de la BD.
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

        /// <summary>
        /// Devuelve la lista de TipoTestFood de la BD
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Devuelve una lista con todos los experimentos de la BD
        /// </summary>
        /// <returns></returns>
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
                            tf.procesado = registro.arecord.Procesado;

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

        public List<N_Experimento> ExperimentoToListNoProcesados()
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                List<N_Experimento> listaExp = new List<N_Experimento>();

                //Selecciona un registro de tipoTestFood por su Id
                var xdf = (from arecord in Context.ExperimentoSet
                           where arecord.Procesado == false && arecord.Procesado == false
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
                            tf.procesado = registro.arecord.Procesado;

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
        
        public List<N_CiclosEvaluacion> CiclosEvaluacionToList(Int32 idMpat)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                List<N_CiclosEvaluacion> lista = new List<N_CiclosEvaluacion>();

                //Selecciona un registro de paciente por su Id
                var xdf = (from arecord in Context.CiclosEvaluacionSet
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
                            //crear instancia de objeto N_CiclosEvaluacion
                            N_CiclosEvaluacion ob = new N_CiclosEvaluacion();
                            ob.id = registro.arecord.Id;
                            ob.idMpat = registro.arecord.idMpat;
                            ob.numeroCiclos = registro.arecord.numeroCiclos;
                            ob.orden = registro.arecord.orden;

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
        
        //-----------PROCEDIMIENTOS DE GRABACIÓN EN BD
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
                Db.ciclosMasticatorios = mpat.CiclosMasticatorios;
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
                if (exp.id != 0) {
                    Db.Id = exp.id; // viene de un update ¿correcto sin el if?
                }
                Db.idMpat = exp.idMpat;
                Db.NumeroPacientes = exp.numeroPacientes;
                Db.Codigo = exp.codigoExperimento;
                Db.Procesado = exp.procesado;

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
        
        public Boolean DeleteExperimento(Int32 id)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de paciente por su Id
                var xdf = (from arecord in Context.ExperimentoSet
                           where arecord.Id == id
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
                        Context.ExperimentoSet.Remove(xdf.arecord);
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

        // --------------------- PROCEDIMIENTOS DE BUSQUEDA
        public Boolean BuscaExperimento(Int32 miid, out N_Experimento exp)
        {

            exp = new N_Experimento();
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
                        exp.procesado = xdf.arecord.Procesado;
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

        public Boolean BuscaMpat(Int32 miid, out N_Mpat mpat)
        {
            var resultado = new N_Mpat();
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de paciente por su DNI
                var xdf = (from arecord in Context.MpatSet
                           where arecord.Id == miid
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                var xdf2 = (from brecord in Context.ProcedimientoClinicoSet
                           where brecord.idMpat == miid
                           select new
                           {
                               brecord
                           }).ToList();
                var xdf3 = (from crecord in Context.CiclosEvaluacionSet
                            where crecord.idMpat == miid
                            select new
                            {
                                crecord
                            }).ToList();
                try
                {
                    //Comprueba si el resultado es vacio
                    if (xdf.arecord != null && xdf2 != null && xdf3 !=null)
                    {
                        resultado.id = xdf.arecord.Id;
                        resultado.CiclosMasticatorios = xdf.arecord.ciclosMasticatorios;
                        resultado.idEstado = xdf.arecord.Estado;
                        resultado.idTestFood = xdf.arecord.idTestFood;
                        resultado.nombre = xdf.arecord.nombre;

                        resultado.CiclosEvaluacion = new List<N_CiclosEvaluacion>();
                        foreach (var c in xdf3)
                        {
                            N_CiclosEvaluacion ciclo = new N_CiclosEvaluacion();
                            ciclo.id = c.crecord.Id;
                            ciclo.idMpat = c.crecord.idMpat;
                            ciclo.numeroCiclos = c.crecord.numeroCiclos;
                            ciclo.orden = c.crecord.orden;

                            resultado.CiclosEvaluacion.Add(ciclo);
                        }
                        resultado.ListaProcedimientos = new List<N_ProcedimientoClinico>();
                        foreach (var b in xdf2)
                        {
                            N_ProcedimientoClinico procedimiento = new N_ProcedimientoClinico();
                            procedimiento.id = b.brecord.Id;
                            procedimiento.idMpat = b.brecord.idMpat;
                            procedimiento.orden = b.brecord.orden;
                            resultado.ListaProcedimientos.Add(procedimiento);
                        }

                        mpat = resultado;
                        return true;
                    }
                    else
                    {

                        mpat = resultado; 
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    mpat = resultado;
                    return false;
                }
            }
        }
        public Boolean BuscaTestFood(Int32 miid, out N_TestFood salida)
        {
            salida = new N_TestFood();
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de paciente por su DNI
                var xdf = (from arecord in Context.TestFoodSet
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
                        salida.id = xdf.arecord.Id;
                        salida.caracteristicasMonitorizadas = xdf.arecord.caracteristicaMonitorzadas;
                        salida.descripcion = xdf.arecord.descripcion;
                        salida.nombre = xdf.arecord.nombre;
                        salida.tipo = xdf.arecord.IdTipo;
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


        //---------------- RECOGIDA DATOS DE IMAGENES
        public Boolean BuscaSiguienteOrdenNCiclos(Int32 idMpat, Int32 ordenAnterior, out Int32 ciclosSiguiente)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de paciente por su DNI
                var xdf = (from arecord in Context.CiclosEvaluacionSet
                           where arecord.idMpat == idMpat
                           && arecord.orden > ordenAnterior
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                try
                {
                    //Comprueba si el resultado es vacio
                    if (xdf.arecord != null)
                    {
                        ciclosSiguiente = xdf.arecord.orden;
                        return true;
                    }
                    else
                    {
                        ciclosSiguiente = 0;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    ciclosSiguiente = 0;
                    return false;
                }
            }
            
        }
    
        //------------ TRATAMIENTO IMAGENES

        public byte[] GetEncodedImageData(ImageSource image, string preferredFormat)
        {

            byte[] result = null;
            BitmapEncoder encoder = null;

            switch (preferredFormat.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    encoder = new JpegBitmapEncoder();
                    break;
                case ".bmp":
                    encoder = new BmpBitmapEncoder();
                    break;
                case ".png":
                    encoder = new PngBitmapEncoder();
                    break;
                case ".tif":
                case ".tiff":
                    encoder = new TiffBitmapEncoder();
                    break;
                case ".gif":
                    encoder = new GifBitmapEncoder();
                    break;
                case ".wmp":
                    encoder = new WmpBitmapEncoder();
                    break;
            }

            if (image is BitmapSource)
            {
                MemoryStream stream = new MemoryStream();
                encoder.Frames.Add(BitmapFrame.Create(image as BitmapSource));
                encoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                result = new byte[stream.Length];
                BinaryReader br = new BinaryReader(stream);
                br.Read(result, 0, (int)stream.Length);
                br.Close();
                stream.Close();
            }
            return result;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        private struct BITMAPFILEHEADER
        {
            public static readonly short BM = 0x4d42; // BM

            public short bfType;
            public int bfSize;
            public short bfReserved1;
            public short bfReserved2;
            public int bfOffBits;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        }

        public ImageSource ImageFromClipboardDib()
        {
            MemoryStream ms = Clipboard.GetData("DeviceIndependentBitmap") as MemoryStream;
            if (ms != null)
            {
                byte[] dibBuffer = new byte[ms.Length];
                ms.Read(dibBuffer, 0, dibBuffer.Length);

                BITMAPINFOHEADER infoHeader = BinaryStructConverter.FromByteArray<BITMAPINFOHEADER>(dibBuffer);

                int fileHeaderSize = Marshal.SizeOf(typeof(BITMAPFILEHEADER));
                int infoHeaderSize = infoHeader.biSize;
                int fileSize = fileHeaderSize + infoHeader.biSize + infoHeader.biSizeImage;

                BITMAPFILEHEADER fileHeader = new BITMAPFILEHEADER();
                fileHeader.bfType = BITMAPFILEHEADER.BM;
                fileHeader.bfSize = fileSize;
                fileHeader.bfReserved1 = 0;
                fileHeader.bfReserved2 = 0;
                fileHeader.bfOffBits = fileHeaderSize + infoHeaderSize + infoHeader.biClrUsed * 4;

                byte[] fileHeaderBytes = BinaryStructConverter.ToByteArray<BITMAPFILEHEADER>(fileHeader);

                MemoryStream msBitmap = new MemoryStream();
                msBitmap.Write(fileHeaderBytes, 0, fileHeaderSize);
                msBitmap.Write(dibBuffer, 0, dibBuffer.Length);
                msBitmap.Seek(0, SeekOrigin.Begin);

                return BitmapFrame.Create(msBitmap);
            }
            return null;
        }

        public static class BinaryStructConverter
        {
            public static T FromByteArray<T>(byte[] bytes) where T : struct
            {
                IntPtr ptr = IntPtr.Zero;
                try
                {
                    int size = Marshal.SizeOf(typeof(T));
                    ptr = Marshal.AllocHGlobal(size);
                    Marshal.Copy(bytes, 0, ptr, size);
                    object obj = Marshal.PtrToStructure(ptr, typeof(T));
                    return (T)obj;
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                        Marshal.FreeHGlobal(ptr);
                }
            }

            public static byte[] ToByteArray<T>(T obj) where T : struct
            {
                IntPtr ptr = IntPtr.Zero;
                try
                {
                    int size = Marshal.SizeOf(typeof(T));
                    ptr = Marshal.AllocHGlobal(size);
                    Marshal.StructureToPtr(obj, ptr, true);
                    byte[] bytes = new byte[size];
                    Marshal.Copy(ptr, bytes, 0, size);
                    return bytes;
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                        Marshal.FreeHGlobal(ptr);
                }
            }
        }

        private static ImageSource CreateImage(byte[] imageData, int decodePixelWidth, int decodePixelHeight)
        {

            if (imageData == null) return null;
            BitmapImage result = new BitmapImage();
            result.BeginInit();

            if (decodePixelWidth > 0)
            {
                result.DecodePixelWidth = decodePixelWidth;
            }

            if (decodePixelHeight > 0)
            {
                result.DecodePixelHeight = decodePixelHeight;
            }

            result.StreamSource = new MemoryStream(imageData);
            result.CreateOptions = BitmapCreateOptions.None;
            result.CacheOption = BitmapCacheOption.Default;
            result.EndInit();
            return result;

        }
        
        public Boolean UpdateExperimento(N_Experimento experimento)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                var xdf = (from arecord in Context.ExperimentoSet
                           where arecord.Id == experimento.id
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                try
                {
                    //Comprueba si el resultado es vacio
                    if (xdf.arecord != null)
                    {
                        xdf.arecord.Procesado = experimento.procesado;

                        //Guardar el objeto Dbtf en el Context
                        try
                        {
                            Context.SaveChanges();
                            return true;
                        }
                        catch (Exception e)
                        {
                            Console.Write("Error " + e);
                            return false;
                        }
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

        public Boolean DeletePaciente(Int32 id)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de paciente por su Id
                var xdf = (from arecord in Context.PacienteSet
                           where arecord.Id == id
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
                        Context.PacienteSet.Remove(xdf.arecord);
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

        public List<N_Paciente> PacientesExpToList(Int32 id)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                List<N_Paciente> listaExp = new List<N_Paciente>();

                //Selecciona un registro de tipoTestFood por su Id
                var xdf = (from arecord in Context.PacienteSet
                           where arecord.idExperimento == id
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
                            N_Paciente tf = new N_Paciente();
                            tf.id = registro.arecord.Id;
                            tf.identificacion = registro.arecord.DNI;
                            tf.idHistoriaClinica = registro.arecord.idHistoriaClinica;
                            //tf.idPacienteExp = registro.arecord.idPacienteExp;
                            tf.nombre = registro.arecord.Nombre;
                            tf.sexo = registro.arecord.Sexo;
                            tf.ubicacion = registro.arecord.Ubicacion;

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

        public Boolean DeleteMpat(Int32 id)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de paciente por su Id
                var xdf = (from arecord in Context.MpatSet
                           where arecord.Id == id
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
                        Context.MpatSet.Remove(xdf.arecord);
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

        public Boolean enviarVectoresServidor(List<EstrucutraImagen> listaImagenesProcesadas)
        {
            Boolean resultado = true;
            foreach (var elem in listaImagenesProcesadas)
            {
                resultado = resultado && enviarVectorServidor(elem);
            }
            return resultado;
        }

        private bool enviarVectorServidor(EstrucutraImagen elementoEstructuraImagen)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Muestra Db = new Muestra();
                Int32 idMuestra = 0;
                Db.Ciclos = elementoEstructuraImagen.numCiclos.ToString();
                //Db.VectorCaracteristicas = elementoEstructuraImagen.Labels;

                //Guardar el objeto Dbtf en el Context
                try
                {
                    Context.MuestraSet.Add(Db);
                    Context.SaveChanges();
                    idMuestra = Db.Id; // devuelvo la id de la muestra para grabar despues las caracteristica con la muestra correcta
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    return false;
                }
            }
        }

        public Boolean BuscaIdMpatPorCodigoExperimento(Int32 idExperimento, out Int32 idMpat) {
        {

            idMpat = 0;
            using (Model1Container1 Context = new Model1Container1())
            {
                //Selecciona un registro de paciente por su DNI
                var xdf = (from arecord in Context.ExperimentoSet
                            where arecord.Id == idExperimento
                            select new
                            {
                                arecord
                            }).FirstOrDefault();
                try
                {
                    //Comprueba si el resultado es vacio
                    if (xdf.arecord != null)
                    {
                        idMpat = xdf.arecord.idMpat;
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

        }

        public String descripcionTestFood(Int32 idTestFood)
        {
            N_TestFood salida = new N_TestFood();
            if (BuscaTestFood(idTestFood, out salida))
            {
                return salida.descripcion;
            }
            else
            {
                return "";
            }
        }
    }    
}