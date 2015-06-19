using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MainCore;
using LibreriaObjetos;

namespace AiGumsPC
{
    /// <summary>
    /// Interaction logic for Muestras.xaml
    /// </summary>
    public partial class Muestras : Window
    {
        //private Int32 estado = 0;
        Int32 PacienteActual = 1;

        //buscamos el experimento que estamos procesando
        N_Experimento experimento = new N_Experimento();

        //buscamos la mpat que estamos procesando
        N_Mpat mpat = new N_Mpat();

        //Lista de Pacientes a procesar
        List<N_Paciente> listaPacientes = new List<N_Paciente>();

        public Muestras(Int32 idExperimento)
        {
            //srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            Metodos mets = new Metodos();
            InitializeComponent();
            if (mets.BuscaExperimento(idExperimento,out experimento))
            {
                if (mets.BuscaMpat(experimento.idMpat, out mpat))
                {
                    estado.Content = "Todo ok";
                }
            }
            this.btNext.IsEnabled = false;
            this.lbTitulo.Content = "Muestras del Experimento: " + experimento.codigoExperimento;
            this.lbNumeroPaciente.Content = PacienteActual.ToString() + " de " + experimento.numeroPacientes + " Pacientes";

        }

        private void siguienteImagen(object sender, RoutedEventArgs e)
        {
            
        }
        private void obtenerMuestras(object sender, RoutedEventArgs e)
        {
            //srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            Metodos mets = new Metodos();
            
            //Creamos nuevoPaciente
            N_Paciente paciente = new N_Paciente();
            N_HistoriaClinica nuevaHistoria = new N_HistoriaClinica();
            

            // Actualizamos el estado
            estado.DataContext = "Introduce datos. ";
            
            // Rellena nuevoPaciente
            if (rb_m.IsChecked == true)
            {
                paciente.sexo = "HOMBRE";       
            }
            else if (rb_f.IsChecked == true)
            {
                paciente.sexo = "MUJER";
            }
            else
            {
                estado.DataContext = "Error: Debe seleccionar el sexo del paciente";
                return;
            }
            paciente.identificacion = txtDNI.Text;
            paciente.idHistoriaClinica = 0;
            paciente.idPacienteExp = experimento.id;
            paciente.nombre = txtNombre.Text;
            paciente.ubicacion = txtUbicacion.Text;
            Int32 idHistoriaClinica = 0;

            // Añado la historiaClinica del paciente a la BD
            if (mets.AddHistoriaClinica(nuevaHistoria, out idHistoriaClinica))
            {
                Int32 idPac = 0;
                paciente.idHistoriaClinica = idHistoriaClinica;
                //Añado el paciente a la BD
                if (mets.AddPaciente(paciente, out idPac))
                {
                    // Comienzo a buscar el primer numero de Ciclos para comenzar la recogida de datos
                    var listaCi = mets.CiclosEvaluacionToList(mpat.id);
                    if(listaCi != null)
                    {
                        // Llamo a la ventana que recogerá los datos del primer nCiclos
                        recogidaDatosPaciente win = new recogidaDatosPaciente(experimento.id, experimento.idMpat, idPac, listaCi);
                        win.Owner = this;
                        win.Show();
                        this.btObtenerMuestras.IsEnabled = false;
                        this.btNext.IsEnabled = true;
                    }
                    else
                    {
                        estado.Content = "El experimento no tiene recogida de datos";
                    }
                }
                else
                {
                    estado.Content = "Error: Paciente no insertado";
                    return;
                }
            }    
        }

        private void Siguiente(object sender, RoutedEventArgs e)
        {
            //srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            Metodos mets = new Metodos();

            if (PacienteActual < experimento.numeroPacientes)
            {
                resetView();
                PacienteActual++;
                this.lbNumeroPaciente.Content = PacienteActual.ToString() + " de " + experimento.numeroPacientes + " Pacientes";
                this.btObtenerMuestras.IsEnabled = true;
                this.btNext.IsEnabled = false;
            }
            else
            {
                resetView();
                this.lbNumeroPaciente.Content = "Pacientes Completados!";
                MessageBox.Show("Recogida de Datos Finalizada");

                experimento.procesado = true;
                if (mets.UpdateExperimento(experimento))
                {
                    this.Close();
                    this.Owner.Show();
                }
                else
                {
                    estado.Content = "Error actualizando experimento";
                }                                    
                
            }
        }

        private void resetView()
        {
            this.lbNumeroPaciente.Content = "# de " + experimento.numeroPacientes + " Pacientes";
            this.txtDNI.Text = "";
            this.txtEdad.Text = "";
            this.txtNombre.Text = "";
            this.txtUbicacion.Text = "";
            this.rb_f.IsChecked = false;
            this.rb_m.IsChecked = false;
            this.estado.Content = "";
        }
    }
}
