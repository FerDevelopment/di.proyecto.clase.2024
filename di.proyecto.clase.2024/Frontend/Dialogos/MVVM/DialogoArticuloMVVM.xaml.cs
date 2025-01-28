namespace di.proyecto.clase._2024.Frontend.Dialogos
{
    using di.proyecto.clase._2024.Backend.Modelo;
    using di.proyecto.clase._2024.Backend.Servicios;
    using di.proyecto.clase._2024.MVVM;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using System.Windows;

    public partial class DialogoArticuloMVVM : MetroWindow
    {
        private MVArticulo _mVArticulo;

        private List<String> estados = ["Operativo", "Obsoleto", "Mantenimiento"];
        private Usuario _usuario;
        private Articulo _articulo => _mVArticulo.crearArticulo;
        private ArticuloServicio _articuloServicio;
        private DptoServicio _dptoServicio;
        private EspacioServicio _espacioServicio;
        private ModeloArticuloServicio _modeloArticuloServicio;

        public MVArticulo mvArticulo => _mVArticulo;
        public DialogoArticuloMVVM(MVArticulo mv, Usuario usu)
        {
            _usuario = usu;
            InitializeComponent();
            CargarDatosAsync();
        }

        private async void CargarDatosAsync()
        {

            fechaAlta.SelectedDate = DateTime.Now;
            fechaAlta.Text = DateTime.Now.ToString();
            comboModelo.ItemsSource = await _modeloArticuloServicio.GetAllAsync();
            comboEstado.ItemsSource = estados;
            comboEspacio.ItemsSource = await _espacioServicio.GetAllAsync();
            comboDepartamento.ItemsSource = await _dptoServicio.GetAllAsync();
            comboArmario.ItemsSource = await _articuloServicio.GetAllAsync();

            usuario.Items.Add(_usuario);
            usuario.SelectedItem = _usuario;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
           
            if (_articuloServicio.NumserieUnico(_articulo.Numserie))
            {
                if (await _articuloServicio.AddAsync(_articulo))
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

     

    }
}
