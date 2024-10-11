# como rodar
na pasta do projeto
rodar no terminal:
docker-compose build
docker-compose up -d
Tem a collection do postman aqui tamb�m.
# parte dois
D�vidas:
- Entraria no escopo Busca das tasks e projetos com v�rios filtros?
- Compartilhamento de projetos com outros usu�rios
- Quais integra��es externas seriam necess�rias?
- Pagina��o entraria no escopo dos endpoints?
- qual a expectativa de carga e de n�mero de usu�rios?
- Relat�rios personalizados, entraria tamb�m?
# parte tr�s

Melhoraria da seguinte forma:
- Pagina��o para endpoints para n�o sobrecarregar a API
- Criar documenta��o mais abrangente
- Usar cache Redis para reduzir a ida e volta no database
- Poderia se colocar a adi��o de tasks numa fila para otimizar performance
- Hist�rico de altera��o mais detalhado
- Usar a biblioteca dapper para as consultas
- Melhorar a estrutura do projeto para deixar mais coeso em alguns pontos
- Testes mais abrangentes al�m de unit�rio