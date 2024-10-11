# como rodar
na pasta do projeto
rodar no terminal:
- docker-compose build
- docker-compose up -d
- Tem a collection do postman aqui também.
# parte dois
Dúvidas:
- Entraria no escopo Busca das tasks e projetos com vários filtros?
- Compartilhamento de projetos com outros usuários
- Quais integrações externas seriam necessárias?
- Paginação entraria no escopo dos endpoints?
- qual a expectativa de carga e de número de usuários?
- Relatórios personalizados, entraria também?
# parte três

Melhoraria da seguinte forma:
- Paginação para endpoints para não sobrecarregar a API
- Criar documentação mais abrangente
- Usar cache Redis para reduzir a ida e volta no database
- Poderia se colocar a adição de tasks numa fila para otimizar performance
- Histórico de alteração mais detalhado
- Usar a biblioteca dapper para as consultas
- Melhorar a estrutura do projeto para deixar mais coeso em alguns pontos
- Testes mais abrangentes além de unitário
