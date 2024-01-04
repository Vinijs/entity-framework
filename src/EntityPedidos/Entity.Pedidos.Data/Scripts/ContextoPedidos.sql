ALTER DATABASE CHARACTER SET utf8mb4;


CREATE TABLE `cupom_descontos` (
    `cupom_desconto_id` int NOT NULL AUTO_INCREMENT,
    `codigo_cupom` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `percentual_desconto` decimal(65,30) NULL,
    `valor_desconto` decimal(65,30) NULL,
    `quantidade` int NOT NULL,
    `tipo_cupom_desconto` int NOT NULL,
    `criado_em` datetime(6) NOT NULL,
    `aplicado_em` datetime(6) NOT NULL,
    `data_expiracao` datetime(6) NOT NULL,
    `ativo` tinyint(1) NOT NULL,
    `aplicado` tinyint(1) NOT NULL,
    CONSTRAINT `PK_cupom_descontos` PRIMARY KEY (`cupom_desconto_id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `enderecos` (
    `id` int NOT NULL AUTO_INCREMENT,
    `logradouro` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `cep` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `bairro` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `numero` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `complemento` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `cidade` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `estado` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    CONSTRAINT `PRIMARY` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `pedidos` (
    `id` int NOT NULL AUTO_INCREMENT,
    `Codigo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `cliente_id` int NOT NULL,
    `endereco_id` int NOT NULL,
    `Desconto` decimal(65,30) NOT NULL,
    `valor_total` double NOT NULL,
    `data` datetime(6) NOT NULL,
    `CupomDescontoId` int NOT NULL,
    `PedidoStatus` int NOT NULL,
    CONSTRAINT `PRIMARY` PRIMARY KEY (`id`),
    CONSTRAINT `FK_pedidos_cupom_descontos_CupomDescontoId` FOREIGN KEY (`CupomDescontoId`) REFERENCES `cupom_descontos` (`CupomDescontoId`) ON DELETE CASCADE,
    CONSTRAINT `FK_pedidos_enderecos_endereco_id` FOREIGN KEY (`endereco_id`) REFERENCES `enderecos` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `pedido_itens` (
    `pedido_item_id` int NOT NULL AUTO_INCREMENT,
    `pedido_id` int NOT NULL,
    `produto_id` int NOT NULL,
    `nome_produto` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `quantidade` int NOT NULL,
    `valor_unitario` decimal(65,30) NOT NULL,
    CONSTRAINT `PK_pedido_itens` PRIMARY KEY (`pedido_item_id`),
    CONSTRAINT `FK_pedido_itens_pedidos_PedidoId` FOREIGN KEY (`pedido_id`) REFERENCES `pedidos` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE INDEX `IX_pedido_itens_PedidoId` ON `pedido_itens` (`PedidoId`);


CREATE INDEX `IX_pedidos_cliente_id` ON `pedidos` (`cliente_id`);


CREATE INDEX `IX_pedidos_CupomDescontoId` ON `pedidos` (`CupomDescontoId`);


CREATE INDEX `IX_pedidos_endereco_id` ON `pedidos` (`endereco_id`);


