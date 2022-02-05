CREATE SCHEMA `esm` ;

#Create the tables
CREATE TABLE `esm`.`FactoryDevices` (
  `id` INT NOT NULL AUTO_INCREMENT,
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
