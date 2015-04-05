using LibreriaObjetos;
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

namespace AiGumsPC
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AltaPaciente : Window
    {
        public AltaPaciente()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
            Activated += window_Activated;
            
        }

        private void window_Activated(object sender, EventArgs e)
        {
            //updateComboBox();
            //this.btCrearTipo.IsEnabled = true;
            //this.cbTipo.IsEnabled = true;
            //this.tbCaracteristicaMonitorzadas.IsEnabled = true;
            //this.tbDescripcion.IsEnabled = true;
            //this.tbNombre.IsEnabled = true;
        }
        private void window_Deactivated(object sender, EventArgs e)
        {
            
        //    this.btCrearTipo.IsEnabled = false;
        //    this.cbTipo.IsEnabled = false;
        //    this.tbCaracteristicaMonitorzadas.IsEnabled = false;
        //    this.tbDescripcion.IsEnabled = false;
        //    this.tbNombre.IsEnabled = false;
        //
        }
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            //updateComboBox();
        }
        private void updateComboBox(List<N_Paciente> lista)
        {
            //MainCore.Metodos metodos = new MainCore.Metodos();
            //List<N_TipoTestFood> lista = new List<N_TipoTestFood>();

            //lista = metodos.tipoTestFoodToList();
            cbTipo.ItemsSource = lista;
            cbTipo.DisplayMemberPath = "nombre";
            cbTipo.SelectedValuePath = "id";
            cbTipo.SelectedIndex = 0;
        }

        private void grabarPaciente(object sender, RoutedEventArgs e)
        {
            Metodos metodo = new Metodos();
            N_Paciente elemento = new N_Paciente();
            List<N_Paciente> lista = new List<N_Paciente>();

            elemento.identificacion = this.txtDNI.Text;
            elemento.nombre = this.txtNombre.Text;
            elemento.sexo = "Hombre"; // pasar de radiobutton a texto
            elemento.ubicacion = this.txtUbicacion.Text;

            lista.Add(elemento);
            updateComboBox(lista);
            this.Close();
            
        }

        private void cancelarPaciente(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void resetview()
        {
            try
            {
                //this.
            }
            catch (Exception e)
            {
                barraEstado.DataContext = e;
            }
        }


    }
}
