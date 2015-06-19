using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaObjetos;

namespace MainCore_Server
{
    public class MetodosServer
    {
        public MetodosServer()
        {

        }
        //-------------- PROCEDIMIENTOS DE ADICCION A LA BASE DE DATOS -------------------//
        public Boolean addMpat(S_Mpat mpat,out Int32 idMpat)
        {
            using (ModeloDatosServidorContainer Context = new ModeloDatosServidorContainer())
            {
                MainCore_Server.MpatSet Db = new MainCore_Server.MpatSet();
                Int32 temp = 0;

                //Completamos el objeto Db
                //Db.Id = mpat.id;
                Db.DescripcionTestFood = mpat.DescripcionTestFood;
                Db.nombre = mpat.nombre;
                Db.ciclosMasticatorios = mpat.ciclosMasticatorios;

                //Guardar el objeto Dbtf en el Context
                try
                {
                    Context.MpatSet.Add(Db);
                    Context.SaveChanges();
                    temp = Db.Id;
                    foreach (var a in mpat.ListaProcedimientos)
                    {
                        if (!addProcedimientoEvaluacion(a))
                        {
                            //si falla el procedimiento de inserccion debo borrar el elemento Mpat
                            //Context.MpatSet.Remove(Db);
                            //Context.SaveChanges();
                            idMpat = 0;
                            return false;
                        }
                    }
                    foreach (var b in mpat.CiclosEvaluacion)
                    {
                        if (!addCiclosEvaluacion(b))
                        {
                            //si falla el procedimiento de inserccion debo borrar el elemento Mpat
                            //borrarListaProcedimientos(idMpat);
                            Context.MpatSet.Remove(Db);
                            Context.SaveChanges();
                            idMpat = 0;
                            return false;
                        }
                    }
                    idMpat = temp;
                    return true;
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    idMpat = temp;
                    return false;
                }
            }
        }

        /// <summary>
        /// Añade un elemento usuario a la base de datos del servidor, devolviendo la ID del elemento creado.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean addUsuario(S_Usuario usuario, out Int32 id)
        {
            using (ModeloDatosServidorContainer Context = new ModeloDatosServidorContainer())
            {
                UsuarioSet Db = new UsuarioSet();
                id = 0;

                //Completamos la variable Db con los datos que nos pasan
                //Db.Id = usuario.Id;
                Db.Password = usuario.Password;
                Db.Usuario = usuario.Usuario;

                try
                {
                    Context.UsuarioSet.Add(Db);
                    Context.SaveChanges();
                    id = Db.Id;
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
        /// Añade un elemento muestra a la base de datos del servidor, devolviendo la ID del elemento creado.
        /// </summary>
        /// <param name="muestra"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean addMuestra(S_Muestra muestra, out Int32 id)
        {
            using (ModeloDatosServidorContainer Context = new ModeloDatosServidorContainer())
            {
                MuestraSet Db = new MuestraSet();
                id = 0;

                //Db.Id = muestra.idExperimento;
                Db.idExperimento = muestra.idExperimento;
                Db.idPaciente = muestra.idPaciente;
                Db.lado = muestra.lado;
                Db.CiclosMasticatorios = muestra.nCiclos;

                try
                {
                    Context.MuestraSet.Add(Db);
                    Context.SaveChanges();
                    id = Db.Id;
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Excepcion: " + e);
                    return false;
                }
            }
        }

        public Boolean addFeatures(S_Caracteristica caracteristica, out Int32 id)
        {
            using (ModeloDatosServidorContainer Context = new ModeloDatosServidorContainer())
            {
                CaracteristicasExtraidasSet Db = new CaracteristicasExtraidasSet();
                id = 0;

                //Db.Id = caracteristica.Id;
                Db.codigoCaracteristica = caracteristica.CodigoCaracteristica;
                Db.idMuestra = caracteristica.IdMuestra;
                Db.Valor = caracteristica.ValorCaracteristica;

                try
                {
                    Context.CaracteristicasExtraidasSet.Add(Db);
                    Context.SaveChanges();
                    id = Db.Id;
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Excepcion: " + e);
                    return false;
                }

            }
        }

        public Boolean addExperimento(S_Experimento experimento, out Int32 id)
        {
            using (ModeloDatosServidorContainer Context = new ModeloDatosServidorContainer())
            {
                ExperimentoSet Db = new ExperimentoSet();
                id = 0;

                //Db.Id = experimento.id;
                Db.idMpat = experimento.idMpat;
                Db.idUsuario = experimento.idUsuario;

                try
                {
                    Context.ExperimentoSet.Add(Db);
                    Context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e);
                    return false;

                }
            }
        }

        public Boolean addCiclosEvaluacion(S_CiclosEvaluacion ciclos)
        {
            using (ModeloDatosServidorContainer Context = new ModeloDatosServidorContainer())
            {
                CiclosEvaluacionSet Db = new CiclosEvaluacionSet();


                //Db.Id = ciclos.Id;
                Db.idMpat = ciclos.idMpat;
                Db.numeroCiclos = ciclos.numeroCiclos;
                Db.orden = ciclos.orden;

                try
                {
                    Context.CiclosEvaluacionSet.Add(Db);
                    Context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e);
                    return false;
                }
            }
        }

        public Boolean addProcedimientoEvaluacion(S_Procedimiento procedimiento)
        {
            using (ModeloDatosServidorContainer Context = new ModeloDatosServidorContainer())
            {
                ProcedimientoClinicoSet Db = new ProcedimientoClinicoSet();


                //Db.Id = procedimiento.Id;
                Db.idMpat = procedimiento.idMpat;
                Db.descripcion = procedimiento.descripcion;
                Db.orden = procedimiento.orden;

                try
                {
                    Context.ProcedimientoClinicoSet.Add(Db);
                    Context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e);
                    return false;
                }
            }
        }

