using Insurance.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeguroController : ControllerBase
    {
        private readonly SeguroService _seguroService;

        public SeguroController(SeguroService seguroService)
        {
            _seguroService = seguroService;
        }

        // POST: api/seguro
        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] RegistroSeguroRequest request)
        {
            try
            {
                // Chama o service que busca o segurado no REST e calcula o seguro
                var resultado = await _seguroService.RegistrarSeguroAsync(
                    request.nome,
                    request.Cpf,
                    request.Idade,
                    request.MarcaModeloVeiculo,
                    request.ValorVeiculo);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // GET: api/seguro/relatorio
        [HttpGet("relatorio")]
        public async Task<IActionResult> ObterRelatorio()
        {
            var relatorio = await _seguroService.GerarRelatorioMediasAsync();
            return Ok(relatorio);
        }
    }
    public record RegistroSeguroRequest(string nome, string Cpf, int Idade, string MarcaModeloVeiculo, decimal ValorVeiculo);
}
