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
    public class MVModeloArticulo : MVBaseCRUD<MVModeloArticulo>
    {
        /*Variables privadas ******************/
        private DiinventarioexamenContext contexto;
        private ModeloArticuloServicio modeloArticuloServicio;
        private TipoArticuloServicio tipoArticuloServicioServicio;
        private Modeloarticulo modelo;
        /*Variables publicas ******************/
        public IEnumerable<Tipoarticulo> listaTipos { get { return Task.Run(tipoArticuloServicioServicio.GetAllAsync).Result; } }
        public bool guarda { get { return Task.Run(() => modeloArticuloServicio.AddAsync(modelo)).Result; } }


        public MVModeloArticulo(DiinventarioexamenContext context)
        {
            contexto = context;
            Inicialize();
        }

        private async void Inicialize()
        {

            if (contexto != null)
            {
                modeloArticuloServicio = new ModeloArticuloServicio(contexto);
                tipoArticuloServicioServicio = new TipoArticuloServicio(contexto);
            }
            modelo = new Modeloarticulo();

        }
    }
}
