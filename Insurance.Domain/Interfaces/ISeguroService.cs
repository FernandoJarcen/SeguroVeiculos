using Insurance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Domain.Interfaces
{
    public interface ISeguroService
    {
        public Task<Seguro> RegistrarSeguroAsync(string nome, string cpf, int idade, string veiculo, decimal valorVeiculo);
        public Task<object> GerarRelatorioMediasAsync();
        public Task<Seguro> ObterPorId(Guid id);
        public Task<List<Seguro>> ObterPorCPF(string cpf);



    }
}
