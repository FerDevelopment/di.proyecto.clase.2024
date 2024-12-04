namespace di.proyecto.clase._2024.Frontend.Dialogos
{
    using di.proyecto.clase._2024.Backend.Modelo;
    using di.proyecto.clase._2024.Backend.Servicios;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using System.Windows;

    /// <summary>
    /// Interaction logic for DialogoArticuloMVC.xaml
    /// </summary>
    public partial class DialogoArticuloMVC : MetroWindow
    {
        /// <summary>
        /// Defines the context
        /// </summary>
        private DiinventarioexamenContext context;

        /// <summary>
        /// Defines the articuloServicio
        /// </summary>
        private ArticuloServicio articuloServicio;

        /// <summary>
        /// Defines the modeloArticuloServicio
        /// </summary>
        private ModeloArticuloServicio modeloArticuloServicio;

        /// <summary>
        /// Defines the estados
        /// </summary>
        private List<String> estados = ["Operativo", "Obsoleto", "Mantenimiento"];

        /// <summary>
        /// Defines the dptoServicio
        /// </summary>
        private DptoServicio dptoServicio;

        /// <summary>
        /// Defines the espacioServicio
        /// </summary>
        private EspacioServicio espacioServicio;

        /// <summary>
        /// Defines the userLogin
        /// </summary>
        private Usuario userLogin;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogoArticuloMVC"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="DiinventarioexamenContext"/></param>
        /// <param name="usu">The usu<see cref="Usuario"/></param>
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

        /// <summary>
        /// The CargarDatosAsync
        /// </summary>
        private async void CargarDatosAsync()
        {

            fechaAlta.SelectedDate = DateTime.Now;
            fechaAlta.Text = DateTime.Now.ToString();
            comboModelo.ItemsSource = await modeloArticuloServicio.GetAllAsync();
            comboEstado.ItemsSource = estados;
            comboEspacio.ItemsSource = await espacioServicio.GetAllAsync();
            comboDepartamento.ItemsSource = await dptoServicio.GetAllAsync();
            comboArmario.ItemsSource = await articuloServicio.GetAllAsync();

            usuario.Items.Add(userLogin);
            usuario.SelectedItem = userLogin;
          
        }

        /// <summary>
        /// The btnCancelar_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The btnGuardar_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (await articuloServicio.AddAsync(recogerDatos()))
            {
                await this.ShowMessageAsync("Gestión de Articulos", "Articulo creado e insertado correctamente");
                DialogResult = true;
            }
            else
            {
                await this.ShowMessageAsync("Gestión de Articulos", "Articulo no insertado, campos incorrectos");

            }
        }

        /// <summary>
        /// The recogerDatos
        /// </summary>
        /// <returns>The <see cref="Articulo"/></returns>
        private Articulo recogerDatos()
        {
            Articulo articulo = new Articulo();

            articulo.Idarticulo = articuloServicio.GetLastId() + 1;
            articulo.Observaciones = obvservaciones.Text;
            articulo.Fechaalta = fechaAlta.SelectedDate.HasValue ? fechaAlta.SelectedDate : DateTime.Now;
            articulo.Numserie = numeroSerie.Text;

            articulo.Estado = (String)comboEstado.SelectedItem;
            articulo.DepartamentoNavigation = (Departamento)comboDepartamento.SelectedItem;
            articulo.EspacioNavigation = (Espacio)comboEspacio.SelectedItem;
            articulo.UsuarioaltaNavigation = userLogin;
            articulo.ModeloNavigation = (Modeloarticulo)comboModelo.SelectedItem;

            return articulo;
        }
    }
}
