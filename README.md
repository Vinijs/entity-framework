# Comandos iniciais:
``` bash
    mkdir entity-framework
    cd entity-framework
    dotnet new mvc
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
