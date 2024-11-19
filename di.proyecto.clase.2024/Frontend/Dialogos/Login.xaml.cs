
using MahApps.Metro.Controls;
namespace di.proyecto.clase._2024.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow ventanaPrincipal= new MainWindow();
            ventanaPrincipal.Show();
            this.Close();

        }
    }
}
