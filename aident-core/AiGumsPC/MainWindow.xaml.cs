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
        }

        private void btAltaTestFood_Click(object sender, RoutedEventArgs e)
        {
            Metodos metodos = new Metodos();
            List<N_TestFood> lista = new List<N_TestFood>();
            N_TestFood tfood = new N_TestFood();
            lista.Add(metodos.testFoodToList());


            cbTextFood.ItemsSource = lista ;
        }
        
    }
}
