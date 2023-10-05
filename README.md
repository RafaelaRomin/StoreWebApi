### Projeto Web API 🌐

Este projeto é uma Web API desenvolvida em .NET utilizando o Entity Framework Core 7 e SQL Server como banco de dados. A principal finalidade é solidificar e compreender os conceitos de mapeamento de dados proporcionados pelo Entity Framework Core.

A API é acessível através do Swagger para facilitar a visualização e interação. As principais funcionalidades incluem operações CRUD (Create, Read, Update, Delete) para entidades de Cliente, Produto e Pedidos.

### Estrutura do Projeto 👩‍💻

 *O projeto é organizado de forma a refletir a estrutura das entidades envolvidas. As principais pastas incluem:*

- **Controllers:** Contém os controladores responsáveis por gerenciar as requisições HTTP.

- **Models:** Classes que representam as entidades do domínio.
  
- **Data:** Configurações e contexto do Entity Framework Core.

### Funcionalidades 💡
- **Cliente:** Operações CRUD para gerenciar informações do cliente.
- **Produto:** Funcionalidades para cadastrar e manipular dados relacionados a produtos.
- **Pedidos:** Gerenciamento dos pedidos, incluindo a associação entre clientes e itens no pedido.

### Mapeamento do banco de dados 🗺
- Feito utilizando o IEntityTypeConfiguration que permite que a configuração de um tipo de entidade seja fatorada em uma classe separada, em vez de em linha em OnModelCreating(ModelBuilder).
- Implementando essa interface, aplicando a configuração para a entidade no método e, em Configure(EntityTypeBuilder<Entity>) seguida, foi aplicada a configuração ao modelo usando ApplyConfiguration em OnModelCreating(ModelBuilder) na Context.
