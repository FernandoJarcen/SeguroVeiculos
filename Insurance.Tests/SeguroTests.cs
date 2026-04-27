using Insurance.Domain.Entities;

namespace Insurance.Tests;

public class SeguroTests
{
    [Fact]
    public void CalcularSeguro_DeveRetornarValoresCorretos_QuandoCenarioPadrao()
    {
        var seguro = new Seguro("Fernando", "123", 50, "Bus", 10000m);

        Assert.Equal(2.5m, seguro.TaxaRisco);
        Assert.Equal(250.00m, seguro.PremioRisco);
        Assert.Equal(257.50m, seguro.PremioPuro);
        // Usando o arredondamento que definimos para a API
        Assert.Equal(270.38m, Math.Round(seguro.PremioComercial, 2, MidpointRounding.AwayFromZero));
    }

    // Teste de Entradas Inválidas (Defesa do Domínio)
    [Theory]
    [InlineData(0)]
    [InlineData(-1000)]
    public void Seguro_NaoDeveAceitarValorVeiculoZeroOuNegativo(decimal valorInvalido)
    {
        Assert.Throws<ArgumentException>(() =>
            new Seguro("Teste", "000", 30, "Carro", valorInvalido));
    }

    // Teste de Idade Mínima
    [Fact]
    public void Seguro_NaoDevePermitirMenorDeIdade()
    {
        Assert.Throws<ArgumentException>(() =>
            new Seguro("Jovem", "000", 17, "Carro", 10000m));
    }

    // Teste de Consistência de Dados (Nomes vazios)
    [Fact]
    public void Seguro_NaoDevePermitirNomeVazio()
    {
        Assert.Throws<ArgumentException>(() =>
            new Seguro("", "000", 30, "Carro", 10000m));
    }

    [Fact]
    public void CriarSeguro_ComDadosValidos_DeveCalcularCorretamente()
    {
        // Arrange & Act
        var seguro = new Seguro("Fernando Jarcen", "123", 50, "Ônibus", 10000m);

        // Assert
        Assert.Equal(2.5m, seguro.TaxaRisco);
        Assert.Equal(250m, seguro.PremioRisco);
        Assert.Equal(257.50m, seguro.PremioPuro);
        Assert.Equal(270.37m, seguro.PremioComercial);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-500)]
    public void CriarSeguro_ComValorInvalido_DeveLancarExcecao(decimal valorErrado)
    {
        // Assert & Act
        var ex = Assert.Throws<ArgumentException>(() =>
            new Seguro("Fernando", "123", 50, "Carro", valorErrado));

        Assert.Equal("O valor do veículo deve ser maior que zero.", ex.Message);
    }

    [Fact]
    public void CriarSeguro_MenorDeIdade_DeveLancarExcecao()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Seguro("Joao", "123", 17, "Moto", 5000m));
    }
}
