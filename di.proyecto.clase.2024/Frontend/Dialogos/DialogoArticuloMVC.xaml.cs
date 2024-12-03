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
        private ArticuloServicio articuloServicio;
        private ModeloArticuloServicio modeloArticuloServicio;
        private List<String> estados = ["operativo"];
        private DptoServicio dptoServicio;
        private EspacioServicio espacioServicio;
        private Usuario userLogin;
  
        public DialogoArticuloMVC(DiinventarioexamenContext context, Usuario usu)
        {
            this.context = context;
            if (this.context != null)
            {
                this.userLogin = usu;
                this.modeloArticuloServicio = new ModeloArticuloServicio(this.context);
                this.articuloServicio = new ArticuloServicio(this.context);
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

            fechaAlta.SelectedDate = DateTime.Now;
            fechaAlta.Text = DateTime.Now.ToString();
            comboModelo.ItemsSource = await modeloArticuloServicio.GetAllAsync();
            comboEstado.ItemsSource = estados;
            comboEspacio.ItemsSource = await espacioServicio.GetAllAsync();
            comboDepartamento.ItemsSource = await dptoServicio.GetAllAsync();
       
            usuario.Items.Add(userLogin);
            usuario.SelectedItem = userLogin;
            usuario.Text = userLogin.Nombre;
            

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (await articuloServicio.AddAsync(recogerDatos()))
            {
                _ = this.ShowMessageAsync("Gestión de Articulos", "Articulo creado e insertado correctamente");
                DialogResult = true;
            }
            else
            {
                _ = this.ShowMessageAsync("Gestión de Articulos", "Articulo no insertado, campos incorrectos");
            }

        }

        private Articulo recogerDatos()
        {
            Articulo articulo = new Articulo();

            articulo.Idarticulo = articuloServicio.GetLastId()+1;
            articulo.ModeloNavigation = (Modeloarticulo)comboModelo.SelectedItem;
            articulo.Fechaalta = fechaAlta.SelectedDate.HasValue ? fechaAlta.SelectedDate : DateTime.Now;
            articulo.Estado = (String)comboEstado.SelectedItem;
            articulo.UsuarioaltaNavigation = userLogin;
            articulo.Numserie = numeroSerie.Text;
            articulo.DepartamentoNavigation = (Departamento)comboDepartamento.SelectedItem;
            articulo.EspacioNavigation = (Espacio)comboEspacio.SelectedItem;
            articulo.Observaciones = obvservaciones.Text;

            return articulo;
        }
    }
}
