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

namespace AiGumsPC
{
    /// <summary>
    /// Interaction logic for verCiclosEvaluacion.xaml
    /// </summary>
    public partial class verCiclosEvaluacion : Window
    {
        public verCiclosEvaluacion(Int32 idMpat)
        {
            InitializeComponent();
            mostrarCiclosEvaluacion(idMpat);
        }
        private void mostrarCiclosEvaluacion(Int32 idMpat)
        {
            srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            var lista = new List<N_CiclosEvaluacion>();

            lista = mets.CiclosEvaluacionToList(idMpat).ToList();
            if (lista != null)
            {
                try
                {
                    this.grid_CiclosEvaluacion.ItemsSource = lista;
                }
                catch (NullReferenceException nullRefEx)
                {
                    Console.WriteLine("Error null exception" + nullRefEx);
                }
            }
            else
            {
                MessageBox.Show("Error al listar mpat");
            }
        }
        private void salir(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.Owner.Show();
        }
    }
}
