namespace Insurance.Domain.Entities;

public class Seguro
{
        private const decimal MARGEM_SEGURANCA = 0.03m; // 3%
        private const decimal LUCRO = 0.05m;           // 5%

        public Guid Id { get; private set; }
        public string NomeSegurado { get; private set; }
        public string CPF { get; private set; }
        public int Idade { get; private set; }
        public string MarcaModeloVeiculo { get; private set; }
        public decimal ValorVeiculo { get; private set; }                
        public decimal TaxadeRisco { get; private set; }
        public decimal PremioRisco { get; private set; }
        public decimal PremioPuro { get; private set; }
        public decimal PremioComercial { get; private set; }

        private Seguro() { }

        public Seguro(string nome, string cpf, int idade, string veiculo, decimal valorVeiculo)
        {
            Id = Guid.NewGuid();
            NomeSegurado = nome;
            CPF = cpf;
            Idade = idade;
            MarcaModeloVeiculo = veiculo;
            ValorVeiculo = valorVeiculo;

            CalcularSeguro();
        }

        private void CalcularSeguro()
        {
            // 1. Taxa de Risco [cite: 28, 32]
            // Fórmula: (ValorVeiculo * 5) / (2 * ValorVeiculo)
            // Note que isso simplifica para 2.5% (0.025)
            TaxadeRisco = (ValorVeiculo * 5) / (2 * ValorVeiculo);

            // 2. Prêmio de Risco [cite: 29, 33]
            // Fórmula: Taxa de Risco * Valor do Veículo
            PremioRisco = TaxadeRisco * ValorVeiculo;

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
    }
