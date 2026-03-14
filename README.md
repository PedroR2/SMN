# SMN
# Instruções para execução do projeto

## 1. Configuração do banco de dados

1. Crie um banco de dados básico.  
   - Nome sugerido: `BlogDB`  
   - Você pode alterar o nome se desejar, mas lembre-se de atualizar o arquivo `appsettings.json` para refletir essa mudança.

2. Crie as seguintes tabelas, seguindo os requisitos definidos para os objetos do teste:

```sql
-- Tabela de BlogPost
CREATE TABLE BlogPost (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Content NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabela de Comment
CREATE TABLE Comment (
    Id INT PRIMARY KEY IDENTITY(1,1),
    BlogPost_ID INT NOT NULL,
    Content NVARCHAR(100) NOT NULL,
    FOREIGN KEY (BlogPost_ID) REFERENCES BlogPost(Id)
);
```
3. Após criar o banco, certifique-se de que o connection string no appsettings.json esteja apontando corretamente para o seu banco de dados.

## 2. Execução do projeto

1. Abra o projeto na sua IDE de preferência.

2. Certifique-se de que o banco de dados está configurado e acessível.

3. Execute o projeto normalmente. Os endpoints já estão implementados conforme os requisitos do teste.

4. Teste os endpoints para garantir que BlogPost e Comment estão funcionando corretamente.

## 3. Melhorias sugeridas para um ambiente de trabalho real (não creio que o tempo requisitado foi um problema, poderia ter adicionado mais no tempo mas quis me ater ao projeto em questão para não fugir muito do que foi pedido.)

Mesmo que o tempo de desenvolvimento tenha sido suficiente para completar os requisitos, posso pensar em algumas melhorias que poderiam ter sido pedidas ou feitas:

- **Enum para códigos de erro ao invés de mensagens diretamente no codigo**  
  Facilita a manutenção e padroniza o retorno de erros da API somada com arquivos de tradução

- **Autenticação e login**  
  Implementar autenticação JWT com validação de token e prazo de expiração para sessões com sistema de Login.

- **Internacionalização (i18n)**  
  Guardar strings de interface e mensagens em arquivos de recursos específicos para suportar múltiplos idiomas.

- **Funcionalidades adicionais**  
  - Adicionar endpoints para **editar** e **deletar** posts e comentários.  
  - Expandir as regras de negócio além do básico já implementado.

- **Testes unitários**  
  Criar testes para os métodos dos endpoints, garantindo que as funcionalidades se comportem conforme esperado.
# SMN
