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
        Metodos metodo = new Metodos();
        //private Int32 estado = 0;
        Int32 numeroPacienteExperimento = 0;
        //buscamos el experimento que estamos procesando
        N_Experimento experimento = new N_Experimento();
        //buscamos la mpat que estamos procesando
        N_Mpat mpat = new N_Mpat();



        public Muestras(Int32 idExperimento)
        {
            InitializeComponent();
            if (metodo.BuscaExperimento(idExperimento, experimento))
            {
                if (metodo.BuscaMpat(experimento.idMpat, mpat))
                {
                    estado.Content = "Todo ok";
                }
            }
            this.txtNumeroExperimento.Content = experimento.codigoExperimento;
            this.txtNumPaciente.Text = numeroPacienteExperimento.ToString();

        }
        private void siguienteImagen(object sender, RoutedEventArgs e)
        {
            //if(this.numeroPacienteExperimento <= experimento.)
        }
        private void obtenerMuestras(object sender, RoutedEventArgs e)
        {
            //Creamos nuevoPaciente
            N_Paciente paciente = new N_Paciente();
            N_HistoriaClinica nuevaHistoria = new N_HistoriaClinica();
            Metodos metodos = new Metodos(); 

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

            if (metodos.AddHistoriaClinica(nuevaHistoria, out idHistoriaClinica))
            {
                Int32 idPac = 0;
                paciente.idHistoriaClinica = idHistoriaClinica;
                if (metodos.AddPaciente(paciente, out idPac))
                {
                    //abrir ventana para obtener las muestras
                    recogidaDatosPaciente win = new recogidaDatosPaciente();
                    win.Show();
                    this.Hide();
                    //this.idPacienteExperimento++; //buscamos el segundo elemento
                }
                else
                {
                    estado.Content = "Error: Paciente no insertado";
                    return;
                }
            }
           

        }

        private void salir(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            this.Owner.Show();
        }
    }
}
