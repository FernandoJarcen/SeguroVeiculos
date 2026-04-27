using Insurance.Domain.Entities;

namespace Insurance.Application
{
    public interface ISeguroRepository
    {
        public Task AdicionarAsync(Seguro seguro);
        public Task<Seguro?> ObterPorIdAsync(Guid id);
        public Task<IEnumerable<Seguro>> ObterTodosAsync();
    }
}
