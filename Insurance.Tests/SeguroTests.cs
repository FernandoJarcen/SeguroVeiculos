using Insurance.Domain.Entities;

namespace Insurance.Tests;

public class SeguroTests
{
    [Fact]
    public void CalcularSeguro_DeveRetornarValoresCorretos_QuandoValorVeiculoForDezMil()
    {
        // Arrange (Organizar)
        decimal valorVeiculo = 10000.00m;
        string nome = "Fernando Jarcen";
        string cpf = "123.456.789-00";
        int idade = 50;
        string veiculo = "Ônibus Urbano Histórico";

        // Act (Agir)
        var seguro = new Seguro(nome, cpf, idade, veiculo, valorVeiculo);

        // Assert (Asserir/Verificar)
        Assert.Equal(0.025m, seguro.TaxaRisco);          // 2,5%
        Assert.Equal(250.00m, seguro.PremioRisco);      // R$ 250,00
        Assert.Equal(257.50m, seguro.PremioPuro);       // R$ 257,50
        Assert.Equal(270.37m, Math.Round(seguro.PremioComercial, 2)); // R$ 270,37
    }

    [Theory]
    [InlineData(5000, 135.19)]
    [InlineData(20000, 540.75)]
    public void CalcularSeguro_DeveSerProporcionalAoValorDoVeiculo(decimal valorVeiculo, decimal resultadoEsperado)
    {
        // Act
        var seguro = new Seguro("Teste", "000", 30, "Carro", valorVeiculo);

        // Assert
        Assert.Equal(resultadoEsperado, Math.Round(seguro.PremioComercial, 2));
    }

}
