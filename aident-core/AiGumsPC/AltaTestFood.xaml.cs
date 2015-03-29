﻿using LibreriaObjetos;
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
using MainCore;

namespace AiGumsPC
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AltaTestFood : Window
    {
        public AltaTestFood()
        {
            InitializeComponent();
            Loaded += MyWindow_Loaded;
            Activated += window_Activated;
            
        }

        private void window_Activated(object sender, EventArgs e)
        {
            updateComboBox();
            this.btCrearTipo.IsEnabled = true;
            this.cbTipo.IsEnabled = true;
            this.tbCaracteristicaMonitorzadas.IsEnabled = true;
            this.tbDescripcion.IsEnabled = true;
            this.tbNombre.IsEnabled = true;
        }
        private void window_Deactivated(object sender, EventArgs e)
        {
            
            this.btCrearTipo.IsEnabled = false;
            this.cbTipo.IsEnabled = false;
            this.tbCaracteristicaMonitorzadas.IsEnabled = false;
            this.tbDescripcion.IsEnabled = false;
            this.tbNombre.IsEnabled = false;
        }
        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            updateComboBox();
        }
        private void updateComboBox()
        {
            MainCore.Metodos metodos = new MainCore.Metodos();
            List<N_TipoTestFood> lista = new List<N_TipoTestFood>();

            lista = metodos.tipoTestFoodToList();
            cbTipo.ItemsSource = lista;
            cbTipo.DisplayMemberPath = "nombre";
            cbTipo.SelectedValuePath = "id";
            cbTipo.SelectedIndex = 0;
        }

        private void grabarTestFood(object sender, RoutedEventArgs e)
        {
            Metodos metodo = new Metodos();
            N_TestFood tf = new N_TestFood();
            tf.nombre = tbNombre.Text;
            tf.descripcion = tbDescripcion.Text;
            tf.caracteristicasMonitorizadas = tbCaracteristicaMonitorzadas.Text;
            tf.tipo = Int32.Parse(cbTipo.SelectedValue.ToString());

            metodo.AddTestFood(tf);
            this.Close();
            
        }

        private void cancelarTestFood(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void resetview()
        {
            try
            {
                this.tbNombre.Text = "";
                this.tbDescripcion.Text = "";
                this.tbCaracteristicaMonitorzadas.Text = "";
                this.cbTipo.Text = "";
            }
            catch (Exception e)
            {
                barraEstado.DataContext = e;
            }
        }

        private void addTipoTestFood(object sender, RoutedEventArgs e)
        {
            AltaTipoTestFood win = new AltaTipoTestFood();
            win.Show();
        }
    }
}