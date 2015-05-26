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
    /// Interaction logic for ProcesamientoImagenes.xaml
    /// </summary>
    public partial class ProcesamientoImagenes : Window
    {
        public ProcesamientoImagenes()
        {
            InitializeComponent();
        }

        private void btCargarImagenMuestra_Click(object sender, RoutedEventArgs e)
        {
            ImageSource imagen = new BitmapImage(new Uri(".\\Images\\MtnChurch.jpg"));
            this.vistaImagenProcesar.Source = imagen;
        }

        private void buscarFicheros(){
            //if (slugName != null)
            //{
            //    List<string> filePathList = Directory.GetFiles(outputFolder.FullName).ToList();
            //    List<string> filePathList_ToBeDeleted = new List<string>();
            //    foreach (string filePath in filePathList)
            //    {                  
            //        if (Path.GetFileNameWithoutExtension(filePath).ToLower().Contains("_70x70"))
            //        {                           
            //            image1.Source = filePath;
            //        }                   
            //    }
            //    int count = 0;

            //    return count;
            //}
        }
    }
}
