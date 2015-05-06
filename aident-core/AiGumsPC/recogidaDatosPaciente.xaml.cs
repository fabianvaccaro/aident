using LibreriaObjetos;
using MainCore;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for recogidaDatosPaciente.xaml
    /// </summary>
    public partial class recogidaDatosPaciente : Window
    {
        //Variables globales a la vista
        private Int32 ExperimentoId;
        private Int32 MpatId;
        private Int32 paciente;
        private List<N_CiclosEvaluacion> lista;
        private Int32 lado;
        private String path = "\\muestras\\";

        public recogidaDatosPaciente(Int32 idExperimento, Int32 idMpat, Int32 idPaciente, List<N_CiclosEvaluacion> listaCiclos)
        {
            InitializeComponent();
            ExperimentoId = idExperimento;
            paciente = idPaciente;
            MpatId = idMpat;
            lista = listaCiclos;
            Inicial(0);
            lado = 0;
        }

        private void Inicial(Int32 numOrden)
        {
            Metodos metodo = new Metodos();
            //Int32 ciclos = 0;
            //this.txtLado1.IsEnabled = false;
            if (lado == 0)
            {
                this.txtLado1.Text = "A";
            }
            else
            {
                this.txtLado1.Text = "B";
            }
            
            this.lbl_orden.Content = numOrden.ToString();
            this.txtCiclosMasticatorios1.IsEnabled = false;

            this.txtNumeroMuestra1.IsEnabled = false;
            this.txtCiclosMasticatorios1.Text = lista[numOrden].numeroCiclos.ToString();
            this.txtNumeroMuestra1.Text = this.ExperimentoId.ToString() + "_" + this.paciente.ToString() + "_" + txtLado1.Text + "_" + lista[numOrden].numeroCiclos.ToString();
            this.lbRutaImagen.Content = String.Empty;

        }

        private void Siguiente(object sender, RoutedEventArgs e)
        {
            Metodos metodo = new Metodos();
            Int32 ciclos = Int32.Parse(txtCiclosMasticatorios1.Text);
            //grabacion de datos
            
            try
            {
                if (lista[Int32.Parse(this.lbl_orden.Content.ToString()) + 1] != null)
                {
                    
                        try
                        {
                            //si no existe la carpeta temporal la creamos 
                            if (!(Directory.Exists(path)))
                            {
                                Directory.CreateDirectory(path);
                            }

                            if (Directory.Exists(path))
                            {
                                FileStream fs = new FileStream(this.txtNumeroMuestra1.Text + ".jpg", FileMode.Create, FileAccess.Write);

                                BinaryWriter bw = new BinaryWriter(fs);

                                byte[] imageBytes = metodo.GetEncodedImageData(imagen.Source, ".jpg");
                                bw.Write(imageBytes);
                                bw.Close();

                                fs.Close();
                                // Cambio de lado, posiblemente habrá que cambiar el orden del lado!!
                                //
                                //
                                //

                                if (lado == 0){
                                    lado = 1;
                                    Inicial(Int32.Parse(this.lbl_orden.Content.ToString()));
                                }
                                else {
                                    lado = 0;
                                    Inicial(Int32.Parse(this.lbl_orden.Content.ToString()) + 1);
                                }
                            }
                        }
                        catch (Exception errorC)
                        {
                            MessageBox.Show("Ha habido un error al intentar " + "crear el fichero temporal:" +
                                      Environment.NewLine + Environment.NewLine + path +
                                      Environment.NewLine + Environment.NewLine + errorC.Message,
                                      "Error al crear fichero temporal");
                        }

                    }
                    
            }
            catch (Exception exep)
            {
                this.Close();
                Console.WriteLine(exep.ToString());
            }
            

        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
               
            this.Close();

        }

        private void CapturaImagen(object sender, RoutedEventArgs e)
        {
            Metodos metodos = new Metodos();
            String nombre = this.txtNumeroMuestra1.Text;

            
            if (Clipboard.ContainsImage())
            {
                ImageSource imageSource;
                imageSource = metodos.ImageFromClipboardDib();
                try
                {
                    imagen.Source = imageSource;
                    if (nombre.CompareTo(string.Empty) == 0)
                    {
                        MessageBox.Show("Para obtener la imagen, debe completar el campo identificación");
                    }
                    else
                    {
                        String ruta = path + this.txtNumeroMuestra1.Text + ".jpg";
                                        
                    }
                    this.lbRutaImagen.Content = path + nombre + ".jpg";
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                }
                

                
            }
            else
            {
                MessageBox.Show("Portapapales vacio");
            }

            
        }

    }
}
