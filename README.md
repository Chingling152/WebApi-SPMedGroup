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
   - 3.1. [Visualização](#Visualização)  
	  - 3.1.1. [Consultas](#Visualizar-Todas-as-Consultas)  
	  - 3.1.2. [Especialidades](#Visualizar-Todas-as-Especialidades-Medicas)  
	  - 3.1.3. [Medicos](#Visualizar-Todos-os-Medicos)  
	  - 3.1.4. [Pacientes](#Visualizar-Todos-os-Pacientes)  
	  - 3.1.5. [Usuarios](#Visualizar-Todos-os-Usuarios)  
   - 3.2. [Cadastro](#Cadastro)  
	  - 3.2.1. [Consultas](#Cadastrar-Consultas)  
	  - 3.2.2. [Especialidades](#Cadastrar-Especialidades-Medicas)  
	  - 3.2.3. [Medicos](#Cadastrar-Medicos)  
	  - 3.2.4. [Pacientes](#Cadastrar-Pacientes)  
      - 3.2.5. [Usuarios](#Cadastrar-Usuarios)  
   - 3.3. [Alteração](#Alteração)  
	  - 3.3.1. [Usuarios](#Alterar-Usuarios)  
	  - 3.3.2. [Consultas](#Alterar-Consultas)  
	  - 3.3.3. [Especialidades](#Alterar-Especialidades-Medicas)  
	  - 3.3.4. [Medicos](#Alterar-Medicos)  
	  - 3.3.5. [Pacientes](#Alterar-Pacientes)  
  
- 4 **[Validação](#Validação)**  
   - 4.1. - [Campos](#Campos)  
   - 4.2. - [Paginas](#Paginas)  
   
- 5 **[Usando a API](#)**
  - 5.1. [Usando o Postman](#)  
  - 5.2. [Autorização](#Autorização)  
  - 5.3. [Acessando Paginas](#Acessando-Paginas)  
  - 5.4. [Exemplos do Postman](#Exemplos-do-Postman)

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
Se o seu banco de dados tiver as mesmas tabelas e colunas , você poderá apenas ir no arquivo [SpMedGroupContext.cs](https://github.com/Chingling152/WebApi-SPMedGroup/blob/master/Senai.WebApi.SpMedGroup/Contexts/SpMedGroupContext.cs)  

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
Primeiro você vai ter que deletar a pasta [Domains](https://github.com/Chingling152/WebApi-SPMedGroup/tree/master/Senai.WebApi.SpMedGroup/Domains) e [Contexts](https://github.com/Chingling152/WebApi-SPMedGroup/tree/master/Senai.WebApi.SpMedGroup/Contexts) . Depois abra o Console do gerenciador de pacotes e digite o seguinte comando : 
Scaffold-DbContext **[String de conexão]** **[Tipo do banco de dados]** -OutputDir **[Nome da pasta Domains]** -ContextDir **[Nome da pasta Contexts]** -Context **[Nome do arquivo Context]**  
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
O Postman é usado para testar todos os metodos e paginas da aplicação, lá tera todos os metodos.  
Recomendo que use este programa, pois as paginas precisam de autorização e nesse programa tem como você usar o token para autenticação  
### Swagger
Swagger é uma documentação já gerada na API onde você pode ver todos os metodos, quais verbos usam (get, post , put (não tem Delete porque não é seguro apagar dados)) e quais os parametros aceitos por eles.  
Particularmente **não recomendo** usar o Swagger para teste e sim só para entender como funciona a entrada de dados em certos metodos, ja que o swagger não aceita autenticação por token e quase todos os metodos da API precisam de autenticação.  

## Funcionalidades  
As funcionalidades da API são divididas em 3 : funcionalidades de **inserção** de dados , **alteração** de dados e de **listagem** de dados  

### Visualização  
Os metodos de visualização são metodos onde retornam valores salvos no banco de dados ao usuario.  
**Alguns metodos de visualização tem retornos diferentes** (depende do que você está usando, SQLClient ou EntityFramework)  
 
- #### Visualizar Todas as Consultas   
**Caminho**: */api/Consulta*  
**Requisitos** : Estar logado com um usuario com privilegios de administrador  
**Retorno(Entity Framework)** : Todas as consultas cadastradas no banco de dados  
**Retorno(SqlClient)** : Todas as consultas cadastradas no banco de dados (com as informações dos pacientes e medicos)  

- #### Visualizar Suas Consultas  
**Caminho(Medico)**: */api/Medico/VerConsultas*  
**Caminho(Paciente)**: */api/Paciente/VerConsultas*  
**Requisitos** : Estar logado com um usuario com privilegios de medico ou Paciente  
**Retorno(Entity Framework)** : Todas as consultas feitas pelo Medico/Paciente  
**Retorno(SqlClient)** : Todas as informações de todas as consultas feitas pelo Medico/Paciente   

- #### Visualizar Todas as Especialidades Medicas  
**Caminho**: */api/Especialidade*  
**Requisitos** : Estar logado com um usuario com privilegios de administrador (Mas em breve deixarei publica)  
**Retorno** : Todas as especialidades medicas cadastradas no banco de dados  

- #### Visualizar Todos os Medicos  
**Caminho**: */api/Medico*  
**Requisitos** : Estar logado com um usuario com privilegios de administrador  
**Retorno(Entity Framework)** : Todas as informações de todos os medicos  
**Retorno(SqlClient)** : Todas as informações dos medicos (porém um pouco mais precisas)  

- #### Visualizar Todos os Pacientes  
**Caminho**: */api/Paciente*
**Requisitos** : Estar logado com um usuario com privilegios de administrador  
**Retorno** : Todas as informações de todos os pacientes  

- #### Visualizar Todos os Usuarios  
**Caminho**: */api/Usuario*  
 **Requisitos** : Estar logado com um usuario com privilegios de administrador  
 **Retorno** : Todos os usuarios cadastrados no banco de dados  

### Cadastro
Metodos de cadastro são metodos onde o usuario (geralmente com privilegios de administrador) insere registros no banco de dados. **Esses metodos devem ser usado com cuidado** (principalmente se estiver usando SQLClient) ja que **são dados que ficarão salvos para sempre no banco de dados** (não há metodos para excluir dados ainda) e nem todos os erros foram tratados ainda.  
Para cadastrar ou alterar veja [Usando a API/Cadastrando Dados](#).  
  
#### Cadastrar Consultas  
Cadastra uma consulta no banco de dados

**Caminho**: */api/Consulta/Cadastrar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador ou Medico  
**Parametros** : Uma consulta com os valores preenchidos (JSON)  

#### Cadastrar Especialidades Medicas  
Cadastra uma especialidade medica no banco de dados  
  
**Caminho**: */api/Especialidade/Cadastrar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador  
**Parametros** : Uma especialidade medica com os valores preenchidos (JSON)  

#### Cadastrar Medicos  
- **Entity framework**  
Cadastra um Medico no banco de dados   
- **SqlClient**  
Cadastra um Medico e um Usuario para o Medico no banco de dados  
  
**Caminho**: */api/Medico/Cadastrar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador  
**Parametros(Entity Framework)** : Um medico com os valores preenchidos (JSON)  
**Parametros(SqlClient)** : Um medico com os valores preenchidos e um Usuario dentro dele (Veja :  [Exemplos do Postman](#Exemplos-do-Postman))  

#### Cadastrar Pacientes  
- **Entity framework**  
Cadastra um Paciente no banco de dados   
- **SqlClient**  
Cadastra um Paciente e um Usuario para o Paciente no banco de dados  
**Caminho**: */api/Paciente/Cadastrar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador  
**Parametros(Entity Framework)** : Um Paciente com os valores preenchidos (JSON)  
**Parametros(SqlClient)** : Um Paciente com os valores preenchidos e um Usuario dentro dele (Veja :  [Exemplos do Postman](#Exemplos-do-Postman))  
  
#### Cadastrar Usuarios  
- **Entity Framework**
Cadastra um Usuario no banco de dados. Que será usado como referencia nos metodos Cadastrar Paciente e/ou Cadastrar Medico atraves do ID  
- **SqlClient**
Cadastra um Usuario no banco de dados com privilegio de administrador, qualquer outro tipo de usuario inserido irá retornar uma exceção  
  
**Caminho**: */api/Usuario/Cadastrar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador  
**Parametros** : Um Usuario com todos os valores preenchidos (JSON)  

### Alteração  
