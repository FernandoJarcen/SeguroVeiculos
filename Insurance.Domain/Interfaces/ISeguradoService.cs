namespace Insurance.Domain.Interfaces
{
    public interface ISeguradoService
    {
        Task<(string Nome, int Idade, string CPF)> ObterDadosSeguradoAsync(string cpf);
    }
}
