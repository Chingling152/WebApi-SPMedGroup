# SP Medical Group (API)  
API desenvolvida na escola SENAI de informatica para a empresa
# Sumario

- 1 **[Requisitos](#Requisitos)**  
   1.1. [Programas](#Programas)  
   1.2. [Banco de dados](#Banco-de-dados)  
   1.3. [Bibliotecas](#Bibliotecas)  
   
- 2 **[Configurações iniciais](#Configurações-iniciais)**  
 2.1. [Criando Banco de dados](#Criando-banco-de-dados)  
 2.2. [Criando o projeto](#Criando-o-projeto)  
 2.3. [Definindo relação com o banco](#Definindo-relação-com-o-banco)
 
- 3 **[Utilizando a API](#Utilizando-a-API)**  
 - 3.1. **[Privilegios de Administrador](#Privilegios-de-administrador)**  
 3.1.1. [Cadastrar usuarios](#Cadastrar-Usuarios)  
 3.1.2. [Agendar consultas](#Agendar-Consultas)  
 3.1.3. [Cadastrar clinicas](#Cadastrar-Clinicas)  
 3.1.4. [Cadastrar tipos de usuarios e especialidades medicas](#Cadastrar-Tipos-De-Usuario-E-Especialidades-Medicas)  
 3.1.5. [Cancelar/Alterar Consultas](#Agendar-Consultas)  
 
 - 3.2. **[Privilegios padrão](#Privilegios-padrão)**  
 3.2.1. [Login](#Login)  
 3.2.2. [Visualizar Consultas](#Visualizar-Consultas)  
 
 3.3. [](#)

## Requisitos  

### Programas necessarios
Aqui ficará a lista com todos os programas que usei no desenvolvimento / Teste do projeto:
- Visual Studio 2017 - Programação  
- Postman - Teste  
- SQL Server Management Studio 2014/2017 - Banco de dados  
- [JWT](https://jwt.io) - Validação de Token  

### Banco de dados  
Toda a documentação sobre o banco de dados está no repositorio [SPMedgroup](https://github.com/Chingling152/SQL-SPMedgroup) 

### Bibliotecas 
As bibliotecas externas de C# que eu utilizei para criar essa API foram : 
- **Microsoft.EntityFrameworkCore (2.1.1)**  
- **Microsoft.EntityFrameworkCore.SqlServer (2.1.1)**  
- **Microsoft.EntityFrameworkCore.Design (2.1.1)**  
- **Microsoft.EntityFrameworkCore.Tools (2.1.1)**  
- **Swashbuckle.AspNetCore (4.0.1)**  
--------------------------------------------------------------------------
Comando
Scaffold-DbContext "Data Source = .\[SERVIDOR]; initial catalog = SENAI_SP_MEDGROUP;user id = sa ; pwd = 132" Microfsoft.EntityFrameworkCore.SqlServer -OutputDir Domains -ContextDir Contexts -Context SpMedGroupContext
