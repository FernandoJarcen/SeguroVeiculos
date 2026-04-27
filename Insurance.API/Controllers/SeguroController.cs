using Insurance.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Insurance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeguroController : ControllerBase
    {
        private readonly ISeguroService _seguroService;

        public SeguroController(ISeguroService seguroService)
        {
            _seguroService = seguroService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrar([FromBody] SeguroRequest request)
        {
            try
            {
                var resultado = await _seguroService.RegistrarSeguroAsync(
                    request.Nome,
                    request.Cpf,
                    request.Idade,
                    request.MarcaModeloVeiculo,
                    request.ValorVeiculo);

                return CreatedAtAction(nameof(ObterPorCPF), new { cpf = request.Cpf }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("relatorio")]
        public async Task<IActionResult> ObterRelatorio()
        {
            var relatorio = await _seguroService.GerarRelatorioMediasAsync();
            return Ok(relatorio);
        }

        [HttpGet("buscarcpf/{cpf}")]
        public async Task<IActionResult> ObterPorCPF(string cpf)
        {
            var seguro = await _seguroService.ObterPorCPF(cpf);

            if (seguro == null)
                return NotFound(new { mensagem = "Seguro não encontrado para o CPF informado." });

            return Ok(seguro);
        }
    }
    public record SeguroRequest(
        [Required] string Nome,
        [Required] string Cpf,
        [Range(18, 120)] int Idade,
        [Required] string MarcaModeloVeiculo,
        [Range(0.01, double.MaxValue)] decimal ValorVeiculo
    );
}
