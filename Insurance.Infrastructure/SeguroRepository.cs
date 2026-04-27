using Insurance.Application;
using Insurance.Domain.Entities;
using Insurance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Infrastructure
{
    public class SeguroRepository : ISeguroRepository
    {
        private readonly InsuranceDbContext _context;

        public SeguroRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Seguro seguro)
        {
            try
            {
                await _context.Seguros.AddAsync(seguro);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                string mensagem = e.Message;                
            }
        }

        public async Task<Seguro> ObterPorIdAsync(Guid id)
        {
            return await _context.Seguros.FindAsync(id);
        }

        public async Task<IEnumerable<Seguro>> ObterTodosAsync()
        {
            return await _context.Seguros.ToListAsync();
        }
    }
}
