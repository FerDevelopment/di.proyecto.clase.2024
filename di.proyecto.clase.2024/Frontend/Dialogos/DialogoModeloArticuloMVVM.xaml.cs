using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace di.proyecto.clase._2024.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for DialogoModeloArticuloMVC.xaml
    /// </summary>
    /// 
    public partial class DialogoModeloArticuloMVVM : MetroWindow
    {
        private DiinventarioexamenContext contexto;
        private ModeloArticuloServicio modeloArticuloServicio;
        private TipoArticuloServicio tipoArticuloServicio; 


        public DialogoModeloArticuloMVVM(DiinventarioexamenContext context)
        {
            this.contexto = context;
            InitializeComponent();
            InicializaAsync();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }



        private async void InicializaAsync()
        {
            modeloArticuloServicio = new ModeloArticuloServicio(contexto);
            tipoArticuloServicio = new TipoArticuloServicio(contexto);

            tipoArticulo.ItemsSource = await tipoArticuloServicio.GetAllAsync();

        }

        private Modeloarticulo RecogeDatos()
        {
            Modeloarticulo modelo = new Modeloarticulo();

            modelo.Nombre = nombre.Text;
            modelo.Modelo = modeloArticulo.Text;
            modelo.Marca = marca.Text;
            modelo.Descripcion = descripcion.Text;
            //Manera 1
            modelo.TipoNavigation = (Tipoarticulo)tipoArticulo.SelectedItem;
            //Manera 2
            //modelo.Tipo = ((Tipoarticulo)tipoArticulo.SelectedItem).Idtipoarticulo;


            return modelo;
        }
        private async void Guardar_Click(object sender, RoutedEventArgs e)
        {

            if (await modeloArticuloServicio.AddAsync(RecogeDatos()))
            {
              await this.ShowMessageAsync("Gestión de Articulos", "Modelo creado e insertado correctamente");
                DialogResult = true;
            }
            else
            {
                await this.ShowMessageAsync("Gestión de Articulos", "Modelo no insertado, campos incorrectos");
            }
        }

        private void visibilidadGuardar()
        {
            if (tipoArticulo.SelectedItem != null && !String.IsNullOrEmpty(nombre.Text))
            {
                Guardar.Visibility = Visibility.Visible;
            }
            else
            {
                Guardar.Visibility = Visibility.Hidden;
            }
        }

        private void nombre_SelectionChanged(object sender, RoutedEventArgs e)
        {
            visibilidadGuardar();
        }

        private void tipoArticulo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            visibilidadGuardar();
        }
    }
}
