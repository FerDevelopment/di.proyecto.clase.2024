using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using System.Windows;


namespace di.proyecto.clase._2024.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for DialogoArticuloMVC.xaml
    /// </summary>
    public partial class DialogoArticuloMVC : MetroWindow
    {
        private DiinventarioexamenContext context;
        private UsuarioServicio usuarioServicio;
        private ModeloArticuloServicio modeloArticuloServicio;
        private List<String> estados = ["operativo"];
        private DptoServicio dptoServicio;
        private EspacioServicio espacioServicio;
        public DialogoArticuloMVC(DiinventarioexamenContext context)
        {
            this.context = context;
            if (this.context != null)
            {
                this.usuarioServicio = new UsuarioServicio(this.context);
                this.modeloArticuloServicio = new ModeloArticuloServicio(this.context);
                this.dptoServicio = new DptoServicio(this.context);
                this.espacioServicio = new EspacioServicio(this.context);
                InitializeComponent();
                CargarDatosAsync();
            }
            else
            {
                _ = this.ShowMessageAsync("Conecction error", "Error al conectar con la base de datos, contacte con su administrador");
                DialogResult = true;
                this.Close();
            }

        }

        private async void CargarDatosAsync()
        {
            comboModelo.ItemsSource = await modeloArticuloServicio.GetAllAsync();
            comboEstado.ItemsSource = estados;
            comboEspacio.ItemsSource = await espacioServicio.GetAllAsync();
            comboDepartamento.ItemsSource = await dptoServicio.GetAllAsync();
            usuario.ItemsSource = await usuarioServicio.GetAllAsync();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
