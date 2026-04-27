using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces;

namespace Insurance.Application.Services
{
    public class SeguroService : ISeguroService
    {
        private readonly ISeguroRepository _repository;
        private readonly ISeguradoService _externalService;

        public SeguroService(ISeguroRepository repository, ISeguradoService externalService)
        {
            _repository = repository;
            _externalService = externalService;
        }

        public async Task<Seguro> RegistrarSeguroAsync(string nome, string cpf, int idade, string veiculo, decimal valorVeiculo)
        {
            //var (nome, idade, CPF) = await _externalService.ObterDadosSeguradoAsync(cpf);
            //string nome = "Fernando Jarcen (Teste Local)";
            //int idade = 50;

            var novoSeguro = new Seguro(nome, cpf, idade, veiculo, valorVeiculo);

            await _repository.AdicionarAsync(novoSeguro);

            return novoSeguro;
        }

        public async Task<object> GerarRelatorioMediasAsync()
        {
            var valores = await _repository.ObterTodosAsync();

            if (!valores.Any()) return new { mensagem = "Nenhum dado encontrado" };

            return new
            {
                MediaValorVeiculo = Math.Round(valores.Average(s => s.ValorVeiculo), 2, MidpointRounding.AwayFromZero),
                MediaPremioRisco = Math.Round(valores.Average(s => s.PremioRisco), 2, MidpointRounding.AwayFromZero),
                MediaPremioPuro = Math.Round(valores.Average(s => s.PremioPuro), 2, MidpointRounding.AwayFromZero),
                MediaPremioComercial = Math.Round(valores.Average(s => s.PremioComercial), 2, MidpointRounding.AwayFromZero)
            };
        }

        public async Task<Seguro> ObterPorId(Guid id)
        {
            var seguro = await _repository.ObterPorIdAsync(id);
            if (seguro == null) return null;

            return seguro;
        }

        public async Task<List<Seguro>> ObterPorCPF(string cpf)
        {
            var seguro = await _repository.ObterPorCPFAsync(cpf);
            if (seguro == null) return null;

            return seguro;
        }
    }
}
