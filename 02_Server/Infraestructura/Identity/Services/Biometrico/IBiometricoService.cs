using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services.Biometrico
{
    public class AsistenciaDetalle
    {
        public string ip { get; set; }
        public int asistencias_insertadas { get; set; }
    }

    public class AsistenciaResponse
    {
        public string message { get; set; }
        public List<AsistenciaDetalle> detalles { get; set; }
    }

    public class UsuarioDetalle
    {
        public string ip { get; set; }
        public int usuarios_insertados { get; set; }
    }

    public class UsuarioResponse
    {
        public string message { get; set; }
        public List<UsuarioDetalle> detalles { get; set; }
    }
    public interface IBiometricoService
    {
        Task<AsistenciaResponse> ExtraerAsistenciaAsync(DateTime fechaInicio, DateTime fechaFin, string[] ips = null);
        Task<UsuarioResponse> ExtraerUsuariosAsync(string[] ips = null);
    }

    public class BiometricoService : IBiometricoService
    {
        private readonly HttpClient _httpClient;

        public BiometricoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AsistenciaResponse> ExtraerAsistenciaAsync(DateTime fechaInicio, DateTime fechaFin, string[] ips = null)
        {
            // Convertir las fechas al formato esperado por la API Python
            var requestPayload = new
            {
                fecha_inicio = fechaInicio.ToString("yyyy-MM-dd HH:mm:ss"),
                fecha_fin = fechaFin.ToString("yyyy-MM-dd HH:mm:ss"),
                ips = ips ?? new string[] { "192.168.50.176", "192.168.50.126","2" }
            };

            // Se hace el POST a la ruta /extraer_asistencia
            var response = await _httpClient.PostAsJsonAsync("/extraer_asistencia", requestPayload);
            response.EnsureSuccessStatusCode();

            // Deserializamos la respuesta en el objeto AsistenciaResponse (definir la clase según lo que retorna la API)
            var result = await response.Content.ReadFromJsonAsync<AsistenciaResponse>();
            return result;
        }

        public async Task<UsuarioResponse> ExtraerUsuariosAsync(string[] ips = null)
        {
            var requestPayload = new
            {
                ips = ips ?? new string[] { "192.168.50.176", "192.168.50.126" }
            };

            // Se hace el POST a la ruta /extraer_usuarios
            var response = await _httpClient.PostAsJsonAsync("/extraer_usuarios", requestPayload);
            response.EnsureSuccessStatusCode();

            // Deserializamos la respuesta en el objeto UsuarioResponse
            var result = await response.Content.ReadFromJsonAsync<UsuarioResponse>();
            return result;
        }
    }
}
