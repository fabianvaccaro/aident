using LibreriaObjetos;
using MainCore;
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
    /// Interaction logic for verProcedimientoClinico.xaml
    /// </summary>
    public partial class verProcedimientoClinico : Window
    {
        public verProcedimientoClinico(Int32 idMpat)
        {
            InitializeComponent();
            PintaDatos(idMpat);
        }

        private void PintaDatos(Int32 idMpat)
        {
            //srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            Metodos mets = new Metodos();
            var listaProcedimientos = new List<N_ProcedimientoClinico>();
            try
            {
                this.prueba.Content = idMpat.ToString();
            }
            catch (NullReferenceException nullRefEx)
            {
                Console.WriteLine("Error null exception" + nullRefEx);
            }

            listaProcedimientos = mets.ProcedimientoToList(idMpat).ToList();
            if (listaProcedimientos != null)
            {
                try
                {
                    grid_ProcedimientoClinico.ItemsSource = listaProcedimientos;
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
