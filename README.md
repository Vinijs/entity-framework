# Comandos iniciais:
``` bash
    mkdir entity-framework
    cd entity-framework
    dotnet new mvc
    dotnet new sln - Criar solução vazia
    dotnet new classlib --name Entity.Clientes.Domain
    dotnet new classlib --name Entity.Clientes.Data
```

# Componentes instalados:
``` bash
    dotnet add package Microsoft.EntityFrameworkCore --version 7.0.14
    dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.14
    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 7.0.0
```

# Comandos git:
``` bash
    git init
    git add .
    git commit -m "Iniciando projeto"
    code .gitignore # gerei o conteúdo para ignorar como (Windows, Linux, Mac, DotnetCore, VisualStudioCode) no link: https://www.toptal.com/developers/gitignore/
    Criei o repositório e rodei os comandos
    git remote add origin https://github.com/Vinijs/entity-framework
    git branch -M main
    git push -u origin main
```

# Comandos para migração:

``` bash
dotnet ef migrations add ClienteAdd
dotnet ef database update
```

# Instalação do code generator
``` bash
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool update -g dotnet-aspnet-codegenerator
```

# Gerando o scaffold de clientes
``` bash
dotnet aspnet-codegenerator controller -name ClientesController -m Cliente -dc DbContexto --relativeFolderPath Controllers --useDefaultLayout
```
# Comando para adicionar projetos a solução vazia
A partir de uma solução criada digitamos o comando para adicionar a referencia ao csproj dos projetos
 dotnet sln add src/entity-framework.csproj
 dotnet sln add src/EntityClientes/Entity.Clientes.Data/Entity.Clientes.Data.csproj
 dotnet sln add src/EntityPedidos/Entity.Pedidos.Data/Entity.Pedidos.Data.csproj
 dotnet sln add src/EntityProdutos/Entity.Produtos.Data/Entity.Produtos.Data.csproj
 dotnet sln remove src/ClienteContexto/ClienteContexto.csproj

## Comando para adicionar referencia em outros projetos
dotnet add reference ../Entity.Clientes.Domain/Entity.Clientes.Domain.csproj

# Gerando scaffold Contexto e Tabelas
## Comando para contexto Clientes
 dotnet ef dbcontext scaffold "server=localhost;database=EntityFrameworkComunidade;uid=root;pwd=carroforte" Pomelo.EntityFrameworkCore.MySql -t clientes -t enderecos -f -c ClienteDbContexto -o Contexto

 dotnet ef dbcontext scaffold "server=localhost;database=EntityFrameworkComunidade;uid=root;pwd=carroforte" Pomelo.EntityFrameworkCore.MySql -n Entity.Clientes.Domain.Entidades -t clientes -t enderecos -f -c ClienteDbContexto --context-dir Contexto --output-dir ..\Entity.Clientes.Domain\Entidades

 dotnet ef dbcontext scaffold "server=localhost;database=EntityFrameworkComunidade;uid=root;pwd=carroforte" Pomelo.EntityFrameworkCore.MySql -n Entity.Domain.Produtos.Entidades -t produtos -f -c ProdutosDbContexto --context-dir Contexto --output-dir ..\Entity.Produtos.Domain\Entidades

 # Geração de scripts via entity
 ``` bash
    dotnet ef migrations script 0 ContextoCompleto -o Scripts\ContextoCompleto.sql -i
    dotnet ef dbcontext script -o Scripts\ContextoProdutos.sql
 ```

 # Links utéis
 ``` bash
    Documentação CLI entity-framework-core : https://docs.microsoft.com/pt-br/ef/core/cli/dotnet
    Documentação completa entity-framework : https://docs.microsoft.com/pt-br/ef/
    Documentação auxiliar para o uso da FluentAPI/Ef Core : https://www.entityframeworktutorial.net/
 ```


