using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.MVVM;
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

namespace di.proyecto.clase._2024.Frontend.ControlUsuario
{
    /// <summary>
    /// Interaction logic for UCArticulo.xaml
    /// </summary>
     public partial class UCArticulo : UserControl
    {
        private MVArticulo _crearArticulo;
        public UCArticulo()
        {
            InitializeComponent();
        }
        public UCArticulo(MVArticulo mvc)
        {
            InitializeComponent();
            _crearArticulo = mvc;
            DataContext = mvc;
            
        }

        private async void mItemBorrar_Click(object sender, RoutedEventArgs e)
        {
            _crearArticulo.crearArticulo = (Backend.Modelo.Articulo)dgCrearArticulo.SelectedItem;

            if (_crearArticulo.borrar)
            {

                MessageBox.Show("Articulo eliminado correctamente", "Gestión articulos");
            }
            else
            {
                MessageBox.Show("Error al intentar eliminar articulo", "Gestión articulos");
            }

            dgCrearArticulo.Items.Refresh();
            _crearArticulo.crearArticulo = new Articulo();
        }

        private void mItemEditar_Click(object sender, RoutedEventArgs e)
        {
            _crearArticulo.crearArticulo = (Backend.Modelo.Articulo)dgCrearArticulo.SelectedItem;

            Articulo articuloAux = _crearArticulo.Clonar;
            ArticuloCrear ac = new ArticuloCrear(_crearArticulo,true);
            ac.ShowDialog();

            if (ac.DialogResult.Equals(true))
            {
                dgCrearArticulo.Items.Refresh();
                _crearArticulo.crearArticulo = new Articulo();
            }
            else
            {
                _crearArticulo.crearArticulo = articuloAux;
                dgCrearArticulo.SelectedItem = articuloAux;

            }
        }
}
