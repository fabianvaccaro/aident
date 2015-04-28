﻿using System;
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
                updateComboBox();
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
                nuevoExperimento.idMpat =  Int32.Parse(txtMPAT.SelectedValue.ToString());
                nuevoExperimento.numeroPacientes = Int32.Parse(txtNumeroPacientes.Text);
                nuevoExperimento.codigoExperimento = txtCodigoExpermiento.Text;

                if(mets.AddExperimento(nuevoExperimento))
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

        private void procesarExperimento(object sender, RoutedEventArgs e)
        {
            Int32 idExperimento = 0;
            idExperimento = Int32.Parse(this.txtDiagExperimentos.SelectedValue.ToString());
            Muestras win = new Muestras( idExperimento);
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
            N_CiclosEvaluacion ciclos = new N_CiclosEvaluacion();
            ciclos.idMpat = 0;
            ciclos.numeroCiclos = Int32.Parse(txtCiclosEvaluacion.Text);
            txtListaCiclosEvaluacion.Items.Add(ciclos);
            txtCiclosEvaluacion.Text = String.Empty;
            
        }

        private void clearCiclosEvaluacionLista(object sender, RoutedEventArgs e)
        {
            txtListaCiclosEvaluacion.Items.Clear();
        }

        private void clearListaProcemientos(object sender, RoutedEventArgs e)
        {
            txtListaProcemientos.Items.Clear();
        }

        private void addProcemientoLista(object sender, RoutedEventArgs e)
        {
            N_ProcedimientoClinico procemiento = new N_ProcedimientoClinico();
            procemiento.idMpat = 0;
            procemiento.descripcion = txtProcedimiento.Text;
            procemiento.orden = Int32.Parse(txtListaProcemientos.Items.Count.ToString())+1;
            txtListaProcemientos.Items.Add(procemiento);
            txtProcedimiento.Text = String.Empty;
        }
    }
}
