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
using Microsoft.Win32;
using Accord.Controls;
using Accord.IO;
using Accord.Math;
using ProcImagen;
using Accord.Imaging.Converters;
using System.Drawing;
using ColorMine.ColorSpaces;

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
            //Loaded += MyWindow_Loaded;
            //Activated += window_Activated;
            //Deactivated += window_Deactivated;
        }

        private void window_Activated(object sender, EventArgs e)
        {
            updateComboBox();
            //this.txtCiclosMasticatorios.IsEnabled = true;
            //this.txtCiclosEvaluacion.IsEnabled = true;
            //this.estado.IsEnabled = true;
            //this.txtTextFood.IsEnabled = true;
            //this.btAltaTestFood.IsEnabled = true;
            //this.txtProcedimiento.IsEnabled = true;
        }

        private void window_Deactivated(object sender, EventArgs e)
        {
            //this.txtCiclosMasticatorios.IsEnabled = false;
            //this.txtCiclosEvaluacion.IsEnabled = false;
            //this.estado.IsEnabled = false;
            //this.txtTextFood.IsEnabled = false;
            //this.btAltaTestFood.IsEnabled = false;
            //this.txtProcedimiento.IsEnabled = false;
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            updateComboBox();
        }

        private void AltaTestFood(object sender, RoutedEventArgs e)
        {
            AltaTestFood win = new AltaTestFood();
            win.Show();
            win.Owner = this;
            this.Hide();

        }

        private void updateComboBox()
        {
            srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            //MainCore.Metodos metodos = new MainCore.Metodos();
            List<N_TestFood> lista = new List<N_TestFood>();
            lista = mets.TestFoodToList().ToList();

            txtTextFood.ItemsSource = lista;
            txtTextFood.DisplayMemberPath = "nombre";
            txtTextFood.SelectedValuePath = "id";
            txtTextFood.SelectedIndex = 0;

            List<N_Mpat> listaMPAT = new List<N_Mpat>();
            listaMPAT = mets.MpatToList().ToList();

            txtMPAT.ItemsSource = listaMPAT;
            txtMPAT.DisplayMemberPath = "nombre";
            txtMPAT.SelectedValuePath = "id";
            txtMPAT.SelectedIndex = 0;

            List<N_Experimento> listaExperimento = new List<N_Experimento>();
            listaExperimento = mets.ExperimentoToList().ToList();

            this.txtDiagExperimentos.ItemsSource = listaExperimento;
            this.txtDiagExperimentos.DisplayMemberPath = "codigoExperimento";
            this.txtDiagExperimentos.SelectedValuePath = "id";
            this.txtDiagExperimentos.SelectedIndex = 0;

        }

        private void BajaTestFood(object sender, RoutedEventArgs e)
        {
            srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();

            mets.DeleteTestFood(Int32.Parse(this.txtTextFood.SelectedValue.ToString()));
            resetview();
        }

        private void resetview()
        {
            try
            {
                this.txtCiclosEvaluacion.Text = String.Empty;
                this.txtCiclosMasticatorios.Text = String.Empty;
                this.txtProcedimiento.Text = String.Empty;
                this.txtCodigoExpermiento.Text = String.Empty;
                this.txtListaCiclosEvaluacion.Items.Clear();
                this.txtListaProcemientos.Items.Clear();
                this.txtNombre.Text = String.Empty;

                //txtListaCiclosEvaluacion.ItemsSource = new List<N_CiclosEvaluacion>();
                //txtListaProcemientos.ItemsSource = new List<N_ProcedimientoClinico>();
                updateComboBox();
                cargarDatosExperimento();
                cargarDatosDiagnostico();
                cargarDatosMPAT();
            }
            catch { }


        }

        private void resetviewExperimento()
        {
            try
            {
                this.txtCodigoExpermiento.Text = "";
                this.txtNumeroPacientes.Text = "";
                this.txtProcedimiento.Text = "";
                updateComboBox();
            }
            catch { }


        }

        private void grabarMpat(object sender, RoutedEventArgs e)
        {
            //Metodos metodo = new Metodos();
            srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            N_Mpat mpat = new N_Mpat();
            Int32 idMpat = 0;

            mpat.nombre = txtNombre.Text;
            mpat.idTestFood = Int32.Parse(txtTextFood.SelectedValue.ToString());
            mpat.CiclosMasticatorios = Int32.Parse(txtCiclosMasticatorios.Text);

            //for (Int32 i = 0; i<)
            if (mets.AddMPAT(mpat, out idMpat))
            {

                // inserta los procedimientos clinicos en la base de datos
                foreach (N_ProcedimientoClinico procedimento in txtListaProcemientos.Items)
                {
                    procedimento.idMpat = idMpat;
                    if (mets.AddProcedimiento(procedimento))
                    {
                        estado.DataContext = "Todo Ok";
                    }
                }
                // inserta los ciclos de evaluación en la base de datos
                foreach (N_CiclosEvaluacion ciclos in txtListaCiclosEvaluacion.Items)
                {
                    ciclos.idMpat = idMpat;
                    if (mets.AddCiclosEvaluacion(ciclos))
                    {
                        estado.DataContext = "Todo Ok";
                    }
                }
                resetview();
            }


        }

        private void AltaExperimento(object sender, RoutedEventArgs e)
        {
            //Metodos metodo = new Metodos();
            srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();

            try
            {
                N_Experimento nuevoExperimento = new N_Experimento();
                //nuevoExperimento.idPaciente;
                nuevoExperimento.idMpat = Int32.Parse(txtMPAT.SelectedValue.ToString());
                nuevoExperimento.numeroPacientes = Int32.Parse(txtNumeroPacientes.Text);
                nuevoExperimento.codigoExperimento = txtCodigoExpermiento.Text;

                if (mets.AddExperimento(nuevoExperimento))
                {
                    resetviewExperimento();
                }
                else
                {

                }
            }
            catch (Exception error)
            {
                Console.Write(error);
            }
        }

        private void cargarDatosMPAT()
        {
            srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();

            List<N_Mpat> listaMPAT = new List<N_Mpat>();
            listaMPAT = mets.MpatToList().ToList();
            if (listaMPAT != null)
            {
                grid_listado_mpat.ItemsSource = listaMPAT;
            }
            else
            {
                MessageBox.Show("Error al listar mpat");
            }
        }

        private void cargarDatosExperimento()
        {
            srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            List<N_Experimento> listaExperimento = new List<N_Experimento>();
            listaExperimento = mets.ExperimentoToList().ToList();
            if (listaExperimento != null)
            {
                grid_listado_experimento.ItemsSource = listaExperimento;
            }
            else
            {
                MessageBox.Show("Error al listar mpat");
            }
        }

        private void cargarDatosDiagnostico()
        {
            srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            List<N_Experimento> listaExperimento = new List<N_Experimento>();
            listaExperimento = mets.ExperimentoToListNoProcesados().ToList();
            if (listaExperimento != null)
            {
                grid_listado_diagnostico.ItemsSource = listaExperimento;
            }
            else
            {
                MessageBox.Show("Error al listar mpat");
            }
        }

        private void procesarExperimento(object sender, RoutedEventArgs e)
        {
            Int32 idExperimento = 0;
            idExperimento = Int32.Parse(this.txtDiagExperimentos.SelectedValue.ToString());
            Muestras win = new Muestras(idExperimento);
            win.Show();
            this.Hide();
            win.Owner = this;

        }

        private void RefrescaExperimento(object sender, RoutedEventArgs e)
        {
            cargarDatosExperimento();
        }

        private void RefrescaMpat(object sender, RoutedEventArgs e)
        {
            cargarDatosMPAT();
        }

        private void addCicloEvaluacionLista(object sender, RoutedEventArgs e)
        {
            var listaCiclosEvaluacion = new List<N_CiclosEvaluacion>();
            //Obtener elementos del control
            foreach (N_CiclosEvaluacion ciclos in this.txtListaCiclosEvaluacion.Items)
            {
                listaCiclosEvaluacion.Add(ciclos);
            }

            //Añade un nuevo procedimiento a la lista
            N_CiclosEvaluacion ciclo = new N_CiclosEvaluacion();
            ciclo.idMpat = 0;
            ciclo.numeroCiclos = Int32.Parse(txtCiclosEvaluacion.Text);
            ciclo.orden = Int32.Parse(txtListaProcemientos.Items.Count.ToString()) + 1;

            //Si se ha seleccionado un item de la lista
            if (this.txtListaCiclosEvaluacion.SelectedIndex > -1)
            {
                var miOrden = Int32.Parse(txtListaCiclosEvaluacion.SelectedValue.ToString());
                var xdf = (from arecord in listaCiclosEvaluacion
                           where arecord.orden == miOrden
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                if (xdf.arecord != null)
                {
                    ciclo.orden = miOrden;
                    foreach (var elemento in listaCiclosEvaluacion)
                    {
                        if (elemento.orden >= miOrden)
                        {
                            elemento.orden = elemento.orden + 1;
                        }
                    }
                }

            }
            //insertar el nuevo elemento

            listaCiclosEvaluacion.Add(ciclo);


            //Ordenar lista
            if (this.txtListaCiclosEvaluacion.SelectedIndex > -1)
            {
                var listaResultante = listaCiclosEvaluacion.OrderBy(x => x.orden).ToList();
                listaCiclosEvaluacion = listaResultante;
                this.txtListaCiclosEvaluacion.SelectedIndex = -1;
            }
            //dibujarlista
            txtListaCiclosEvaluacion.ItemsSource = listaCiclosEvaluacion;
            txtListaCiclosEvaluacion.SelectedValuePath = "orden";
            txtListaCiclosEvaluacion.DisplayMemberPath = "numeroCiclos";

            //txtListaProcemientos.Items.Add(procemiento);
            txtCiclosEvaluacion.Text = String.Empty;
        }

        private void clearCiclosEvaluacionLista(object sender, RoutedEventArgs e)
        {
            txtListaCiclosEvaluacion.Items.Clear();
        }

        private void clearListaProcemientos(object sender, RoutedEventArgs e)
        {
            txtListaProcemientos.ItemsSource = new List<N_ProcedimientoClinico>();
        }

        private void addProcemientoLista(object sender, RoutedEventArgs e)
        {
            var listaProcedimientosClinicos = new List<N_ProcedimientoClinico>();
            //Obtener elementos del control
            foreach (N_ProcedimientoClinico prct in txtListaProcemientos.Items)
            {
                listaProcedimientosClinicos.Add(prct);
            }

            //Añade un nuevo procedimiento a la lista
            N_ProcedimientoClinico procemiento = new N_ProcedimientoClinico();
            procemiento.idMpat = 0;
            procemiento.descripcion = txtProcedimiento.Text;

            procemiento.orden = Int32.Parse(txtListaProcemientos.Items.Count.ToString()) + 1;

            //Si se ha seleccionado un item de la lista
            if (txtListaProcemientos.SelectedIndex > -1)
            {
                var miOrden = Int32.Parse(txtListaProcemientos.SelectedValue.ToString());
                var xdf = (from arecord in listaProcedimientosClinicos
                           where arecord.orden == miOrden
                           select new
                           {
                               arecord
                           }).FirstOrDefault();
                if (xdf.arecord != null)
                {
                    procemiento.orden = miOrden;
                    foreach (var elemento in listaProcedimientosClinicos)
                    {
                        if (elemento.orden >= miOrden)
                        {
                            elemento.orden = elemento.orden + 1;
                        }
                    }
                }

            }
            //insertar el nuevo elemento

            listaProcedimientosClinicos.Add(procemiento);


            //Ordenar lista
            if (txtListaProcemientos.SelectedIndex > -1)
            {
                var listaResultante = listaProcedimientosClinicos.OrderBy(x => x.descripcion).ToList();
                listaProcedimientosClinicos = listaResultante;
                txtListaProcemientos.SelectedIndex = -1;
            }
            //dibujarlista
            txtListaProcemientos.ItemsSource = listaProcedimientosClinicos;
            txtListaProcemientos.SelectedValuePath = "orden";
            txtListaProcemientos.DisplayMemberPath = "descripcion";

            //txtListaProcemientos.Items.Add(procemiento);
            txtProcedimiento.Text = String.Empty;
        }

        private void verProcedimiento(object sender, RoutedEventArgs e)
        {
            object Id = ((Button)sender).CommandParameter;
            Int32 idMpat = (Int32)Id;
            verProcedimientoClinico win = new verProcedimientoClinico(idMpat);
            win.Owner = this;
            //this.Hide();
            win.Show();
        }

        private void verCiclosEvaluacion(object sender, RoutedEventArgs e)
        {
            object Id = ((Button)sender).CommandParameter;
            Int32 idMpat = (Int32)Id;
            verCiclosEvaluacion win = new verCiclosEvaluacion(idMpat);
            win.Owner = this;
            //this.Hide();
            win.Show();
        }

        private void RefrescaDiagnostico(object sender, RoutedEventArgs e)
        {
            srvweb.NegocioServiceClient mets = new srvweb.NegocioServiceClient();
            List<N_Experimento> listaExperimento = new List<N_Experimento>();
            listaExperimento = mets.ExperimentoToList().ToList();
            if (listaExperimento != null)
            {
                grid_listado_diagnostico.ItemsSource = listaExperimento;
            }
            else
            {
                MessageBox.Show("Error al listar mpat");
            }
        }

        private void DeleteDiagnostico(object sender, RoutedEventArgs e)
        {
            Metodos metodo = new Metodos();

            object Id = ((Button)sender).CommandParameter;
            List<N_Paciente> listaPacientesBorrar = metodo.PacientesExpToList(Int32.Parse(Id.ToString()));
            if (metodo.DeleteExperimento(Int32.Parse(Id.ToString())))
            {
                foreach (N_Paciente paciente in listaPacientesBorrar)
                {
                    if (metodo.DeletePaciente(paciente.id))
                    {

                    }
                    else
                    {
                        Console.WriteLine("Error borrando pacientes");
                    }
                }
            }
        }

        private void BajaMpat(object sender, RoutedEventArgs e)
        {
            //Metodos metodo = new Metodos();
            //if (metodo.DeleteMpat(Int32.Parse(this.txtMPAT.SelectedValue.ToString())))
            //{
            //    estado.DataContext = txtMPAT.ToString() + " eliminada";
            //}
            //resetview();
            //NO DAR DE BAJA NADA, SOlO ELIMINAR VISIBILIDAD
        }

        private void btCargarImagenMuestra_Click(object sender, RoutedEventArgs e)
        {
            Procesamiento procesamiento = new Procesamiento();
            ArrayToImage arrayToImage;

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                //Double hs = 4;
                //Double hr = 2;
                //BitmapImage imagen = new BitmapImage(new Uri(op.FileName));
                //arrayToImage = new ArrayToImage((int)imagen.Width, (int)imagen.Height, min: -1, max: +1);
                //Bitmap result; arrayToImage.Convert(imagen, out result);
                //this.vistaImagenProcesar.Source = new BitmapImage(new Uri(op.FileName));
                //FeatureImage imagen_1 = procesamiento.runMeanShift(imagen, hs, hr);
            }




        }

        private void bt_Segmentar_Click (object sender, RoutedEventArgs e)
        {

            Bitmap image1;
            Procesamiento pro = new Procesamiento();

            //protected static void ExpectedValuesForKnownColor(IColorSpace knownColor, ILuv expectedColor)
            //{
            //    var target = knownColor.To<Luv>();

            //    Assert.IsTrue(CloseEnough(expectedColor.L,target.L),"(L)" + expectedColor.L + " != " + target.L);
            //    Assert.IsTrue(CloseEnough(expectedColor.U,target.U),"(U)" + expectedColor.U + " != " + target.U);
            //    Assert.IsTrue(CloseEnough(expectedColor.V,target.V),"(V)" + expectedColor.V + " != " + target.V);
            //}

            try
            {
                // Retrieve the image.
                image1 = new Bitmap(@"C:\Users\Jaime\Source\Repos\AiDent\aident-core\AiGumsPC\Images\MtnChurch.jpg", true);
                int x = image1.Size.Height, y = image1.Size.Width;

                // Matrices de componentes de los espacios de colores de RGB, Luv, HSV y RGB normalizado.
                // Matrices por capas y sin mascarasde dimensiones imagen1.H * imagen1.W

                Double[,] xR = new Double[y, x];
                Double[,] xG = new Double[y, x];
                Double[,] xB = new Double[y, x];

                Double[,] xL = new Double[y, x];
                Double[,] xu = new Double[y, x];
                Double[,] xv = new Double[y, x];

                Double[,] xH = new Double[y, x];
                Double[,] xS = new Double[y, x];
                Double[,] xV = new Double[y, x];

                Double[,] xRn = new Double[y, x];
                Double[,] xGn = new Double[y, x];
                Double[,] xBn = new Double[y, x];

                // Loop through the images pixels to reset color.
                for (x = 0; x < image1.Width; x++)
                {
                    for (y = 0; y < image1.Height; y++)
                    {
                        System.Drawing.Color pixelColor = image1.GetPixel(x, y);
                        
                        //Calculo RGBn
                        
                        double exponent = 2;
                        double redDouble = Convert.ToDouble(pixelColor.R);
                        double blueDouble = Convert.ToDouble(pixelColor.B);
                        double greenDouble = Convert.ToDouble(pixelColor.G);

                        double redResult = Math.Pow(redDouble, exponent);
                        double blueResult = Math.Pow(blueDouble, exponent);
                        double greenResult = Math.Pow(greenDouble, exponent);

                        double totalResult = redResult + blueResult + greenResult;

                        //Almaceno el pixel en RGBn
                        xRn[y,x] = Convert.ToDouble(pixelColor.R) / Math.Sqrt(totalResult);
                        xGn[y,x] = Convert.ToDouble(pixelColor.G) / Math.Sqrt(totalResult);
                        xBn[y,x] = Convert.ToDouble(pixelColor.B) / Math.Sqrt(totalResult);


                        
                        //Almaceno el pixel en RGB 
                        xR[ y, x] = Convert.ToDouble(pixelColor.R);
                        xG[ y, x] = Convert.ToDouble(pixelColor.G);
                        xB[ y, x] = Convert.ToDouble(pixelColor.B);
                        
                        //Creo un elemento RGB 
                        var rgb = new Rgb();// 
                        rgb.R = pixelColor.R;
                        rgb.G = pixelColor.G;
                        rgb.B = pixelColor.B;

                        // Convierto RGB a LUV y HSV mediante ColorMine
                        Luv luv = rgb.To<Luv>();
                        Hsv hsv = rgb.To<Hsv>();

                        //Almaceno el Pixel en LUV
                        xL[y,x] = luv.L;
                        xu[y,x] = luv.U;
                        xv[y,x] = luv.V;

                        //Almaceno el Pixel en HSV
                        xH[y,x] = hsv.H;
                        xS[y,x] = hsv.S;
                        xV[y,x] = hsv.V;

                        System.Drawing.Color newColor = System.Drawing.Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);

                        image1.SetPixel(x, y, newColor);
                    }
                }

                //Calculo las entradas de SacarMascara
                Double[,] Alterno = pro.Alterno(xu, image1.Width, image1.Height);
                Double umbral= pro.calcularUmbral(Alterno);
                Int32 tipoUmbral = 0;// 0 = mayor que ; 1 = menor que
                if (umbral < 0)
                {
                    tipoUmbral = 1;
                }
                 
                // funcion MatrizResultado SacarMascara(Alterno, umbral, tipoUmbral(mayor o menor) por defecto mayor)
                Double[,] mascara = pro.SacarMascara(Alterno, umbral, tipoUmbral);

                // Obtener los valores de las posiciones que quedan dentro del AOI
                Double[] AOIxR = pro.matrizCapaAOIaVector(xR, mascara);
                Double[] AOIxG = pro.matrizCapaAOIaVector(xG, mascara);
                Double[] AOIxB = pro.matrizCapaAOIaVector(xB, mascara);

                Double[] AOIxL = pro.matrizCapaAOIaVector(xL, mascara);
                Double[] AOIxu = pro.matrizCapaAOIaVector(xu, mascara);
                Double[] AOIxv = pro.matrizCapaAOIaVector(xv, mascara);

                Double[] AOIxH = pro.matrizCapaAOIaVector(xH, mascara);
                Double[] AOIxS = pro.matrizCapaAOIaVector(xS, mascara);
                Double[] AOIxV = pro.matrizCapaAOIaVector(xV, mascara);

                Double[] AOIxRn = pro.matrizCapaAOIaVector(xRn, mascara);
                Double[] AOIxGn = pro.matrizCapaAOIaVector(xGn, mascara);
                Double[] AOIxBn = pro.matrizCapaAOIaVector(xBn, mascara);
                
                // Obtener las Features de los vectoresAOI de cada capa
                Double[] vectorCaracteristicas = pro.calcularFeatures(AOIxR, AOIxG, AOIxB, AOIxL, AOIxu, AOIxv, AOIxH, AOIxS, AOIxV, AOIxRn, AOIxGn, AOIxBn);

                double Hr = Double.Parse(this.tb_HR.Text);
                this.vistaImagenResultado.Source = ConvertDrawingImageToWPFImage(pro.runMeanShift(image1, Hr , 2)).Source;
                // Set the PictureBox to display the image.
                this.vistaImagenProcesar.Source = ConvertDrawingImageToWPFImage(image1).Source;

                // Display the pixel format in Label1.
                this.lb_RutaImagenMuestra.Content = "Pixel format: " + image1.PixelFormat.ToString();

            }
            catch (ArgumentException)
            {
                MessageBox.Show("There was an error." +
                    "Check the path to the image file.");
            }

        }

        private System.Windows.Controls.Image ConvertDrawingImageToWPFImage(System.Drawing.Image gdiImg)
        { 
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();

            //convert System.Drawing.Image to WPF image
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(gdiImg);
            IntPtr hBitmap = bmp.GetHbitmap();
            System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            img.Source = WpfBitmap;
            img.Width = gdiImg.Width;
            img.Height = gdiImg.Height;
            img.Stretch = System.Windows.Media.Stretch.Fill;
            return img;
        }
    }
}