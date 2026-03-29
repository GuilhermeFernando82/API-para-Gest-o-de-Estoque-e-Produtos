CREATE TABLE IF NOT EXISTS usuarios (
    id UUID PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    senhahash VARCHAR(200) NOT NULL,
    tipo INT NOT NULL
);

CREATE TABLE IF NOT EXISTS produtos (
    id UUID PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    descricao TEXT NOT NULL,
    preco NUMERIC(18,2) NOT NULL,
    categoria varchar(50) NULL
);

CREATE TABLE IF NOT EXISTS estoques (
    id UUID PRIMARY KEY,
    produtoid UUID NOT NULL REFERENCES produtos(id),
    quantidade INT NOT NULL,
    numeronotafiscal VARCHAR(100) NOT NULL,
    datamovimentacao TIMESTAMP NOT NULL
);

CREATE TABLE IF NOT EXISTS pedidos (
    id UUID PRIMARY KEY,
    documentocliente VARCHAR(50) NOT NULL,
    nomevendedor VARCHAR(100) NOT NULL,
    datacriacao TIMESTAMP NOT NULL
);

CREATE TABLE IF NOT EXISTS itenspedido (
    id UUID PRIMARY KEY,
    pedidoid UUID NOT NULL REFERENCES pedidos(id),
    produtoid UUID NOT NULL REFERENCES produtos(id),
    quantidade INT NOT NULL,
    precounitario NUMERIC(18,2) NOT NULL
);
