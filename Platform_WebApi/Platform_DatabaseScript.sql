/*  BRUNO FELIPE DE SOUZA ARAUJO
	Teste prático - processo seletivo UpperTools 1.2019
	Script de criação das tabelas do banco de dados para o software plataforma integrada
	Carga do sistema com dados fictícios
*/

/* Criação das tabelas */

create table if not exists platform_database.Item (
	idItem int,
    descricao varchar(100) not null,
    valorUnit decimal(10,2) not null,
    qtdeEstoque int not null
);

alter table platform_database.Item add constraint PK_idItem primary key (idItem);
alter table platform_database.Item modify idItem int auto_increment;

create table if not exists platform_database.Ranking (
	idRank int,
    dtRank date not null
);

alter table platform_database.Ranking add constraint PK_idRank primary key (idRank);
alter table platform_database.Ranking modify idRank int auto_increment;

create table if not exists platform_database.Clientes (
	idCliente int,
    nomeCliente varchar(100) not null,
    senhaCliente varchar(50) not null,
    emailCliente varchar(100) not null,
    ufCliente varchar(2) not null
);

alter table platform_database.Clientes add constraint PK_idCliente primary key (idCliente);
alter table platform_database.Clientes modify idCliente int auto_increment;
alter table platform_database.Clientes add constraint UK_emailCliente UNIQUE (emailCliente);

create table if not exists platform_database.Clientes_Ranking (
	idClienteRanking int,
    idCliente int,
    idRank int,
    posicaoRank int,
    stat varchar(7)
);

alter table platform_database.Clientes_Ranking add constraint PK_idClienteRanking primary key (idClienteRanking,idCliente,idRank);
alter table platform_database.Clientes_Ranking add constraint FK_idCliente foreign key (idCliente) references platform_database.Clientes(idCliente);
alter table platform_database.Clientes_Ranking add constraint FK_idRank foreign key (idRank) references platform_database.Ranking(idRank);
alter table platform_database.Clientes_Ranking modify idClienteRanking int auto_increment;

create table if not exists platform_database.PedVenda_cabecalho (
	idPedCabecalho int,
    idCliente int,
    dtPedido date not null,
    dtEntrega date not null,
    desconto decimal(3,2) not null,
    valTotal decimal(10,2) not null
);

alter table platform_database.PedVenda_cabecalho add constraint PK_idPedCabecalho primary key (idPedCabecalho,idCliente);
alter table platform_database.PedVenda_cabecalho add constraint FK_idClienteVend foreign key (idCliente) references platform_database.Clientes(idCliente);
alter table platform_database.PedVenda_cabecalho modify idPedCabecalho int auto_increment;

create table if not exists platform_database.PedVenda_linhas (
	idPedLinhas int,
    idPedCabecalho int,
    idItem int,    
    valUnit decimal(10,2) not null,
    pedQtde int not null
);

alter table platform_database.PedVenda_linhas add constraint PK_idPedLinhas primary key (idPedLinhas,idPedCabecalho,idItem);
alter table platform_database.PedVenda_linhas add constraint FK_idPedCabecalho foreign key (idPedCabecalho) references platform_database.PedVenda_cabecalho(idPedCabecalho);
alter table platform_database.PedVenda_linhas add constraint FK_idItem foreign key (idItem) references platform_database.Item(idItem);
alter table platform_database.PedVenda_linhas modify idPedLinhas int auto_increment;

/* Carga de dados nas tabelas Item / Clientes / Pedidos */

insert into platform_database.Item (descricao, valorUnit, qtdeEstoque) values ('Produto A',10 ,100);
insert into platform_database.Item (descricao, valorUnit, qtdeEstoque) values ('Produto B',15 ,200);
insert into platform_database.Item (descricao, valorUnit, qtdeEstoque) values ('Produto C',20 ,300);
insert into platform_database.Item (descricao, valorUnit, qtdeEstoque) values ('Produto D',25 ,400);
insert into platform_database.Item (descricao, valorUnit, qtdeEstoque) values ('Produto E',30 ,500);

insert into platform_database.Clientes (nomeCliente, senhaCliente, emailCliente, ufCliente) values ('Cliente 1', '@senha1', 'teste1@gmail.com', 'SP');
insert into platform_database.Clientes (nomeCliente, senhaCliente, emailCliente, ufCliente) values ('Cliente 2', '@senha2', 'teste2@gmail.com', 'RJ');
insert into platform_database.Clientes (nomeCliente, senhaCliente, emailCliente, ufCliente) values ('Cliente 3', '@senha3', 'teste3@gmail.com', 'SC');
insert into platform_database.Clientes (nomeCliente, senhaCliente, emailCliente, ufCliente) values ('Cliente 4', '@senha4', 'teste4@gmail.com', 'PB');
insert into platform_database.Clientes (nomeCliente, senhaCliente, emailCliente, ufCliente) values ('Cliente 5', '@senha5', 'teste5@gmail.com', 'AM');
insert into platform_database.Clientes (nomeCliente, senhaCliente, emailCliente, ufCliente) values ('Cliente 6', '@senha6', 'teste6@gmail.com', 'PE');

insert into platform_database.PedVenda_cabecalho (idCliente, dtPedido, dtEntrega, desconto, valTotal) values ( 1, '2019/01/01', '2019/01/11', 0.12, 1254);
insert into platform_database.PedVenda_cabecalho (idCliente, dtPedido, dtEntrega, desconto, valTotal) values ( 1, '2019/02/01', '2019/02/11', 0.12, 396);
insert into platform_database.PedVenda_cabecalho (idCliente, dtPedido, dtEntrega, desconto, valTotal) values ( 2, '2019/01/01', '2019/01/11', 0.07, 813.75);

insert into platform_database.PedVenda_linhas (idPedCabecalho, idItem, valUnit, pedQtde) values ( 1,  1,  10, 50);
insert into platform_database.PedVenda_linhas (idPedCabecalho, idItem, valUnit, pedQtde) values ( 1,  2,  15, 35);
insert into platform_database.PedVenda_linhas (idPedCabecalho, idItem, valUnit, pedQtde) values ( 1,  3,  20, 20);
insert into platform_database.PedVenda_linhas (idPedCabecalho, idItem, valUnit, pedQtde) values ( 2,  1,  10, 15);
insert into platform_database.PedVenda_linhas (idPedCabecalho, idItem, valUnit, pedQtde) values ( 2,  2,  15, 20);
insert into platform_database.PedVenda_linhas (idPedCabecalho, idItem, valUnit, pedQtde) values ( 3,  4,  25, 35);

