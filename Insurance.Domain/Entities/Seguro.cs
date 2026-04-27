namespace Insurance.Domain.Entities;

public class Seguro
{
    private const decimal MARGEM_SEGURANCA = 0.03m; // 3%
    private const decimal LUCRO = 0.05m;           // 5%

    #region Dados do segurado
        
    public Guid Id { get; set; }
    public string NomeSegurado { get; set; }
    public string CPF { get; set; }
    public int Idade { get; set; }

    #endregion

    #region Dados do veiculo
        
    public string MarcaModelo { get; set; }
    public decimal ValorVeiculo { get; set; }

    #endregion

    #region Dados do calculo
        
    public decimal TaxaRisco { get; set; }
    public decimal PremioRisco { get; set; }
    public decimal PremioPuro { get; set; }
    public decimal PremioComercial { get; set; }
    
    #endregion
    
    private Seguro() { }

    public Seguro(string nome, string cpf, int idade, string veiculo, decimal valorVeiculo)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do segurado é obrigatório.");

        if (valorVeiculo <= 0)
            throw new ArgumentException("O valor do veículo deve ser maior que zero.");

        if (idade < 18)
            throw new ArgumentException("O segurado deve ter pelo menos 18 anos.");

        Id = Guid.NewGuid();
        NomeSegurado = nome;
        CPF = cpf;
        Idade = idade;
        MarcaModelo = veiculo;
        ValorVeiculo = valorVeiculo;

        Seguro SeguroCalculado =CalcularSeguro(valorVeiculo);
        TaxaRisco = SeguroCalculado.TaxaRisco;
        PremioRisco = SeguroCalculado.PremioRisco;
        PremioPuro = SeguroCalculado.PremioPuro;
        PremioComercial = SeguroCalculado.PremioComercial;
    }

    public Seguro CalcularSeguro(decimal valorVeiculo)
    {
        var seguro = new Seguro { ValorVeiculo = valorVeiculo };
        seguro.TaxaRisco = (valorVeiculo * 5) / (2 * valorVeiculo);
        seguro.PremioRisco = (seguro.TaxaRisco / 100) * valorVeiculo;
        seguro.PremioPuro = seguro.PremioRisco * 1.03m;
        seguro.PremioComercial = Math.Round(seguro.PremioPuro * 1.05m, 2);

        return seguro;
    }
}
