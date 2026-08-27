-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: localhost    Database: indicadores
-- ------------------------------------------------------
-- Server version	8.0.27

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
-- Table structure for table `almacen`
--

DROP TABLE IF EXISTS `almacen`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `almacen` (
  `cod` int NOT NULL AUTO_INCREMENT,
  `mes` varchar(10) NOT NULL,
  `año` varchar(10) NOT NULL,
  `valordelaperdida` varchar(15) NOT NULL DEFAULT '',
  `inventariototal` varchar(15) NOT NULL DEFAULT '',
  `result1` varchar(15) NOT NULL DEFAULT '',
  `productosverificados` varchar(15) NOT NULL DEFAULT '',
  `productosaverificar` varchar(15) NOT NULL DEFAULT '',
  `result2` varchar(15) NOT NULL DEFAULT '',
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `almacen`
--

LOCK TABLES `almacen` WRITE;
/*!40000 ALTER TABLE `almacen` DISABLE KEYS */;
INSERT INTO `almacen` VALUES (14,'enero','2022','','','','95','100','95.00'),(15,'febrero','2022','','','','46','50','92.00'),(16,'marzo','2022','','','','45','49','91.84');
/*!40000 ALTER TABLE `almacen` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `compras`
--

DROP TABLE IF EXISTS `compras`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `compras` (
  `cod` int NOT NULL AUTO_INCREMENT,
  `mes` varchar(10) DEFAULT NULL,
  `año` varchar(10) DEFAULT NULL,
  `ordenesrealizadas` varchar(15) NOT NULL DEFAULT '',
  `totaldeordenes` varchar(15) NOT NULL DEFAULT '',
  `result1` varchar(15) NOT NULL DEFAULT '',
  `ordenesabastecidas` varchar(15) NOT NULL DEFAULT '',
  `totalordenesabastecidas` varchar(15) NOT NULL DEFAULT '',
  `result2` varchar(15) NOT NULL DEFAULT '',
  `reportesentregados` varchar(15) NOT NULL DEFAULT '',
  `totalreportes` varchar(15) NOT NULL DEFAULT '',
  `result3` varchar(15) NOT NULL DEFAULT '',
  `evaluacionesrealizadas` varchar(15) NOT NULL DEFAULT '',
  `evaluacionesprogramadas` varchar(15) NOT NULL DEFAULT '',
  `result4` varchar(15) NOT NULL DEFAULT '',
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=200 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `compras`
--

LOCK TABLES `compras` WRITE;
/*!40000 ALTER TABLE `compras` DISABLE KEYS */;
INSERT INTO `compras` VALUES (198,'enero','2022','152','150','101.33','12','13','92.31','21','25','84.00','','',''),(199,'febrero','2022','','','','23','24','95.83','15','17','88.24','','','');
/*!40000 ALTER TABLE `compras` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contabilidad`
--

DROP TABLE IF EXISTS `contabilidad`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `contabilidad` (
  `cod` int NOT NULL AUTO_INCREMENT,
  `mes` varchar(10) DEFAULT NULL,
  `año` varchar(10) DEFAULT NULL,
  `diadelmes` varchar(15) NOT NULL DEFAULT '',
  `result1` varchar(15) NOT NULL DEFAULT '',
  `librosactualizados` varchar(15) NOT NULL DEFAULT '',
  `libroscontables` varchar(15) NOT NULL DEFAULT '',
  `result2` varchar(15) NOT NULL DEFAULT '',
  `ordenesabastecidas` varchar(15) NOT NULL DEFAULT '',
  `totaldeordenes` varchar(15) NOT NULL DEFAULT '',
  `result3` varchar(15) NOT NULL DEFAULT '',
  `díadepresentación` varchar(15) NOT NULL DEFAULT '',
  `result4` varchar(15) NOT NULL DEFAULT '',
  `tiempotramiteendías` varchar(15) NOT NULL DEFAULT '',
  `result5` varchar(15) NOT NULL DEFAULT '',
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contabilidad`
--

LOCK TABLES `contabilidad` WRITE;
/*!40000 ALTER TABLE `contabilidad` DISABLE KEYS */;
INSERT INTO `contabilidad` VALUES (13,'enero','2022','10','10.00','36','41','87.80','65','77','84.42','4','4.00','1','1.00'),(14,'febrero','2022','9','9.00','12','13','92.31','27','29','93.10','5','5.00','5','5.00');
/*!40000 ALTER TABLE `contabilidad` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `credito`
--

DROP TABLE IF EXISTS `credito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `credito` (
  `cod` int NOT NULL AUTO_INCREMENT,
  `mes` varchar(10) DEFAULT NULL,
  `año` varchar(10) DEFAULT NULL,
  `valordesaldosvencidos` varchar(15) NOT NULL DEFAULT '',
  `valortotalcuentasxcobrar` varchar(15) NOT NULL DEFAULT '',
  `result1` varchar(15) NOT NULL DEFAULT '',
  `cuentasporcobrar` varchar(15) NOT NULL DEFAULT '',
  `ventasalcreditopromedio` varchar(15) NOT NULL DEFAULT '',
  `result2` varchar(15) NOT NULL DEFAULT '',
  `solucitudesatendidas` varchar(15) NOT NULL DEFAULT '',
  `totalsolicitudes` varchar(15) NOT NULL DEFAULT '',
  `result3` varchar(15) NOT NULL DEFAULT '',
  `clientesactualizados` varchar(15) NOT NULL DEFAULT '',
  `totalclientes` varchar(15) NOT NULL DEFAULT '',
  `result4` varchar(15) NOT NULL DEFAULT '',
  `clientesanalizados` varchar(15) NOT NULL DEFAULT '',
  `clientesalcredito` varchar(15) NOT NULL DEFAULT '',
  `result5` varchar(15) NOT NULL DEFAULT '',
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `credito`
--

LOCK TABLES `credito` WRITE;
/*!40000 ALTER TABLE `credito` DISABLE KEYS */;
INSERT INTO `credito` VALUES (15,'enero','2022','23','156','14.74','15644','15216','1.03','18','19','94.74','23','25','92.00','','',''),(16,'febrero','2022','14','101','13.86','234500','5000','46.90','45','52','86.54','23','28','82.14','','','');
/*!40000 ALTER TABLE `credito` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `login`
--

DROP TABLE IF EXISTS `login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `login` (
  `u_cod` int NOT NULL AUTO_INCREMENT,
  `u_usuario` varchar(30) DEFAULT NULL,
  `u_contraseña` varchar(30) DEFAULT NULL,
  `u_tipo` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`u_cod`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `login`
--

LOCK TABLES `login` WRITE;
/*!40000 ALTER TABLE `login` DISABLE KEYS */;
INSERT INTO `login` VALUES (7,'admin','1234','administrador'),(8,'april','11807','compras');
/*!40000 ALTER TABLE `login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `nombreindicadores`
--

DROP TABLE IF EXISTS `nombreindicadores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nombreindicadores` (
  `cod` int NOT NULL AUTO_INCREMENT,
  `orden` varchar(5) NOT NULL,
  `indicadores` varchar(100) DEFAULT NULL,
  `area` varchar(50) DEFAULT NULL,
  `ref1` varchar(20) DEFAULT NULL,
  `ref2` varchar(20) DEFAULT NULL,
  `ref3` varchar(20) DEFAULT NULL,
  `ref4` varchar(20) DEFAULT NULL,
  `frecuenciaMedicion` varchar(30) DEFAULT NULL,
  `valor1` varchar(80) DEFAULT NULL,
  `valor2` varchar(80) DEFAULT NULL,
  `porcentaje` varchar(2) DEFAULT NULL,
  `estado` varchar(10) NOT NULL,
  `creacion` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=72 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nombreindicadores`
--

LOCK TABLES `nombreindicadores` WRITE;
/*!40000 ALTER TABLE `nombreindicadores` DISABLE KEYS */;
INSERT INTO `nombreindicadores` VALUES (6,'1','Tiempo de contracion del personal','rrhh','>11','11','','<=10','trimestral','Tiempo de contratacion','','','activo',''),(7,'2','Indice de rotacion','rrhh','>15','<=15','>=5.01','<=5','mensual','Personal que sale','Total personal','%','activo',''),(8,'3','Ausentismo laboral','rrhh','>6','<=6','>=5.01','<=5','mensual','Dias de ausencia','Total dias laborales','%','activo',''),(23,'4','Cumplimiento al plan anual de vacaciones','rrhh','<90','>=90','<=94.99','>=95','mensual','Vacaciones gozadas','Vacaciones programadas','%','activo',''),(24,'5','Cumplimiento al plan anual de capacitación','rrhh','<80','>=80','<=80.99','>=90','trimestral','Capacitaciones ejecutadas','Capacitaciones programadas','%','activo',''),(29,'1','Meta mensual de ventas','ventas','<90','>=90','<=94.99','>=95','mensual','Ventas del mes','Meta mensual de ventas','%','activo',''),(30,'2','Meta trimestral de ventas','ventas','<90','>=90','<=94.99','>=95','trimestral','Ventas trimestral','Meta trimestral de ventas','%','activo',''),(31,'3','Mantenimiento de clientes de crédito','ventas','<90','>=90','<=94.99','>=95','semestral','Clientes activos','Total clientes','%','activo',''),(32,'4','Clientes nuevos','ventas','<80','>=80','<=89.99','>=90','mensual','Clientes nuevos','Meta clientes nuevos','%','activo',''),(33,'5','Cumplimiento del plan de visitas a clientes','ventas','<80','>=80','<=89.99','>=90','mensual','Visitas realizadas','Visitas programadas','%','activo',''),(36,'1','Tiempo del ciclo de adquisición de productos locales para venta','compras','<90','>=90','<=94.99','>=95','mensual','Ordenes realizadas','Total de ordenes','%','activo',''),(37,'2','Tiempo del ciclo de adquisición de productos locales no para venta','compras','<90','>=90','<=94.99','>=95','mensual','Ordenes abastecidas','Total ordenes abastecidas','%','activo',''),(38,'3','Tiempo de entrega de reportes de compra locales a la Gerencia General','compras','<80','>=80','<=89.99','>=90','mensual','Reportes entregados','Total reportes','%','activo',''),(39,'4','Evaluación a proveedores locales de productos para venta','compras','<80','>=80','<=89.99','>=90','anual','Evaluaciones realizadas','Evaluaciones programadas','%','activo',''),(42,'1','Manejo de la mora','credito','>15','<=15','>=13','<13','mensual','Valor de saldos vencidos','Valor total Cuentas x cobrar','%','activo',''),(43,'2','Periodo Promedio de cobro','credito','>60','<=51','>=60','<=50','mensual','Cuentas por cobrar','Ventas al credito promedio','','activo',''),(44,'3','Tiempo de respuesta para nuevos créditos','credito','<80','>=80','<=89.99','>=90','mensual','Solucitudes atendidas','Total solicitudes','%','activo',''),(45,'4','Actualización de expedientes de clientes de crédito actuales','credito','<90','>=90','<=94.99','>=95','mensual','Clientes actualizados','Total clientes','%','activo',''),(46,'5','Análisis de límites de créditos de clientes','credito','<90','>=90','<=94.99','>=95','semestral','Clientes analizados','Clientes al credito','%','activo',''),(49,'1','Entrega de estados financieros a tiempo','contabilidad','>11','11','','<=10','mensual','Dia del mes','','','activo',''),(50,'2','Posteo libros legales','contabilidad','<80','>=80','<=89.99','>=90','mensual','Libros actualizados','Libros contables','%','activo',''),(51,'3','Posteo libros de compras y ventas','contabilidad','<80','>=80','<=89.99','>=90','mensual','Ordenes abastecidas','Total de ordenes','%','activo',''),(52,'4','Conciliaciones bancarias','contabilidad','>6','6','','>=5','mensual','Día de presentación','','','activo',''),(53,'5','Reembolsos de caja chica','contabilidad','>2','2','','>=1','mensual','Tiempo tramite en días','','','activo',''),(54,'1','Porcentaje de conectividad del sistema','tecnologias','<95','>=95','<=97.99','>98','mensual','Horas conectados','Horas del mes','%','activo',''),(55,'2','Cumplimiento al plan de mantenimiento preventivo de equipo IT','tecnologias','<80','>=80','<=89.99','>=90','mensual','Mantenimientos Realizados','Mantenimientos programados','%','activo',''),(56,'3','Plan de reemplazo de equipo','tecnologias','<80','>=80','<=89.99','>=90','semestral','Equipo reemplazado','Equipo planificado reemplazo','%','activo',''),(58,'1','Efectividad de la administración de inventarios','almacen','>0.15','>=0.10','<=0.15','>=0.10','anual','Valor de la perdida','Inventario total','%','activo',''),(59,'2','Levantamiento de inventario selectivo','almacen','<90','>=90','<=94.99','>=95','mensual','Productos verificados','Productos a verificar','%','activo','');
/*!40000 ALTER TABLE `nombreindicadores` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rrhh`
--

DROP TABLE IF EXISTS `rrhh`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rrhh` (
  `cod` int NOT NULL AUTO_INCREMENT,
  `mes` varchar(10) DEFAULT NULL,
  `año` varchar(10) DEFAULT NULL,
  `tiempodecontratacion` varchar(15) NOT NULL DEFAULT '',
  `result1` varchar(10) NOT NULL DEFAULT '',
  `personalquesale` varchar(15) NOT NULL DEFAULT '',
  `totalpersonal` varchar(15) NOT NULL DEFAULT '',
  `result2` varchar(10) NOT NULL DEFAULT '',
  `diasdeausencia` varchar(15) NOT NULL DEFAULT '',
  `totaldiaslaborales` varchar(15) NOT NULL DEFAULT '',
  `result3` varchar(10) NOT NULL DEFAULT '',
  `vacacionesgozadas` varchar(15) NOT NULL DEFAULT '',
  `vacacionesprogramadas` varchar(15) NOT NULL DEFAULT '',
  `result4` varchar(10) NOT NULL DEFAULT '',
  `capacitacionesejecutadas` varchar(15) NOT NULL DEFAULT '',
  `capacitacionesprogramadas` varchar(15) NOT NULL DEFAULT '',
  `result5` varchar(10) NOT NULL DEFAULT '',
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=596 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rrhh`
--

LOCK TABLES `rrhh` WRITE;
/*!40000 ALTER TABLE `rrhh` DISABLE KEYS */;
INSERT INTO `rrhh` VALUES (591,'enero','2022','','','3','52','5.77','100','1120','8.93','10','11','90.91','','',''),(592,'febrero','2022','','','54','1140','4.74','56','1034','5.42','16','17','94.12','','','');
/*!40000 ALTER TABLE `rrhh` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tecnologias`
--

DROP TABLE IF EXISTS `tecnologias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tecnologias` (
  `cod` int NOT NULL AUTO_INCREMENT,
  `mes` varchar(10) DEFAULT NULL,
  `año` varchar(10) DEFAULT NULL,
  `horasconectados` varchar(15) NOT NULL DEFAULT '',
  `horasdelmes` varchar(15) NOT NULL DEFAULT '',
  `result1` varchar(15) NOT NULL DEFAULT '',
  `mantenimientosrealizados` varchar(15) NOT NULL DEFAULT '',
  `mantenimientosprogramados` varchar(15) NOT NULL DEFAULT '',
  `result2` varchar(15) NOT NULL DEFAULT '',
  `equiporeemplazado` varchar(15) NOT NULL DEFAULT '',
  `equipoplanificadoreemplazo` varchar(15) NOT NULL DEFAULT '',
  `result3` varchar(15) NOT NULL DEFAULT '',
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tecnologias`
--

LOCK TABLES `tecnologias` WRITE;
/*!40000 ALTER TABLE `tecnologias` DISABLE KEYS */;
INSERT INTO `tecnologias` VALUES (12,'enero','2022','98','102','96.08','15','16','93.75','','',''),(13,'febrero','2022','38','37','102.70','9','10','90.00','','','');
/*!40000 ALTER TABLE `tecnologias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ventas`
--

DROP TABLE IF EXISTS `ventas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ventas` (
  `cod` int NOT NULL AUTO_INCREMENT,
  `mes` varchar(15) DEFAULT NULL,
  `año` varchar(15) DEFAULT NULL,
  `ventasdelmes` varchar(15) NOT NULL DEFAULT '',
  `metamensualdeventas` varchar(15) NOT NULL DEFAULT '',
  `result1` varchar(15) NOT NULL DEFAULT '',
  `ventastrimestral` varchar(15) NOT NULL DEFAULT '',
  `metatrimestraldeventas` varchar(15) NOT NULL DEFAULT '',
  `result2` varchar(15) NOT NULL DEFAULT '',
  `clientesactivos` varchar(15) NOT NULL DEFAULT '',
  `totalclientes` varchar(15) NOT NULL DEFAULT '',
  `result3` varchar(15) NOT NULL DEFAULT '',
  `clientesnuevos` varchar(15) NOT NULL DEFAULT '',
  `metaclientesnuevos` varchar(15) NOT NULL DEFAULT '',
  `result4` varchar(15) NOT NULL DEFAULT '',
  `visitasrealizadas` varchar(15) NOT NULL DEFAULT '',
  `visitasprogramadas` varchar(15) NOT NULL DEFAULT '',
  `result5` varchar(15) NOT NULL DEFAULT '',
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=1633 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ventas`
--

LOCK TABLES `ventas` WRITE;
/*!40000 ALTER TABLE `ventas` DISABLE KEYS */;
INSERT INTO `ventas` VALUES (1619,'enero','2022','122000','112000','108.93','','','','','','','15','16','93.75','9','10','90.00'),(1620,'febrero','2022','235000','245000','95.92','','','','','','','12','15','80.00','20','22','90.91'),(1622,'marzo','2022','156000','156120','99.92','450500','500000','90.10','','','','14','15','93.33','16','18','88.89'),(1623,'abril','2022','1236000','1251000','98.80','','','','','','','9','12','75.00','7','8','87.50');
/*!40000 ALTER TABLE `ventas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-03-09 20:38:08
