using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Frontend.ControlUsuario;
using di.proyecto.clase._2024.Frontend.Dialogos;
using di.proyecto.clase._2024.MVVM;
using MahApps.Metro.Controls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace di.proyecto.clase._2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : MetroWindow
    {
        Usuario usuario;
        DiinventarioexamenContext contexto;
        private MVModeloArticulo mVModeloArticulo;
        private MVArticulo mvArt;
        public MainWindow(Usuario usu, DiinventarioexamenContext context)
        {
            this.usuario = usu;
            this.contexto = context;
            InitializeComponent();
            nombreUsu.Text = usuario.Nombre;
            mVModeloArticulo= new MVModeloArticulo(contexto);
            mvArt= new MVArticulo(contexto);
        }

        private void cerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DialogoModeloArticuloMVC diaMod = new DialogoModeloArticuloMVC(contexto);
            diaMod.ShowDialog();

        }

        private void btnArticuloNuevo_Click(object sender, RoutedEventArgs e)
        {
            DialogoArticuloMVC diaAr = new DialogoArticuloMVC(contexto, usuario);
            diaAr.ShowDialog();
        }

        private void btnUsuarioNuevo_Click(object sender, RoutedEventArgs e)
        {

        }

     

        private void listaModeloBoton_Click(object sender, RoutedEventArgs e)
        {
            UCModeloArticulo uc= new UCModeloArticulo(mVModeloArticulo);
            panelCentral.Children.Clear();
            Grid.SetColumn(uc, 0);
            Grid.SetRow(uc, 0);
            panelCentral.Children.Add(uc);
        }

        private void btnListaArticulos_Click(object sender, RoutedEventArgs e)
        {

            UCArticulo uc = new UCArticulo(mvArt);
            panelCentral.Children.Clear();
            Grid.SetColumn(uc, 0);
            Grid.SetRow(uc, 0);
            panelCentral.Children.Add(uc);

        }
    }
}