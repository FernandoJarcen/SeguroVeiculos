using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces;

namespace Insurance.Application.Services
{
    public class SeguroService
    {
        private readonly ISeguroRepository _repository;
        private readonly ISeguradoService _externalService;

        public SeguroService(ISeguroRepository repository, ISeguradoService externalService)
        {
            _repository = repository;
            _externalService = externalService;
        }

        public async Task<Seguro> RegistrarSeguroAsync(string cpf, string veiculo, decimal valorVeiculo)
        {
            //var (nome, idade, CPF) = await _externalService.ObterDadosSeguradoAsync(cpf);
            string nome = "Fernando Jarcen (Teste Local)";
            int idade = 50;
            cpf = "123.456.789-00";

            var novoSeguro = new Seguro(nome, cpf, idade, veiculo, valorVeiculo);

            await _repository.AdicionarAsync(novoSeguro);

            return novoSeguro;
        }

        public async Task<object> GerarRelatorioMediasAsync()
        {
            var todos = await _repository.ObterTodosAsync();

            if (!todos.Any()) return new { mensagem = "Nenhum dado encontrado" };

            return new
            {
                MediaValorVeiculo = todos.Average(s => s.ValorVeiculo),
                MediaPremioComercial = todos.Average(s => s.PremioComercial)
            };
        }
    }
}
