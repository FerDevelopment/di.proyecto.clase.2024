using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using MahApps.Metro.Controls;
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
    /// Interaction logic for DialogoUsuarioMVC.xaml
    /// </summary>
    public partial class DialogoUsuarioMVC : MetroWindow
    {
        private DiinventarioexamenContext contexto;
        private UsuarioServicio usuarioServicio;
        public DialogoUsuarioMVC(DiinventarioexamenContext contexto)
        {
            if(contexto != null)
            {
                this.contexto = contexto;
            }
            InitializeComponent();
        }
    }
}
