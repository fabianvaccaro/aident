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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibreriaObjetos;
using MainCore;

namespace AiGumsPC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            
            InitializeComponent();
            Loaded += MyWindow_Loaded;
            Activated += window_Activated;
            Deactivated += window_Deactivated;

        }


        private void window_Activated(object sender, EventArgs e)
        {
            updateComboBox();
            this.txtCiclosMasticatorios.IsEnabled = true;
            this.txtCiclosEvaluacion.IsEnabled = true;
            this.estado.IsEnabled = true;
            this.txtTextFood.IsEnabled = true;
            this.btAltaTestFood.IsEnabled = true;
            this.txtProcedimiento.IsEnabled = true;

        }
        private void window_Deactivated(object sender, EventArgs e)
        {
            this.txtCiclosMasticatorios.IsEnabled = false;
            this.txtCiclosEvaluacion.IsEnabled = false;
            this.estado.IsEnabled = false;
            this.txtTextFood.IsEnabled = false;
            this.btAltaTestFood.IsEnabled = false;
            this.txtProcedimiento.IsEnabled = false;
        }
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            updateComboBox();
        }
        private void AltaTestFood(object sender, RoutedEventArgs e)
        {
            AltaTestFood win = new AltaTestFood();
            win.Show();

        }

        private void updateComboBox()
        {
            MainCore.Metodos metodos = new MainCore.Metodos();
            List<N_TestFood> lista = new List<N_TestFood>();

            lista = metodos.testFoodToList();
            txtTextFood.ItemsSource = lista;
            txtTextFood.DisplayMemberPath = "nombre";
            txtTextFood.SelectedValuePath = "id";
            txtTextFood.SelectedIndex = 0;

        }

        private void BajaTestFood(object sender, RoutedEventArgs e)
        {
            Metodos metodo = new Metodos();
            

            metodo.DeleteTestFood(Int32.Parse(this.txtTextFood.SelectedValue.ToString()));
            resetview();
        }
        private void resetview()
        {
            try
            {
                this.txtCiclosEvaluacion.Text = "";
                this.txtCiclosMasticatorios.Text = "";
                this.txtProcedimiento.Text = "";
                updateComboBox();
            }
            catch { }

    
        }

        private void grabarMpat_Click(object sender, RoutedEventArgs e)
        {
            Metodos metodo = new Metodos();

            try{
                metodo.validaMPAT(txtProcedimiento.Text, Int32.Parse(this.txtTextFood.SelectedValue.ToString()), Int32.Parse(txtCiclosMasticatorios.Text), Int32.Parse(txtCiclosEvaluacion.Text));
                resetview();
            }
            catch (Exception error)
            {
                Console.Write(error);
            }
        }

        private void bt_AddExperimento_Click(object sender, RoutedEventArgs e)
        {
            Metodos metodo = new Metodos();
            List<N_Paciente> lista = new List<N_Paciente>();

            try
            {
                metodo.validaExperimento(txtCodigo.Text, Int32.Parse(this.txtMPAT.SelectedValue.ToString()), Int32.Parse(this.txtNumeroPacientes.Text), lista);
                resetview();
            }
            catch (Exception error)
            {
                Console.Write(error);
            }
        }

        private void bt_AddMpat_Click(object sender, RoutedEventArgs e)
        {
            // habilito crear MPAT desde Alta de Experimento o solo se pueden crear experimentos con MPAT creadas?¿?¿
        }

        private void bt_AddPaciente_Click(object sender, RoutedEventArgs e)
        {
            AltaPaciente win = new AltaPaciente();
            win.Show();
        }
    }
}
