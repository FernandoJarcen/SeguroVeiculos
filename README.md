# SeguroVeiculos
Exame de seguro de veículos
Nesse repositorio tem todos os artefactos solicitados do exame.
1 - criar o Banco de dados através da migration que acompanha o projeto
2 - Alterar a connectionString para que possa refletir o que foi desenvolvido na máquina
3 - Inserir dados nos endpoints através do endereço http://localhost:5240/swagger/index.html
4 - o endpoint api/seguro calcula os valores e insere através de EF na base de dados SeguroVeiculosDb que foi testada localmente.
5 - o endpoint api/seguro/relatorio vai mostrar os valores das médias que compoem os dados gravados pelo endpoint anterior
6 - o endpoint api/seguro/buscarcpf vai mostrar todos os registros associados ao cpf informado desde que esse exista na base de dados
a página html e o swagger podem ser instanciados na mesma aba do navegador
