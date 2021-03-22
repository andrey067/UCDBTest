# UCDB Teste entrevista do dia 17/3/21

Prova prática - Desenvolvedor

Projeto desenvolvido em .NET 5 utilizando a IDE Visual Studio Community


## Desenvolvimento
Projeto foi desenvolvido aplicando princípios do DDD(Domian Drive Design), separando a arquitetura em camadas, separando as reponsabilidade da aplicação. Ficando mais fácil a manutenção.

## Execução
1 - Execute o arquivo "UCDBTeste.sln"
2 - No "Console de Gerenciamento de Pacotes" digite o comando "update-database".
3 - Clique no botão iniciar.
4 - Irá abrir dois projetos, um sendo a MANAGER.API, outro o projeto FrontEnd.
5 - No MANAGER.API, abrir o Swagger com todos as chamadas da API.
6 - No FrontEnd será executado uma instancia de um projeto MVC, a onde faz as requisições para a API.
7 - O metodo de status mostrando a cor, é gerado automaticamente quando faz a requição buscando todos os produtos, deixando assim o status sempre atualizado quando o produto esta proxima ou já passou do pra de validade.

obs: A função "Adicionar desconto" não implementada no FRONTEND, mas na API sim.

## Para mais informações

email: audrey.ernesto.lima@gmail.com
git: https://github.com/andrey067
