﻿using LibreriaObjetos;
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
        //Variables para el control de imagenes
        CroppingAdorner _clp;
        FrameworkElement _felCur = null;
        Brush _brOriginal;

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
                //si no existe la carpeta temporal la creamos 
                if (!(Directory.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                }

                if (Directory.Exists(path))
                {
                    FileStream fs = new FileStream(this.txtNumeroMuestra1.Text + ".jpg", FileMode.Create, FileAccess.Write);

                    BinaryWriter bw = new BinaryWriter(fs);

                    byte[] imageBytes = metodo.GetEncodedImageData(this.img_vistaPrevia.Source, ".jpg");
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
                            try
                            {
                                if (lista[Int32.Parse(this.lbl_orden.Content.ToString()) + 1] != null)
                                {
                                    lado = 0;
                                    Inicial(Int32.Parse(this.lbl_orden.Content.ToString()) + 1);
                                }
                            }
                            catch (Exception exp)
                            {
                                Console.WriteLine("Error: " + exp.ToString());
                                this.Close();
                            }
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

        private void Cerrar(object sender, RoutedEventArgs e)
        {
               
            this.Close();

        }

        private void CapturaImagen(object sender, RoutedEventArgs e)
        {
            Metodos metodos = new Metodos();
            String nombre = this.txtNumeroMuestra1.Text;

            
            //if (Clipboard.ContainsImage())
            //{
                ImageSource imageSource;
                imageSource = this.img_vistaPrevia.Source;
                try
                {
                    this.img_vistaPrevia.Source = imageSource;
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
                

                
            //}
            //else
            //{
            //    MessageBox.Show("Portapapales vacio");
            //}
        }

        #region
        private void RemoveCropFromCur()
        {
            //AdornerLayer aly = AdornerLayer.GetAdornerLayer(_felCur);
            //aly.Remove(_clp);
        }

        private void AddCropToElement(FrameworkElement fel)
        {
            bool rbRed = false;
            if (_felCur != null)
            {
                RemoveCropFromCur();
            }
            Rect rcInterior = new Rect(
                fel.ActualWidth * 0.2,
                fel.ActualHeight * 0.2,
                fel.ActualWidth * 0.6,
                fel.ActualHeight * 0.6);
            AdornerLayer aly = AdornerLayer.GetAdornerLayer(fel);
            _clp = new CroppingAdorner(fel, rcInterior);
            aly.Add(_clp);
            this.img_vistaPrevia.Source = _clp.BpsCrop();
            _clp.CropChanged += CropChanged;
            _felCur = fel;
            if (rbRed != true)
            {
                SetClipColorGrey();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddCropToElement(imgChurch);
            _brOriginal = _clp.Fill;
            RefreshCropImage();
        }

        private void RefreshCropImage()
        {
            if (_clp != null)
            {
                Rect rc = _clp.ClippingRectangle;

                tblkClippingRectangle.Text = string.Format(
                    "Clipping Rectangle: ({0:N1}, {1:N1}, {2:N1}, {3:N1})",
                    rc.Left,
                    rc.Top,
                    rc.Right,
                    rc.Bottom);
                this.img_vistaPrevia.Source = _clp.BpsCrop();
            }
        }

        private void CropChanged(Object sender, RoutedEventArgs rea)
        {
            RefreshCropImage();
        }

        private void CropControls_Checked(object sender, RoutedEventArgs e)
        {
            if (dckControls != null)
            {
                dckControls.Visibility = Visibility.Visible;
                AddCropToElement(dckControls);
                RefreshCropImage();
            }
        }

        private void CropImage_Checked(object sender, RoutedEventArgs e)
        {
            if (dckControls != null && imgChurch != null)
            {
                dckControls.Visibility = Visibility.Hidden;
                AddCropToElement(imgChurch);
                RefreshCropImage();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshCropImage();
        }

        private void SetClipColorRed()
        {
            if (_clp != null)
            {
                _clp.Fill = _brOriginal;
            }
        }

        private void SetClipColorGrey()
        {
            if (_clp != null)
            {
                Color clr = Colors.Black;
                clr.A = 110;
                _clp.Fill = new SolidColorBrush(clr);
            }
        }

        private void Red_Checked(object sender, RoutedEventArgs e)
        {
            SetClipColorRed();
        }

        private void Grey_Checked(object sender, RoutedEventArgs e)
        {
            SetClipColorGrey();
        }


        #endregion

        private void bt_Escanear_Click(object sender, RoutedEventArgs e)
        {
            Metodos metodos = new Metodos();
            String nombre = this.txtNumeroMuestra1.Text;


            if (Clipboard.ContainsImage())
            {
            ImageSource imageSource;
            imageSource = this.imgChurch.Source;
            try
            {
                this.img_vistaPrevia.Source = imageSource;
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