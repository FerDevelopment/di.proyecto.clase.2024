
using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace di.proyecto.clase._2024.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        private DiinventarioexamenContext contexto;
        private UsuarioServicio usuarioServicio;
        public Login()
        {
            if (ConectarBD())
            {

                InitializeComponent();
                introName.Focus(); 
            }
        }

        private async void btnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(introName.Text) && !String.IsNullOrEmpty(introPWD.Password))
            {


                if (await usuarioServicio.Login(introName.Text.ToLower(), introPWD.Password))
                {

                    MainWindow ventanaPrincipal = new MainWindow(usuarioServicio.usuLogin, contexto);
                    ventanaPrincipal.Show();
                    this.Close();
                }
                else
                {
                    _ = this.ShowMessageAsync("INICIO DE SESIÓN", "No se ha encontrado el usuario y/o la contraseña no son correctos");
                }
            }
            else
            {
                _ = this.ShowMessageAsync("Faltan Datos", "Rellene todos los apartados para iniciar sesión");
            }

        }

        private bool ConectarBD()
        {
            bool correcto = true;
            contexto = new DiinventarioexamenContext();
            try
            {
                contexto.Database.OpenConnection();
                usuarioServicio = new UsuarioServicio(contexto);
            }
            catch (Exception ex)
            {
                correcto = false;
                this.ShowMessageAsync("CONEXIÓN CON LA BASE DE DATOS", "Upps!!! no podemos conectar con la BD administrados");
            }

            return correcto;

        }



        private void github_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/FerDevelopment",
                UseShellExecute = true // Esto es necesario para abrir el navegador predeterminado
            });
        }

        private void linkedin_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.linkedin.com/in/fer-developer/",
                UseShellExecute = true // Esto es necesario para abrir el navegador predeterminado
            });
        }

        private void Instagram_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.instagram.com/fer_developer?igsh=MXhreHNjZXNiYmV5MQ==",
                UseShellExecute = true // Esto es necesario para abrir el navegador predeterminado
            });
        }

        private void introName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
