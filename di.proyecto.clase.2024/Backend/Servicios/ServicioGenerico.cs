using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace di.proyecto.clase._2024.Backend.Servicios
{
    public class ServicioGenerico<T> : IServicioGenerico<T> where T : class
    {
        private readonly DbContext _context; // Contexto de la base de datos
        private readonly DbSet<T> _dbSet;    // DbSet para la entidad genérica
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public ServicioGenerico(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            try
            {
                Logger.Info($"Obteniendo entidad de tipo {typeof(T).Name} con ID {id}");
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                {
                    Logger.Warn($"No se encontró entidad de tipo {typeof(T).Name} con ID {id}");
                }
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error al obtener entidad de tipo {typeof(T).Name} con ID {id}");
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                Logger.Info($"Obteniendo todas las entidades de tipo {typeof(T).Name}");
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error al obtener todas las entidades de tipo {typeof(T).Name}");
                throw;
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                Logger.Info($"Agregando nueva entidad de tipo {typeof(T).Name}");
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                Logger.Info($"Entidad de tipo {typeof(T).Name} agregada con éxito");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error al agregar entidad de tipo {typeof(T).Name}");
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                Logger.Info($"Actualizando entidad de tipo {typeof(T).Name}");
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                Logger.Info($"Entidad de tipo {typeof(T).Name} actualizada con éxito");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error al actualizar entidad de tipo {typeof(T).Name}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                Logger.Info($"Eliminando entidad de tipo {typeof(T).Name} con ID {id}");
                var entity = await GetByIDAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                    Logger.Info($"Entidad de tipo {typeof(T).Name} con ID {id} eliminada con éxito");
                }
                else
                {
                    Logger.Warn($"No se encontró entidad de tipo {typeof(T).Name} con ID {id} para eliminar");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error al eliminar entidad de tipo {typeof(T).Name} con ID {id}");
                throw;
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                Logger.Info($"Buscando entidades de tipo {typeof(T).Name} con una condición específica");
                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error al buscar entidades de tipo {typeof(T).Name}");
                throw;
            }
        }
    }
}
