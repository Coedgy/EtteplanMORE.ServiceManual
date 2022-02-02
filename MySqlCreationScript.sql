CREATE SCHEMA `esm` ;

#Create the tables
CREATE TABLE `esm`.`FactoryDevices` (
  `id` INT NOT NULL,
  `name` VARCHAR(68) NOT NULL,
  `year` INT NOT NULL,
  `type` VARCHAR(45) NULL,
  PRIMARY KEY (`id`));

CREATE TABLE `esm`.`MaintenanceTasks` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `deviceId` INT NOT NULL,
  `issueDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `description` VARCHAR(200) NULL,
  `importance` INT NOT NULL DEFAULT 1,
  `closed` TINYINT NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  INDEX `deviceId_idx` (`deviceId` ASC) VISIBLE,
  CONSTRAINT `deviceId`
    FOREIGN KEY (`deviceId`)
    REFERENCES `esm`.`FactoryDevices` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

#Import sample data
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 0', '2004', 'Type 19');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 1', '1987', 'Type 2');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 2', '1982', 'Type 11');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 3', '1995', 'Type 5');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 4', '1995', 'Type 8');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 5', '2002', 'Type 15');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 6', '1996', 'Type 18');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 7', '1981', 'Type 13');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 8', '2006', 'Type 10');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 9', '1990', 'Type 18');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 10', '2005', 'Type 15');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 11', '2003', 'Type 10');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 12', '2002', 'Type 7');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 13', '2001', 'Type 14');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 14', '1991', 'Type 8');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 15', '1980', 'Type 7');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 16', '2018', 'Type 8');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 17', '1987', 'Type 8');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 18', '1981', 'Type 18');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 19', '2018', 'Type 17');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 20', '1990', 'Type 17');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 21', '2008', 'Type 6');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 22', '2011', 'Type 18');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 23', '1983', 'Type 3');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 24', '2016', 'Type 7');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 25', '2014', 'Type 5');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 26', '1988', 'Type 17');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 27', '1992', 'Type 6');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 28', '1998', 'Type 2');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 29', '1985', 'Type 19');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 30', '1996', 'Type 11');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 31', '1986', 'Type 9');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 32', '2005', 'Type 10');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 33', '1984', 'Type 19');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 34', '2015', 'Type 7');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 35', '2016', 'Type 3');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 36', '2011', 'Type 14');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 37', '2009', 'Type 7');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 38', '1982', 'Type 6');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 39', '2008', 'Type 18');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 40', '1993', 'Type 5');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 41', '1984', 'Type 3');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 42', '2007', 'Type 9');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 43', '2018', 'Type 14');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 44', '2001', 'Type 2');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 45', '1993', 'Type 18');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 46', '1998', 'Type 11');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 47', '2012', 'Type 14');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 48', '1987', 'Type 10');
INSERT INTO `esm`.`FactoryDevices` (`name`, `year`, `type`) VALUES ('Device 49', '2006', 'Type 4');

INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('2', '2022-01-01 19:04:32', 'Do something', 1, 0);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('16', '2022-01-01 19:04:32', 'Do something', 2, 0);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('25', '2021-12-25 19:04:32', 'Do something', 2, 0);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('21', '2021-12-24 19:04:32', 'Fix something', 3, 1);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('32', '2021-03-01 19:04:32', 'Do something', 2, 1);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('5', '2021-04-25 19:04:32', 'Do something', 2, 0);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('45', '2021-05-28 19:04:32', 'Do something', 1, 0);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('37', '2021-05-02 19:04:32', 'Check something', 1, 0);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('5', '2021-11-15 19:04:32', 'Change something', 3, 1);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('18', '2021-04-07 19:04:32', 'Do something', 2, 1);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('22', '2021-06-27 19:04:32', 'Do something', 2, 1);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('33', '2022-01-14 19:04:32', 'Clean something', 1, 1);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('44', '2022-01-05 19:04:32', 'Check something', 3, 0);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('30', '2021-10-25 19:04:32', 'Do something', 3, 0);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('25', '2021-12-10 19:04:32', 'Do something small', 1, 0);
INSERT INTO `esm`.`MaintenanceTasks` (`deviceId`, `issueDate`, `description`, `importance`, `closed`) VALUES ('25', '2020-05-06 19:04:32', 'Fix important stuff', 3, 0);