# SP Medical Group (API)  
Desenvolvida na escola SENAI de informatica para a empresa SP Medical Group , a API tem o objetivo de facilitar o agendamento e visualização de consultas  
# Sumario

- 1 **[Requisitos](#Requisitos)**  
   - 1.1. [Programas](#Programas)  
   - 1.2. [Banco de dados](#Banco-de-dados)  
   - 1.3. [Bibliotecas](#Bibliotecas)  
   
- 2 **[Configurações iniciais](#Configurações-iniciais)**  
   - 2.1. [Criando banco de dados](#Criando-banco-de-dados)  
   - 2.2. [Criando o projeto](#Criando-o-projeto)  
   - 2.3. [Definindo relação com o banco](#Definindo-relação-com-o-banco)  
   - 2.4. [Mudando repositorio](#Mudando-Repositorio)  
   - 2.5. [Postman](#Postman)  
   - 2.6. [Swagger](#Swagger)  
 
- 3 **[Funcionalidades](#Funcionalidades)**  
   - 3.1. [Administrador](#Administrador)  
   - 3.2. [Medico](#Medico)  
   - 3.3. [Paciente](#Paciente)  
   - 3.4. [Usuario não autenticado](#Usuario-não-autenticado)  
  
- 4 **[Validação](#Validação)**  
   - 4.1. - [Campos](#Campos)  
   - 4.2. - [Paginas](#Paginas)  

## Requisitos  
A API tem alguns requisitos para que a mesma funcione e seja executada.  

### Programas necessarios
Aqui ficará a lista com todos os programas que usei no desenvolvimento : 
- Visual Studio 2017 - Programação  
- _Postman - Teste_ (Pode-se usar o swagger)  
- SQL Server Management Studio 2014/2017 - Banco de dados  

### Banco de dados  
O Banco de dados usado por essa API foi criado aqui : [SPMedgroupDatabase](https://github.com/Chingling152/SQL-SPMedgroup) 

### Bibliotecas 
As bibliotecas externas de C# que são necessarias para criar esta API : 
- **Microsoft.EntityFrameworkCore (2.1.1)**  
- **Microsoft.EntityFrameworkCore.SqlServer (2.1.1)** (Ou outra biblioteca EntityFrameworkCore que use qualquer outro banco de dados)  
- **Microsoft.EntityFrameworkCore.Design (2.1.1)**  
- **Microsoft.EntityFrameworkCore.Tools (2.1.1)**  
- **Swashbuckle.AspNetCore (4.0.1)**  

## Configurações iniciais  
Antes de iniciar a criação , a API precisa de coisas que precisam ser definidas antes.
 ### Criando Banco de dados
 Caso queria valores iniciais para o banco de dados veja a documentação do [SPMedgroupDatabase](https://github.com/Chingling152/SQL-SPMedgroup) (lá eu só não explico como importar de um arquivo .xlsx)  
 ### Criando o projeto
 Com o banco de dados criado você poderá apenas clicar abrir o arquivo e os arquivos estarão lá  

 ### Definindo relação com o banco  
O banco de dados do projeto já está importado e conectado com o projeto , mas caso você queira conectar-se a um outro banco de dados , ou caso sua instancia do banco de dados tem o nome diferente ou se você não está usando o SQL Server (apenas respeite as colunas e tabelas por favor...)
Você pode fazer uma dessas 2 opções  
- **Menor impacto**
Se o seu banco de dados tiver as mesmas tabelas e colunas , você poderá apenas ir no arquivo [SpMedGroupContext.cs](#https://github.com/Chingling152/WebApi-SPMedGroup/blob/master/Senai.WebApi.SpMedGroup/Contexts/SpMedGroupContext.cs)  

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
 	if (!optionsBuilder.IsConfigured){
	
 		optionsBuilder.UseSqlServer("Data Source = .\\[NOME DO SEU SERVIDOR]; initial catalog = SENAI_SP_MEDGROUP;user id = sa ; pwd = 132");
		// No lugar de [NOMESERVIDOR] coloque o nome da sua instancia do SQL Server 
		// initial catalog seria o banco de dados que ele irá iniciar usando
		// user id é o seu usuario administrador do banco de dados
		// pwd é a senha do usuario administrador 
		//  Caso seu banco não precise de login para abrir utilize -> Integrated Security=SSPI 
		// no lugar de user id = sa; pwd = 132
		// Caso você esteja usando outro banco de dados (como oracle por exemplo) apenas mude o UseSqlServer par UseOracle (ah, e instale a biblioteca pra que funcione , não vou ter que te ensinar tudo né? ;-;)
       }
}
```
- **Maior impacto**
Eu particularmente prefiro que você não faça isso (meu projeto tá tão bonitinho :'( , por que você quer fazer isso com ele?)  
Primeiro você vai ter que deletar a pasta [Domains](#) e [Contexts](#) . Depois abra o Console do gerenciador de pacotes e digite o seguinte comando : 
Scaffold-DbContext **[String de conexão]** **[Tipo do banco de dados]** -OutputDir **[Nome aa pasta Domains]** -ContextDir **[Nome da pasta Contexts]** -Context **[Nome do arquivo Context]**  
**String de conexão** : A string que fará conexão com o banco de dados. O Unico comando que deve ser colocado entre aspas (") *Não é necessario colocar duas barras invertidas(\\) no endereço do banco de dados*  
**Tipo do banco de dados** : Qual banco de dados será usado , Deve ser colocado a bibilioteca de referencia , Exemplo : SQLServer = **Microsoft.EntityFrameworkCore.SqlServer**.
Então , caso queira usar outro banco de daos , você terá que importar a biblioteca que de suporte a ela.  
**Pasta Domains** : Seria o nome onde ficariam todos os modelos , classes com os dados e variaveis já baseadas em cada tabela  
**Pasta Contex** : Ficara os arquivos com as regras , tabelas e valores do banco de dados  
**Arquivo Context** : O Arquivo onde tem em forma de objeto , uma tabela SQL com uma varias listas de Classes (classes que ficam na pasta **Domains**)  
  
### Mudando Repositorio  
A API Tem uma segunda maneira de se conectar ao banco de dados, utilizando SqlClient. Há algumas diferenças na performace mas eu não vou ficar falando sobre isso.   
Para mudar o repositorio apenas vá no topo de cada script e mude a seguinte parte.  
``` csharp
// Usando entity framework (padrão)
using Senai.WebApi.SpMedGroup.Repositories.EntityFramework;
// Apague o .EntityFramework para mudar do EntityFramework para o SqlClient
// As classes possuem o mesmo nome e herdam das mesmas interfaces
```
### Postman
O Postman é usado para testar todos os metodos e paginas da aplicação, lá tera todos os metodos 
### Swagger
Swagger é uma documentação já gerada na API onde você pode ver todos os metodos, quais verbos usam (get, post , put (não tem Delete porque não é seguro apagar dados)) e quais os parametros aceitos por eles.  
Particularmente **não recomendo** usar o Swagger para teste e sim só para entender como funciona a entrada de dados em certos metodos, ja que o swagger não aceita autenticação por token e quase todos os metodos da API precisam de autenticação.  

## Funcionalidades  

### Administrador  

### Medico  

### Paciente  

### Usuario não autenticado
