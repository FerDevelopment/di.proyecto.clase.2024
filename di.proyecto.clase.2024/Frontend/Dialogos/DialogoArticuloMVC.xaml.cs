namespace di.proyecto.clase._2024.Frontend.Dialogos
{
    using di.proyecto.clase._2024.Backend.Modelo;
    using di.proyecto.clase._2024.Backend.Servicios;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using System.Windows;

    public partial class DialogoArticuloMVC : MetroWindow
    {
        private DiinventarioexamenContext context;

        private Articulo articulo;

        private ArticuloServicio articuloServicio;

        private ModeloArticuloServicio modeloArticuloServicio;

        private List<String> estados = ["Operativo", "Obsoleto", "Mantenimiento"];

        private DptoServicio dptoServicio;

        private EspacioServicio espacioServicio;

        private Usuario userLogin;

        public DialogoArticuloMVC(DiinventarioexamenContext context, Usuario usu)
        {
            this.context = context;
            if (this.context != null)
            {
                articulo = new Articulo();
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
            comboArmario.ItemsSource = await articuloServicio.GetAllAsync();

            usuario.Items.Add(userLogin);
            usuario.SelectedItem = userLogin;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            recogerDatos();
            if (articuloServicio.NumserieUnico(articulo.Numserie))
            {
                if (await articuloServicio.AddAsync(articulo))
                {
                    await this.ShowMessageAsync("Gestión de Articulos", "Articulo creado e insertado correctamente");
                    DialogResult = true;
                }
                else
                {
                    await this.ShowMessageAsync("Gestión de Articulos", "Articulo no insertado, campos incorrectos");


                }
            }
            else
            {
                await this.ShowMessageAsync("Gestión de Articulos", "Articulo no insertado, Numero de serie Repetido");


            }

        }

        private void recogerDatos()
        {

            articulo.Idarticulo = articuloServicio.GetLastId() + 1;
            articulo.Observaciones = observaciones.Text;
            articulo.Fechaalta = fechaAlta.SelectedDate.HasValue ? fechaAlta.SelectedDate : DateTime.Now;
            articulo.Numserie = numeroSerie.Text;

            articulo.Estado = comboEstado.SelectedItem.ToString();
            articulo.DepartamentoNavigation = (Departamento)comboDepartamento.SelectedItem;
            articulo.EspacioNavigation = (Espacio)comboEspacio.SelectedItem;
            articulo.UsuarioaltaNavigation =  userLogin;
            articulo.ModeloNavigation = (Modeloarticulo)comboModelo.SelectedItem;
            articulo.DentrodeNavigation = (Articulo)comboArmario.SelectedItem;
        }
    }
}
