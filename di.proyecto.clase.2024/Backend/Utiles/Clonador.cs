using System.Reflection;

namespace di.proyecto.clase._2024.Backend.Utiles
{
    

    public static class Clonador
    {
        public static T ClonarDatos<T>(T origen) where T : new()
        {
            if (origen == null)
                throw new ArgumentNullException(nameof(origen));

            T destino = new T();
            Type tipo = typeof(T);

            foreach (PropertyInfo propiedad in tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (propiedad.CanRead && propiedad.CanWrite)
                {
                    object valor = propiedad.GetValue(origen);
                    propiedad.SetValue(destino, valor);
                }
            }

            return destino;
        }
    }

}
