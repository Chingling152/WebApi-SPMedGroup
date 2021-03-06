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
 
- 3 **[Endpoints](#Endpoints)**  
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
  
- 4 **[Validação](#Validação-e-Autorização)**  
   - 4.1. - [Campos](#Campos)  
   - 4.2. - [Requisições](#Requisições)  
   
## Requisitos  
A API tem alguns requisitos para que a mesma funcione e seja executada.  

### Programas necessarios
Aqui ficará a lista com todos os programas que usei no desenvolvimento : 
- Visual Studio 2017 - Programação  
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
	
 		optionsBuilder.UseSqlServer(
		"Data Source = .\\[NOME DO SEU SERVIDOR]; initial catalog = SENAI_SP_MEDGROUP;user id = sa ; pwd = 132"
		);
		// No lugar de [NOMESERVIDOR] coloque o nome da sua instancia do SQL Server 
		// initial catalog seria o banco de dados que ele irá iniciar usando
		// user id é o seu usuario administrador do banco de dados
		// pwd é a senha do usuario administrador 
		//  Caso seu banco não precise de login para abrir utilize -> Integrated Security=SSPI 
		// no lugar de user id = sa; pwd = 132
		// Caso você esteja usando outro banco de dados
		//(como oracle por exemplo) apenas mude o UseSqlServer par UseOracle
		//(ah, e instale a biblioteca pra que funcione , não vou ter que te ensinar tudo né? ;-;)
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
Pode acessa-lo no endpoint *\swagger\index.html*
Particularmente **não recomendo** usar o Swagger para teste e sim só para entender como funciona a entrada de dados em certos metodos, ja que o swagger não aceita autenticação por token e quase todos os metodos da API precisam de autenticação.  

## Endpoints  
Os endpoints da API estarão todos listados por aqui, Eles são divididos em 3 partes : Alteração , Inserção e visualização . Endpoints são qualquer interação com a API.  

### Visualização  
Os endpoints de visualização são metodos onde retornam valores salvos no banco de dados ao usuario. Muito dos endpoints de visualização estão disponiveis para usuarios e outras apenas para administradores  
**Verbo Http** : GET  
**Alguns metodos de visualização tem retornos diferentes** (depende do que você está usando, SQLClient ou EntityFramework)  
 
- #### Visualizar Todas as Consultas   
**Caminho**: */apiv1/Consulta*  
**Requisitos** : Estar logado com um usuario com privilegios de administrador  
**Retorno(Entity Framework)** : Todas as consultas cadastradas no banco de dados  
**Retorno(SqlClient)** : Todas as consultas cadastradas no banco de dados (com as informações dos pacientes e medicos)  

- #### Visualizar Suas Consultas  
**Caminho(Medico)**: */api/v1/Medico/VerConsultas*  
**Caminho(Paciente)**: */api/v1/Paciente/VerConsultas*  
**Requisitos** : Estar logado com um usuario com privilegios de medico ou Paciente  
**Retorno(Entity Framework)** : Todas as consultas feitas pelo Medico/Paciente  
**Retorno(SqlClient)** : Todas as informações de todas as consultas feitas pelo Medico/Paciente   

- #### Visualizar Todas as Especialidades Medicas  
**Caminho**: */apiv1/Especialidade*  
**Requisitos** : Estar logado com um usuario com privilegios de administrador (Mas em breve deixarei publica)  
**Retorno** : Todas as especialidades medicas cadastradas no banco de dados  

- #### Visualizar Todos os Medicos  
**Caminho**: */api/v1/Medico*  
**Requisitos** : Estar logado com um usuario com privilegios de administrador  
**Retorno(Entity Framework)** : Todas as informações de todos os medicos  
**Retorno(SqlClient)** : Todas as informações dos medicos (porém um pouco mais precisas)  

- #### Visualizar Todos os Pacientes  
**Caminho**: */api/v1/Paciente*
**Requisitos** : Estar logado com um usuario com privilegios de administrador  
**Retorno** : Todas as informações de todos os pacientes  

- #### Visualizar Todos os Usuarios  
**Caminho**: */api/v1/Usuario*  
 **Requisitos** : Estar logado com um usuario com privilegios de administrador  
 **Retorno** : Todos os usuarios cadastrados no banco de dados  

### Cadastro
Os endpoints de cadastro são usados para enviar dados para a API 
**Verbo Http** : POST  
Os dados deverão ser enviados para API atraves do corpo da requisição 
  
#### Cadastrar Consultas  
Cadastra uma consulta no banco de dados

**Caminho**: */api/v1/Consulta/Cadastrar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador ou Medico  
**Parametros** : Uma consulta com os valores preenchidos (JSON)  

#### Cadastrar Especialidades Medicas  
Cadastra uma especialidade medica no banco de dados  
  
**Caminho**: */api/v1/Especialidade/Cadastrar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador  
**Parametros** : Uma especialidade medica com os valores preenchidos (JSON)  

#### Cadastrar Medicos  
- **Entity framework**  
Cadastra um Medico no banco de dados   
- **SqlClient**  
Cadastra um Medico e um Usuario para o Medico no banco de dados  
  
**Caminho**: */api/v1/Medico/Cadastrar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador  
**Parametros(Entity Framework)** : Um medico com os valores preenchidos (JSON)  
**Parametros(SqlClient)** : Um medico com os valores preenchidos e um Usuario dentro dele (Veja :  [Requisições](#Requisições))  

#### Cadastrar Pacientes  
- **Entity framework**  
Cadastra um Paciente no banco de dados   
- **SqlClient**  
Cadastra um Paciente e um Usuario para o Paciente no banco de dados  
**Caminho**: */api/v1/Paciente/Cadastrar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador  
**Parametros(Entity Framework)** : Um Paciente com os valores preenchidos (JSON)  
**Parametros(SqlClient)** : Um Paciente com os valores preenchidos e um Usuario dentro dele (Veja :  [Requisições](#Requisições))  
  
#### Cadastrar Usuarios  
- **Entity Framework**
Cadastra um Usuario no banco de dados. Que será usado como referencia nos metodos Cadastrar Paciente e/ou Cadastrar Medico atraves do ID  
- **SqlClient**
Cadastra um Usuario no banco de dados com privilegio de administrador, qualquer outro tipo de usuario inserido irá retornar uma exceção  
  
**Caminho**: */api/v1/Usuario/Cadastrar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador  
**Parametros** : Um Usuario com todos os valores preenchidos (JSON)  

### Alteração  
Os metodos de alteração, também usados pelo **administrador** onde os valores de registro do banco de dados são alterados. Esses metodos sempre são usados com um ID que será o ID do registro que será alterado.

#### Alterar Usuarios  
- **Entity framework**  
Altera os valores de um usuario no banco de dados  
- **SqlClient**  
Altera os valores apenas de um *administrador*  

**Caminho**: */api/v1/Usuario/Alterar*  
**Requisitos** : Estar logado com um usuario com privilegios de *Administrador*  
**Parametros(Entity Framework)** : Um Usuario com os valores preenchidos e alterados (com o ID,em JSON)  
**Parametros(SqlClient)** : Altera os valores de um administrador  

#### Alterar Consultas  
Altera os valores de uma consulta no banco de dados (é usado pelo medico para alterar uma descrição)  
  
**Caminho**: */api/v1/Consulta/Alterar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador  
**Parametros** : Uma consulta com todos os valores ja alterados (precisa do ID para saber qual registro será alterado)  

#### Alterar Especialidades Medicas  
Altera o nome de uma especialidade medica no banco de dados 
**Caminho**: */api/v1/Especialidade/Alterar*  
**Requisitos** : Estar logado com um usuario com privilegios de Administrador  
**Parametros** : Uma Especialidade com o nome ja alterado (precisa do ID para saber qual registro será alterado)  

#### Alterar Medicos  
- **Entity framework**  
Altera os valores de um medicos no banco de dados (não afeta os dados de usuario)  
- **SqlClient**  
Altera **todos** os valores de um medico (desde os dados de medico até o de usuario como email e senha) **não muda o usuario de origem**  

**Caminho**: */api/v1/Medico/Alterar*  
**Requisitos** : Estar logado com um usuario com privilegios de *Administrador* ou *Medico* (porém você poderá apenas alteras suas informações)  
**Parametros(Entity Framework)** : Um Medico com o usuario dentro dele com os valores preenchidos e alterados (com o ID, o ID do Usuario deve ser o mesmo registrado no banco de dados)  
**Parametros(SqlClient)** : Altera os valores de um administrador  
#### Alterar Pacientes  
- **Entity framework**  
Altera os valores de um medicos no banco de dados (não afeta os dados de usuario)  
- **SqlClient**  
Altera **todos** os valores de um paciente (desde os dados de medico até o de usuario como email e senha)  

**Caminho**: */api/v1/Paciente/Alterar*  
**Requisitos** : Um usuario com privilegios de Administrador ou um Paciente (mas ele só poderá alterar seus valores)  
**Parametros(Entity Framework)** :  Um paciente com todas as informações ja alteradas  
**Parametros(SqlClient)** : Um paciente com o usuario que o referencia dentro dele  






# Validação e Autorização  
Nem todos os metodos da API estão disponiveis para todos usarem, muitos desses metodos precisam de autenticação. Alem disso , metodos onde você precisa envia dados para o banco de dados tem os campos inseridos validados.  
### Campos  
Validação de campos da API apenas uma maneira de prevenir que nenhum dado sera inserido incorretamente no banco de dados.  
A Maioria dos campos são requiridos e tem um certo tipo de dado a ser inserido (data, numerico , texto,email). Todos os valores devem ser inseridos (POST ou PUT) corretamente para que a API possa cadastrar ou alterar os dados  
#### Geral  
- **ID**  
O ID de qualquer instancia **não é obrigatorio no caso de inserção**. Caso queira alterar algum registro , precisará informar o ID para que o banco de dados saiba qual registro deve alterar  

#### Usuario  
 - **Email** :  
   - Letras , numeros e caracteres especiais  
   - Não pode ser nulo  
   - O Email ser valido  
   - O Email não pode já estar cadastrado no banco de dados  
   - Deve conter entre 5 e 200 caracteres  
 - **Senha** :  
   - Letras , numeros e caracteres especiais  
   - Não pode ser nulo  
   - Deve conter entre 8 e 200 caracteres  
 - **Tipo de usuario**:
	- Numero inteiro  
	- Não pode ser nulo  
	- Deve se referir à algum valor na enumeração [EnTipoUsuario](#https://github.com/Chingling152/WebApi-SPMedGroup/blob/master/Senai.WebApi.SpMedGroup/Enums/EnTipoUsuario.cs)  
	
#### Paciente  
 - **ID da conta**  
   - Numero inteiro  
   - Não pode ser nulo  
   - O Numero deve se referir ao ID de algum registro de Usuarios no banco de dados  
   - Não pode existir outro paciente referenciando esta mesma conta  
 - **Nome**  
    - Deve conter apenas letras  
    - Deve conter no maximo 200 caracteres  
    - Não pode ser nulo  
 - **CPF**  
   - Deve conter apenas numeros  
   - Deve ter exatos 9 caracteres  
   - Não pode existir um paciente cadastrado com este CPF  
   - Não pode ser nulo  
 - **RG**  
   - Deve conter apenas numeros  
   - Deve ter exatos 11 caracteres  
   - Não pode existir outro paciente com este RG
   - Não pode ser nulo  
 - **Telefone**  
   - Deve conter apenas numeros  
   - Deve ter 10 ou 11 caracteres  
   - Não pode ser nulo   
 - **Data de nascimento**  
   - Deve ser uma data valida (yyyy-MM-dd)  
   - Não pode ser uma data futura  
   - Não pode ser nula  
   
#### Medico  
 - **ID da conta**  
    - Numero inteiro  
    - Não pode ser nulo  
    - O Numero deve se referir ao ID de algum registro de Usuarios no banco de dados  
    - Não pode existir outro medico referenciando esta mesma conta  
 - **Nome**
    - Deve conter apenas letras  
    - Deve conter no maximo 200 caracteres  
    - Não pode ser nulo  
 - **Especialidade**  
   - Deve ser um numero inteiro  
   - Deve referenciar a um registro de uma Especialidade do banco de dados  
   - Não pode ser nulo  
 - **Clinica**  
   - Deve ser um numero inteiro  
   - Deve referenciar a um registro de uma Clinica do banco de dados  
   - Não pode ser nulo  
 - **CRM**  
   - Deve conter apenas numeros  
   - Deve ter exatos 11 caracteres  
   - Deve ser unico , ou seja , não pode existir nenhum outro medico utilizando este CRM  
   - Não pode ser nulo  
  
#### Clinica  
 - **Nome Fantasia**
   - Letras numeros e caracteres especiais são aceitos  
   - Deve conter no maximo 200 caracteres  
   - Não pode ser nulo  
 - **Endereço**
   - Letras numeros e caracteres especiais são aceitos  
   - Deve conter no maximo 250 caracteres  
   - Não pode ser nulo  
 - **Numero**
   - Apenas numeros são aceitos  
   - Não pode ser nulo  
 - **CEP**
   - Apenas numeros são aceitos  
   - Deve conter exatos 8 caracteres  
   - Não pode ser nulo  
 - ** Razão social**  
   - Letras numeros e caracteres especiais são aceitos  
   - Deve conter no maximo 200 caracteres  
   - Não pode ser nulo  
   - Deve ser unico , não pode existir qualquer outra Clinica com essa Razão social cadastrada no banco de dado  

#### Consulta 
- **ID Paciente**  
   - Numero inteiro  
   - Não pode ser nulo  
   - O Numero deve referenciar um  ID de algum registro de um Paciente no banco de dados  
- **ID Medico**  
   - Numero inteiro  
   - Não pode ser nulo  
   - O Numero deve referenciar um  ID de algum registro de um Paciente no banco de dados  
- **Data da consulta**  
   - Deve ser uma data valida (yyyy-MM-dd hh:mm)  
   - Não pode ser nula  
- **Descrição**  
   - Aceita letras , numeros caracteres especiais  
   - Opcional  
   - Sem limite de caracteres  
- **Situação da consulta**
   - Numero inteiro  
   - Não pode ser nulo  
   - Deve ser um valor que exista na Enumeração [EnSituacaoConsulta](https://github.com/Chingling152/WebApi-SPMedGroup/blob/master/Senai.WebApi.SpMedGroup/Enums/EnSituacaoConsulta.cs)  
 
#### Requisições
Aqui ficará tudo que deve ser inserido para cada requisição.  
** Todas essas requisições precisam que o usuario tenha um token (recebido quando for logado)**  
**Você precisará adicionar o id caso queira alterar um cadastro (pra especificar qual registro será alterado)**  
 - **Cadastrar /Alterar Usuario**  
 ```json
{
  		"email": "string",
  		"senha": "string"
  }
```
  
- **Cadastrar /Alterar Paciente**  
 - **(Entity Framework)**  
```json
{
  		 "idUsuario": 0,
		  "nome": "string",
		  "cpf": "string",
		  "rg": "string",
		  "telefone": "string",
		  "dataNascimento": "string"
 }
```
  
 - **(SqlClient)**  
```json
{
		  "nome": "string",
		  "cpf": "string",
		  "rg": "string",
		  "telefone": "string",
		  "dataNascimento": "string",
		  "idUsuarioNavigation": {
				"email": "string",
				"senha": "string",
				"tipoUsuario": 1
			}
}
```
  
- **Cadastrar /Alterar Medico**  
 - **(Entity Framework)**  
```json
{
 		 "idUsuario": 0,
 		 "nome": "string",
  		"crm": "string",
  		"idClinica": 0,
  		"idEspecialidade": 0
 }
```
  
 - **(SqlClient)**  
```json
{
		  "nome": "string",
		  "cpf": "string",
		  "rg": "string",
		  "telefone": "string",
		  "dataNascimento": "string",
		   "idClinica": 0,
		   "idEspecialidade": 0
		   "idUsuarioNavigation": {
				"email": "string",
				"senha": "string",
				"tipoUsuario": 1
			}
}
```
  
 - **Cadastrar / Alterar Clinica**  
```json
{
		  "nomeFantasia": "string",
		  "endereco": "string",
		  "numero": 0,
		  "cep": "string",
		  "razaoSocial": "string"
 }
```
  
 - **Cadastrar / Alterar Especialidades**  
```json
{
  		"nome": "string"
 }
```
  
 - **Cadastrar / Alterar Consulta**  
```json
{
		  "id": 0,
		  "idMedico": 0,
		  "idPaciente": 0,
		  "dataConsulta": "YYYY-MM-DD hh:mm",
		  "descricao": "string",
		  "statusConsulta": 1
}
```
