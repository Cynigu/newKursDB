-- MySQL dump 10.13  Distrib 8.0.24, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: kursdb
-- ------------------------------------------------------
-- Server version	8.0.24

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `документ_поступления`
--

DROP TABLE IF EXISTS `документ_поступления`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `документ_поступления` (
  `id_Документ` int NOT NULL AUTO_INCREMENT,
  `Описание` varchar(45) COLLATE utf8_esperanto_ci DEFAULT NULL,
  `Дата_изменения` datetime DEFAULT NULL,
  `Статус` varchar(45) COLLATE utf8_esperanto_ci DEFAULT NULL,
  `id_Склада` int NOT NULL,
  PRIMARY KEY (`id_Документ`),
  UNIQUE KEY `id_Документ_UNIQUE` (`id_Документ`),
  KEY `Склад_idx` (`id_Склада`),
  CONSTRAINT `Номер_Склада` FOREIGN KEY (`id_Склада`) REFERENCES `склад` (`id Склад`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `документ_поступления`
--

LOCK TABLES `документ_поступления` WRITE;
/*!40000 ALTER TABLE `документ_поступления` DISABLE KEYS */;
/*!40000 ALTER TABLE `документ_поступления` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `документ_реализации`
--

DROP TABLE IF EXISTS `документ_реализации`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `документ_реализации` (
  `id_Документ` int NOT NULL AUTO_INCREMENT,
  `Описание` varchar(45) COLLATE utf8_esperanto_ci DEFAULT NULL,
  `Дата_изменения` datetime DEFAULT NULL,
  `Статус` varchar(45) COLLATE utf8_esperanto_ci DEFAULT NULL,
  `id_Склада` int NOT NULL,
  PRIMARY KEY (`id_Документ`),
  UNIQUE KEY `id_Документ_UNIQUE` (`id_Документ`),
  KEY `Склад_idx` (`id_Склада`),
  CONSTRAINT `Склад_Документ_Реализации` FOREIGN KEY (`id_Склада`) REFERENCES `склад` (`id Склад`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `документ_реализации`
--

LOCK TABLES `документ_реализации` WRITE;
/*!40000 ALTER TABLE `документ_реализации` DISABLE KEYS */;
/*!40000 ALTER TABLE `документ_реализации` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `запас`
--

DROP TABLE IF EXISTS `запас`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `запас` (
  `Склад` int NOT NULL,
  `НП` int NOT NULL,
  `Кол-во` int DEFAULT NULL,
  PRIMARY KEY (`Склад`,`НП`),
  KEY `НП_idx` (`НП`),
  KEY `Склад_idx` (`Склад`),
  CONSTRAINT `Номер_Склада_Запас` FOREIGN KEY (`Склад`) REFERENCES `склад` (`id Склад`),
  CONSTRAINT `НП_Запас` FOREIGN KEY (`НП`) REFERENCES `нп` (`id_НП`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `запас`
--

LOCK TABLES `запас` WRITE;
/*!40000 ALTER TABLE `запас` DISABLE KEYS */;
INSERT INTO `запас` VALUES (1,34,104),(2,35,1230),(2,36,2312),(3,37,123),(4,31,0),(4,32,1),(4,33,0),(4,38,0);
/*!40000 ALTER TABLE `запас` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `компонент`
--

DROP TABLE IF EXISTS `компонент`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `компонент` (
  `Позиция` int NOT NULL AUTO_INCREMENT,
  `Спецификация` int NOT NULL,
  `НП` int NOT NULL,
  `Кол-во` int DEFAULT NULL,
  PRIMARY KEY (`Позиция`,`Спецификация`),
  KEY `Компонент_idx` (`НП`),
  KEY `Номер_спецификации_idx` (`Спецификация`),
  CONSTRAINT `Компонент` FOREIGN KEY (`НП`) REFERENCES `нп` (`id_НП`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Номер_спецификации` FOREIGN KEY (`Спецификация`) REFERENCES `спецификация` (`id_Спецификация`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `компонент`
--

LOCK TABLES `компонент` WRITE;
/*!40000 ALTER TABLE `компонент` DISABLE KEYS */;
INSERT INTO `компонент` VALUES (1,2,34,1),(6,1,37,1),(8,3,36,12),(9,8,36,33),(11,9,35,12),(12,9,35,12),(13,1,35,4),(14,1,36,4);
/*!40000 ALTER TABLE `компонент` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `метод_возобновления`
--

DROP TABLE IF EXISTS `метод_возобновления`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `метод_возобновления` (
  `idМетод_возобновления` int NOT NULL,
  `название` varchar(45) COLLATE utf8_esperanto_ci NOT NULL,
  PRIMARY KEY (`idМетод_возобновления`),
  UNIQUE KEY `название_UNIQUE` (`название`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `метод_возобновления`
--

LOCK TABLES `метод_возобновления` WRITE;
/*!40000 ALTER TABLE `метод_возобновления` DISABLE KEYS */;
INSERT INTO `метод_возобновления` VALUES (3,'Перемещение'),(1,'Покупка'),(2,'Производство');
/*!40000 ALTER TABLE `метод_возобновления` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `метод_списания`
--

DROP TABLE IF EXISTS `метод_списания`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `метод_списания` (
  `idМетод_списания` int NOT NULL,
  `название` varchar(45) COLLATE utf8_esperanto_ci NOT NULL,
  PRIMARY KEY (`idМетод_списания`),
  UNIQUE KEY `название_UNIQUE` (`название`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `метод_списания`
--

LOCK TABLES `метод_списания` WRITE;
/*!40000 ALTER TABLE `метод_списания` DISABLE KEYS */;
INSERT INTO `метод_списания` VALUES (1,'Автоматичский'),(2,'Ручной');
/*!40000 ALTER TABLE `метод_списания` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `мк`
--

DROP TABLE IF EXISTS `мк`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `мк` (
  `id_МК` int NOT NULL AUTO_INCREMENT,
  `Дата_изменения` datetime DEFAULT NULL,
  `Статус` varchar(45) COLLATE utf8_esperanto_ci DEFAULT NULL,
  PRIMARY KEY (`id_МК`),
  UNIQUE KEY `id_МК_UNIQUE` (`id_МК`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `мк`
--

LOCK TABLES `мк` WRITE;
/*!40000 ALTER TABLE `мк` DISABLE KEYS */;
INSERT INTO `мк` VALUES (1,'2005-05-20 21:00:00','сертифицирована'),(4,'2005-05-20 21:00:00','в разработке');
/*!40000 ALTER TABLE `мк` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `нп`
--

DROP TABLE IF EXISTS `нп`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `нп` (
  `id_НП` int NOT NULL AUTO_INCREMENT,
  `Наименование` varchar(45) COLLATE utf8_esperanto_ci NOT NULL,
  `Ед_изм` varchar(45) COLLATE utf8_esperanto_ci NOT NULL,
  `Точка_доказа` int DEFAULT NULL,
  `Кол-во_для_доказа` int DEFAULT NULL,
  `Период_ожидания` int DEFAULT NULL,
  `Метод_возобновления` int DEFAULT NULL,
  `Метод_списания` int DEFAULT NULL,
  `Маршрут` int DEFAULT NULL,
  `Спецификация` int DEFAULT NULL,
  PRIMARY KEY (`id_НП`),
  UNIQUE KEY `id_НП_UNIQUE` (`id_НП`) /*!80000 INVISIBLE */,
  KEY `id_МК_idx` (`Маршрут`),
  KEY `Спецификация_idx` (`Спецификация`),
  KEY `Метод_списания_idx` (`Метод_списания`),
  KEY `м_возобновления_idx` (`Метод_возобновления`),
  CONSTRAINT `м_возобновления` FOREIGN KEY (`Метод_возобновления`) REFERENCES `метод_возобновления` (`idМетод_возобновления`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `м_списания` FOREIGN KEY (`Метод_списания`) REFERENCES `метод_списания` (`idМетод_списания`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `Маршрутная_карта` FOREIGN KEY (`Маршрут`) REFERENCES `мк` (`id_МК`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `Спецификация` FOREIGN KEY (`Спецификация`) REFERENCES `спецификация` (`id_Спецификация`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `нп`
--

LOCK TABLES `нп` WRITE;
/*!40000 ALTER TABLE `нп` DISABLE KEYS */;
INSERT INTO `нп` VALUES (31,'Диван 1','22',0,0,0,2,1,1,2),(32,'Диван 2','шт',0,0,0,2,1,1,1),(33,'Кресло 1','шт',0,0,0,2,1,1,NULL),(34,'Дуб красный','т',12,100,1233,1,1,NULL,NULL),(35,'Ткань хлопок','м',12,100,123,1,NULL,NULL,NULL),(36,'Ткань белая','м',12,100,123,1,NULL,NULL,NULL),(37,'Каркас дуб красный','шт',0,0,0,2,1,4,2),(38,'Табурет','шт',0,0,0,2,1,4,3);
/*!40000 ALTER TABLE `нп` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `операция`
--

DROP TABLE IF EXISTS `операция`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `операция` (
  `id_Операция` int NOT NULL AUTO_INCREMENT,
  `id_Маршрута` int NOT NULL,
  `Время_обработки` int NOT NULL,
  `Время_перехода` int NOT NULL,
  `Время_наладки` int NOT NULL,
  `РЦ` int DEFAULT NULL,
  `Описание` varchar(45) COLLATE utf8_esperanto_ci DEFAULT NULL,
  PRIMARY KEY (`id_Операция`,`id_Маршрута`),
  KEY `Маршрут_idx` (`id_Маршрута`),
  KEY `РЦ_idx` (`РЦ`),
  CONSTRAINT `Маршрут` FOREIGN KEY (`id_Маршрута`) REFERENCES `мк` (`id_МК`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `РЦ` FOREIGN KEY (`РЦ`) REFERENCES `рц` (`id_РЦ`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `операция`
--

LOCK TABLES `операция` WRITE;
/*!40000 ALTER TABLE `операция` DISABLE KEYS */;
INSERT INTO `операция` VALUES (1,1,234,34,12,2,'Распил фанеры и досок'),(1,4,234,24,15,2,'цу'),(2,1,343,34,12,2,'Сборка каркаса'),(3,1,400,12,15,3,'Монтаж пружинного блока'),(4,1,323,10,12,4,'Раскрой и закрепление обивки'),(5,1,222,24,12,4,'Пошив чехлов'),(6,1,183,24,12,4,'Сборка');
/*!40000 ALTER TABLE `операция` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `пз`
--

DROP TABLE IF EXISTS `пз`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `пз` (
  `id_ПЗ` int NOT NULL AUTO_INCREMENT,
  `id_НП` int NOT NULL,
  `Описание` varchar(45) COLLATE utf8_esperanto_ci DEFAULT NULL,
  `Кол-во` int DEFAULT NULL,
  `Дата_начала` datetime DEFAULT NULL,
  `Дата_завершения` datetime DEFAULT NULL,
  `Статус` int DEFAULT NULL,
  PRIMARY KEY (`id_ПЗ`),
  UNIQUE KEY `id_ПЗ_UNIQUE` (`id_ПЗ`),
  KEY `ГП_idx` (`id_НП`),
  KEY `СтатусЗ_idx` (`Статус`),
  CONSTRAINT `ГП` FOREIGN KEY (`id_НП`) REFERENCES `нп` (`id_НП`),
  CONSTRAINT `СтатусЗ` FOREIGN KEY (`Статус`) REFERENCES `статус_заказа` (`idСтатус_Заказа`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `пз`
--

LOCK TABLES `пз` WRITE;
/*!40000 ALTER TABLE `пз` DISABLE KEYS */;
INSERT INTO `пз` VALUES (1,31,'Диван1',1,'0001-01-13 00:00:00','0001-02-14 03:33:00',1),(2,32,'Диван2',12,'2021-05-31 00:00:00','2021-07-08 08:38:00',3);
/*!40000 ALTER TABLE `пз` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `пользователи`
--

DROP TABLE IF EXISTS `пользователи`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `пользователи` (
  `Логин` varchar(45) COLLATE utf8_esperanto_ci NOT NULL,
  `Пароль` varchar(45) COLLATE utf8_esperanto_ci NOT NULL,
  `Роль` varchar(45) COLLATE utf8_esperanto_ci NOT NULL,
  PRIMARY KEY (`Логин`),
  UNIQUE KEY `Логин_UNIQUE` (`Логин`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `пользователи`
--

LOCK TABLES `пользователи` WRITE;
/*!40000 ALTER TABLE `пользователи` DISABLE KEYS */;
INSERT INTO `пользователи` VALUES ('admin','admin','Администратор');
/*!40000 ALTER TABLE `пользователи` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `поступление`
--

DROP TABLE IF EXISTS `поступление`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `поступление` (
  `НП` int NOT NULL,
  `Кол-во` int DEFAULT NULL,
  `Id_Документа` int NOT NULL,
  PRIMARY KEY (`Id_Документа`,`НП`),
  KEY `НП_idx` (`НП`),
  CONSTRAINT `Номер_документа_поступления` FOREIGN KEY (`Id_Документа`) REFERENCES `документ_поступления` (`id_Документ`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `НП` FOREIGN KEY (`НП`) REFERENCES `нп` (`id_НП`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `поступление`
--

LOCK TABLES `поступление` WRITE;
/*!40000 ALTER TABLE `поступление` DISABLE KEYS */;
/*!40000 ALTER TABLE `поступление` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `реализация`
--

DROP TABLE IF EXISTS `реализация`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `реализация` (
  `НП` int NOT NULL,
  `Кол-во` int DEFAULT NULL,
  `Id_Документа` int NOT NULL,
  PRIMARY KEY (`Id_Документа`,`НП`),
  KEY `НП_idx` (`НП`),
  CONSTRAINT `Номер_Документа_Реализации` FOREIGN KEY (`Id_Документа`) REFERENCES `документ_реализации` (`id_Документ`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `НП_Реализация` FOREIGN KEY (`НП`) REFERENCES `нп` (`id_НП`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `реализация`
--

LOCK TABLES `реализация` WRITE;
/*!40000 ALTER TABLE `реализация` DISABLE KEYS */;
/*!40000 ALTER TABLE `реализация` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `рц`
--

DROP TABLE IF EXISTS `рц`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `рц` (
  `id_РЦ` int NOT NULL AUTO_INCREMENT,
  `Ежедневная_мощность` int NOT NULL,
  `Описание` varchar(45) COLLATE utf8_esperanto_ci DEFAULT NULL,
  PRIMARY KEY (`id_РЦ`),
  UNIQUE KEY `id_РЦ_UNIQUE` (`id_РЦ`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `рц`
--

LOCK TABLES `рц` WRITE;
/*!40000 ALTER TABLE `рц` DISABLE KEYS */;
INSERT INTO `рц` VALUES (2,600,'Рабочий1'),(3,600,'Рабочий2'),(4,600,'рабочий3');
/*!40000 ALTER TABLE `рц` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `склад`
--

DROP TABLE IF EXISTS `склад`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `склад` (
  `id Склад` int NOT NULL AUTO_INCREMENT,
  `Описание` varchar(45) COLLATE utf8_esperanto_ci DEFAULT NULL,
  PRIMARY KEY (`id Склад`),
  UNIQUE KEY `id_Warehouse_UNIQUE` (`id Склад`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `склад`
--

LOCK TABLES `склад` WRITE;
/*!40000 ALTER TABLE `склад` DISABLE KEYS */;
INSERT INTO `склад` VALUES (1,'Склад дерева, фанеры'),(2,'Склад ткани, фурнитуры'),(3,'Склад промежуточной продукции'),(4,'Склад готовой продукции');
/*!40000 ALTER TABLE `склад` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `спецификация`
--

DROP TABLE IF EXISTS `спецификация`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `спецификация` (
  `id_Спецификация` int NOT NULL AUTO_INCREMENT,
  `Статус` int DEFAULT NULL,
  `Дата_изменения` datetime DEFAULT NULL,
  PRIMARY KEY (`id_Спецификация`),
  UNIQUE KEY `id_Specification_UNIQUE` (`id_Спецификация`),
  KEY `статусСП_idx` (`Статус`),
  CONSTRAINT `статусСП` FOREIGN KEY (`Статус`) REFERENCES `статус_спецификации` (`idстатус_спецификации`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `спецификация`
--

LOCK TABLES `спецификация` WRITE;
/*!40000 ALTER TABLE `спецификация` DISABLE KEYS */;
INSERT INTO `спецификация` VALUES (1,2,'2021-05-23 22:35:47'),(2,2,'2021-05-23 22:35:51'),(3,3,'2021-05-23 22:35:26'),(8,1,'2021-05-23 22:34:23'),(9,1,'2021-05-24 14:07:58');
/*!40000 ALTER TABLE `спецификация` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `статус_заказа`
--

DROP TABLE IF EXISTS `статус_заказа`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `статус_заказа` (
  `idСтатус_Заказа` int NOT NULL,
  `Описание` varchar(45) COLLATE utf8_esperanto_ci NOT NULL,
  PRIMARY KEY (`idСтатус_Заказа`),
  UNIQUE KEY `Описание_UNIQUE` (`Описание`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `статус_заказа`
--

LOCK TABLES `статус_заказа` WRITE;
/*!40000 ALTER TABLE `статус_заказа` DISABLE KEYS */;
INSERT INTO `статус_заказа` VALUES (4,'Завершенный'),(3,'Запущенный'),(1,'Плановый'),(2,'Утвержденный');
/*!40000 ALTER TABLE `статус_заказа` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `статус_спецификации`
--

DROP TABLE IF EXISTS `статус_спецификации`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `статус_спецификации` (
  `idстатус_спецификации` int NOT NULL,
  `название` varchar(45) COLLATE utf8_esperanto_ci DEFAULT NULL,
  PRIMARY KEY (`idстатус_спецификации`),
  UNIQUE KEY `название_UNIQUE` (`название`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `статус_спецификации`
--

LOCK TABLES `статус_спецификации` WRITE;
/*!40000 ALTER TABLE `статус_спецификации` DISABLE KEYS */;
INSERT INTO `статус_спецификации` VALUES (1,'в разработке'),(3,'закрыта'),(2,'сертифицирована');
/*!40000 ALTER TABLE `статус_спецификации` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `товарные_операции_выходгп`
--

DROP TABLE IF EXISTS `товарные_операции_выходгп`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `товарные_операции_выходгп` (
  `id_ПЗ` int NOT NULL,
  `id_НП` int NOT NULL,
  `Кол-во` int DEFAULT NULL,
  PRIMARY KEY (`id_ПЗ`,`id_НП`),
  KEY `нп_выходгп_idx` (`id_НП`),
  CONSTRAINT `нп_выходгп` FOREIGN KEY (`id_НП`) REFERENCES `нп` (`id_НП`),
  CONSTRAINT `пз_выходгп` FOREIGN KEY (`id_ПЗ`) REFERENCES `пз` (`id_ПЗ`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `товарные_операции_выходгп`
--

LOCK TABLES `товарные_операции_выходгп` WRITE;
/*!40000 ALTER TABLE `товарные_операции_выходгп` DISABLE KEYS */;
INSERT INTO `товарные_операции_выходгп` VALUES (2,32,1),(2,37,1);
/*!40000 ALTER TABLE `товарные_операции_выходгп` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `товарные_операции_выходгп_AFTER_INSERT` AFTER INSERT ON `товарные_операции_выходгп` FOR EACH ROW BEGIN
update запас 
set `Кол-во` = 
(select (запас.`Кол-во`+new.`Кол-во`) 
from товарные_операции_выходгп
where id_НП = new.id_НП)
where НП = new.id_НП;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `товарные_операции_выходгп_AFTER_UPDATE` AFTER UPDATE ON `товарные_операции_выходгп` FOR EACH ROW BEGIN
update запас 
set `Кол-во` = 
(select (запас.`Кол-во`+(new.`Кол-во`-old.`Кол-во`)) 
from товарные_операции_выходгп
where id_НП = new.id_НП)
where НП = new.id_НП;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `товарные_операции_выходгп_BEFORE_DELETE` BEFORE DELETE ON `товарные_операции_выходгп` FOR EACH ROW BEGIN
update запас 
set `Кол-во` = 
(select (запас.`Кол-во`- old.`Кол-во`) 
from товарные_операции_выходгп
where id_НП = old.id_НП)
where НП = old.id_НП;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `товарные_операции_потребление`
--

DROP TABLE IF EXISTS `товарные_операции_потребление`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `товарные_операции_потребление` (
  `id_ПЗ` int NOT NULL,
  `id_НП` int NOT NULL,
  `Кол-во` int DEFAULT NULL,
  PRIMARY KEY (`id_ПЗ`,`id_НП`),
  KEY `нп_потребление_idx` (`id_НП`),
  CONSTRAINT `нп_потребление` FOREIGN KEY (`id_НП`) REFERENCES `нп` (`id_НП`),
  CONSTRAINT `пз_потребление` FOREIGN KEY (`id_ПЗ`) REFERENCES `пз` (`id_ПЗ`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `товарные_операции_потребление`
--

LOCK TABLES `товарные_операции_потребление` WRITE;
/*!40000 ALTER TABLE `товарные_операции_потребление` DISABLE KEYS */;
INSERT INTO `товарные_операции_потребление` VALUES (2,34,4),(2,35,4),(2,37,1);
/*!40000 ALTER TABLE `товарные_операции_потребление` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `товарные_операции_потребление_AFTER_INSERT` AFTER INSERT ON `товарные_операции_потребление` FOR EACH ROW BEGIN
update запас 
set `Кол-во` = 
(select (запас.`Кол-во`-new.`Кол-во`) 
from товарные_операции_потребление
where id_НП = new.id_НП)
where НП = new.id_НП;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `товарные_операции_потребление_AFTER_UPDATE` AFTER UPDATE ON `товарные_операции_потребление` FOR EACH ROW BEGIN
update запас 
set `Кол-во` = 
(select (запас.`Кол-во`-(new.`Кол-во`-old.`Кол-во`)) 
from товарные_операции_потребление
where id_НП = new.id_НП)
where НП = new.id_НП;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `товарные_операции_потребление_BEFORE_DELETE` BEFORE DELETE ON `товарные_операции_потребление` FOR EACH ROW BEGIN
update запас 
set `Кол-во` = 
(select (запас.`Кол-во`+ old.`Кол-во`) 
from товарные_операции_потребление
where id_НП = old.id_НП)
where НП = old.id_НП;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `учтенные_операции_мощности`
--

DROP TABLE IF EXISTS `учтенные_операции_мощности`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `учтенные_операции_мощности` (
  `id_ПЗ` int NOT NULL,
  `id_Операции` int NOT NULL,
  `Начало работы` datetime DEFAULT NULL,
  `Окончание работы` datetime DEFAULT NULL,
  PRIMARY KEY (`id_ПЗ`,`id_Операции`),
  KEY `Операция_idx` (`id_Операции`),
  CONSTRAINT `Операция` FOREIGN KEY (`id_Операции`) REFERENCES `операция` (`id_Операция`),
  CONSTRAINT `ПЗ` FOREIGN KEY (`id_ПЗ`) REFERENCES `пз` (`id_ПЗ`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_esperanto_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `учтенные_операции_мощности`
--

LOCK TABLES `учтенные_операции_мощности` WRITE;
/*!40000 ALTER TABLE `учтенные_операции_мощности` DISABLE KEYS */;
INSERT INTO `учтенные_операции_мощности` VALUES (2,1,'2021-05-31 02:32:59','2021-05-31 02:33:00'),(2,2,'2021-05-31 02:33:07','2021-06-01 00:00:00'),(2,3,'2021-05-31 02:33:12','2021-06-02 00:00:00'),(2,4,'2021-06-01 00:00:00','2021-06-02 00:00:00'),(2,5,'2021-05-31 02:33:27','2021-05-31 02:33:27'),(2,6,'2021-05-31 02:33:31','2021-05-31 02:33:32');
/*!40000 ALTER TABLE `учтенные_операции_мощности` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'kursdb'
--

--
-- Dumping routines for database 'kursdb'
--
/*!50003 DROP FUNCTION IF EXISTS `compute_sum` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `compute_sum`(id int, count int) RETURNS int
    DETERMINISTIC
BEGIN
DECLARE result int;
create temporary table `t1` 
select summar, РЦ  from 
(select count*sum(Время_обработки + Время_перехода + Время_наладки) as summar, РЦ  from  операция as o1
where id_Маршрута = (select Маршрут from нп where id_НП = id) group by РЦ) as o2
inner join рц on рц.id_РЦ = o2.РЦ
where Ежедневная_мощность > o2.summar 
union
select (summar + (summar/Ежедневная_мощность-1)*(1440-Ежедневная_мощность))as summar, РЦ  from 
(select count*sum(Время_обработки + Время_перехода + Время_наладки) as summar, РЦ  from  операция as o1
where id_Маршрута = (select Маршрут from нп where id_НП = id) group by РЦ) as o2
inner join рц on рц.id_РЦ = o2.РЦ
where Ежедневная_мощность < o2.summar
and o2.summar%Ежедневная_мощность = 0
union
select (summar + summar/Ежедневная_мощность*(1440-Ежедневная_мощность))as summar, РЦ from 
(select count*sum(Время_обработки + Время_перехода + Время_наладки) as summar, РЦ  from  операция as o1
where id_Маршрута = (select Маршрут from нп where id_НП = id) group by РЦ) as o2
inner join рц on рц.id_РЦ = o2.РЦ
where Ежедневная_мощность < o2.summar
and o2.summar%Ежедневная_мощность != 0;

(select sum(summar) as s into result from t1 limit 1);

drop temporary table t1;

RETURN (result);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-06-10  3:56:19
