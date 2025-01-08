using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.proyecto.clase._2024.MVVM
{
    public class MVUsuario : MVBaseCRUD<Usuario>
    {
        private UsuarioServicio usuarioServicio;
        private TipoArticuloServicio tipoArticuloServicio;
        private DiinventarioexamenContext contexto;
        private Usuario _usuario;
        private ServicioGenerico<Tipousuario> tipoUsuServicio;
        public IEnumerable<Tipousuario> listaTipos { get { return Task.Run(() => tipoUsuServicio.GetAllAsync()).Result; } }
        public IEnumerable<Usuario> _listaUsuarios { get { return Task.Run(() => usuarioServicio.GetAllAsync()).Result; } }
        public bool guarda { get { return Task.Run(() => Add(usuario)).Result; } }

        //public Task<bool> Guarda { return await Add(Modeloarticulo)}

        public Usuario usuario
        {
            get => _usuario;
            set { _usuario = value; OnPropertyChanged(nameof(usuario)); }
        }

        public MVUsuario(DiinventarioexamenContext context)
        {
            contexto = context;
            Inicializa();
        }

        private void Inicializa()
        {
            usuarioServicio = new UsuarioServicio(contexto);
            servicio = usuarioServicio;
            tipoArticuloServicio = new TipoArticuloServicio(contexto);
            _usuario = new Usuario();
        }
    }
}
