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
    /// Interaction logic for AltaTipoTestFood.xaml
    /// </summary>
    public partial class AltaTipoTestFood : Window
    {
        public AltaTipoTestFood()
        {
            InitializeComponent();
        }

        private void cancelarTipoTestFood(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grabarTipoTestFood(object sender, RoutedEventArgs e)
        {
            
            N_TipoTestFood ttf = new N_TipoTestFood();
            ttf.nombre = tbNombre.Text;
            ttf.descripcion = tbDescripcion.Text;
            

            srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            if (mets.AddTipoTestFood(ttf))
            {
                this.resetview();
                this.Close();
            }
            
        }
        private void resetview()
        {
            try
            {
                this.tbNombre.Text = "";
                this.tbDescripcion.Text = "";

            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
