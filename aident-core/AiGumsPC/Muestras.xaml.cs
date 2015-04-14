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
using LibreriaObjetos;

namespace AiGumsPC
{
    /// <summary>
    /// Interaction logic for Muestras.xaml
    /// </summary>
    public partial class Muestras : Window
    {
        //private Int32 estado = 0;
        public Muestras()
        {
            InitializeComponent();
        }

        private void capturaPortapapeles(object sender, RoutedEventArgs e)
        {

        }
        private void siguienteImagen(object sender, RoutedEventArgs e)
        {
            //if (estado == 0) {
            //    N_Muestra muestra = new N_Muestra();

            //    txtCiclosMasticatorios.Text = "0";
            //    txtLado.Text = "A";
            //    txtNumeroMuestra.Text = String.Empty;
            //    txtNumPaciente.Text = String.Empty;
            //    imagen = capturaImagen();
            //    muestra.lado = txtLado;
            //    muestra.numero = txtNumeroMuestra;
            //    muestra.paciente = txtNumPaciente;
            //    muestra.imagen = imagen;
            //    if (addMuestra(muestra))
            //        siguiente;
            //    else
            //    {
            //        error;
            //    }
            //}
        }
        public void siguienteImagen(object sender, RoutedEventArgs e){
            N_Paciente paciente = new N_Paciente();
            Metodos metodos = new Metodos(); 
            estado.DataContext = "Introduce datos. ";
            if (rb_m.IsChecked == true)
            {
                paciente.sexo = "HOMBRE";       
            }
            else if (rb_f.IsChecked == true)
            {
                paciente.sexo = "MUJER";
            }
            else
            {
                estado.DataContext = "Error: Debe seleccionar el sexo del paciente";
                return;
            }
            //ESTOY POR AQUI
           // Compruebo si el elemento existe
                    if (metodos.existe(pac.DNI))
                    {
                        if (metodos.updatePaciente(pac, historia, imagen, mpat))
                        {
                            estado.Content = "Paciente actualizado con éxito";
                            return;
                        }
                    }
                    else
                    {
                        if (metodos.NuevoPaciente(pac))
                        {
                            if (metodos.addHistoria(historia, pac))
                            {

                                if (metodos.addImagen(imagen, pac, mpat))
                                {
                                    estado.Content = "Paciente registrado con éxito. " + "Historia registrada con éxito";
                                    return;
                                }
                            }
                            else
                            {
                                estado.Content = "Error al registrar historia del paciente" + pac.DNI;
                                return;
                            }


                        }
                        else
                        {
                            estado.Content = "Error al registrar paciente";
                            return;
                        }

                    }
                }
                else
                {
                    estado.Content = error;
                    return;
                }
            }
            bloqueaDatosPaciente();
        
        }
    }
}
