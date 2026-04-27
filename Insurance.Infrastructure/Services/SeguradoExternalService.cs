using System.Net.Http.Json;
using Insurance.Domain.Interfaces;
namespace Insurance.Infrastructure.Services
{
    public class SeguradoExternalService : ISeguradoService
    {
        private readonly HttpClient _httpClient;

        public SeguradoExternalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(string Nome, int Idade, string CPF)> ObterDadosSeguradoAsync(string cpf)
        {
            // Simulando a busca no endpoint do JSON Server (ex: http://localhost:3000/segurados/12345678900)
            var response = await _httpClient.GetFromJsonAsync<SeguradoResponse>($"segurados/{cpf}");

            if (response == null) throw new Exception("Segurado não encontrado no serviço externo.");

            return (response.Nome, response.Idade, response.CPF);
        }
    }

    // DTO interno para mapear o JSON do mock
    public record SeguradoResponse(string Nome, int Idade, string CPF);
}
