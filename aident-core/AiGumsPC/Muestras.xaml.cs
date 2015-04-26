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
        N_Experimento nuevoExperimento = new N_Experimento();
        Int32 idPacienteExperimento = 0;
        private Int32 experimento; 
        //nuevoExperimento = "1234"; // obtener Código Experimento


        public Muestras(Int32 idExperimento)
        {
            experimento = idExperimento;
            InitializeComponent();
            idPacienteExperimento++;
            this.txtNumeroExperimento.Content = experimento;
            this.txtNumPaciente.Text = idPacienteExperimento.ToString();

        }
        private void siguienteImagen(object sender, RoutedEventArgs e)
        {
            //if (estado == 0) {
            //    N_Muestra muestra = new N_Muestra();

            //    txtCiclosMasticatorios.Text = "0";
            //    txtLado.Text = "A";
            //    txtNumeroMuestra.Text = String.Empty;
            //    txtNumPaciente.Text = String.Empty;
            //    imagen = capturaImagen();
            //    muestra.lado = txtLado;
            //    muestra.numero = txtNumeroMuestra;
            //    muestra.paciente = txtNumPaciente;
            //    muestra.imagen = imagen;
            //    if (addMuestra(muestra))
            //        siguiente;
            //    else
            //    {
            //        error;
            //    }
            //}
        }
        private void obtenerMuestras(object sender, RoutedEventArgs e)
        {
            //grabarPaciente
            N_Paciente paciente = new N_Paciente();
            N_HistoriaClinica nuevaHistoria = new N_HistoriaClinica();
            Metodos metodos = new Metodos(); 
            estado.DataContext = "Introduce datos. ";

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
           // paciente.id = nuevoExperimento.id;
            paciente.idHistoriaClinica = 0;
            paciente.idPacienteExp = this.experimento;
            paciente.nombre = txtNombre.Text;
            paciente.ubicacion = txtUbicacion.Text;
            Int32 idHistoriaClinica = 0;

           if (metodos.addHistoriaClinica(nuevaHistoria, out idHistoriaClinica))
           {
               Int32 idPac = 0;
               paciente.idHistoriaClinica = idHistoriaClinica;
                if (metodos.addPaciente(paciente, out idPac))
                {
                    
                    this.idPacienteExperimento++;
                    //abrir ventana para obtener las muestras
                    recogidaDatosPaciente win = new recogidaDatosPaciente();
                    win.Show();
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
        }
    }
}
