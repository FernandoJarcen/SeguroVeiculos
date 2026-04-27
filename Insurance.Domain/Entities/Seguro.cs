namespace Insurance.Domain.Entities;

public class Seguro
{
    private const decimal MARGEM_SEGURANCA = 0.03m; // 3%
    private const decimal LUCRO = 0.05m;           // 5%

    public Guid Id { get; set; }
    public string MarcaModelo { get; set; }
    public decimal ValorVeiculo { get; set; }
    public string NomeSegurado { get; set; }
    public string CPF { get; set; }
    public int Idade { get; set; }

    // Resultados do Cálculo
    public decimal TaxaRisco { get; set; }
    public decimal PremioRisco { get; set; }
    public decimal PremioPuro { get; set; }
    public decimal PremioComercial { get; set; }

    private Seguro() { }

    public Seguro(string nome, string cpf, int idade, string veiculo, decimal valorVeiculo)
    {
        Id = Guid.NewGuid();
        NomeSegurado = nome;
        CPF = cpf;
        Idade = idade;
        MarcaModelo = veiculo;
        ValorVeiculo = valorVeiculo;

        //CalcularSeguro();
        Seguro SeguroCalculado =CalcularSeguro(valorVeiculo);
        TaxaRisco = SeguroCalculado.TaxaRisco;
        PremioRisco = SeguroCalculado.PremioRisco;
        PremioPuro = SeguroCalculado.PremioPuro;
        PremioComercial = SeguroCalculado.PremioComercial;
    }

    private void CalcularSeguro()
    {
        // 1. Taxa de Risco [cite: 28, 32]
        // Fórmula: (ValorVeiculo * 5) / (2 * ValorVeiculo)
        // Note que isso simplifica para 2.5% (0.025)
        TaxaRisco = (ValorVeiculo * 5) / (2 * ValorVeiculo);
        
        // 2. Prêmio de Risco [cite: 29, 33]
        // Fórmula: Taxa de Risco * Valor do Veículo
        PremioRisco = TaxaRisco / 100 * ValorVeiculo;

        // 3. Prêmio Puro [cite: 30, 34]
        // Fórmula: Prêmio de Risco * (1 + MARGEM_SEGURANÇA)
        PremioPuro = PremioRisco * (1 + MARGEM_SEGURANCA);

        // 4. Prêmio Comercial [cite: 30, 35]
        // Fórmula: LUCRO * Prêmio Puro (Conforme exemplo R$ 257,50 * 0.05 = 12.87...)
        // O enunciado diz: Valor do Seguro é R$ 270,37. 
        // Para chegar no valor do exemplo, o Prêmio Comercial é a SOMA (PremioPuro + Lucro)
        var valorLucro = LUCRO * PremioPuro;
        PremioComercial = PremioPuro + valorLucro;
    }

    public Seguro CalcularSeguro(decimal valorVeiculo)
    {
        var seguro = new Seguro { ValorVeiculo = valorVeiculo };

        // Taxa de Risco: (10000 * 5) / (2 * 10000) = 2.5
        seguro.TaxaRisco = (valorVeiculo * 5) / (2 * valorVeiculo);

        // Prêmio de Risco: (2.5 / 100) * 10000 = 250.00
        seguro.PremioRisco = (seguro.TaxaRisco / 100) * valorVeiculo;

        // Prêmio Puro: 250 * 1.03 = 257.50
        seguro.PremioPuro = seguro.PremioRisco * 1.03m;

        // Prêmio Comercial: 257.50 + (5% de 257.50) 
        // Obs: O cálculo do PDF (270.37) sugere PrêmioPuro * 1.05
        seguro.PremioComercial = Math.Round(seguro.PremioPuro * 1.05m, 2);

        return seguro;
    }
}
