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
    /// Interaction logic for UCModeloArticulo.xaml
    /// </summary>
    public partial class UCModeloArticulo : UserControl
    {
        private MVModeloArticulo _modeloArticulo;
      
        public UCModeloArticulo(MVModeloArticulo modeloArticulo)
        {
            InitializeComponent();
            _modeloArticulo = modeloArticulo;

            DataContext = _modeloArticulo;
        }
    }
}