        //-------------- PROCEDIMIENTOS DE BUSQUEDA EN LA BASE DE DATOS -------------------//
        public Boolean BuscaMpat(Int32 miid, out S_Mpat salida)
        {   
            S_Mpat temp = new S_Mpat();
            List<S_Procedimiento> listaProcedimientoClinico = new List<S_Procedimiento>();
            List<S_CiclosEvaluacion> listaCiclosEvaluacion = new List<S_CiclosEvaluacion>();

            using (ModeloDatosServidorContainer Context = new ModeloDatosServidorContainer())
            {
                
                //Selecciona un registro de paciente por su DNI
                var xdf = (from arecord in Context.MpatSet
                           where arecord.Id == miid 
                           select new
                           {
                               arecord
                           }).FirstOrDefault();

                var asd = (from brecord in Context.ProcedimientoClinicoSet
                           where brecord.idMpat == miid
                           select new
                           {
                               brecord
                           }).ToList();

                var qwe = (from crecord in Context.CiclosEvaluacionSet
                           where crecord.idMpat == miid
                           select new
                           {
                               crecord
                           }).ToList();
                try
                {
                    //Comprueba si el resultado es vacio
                    if (xdf.arecord != null)
                    {
                        temp.Id = xdf.arecord.Id;
                        temp.DescripcionTestFood = xdf.arecord.DescripcionTestFood;
                        temp.ciclosMasticatorios = xdf.arecord.ciclosMasticatorios;
                        temp.nombre = xdf.arecord.nombre;
                        foreach (var a in asd)
                        {
                            S_Procedimiento procedimiento = new S_Procedimiento();
                            procedimiento.Id = a.brecord.Id;
                            procedimiento.idMpat = a.brecord.idMpat;
                            procedimiento.orden = a.brecord.orden;

                            listaProcedimientoClinico.Add(procedimiento);
                        }
                        foreach (var q in qwe)
                        {
                            S_CiclosEvaluacion ciclo = new S_CiclosEvaluacion();
                            ciclo.Id = q.crecord.Id;
                            ciclo.idMpat = q.crecord.idMpat;
                            ciclo.numeroCiclos = q.crecord.numeroCiclos;
                            ciclo.orden = q.crecord.orden;
                            listaCiclosEvaluacion.Add(ciclo);
                        }
                        temp.ListaProcedimientos = listaProcedimientoClinico;
                        temp.CiclosEvaluacion = listaCiclosEvaluacion;
                        salida = temp;
                        return true;
                    }
                    else
                    {
                        salida = temp;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error " + e);
                    salida = temp;
                    return false;
                }
            }
        }

        //-------------- PROCEDIMIENTOS VALIDACION MPAT ----------------------------------//
        public Boolean login(String Usuario, String Password){
            if (!addUser(Usuario, Password)) { return false; }
            using (ModeloDatosServidorContainer Context = new ModeloDatosServidorContainer())
            {
                var xdf = (from arecord in Context.UsuarioSet
                           where arecord.Usuario == Usuario
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                try
                {
                    Boolean resultado = false;
                    if ((xdf != null) && (Password.CompareTo(xdf.arecord.Password)==0))
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                    return resultado;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e);
                    return false;
                }
            }
            
        }
        private Boolean addUser(String Usuario, String Password)
        {
            using (ModeloDatosServidorContainer Context = new ModeloDatosServidorContainer())
            {
                UsuarioSet Db = new UsuarioSet();
                Db.Usuario = Usuario;
                Db.Password = Password;
                try
                {
                    Context.UsuarioSet.Add(Db);
                    Context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    return false;
                }
            }
        }
        //-------------- PROCEDIMIENTOS VALIDACION MPAT ----------------------------------//
        
        public Boolean procesoValidacionMPAT(S_Mpat mpat, List<Double[]> listaVectoresCaracteristicas,out Double[] vector )
        {
            Int32 idMpat;
            //Double[] vectorPesos= new Double[10];
            try
            {
                if (addMpat(mpat, out idMpat))
                {
                    //vectorPesos = entrenarRNA(Rna, idMpat);
                    //vector = vectorPesos;
                }
                else
                {
                    vector = null;
                    return false;
                }
                vector = null;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e);
                vector = null;
                return false;
            }

            
        }

    }
}
