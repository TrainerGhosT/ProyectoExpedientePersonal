using System.Text.Json;
using System.Text.RegularExpressions;
using AdminExpedientePersonal.Entities;
using AdminExpedientePersonal.Repository;
using AdminExpedientePersonal.Services.Interfaces;

namespace AdminExpedientePersonal.Services
{
    public class InstitucionService : IInstitucionService
    {
        private readonly IInstitucionRepository _institucionRepository;
        private readonly IEventBus _eventBus;

        public InstitucionService(IInstitucionRepository institucionRepository, IEventBus eventBus)
        {
            _institucionRepository = institucionRepository;
            _eventBus = eventBus;
        }

        // Validación para Nombre: máximo 150 caracteres y solo letras (se pueden incluir espacios)
        private void ValidarInstitucion(InstitucionEntity institucion)
        {
            if (string.IsNullOrWhiteSpace(institucion.Nombre))
                throw new ArgumentException("El nombre es requerido.");

            if (institucion.Nombre.Length > 150)
                throw new ArgumentException("El nombre no debe exceder 150 caracteres.");

            if (!Regex.IsMatch(institucion.Nombre, @"^[a-zA-Z\s]+$"))
                throw new ArgumentException("El nombre solo puede contener letras y espacios.");
        }

        public async Task<IEnumerable<InstitucionEntity>> ObtenerInstituciones(int userId)
        {
            var result = await _institucionRepository.ObtenerInstituciones();
            // Publicar evento de consulta
            _eventBus.Publish(new BitacoraEvent
            {
                UserId = userId,
                Action = "Consulta",
                JsonData = "El usuario consulta <Instituciones Educativas>",
                ModuloAfectado = "Gestión Instituciones Educativas"
            });
            return result;
        }

        public async Task<InstitucionEntity?> ObtenerInstitucionPorId(int id, int userId)
        {
            var result = await _institucionRepository.ObtenerInstitucionPorId(id);
            // Publicar evento de consulta
            _eventBus.Publish(new BitacoraEvent
            {
                UserId = userId,
                Action = "Consulta",
                JsonData = $"El usuario consulta la <Institución Educativa> con Id {id}",
                ModuloAfectado = "Gestión Instituciones Educativas"
            });
            return result;
        }

        public async Task<int> CrearInstitucion(InstitucionEntity institucion, int userId)
        {
            ValidarInstitucion(institucion);
            int newId = await _institucionRepository.CrearInstitucion(institucion);
            institucion.IdInstitucion = newId;

            // Publicar evento de creación con detalle en JSON
            string jsonData = JsonSerializer.Serialize(institucion);
            _eventBus.Publish(new BitacoraEvent
            {
                UserId = userId,
                Action = "Creacion",
                JsonData = $"El usuario creó la Institución Educativa: {jsonData}",
                ModuloAfectado = "Gestión Instituciones Educativas"
            });
            return newId;
        }

        public async Task<int> ActualizarInstitucion(InstitucionEntity institucion, int userId)
        {
            ValidarInstitucion(institucion);
            // Obtener registro anterior para log
            var existing = await _institucionRepository.ObtenerInstitucionPorId(institucion.IdInstitucion);
            if (existing == null)
                throw new Exception("Institución Educativa no encontrada.");

            int updated = await _institucionRepository.ActualizarInstitucion(institucion);

            // Publicar evento de actualización con detalles de antes y después
            var jsonDataBefore = JsonSerializer.Serialize(new { Before = existing });
            var jsonDataAfter = JsonSerializer.Serialize(new { After = institucion });
            _eventBus.Publish(new BitacoraEvent
            {
                UserId = userId,
                Action = "Actualizacion",
                JsonData =
                    $"El usuario Actualizó la Institución Educativa: RegistroAnterior: {jsonDataBefore}, RegistroActual: {jsonDataAfter}",
                ModuloAfectado = "Gestión Instituciones Educativas"
            });

            return updated;
        }

        public async Task<bool> EliminarInstitucion(int id, int userId)
        {
            // Verificar si la institución está asignada a algún registro (en 'preparacion')
            bool isAssigned = await _institucionRepository.InstitucionRelacionada(id);
            if (isAssigned)
                throw new Exception("No se puede eliminar un registro con datos relacionados.");

            // Obtener registro antes de eliminar para el log
            var existing = await _institucionRepository.ObtenerInstitucionPorId(id);
            if (existing == null)
                throw new Exception("Institución Educativa no encontrada.");

            bool deleted = await _institucionRepository.EliminarInstitucion(id);

            // Publicar evento de eliminación con detalles en JSON
            var jsonData = JsonSerializer.Serialize(existing);
            _eventBus.Publish(new BitacoraEvent
            {
                UserId = userId,
                Action = "Eliminacion",
                JsonData = jsonData,
                ModuloAfectado = "Gestión Instituciones Educativas"
            });

            return deleted;
        }
    }
}