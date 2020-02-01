/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50505
Source Host           : localhost:3306
Source Database       : whatsapp_massive

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2020-02-01 15:09:52
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for campana
-- ----------------------------
DROP TABLE IF EXISTS `campana`;
CREATE TABLE `campana` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(255) DEFAULT NULL,
  `mensaje` text,
  `imagen` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of campana
-- ----------------------------
INSERT INTO `campana` VALUES ('1', 'VENTA DE CELULARES', 'Compra nuestros celulares a precio económico', null);
INSERT INTO `campana` VALUES ('2', 'BLACK FRIDAY', 'Aprovecha nuestro black friday', null);

-- ----------------------------
-- Table structure for contacto
-- ----------------------------
DROP TABLE IF EXISTS `contacto`;
CREATE TABLE `contacto` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(255) DEFAULT NULL,
  `celular` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of contacto
-- ----------------------------
INSERT INTO `contacto` VALUES ('3', 'LUIS FERNANDO AGUAS', '094333222');
INSERT INTO `contacto` VALUES ('5', 'MARCIA TRELLES', '098333444');
INSERT INTO `contacto` VALUES ('6', 'MARTHA CARDONA', '097333222');

-- ----------------------------
-- Procedure structure for editar_campana
-- ----------------------------
DROP PROCEDURE IF EXISTS `editar_campana`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `editar_campana`(IN `_id` INT, IN `nombre` VARCHAR(255), IN `mensaje` TEXT, IN `imagen` VARCHAR(255))
BEGIN
	UPDATE campana SET
	nombre= nombre,
	mensaje= mensaje,
	imagen = imagen
	WHERE id = _id;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for editar_contacto
-- ----------------------------
DROP PROCEDURE IF EXISTS `editar_contacto`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `editar_contacto`(IN `_id` int, IN `nombre` VARCHAR(255), IN `celular` VARCHAR(10))
BEGIN
	UPDATE contacto SET
	nombre = nombre,
	celular = celular
WHERE id = _id;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for eliminar_campana
-- ----------------------------
DROP PROCEDURE IF EXISTS `eliminar_campana`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `eliminar_campana`(IN `_id` INT)
BEGIN
	DELETE FROM campana WHERE id = _id ;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for eliminar_contacto
-- ----------------------------
DROP PROCEDURE IF EXISTS `eliminar_contacto`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `eliminar_contacto`(IN `_id` INT)
BEGIN
	DELETE FROM contacto WHERE id = _id ;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for insertar_campana
-- ----------------------------
DROP PROCEDURE IF EXISTS `insertar_campana`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertar_campana`(IN `nombre` VARCHAR(255), IN `mensaje` TEXT, IN `imagen` VARCHAR(255))
BEGIN
	INSERT INTO campana VALUES (
	NULL,
	nombre,
	mensaje,
	imagen
	);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for insertar_contacto
-- ----------------------------
DROP PROCEDURE IF EXISTS `insertar_contacto`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertar_contacto`(IN `nombre` VARCHAR(255), IN `celular` VARCHAR(10))
BEGIN
	INSERT INTO contacto VALUES (
	NULL,
	nombre,
	celular
	);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for mostrar_campana
-- ----------------------------
DROP PROCEDURE IF EXISTS `mostrar_campana`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `mostrar_campana`()
BEGIN
	select * from campana;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for mostrar_contacto
-- ----------------------------
DROP PROCEDURE IF EXISTS `mostrar_contacto`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `mostrar_contacto`()
begin
 select * from contacto;
end
;;
DELIMITER ;
