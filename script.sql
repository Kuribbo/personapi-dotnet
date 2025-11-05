-- ===================================================
-- LABORATORIO 1 - personapi-dotnet
-- Script DDL y DML
-- ===================================================

-- Crear base de datos
CREATE DATABASE persona_db;
GO
USE persona_db;
GO

-- =======================
-- DDL - CREACIÓN DE TABLAS
-- =======================

CREATE TABLE profesion (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nom VARCHAR(90) NOT NULL,
    des TEXT
);
GO

CREATE TABLE persona (
    cc INT PRIMARY KEY,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    genero CHAR(1) CHECK (genero IN ('M', 'F')),
    edad INT CHECK (edad >= 0)
);
GO

CREATE TABLE estudios (
    id_prof INT NOT NULL,
    cc_per INT NOT NULL,
    fecha DATE,
    univ VARCHAR(100),
    PRIMARY KEY (id_prof, cc_per),
    FOREIGN KEY (id_prof) REFERENCES profesion(id),
    FOREIGN KEY (cc_per) REFERENCES persona(cc)
);
GO

CREATE TABLE telefono (
    num VARCHAR(15) PRIMARY KEY,
    oper VARCHAR(45),
    dueno INT,
    FOREIGN KEY (dueno) REFERENCES persona(cc)
);
GO

-- =======================
-- DML - DATOS DE PRUEBA
-- =======================

-- Profesiones
INSERT INTO profesion (nom, des) VALUES
('Ingeniero de Sistemas', 'Diseña, desarrolla y mantiene sistemas informáticos.'),
('Administrador de Empresas', 'Gestiona recursos y operaciones en organizaciones.'),
('Médico', 'Diagnostica y trata enfermedades.'),
('Abogado', 'Asesora legalmente a personas y empresas.'),
('Arquitecto', 'Diseña y supervisa la construcción de edificaciones.');
GO

-- Personas
INSERT INTO persona (cc, nombre, apellido, genero, edad) VALUES
(1001, 'Juan', 'Pérez', 'M', 28),
(1002, 'María', 'Gómez', 'F', 34),
(1003, 'Carlos', 'López', 'M', 22),
(1004, 'Ana', 'Rodríguez', 'F', 29),
(1005, 'Luis', 'Martínez', 'M', 41);
GO

-- Estudios (relación Persona - Profesión)
INSERT INTO estudios (id_prof, cc_per, fecha, univ) VALUES
(1, 1001, '2018-06-15', 'Universidad Javeriana'),
(2, 1002, '2015-11-20', 'Universidad de los Andes'),
(3, 1003, '2022-07-10', 'Universidad Nacional'),
(4, 1004, '2019-05-25', 'Universidad del Rosario'),
(5, 1005, '2010-12-18', 'Universidad del Valle');
GO

-- Teléfonos
INSERT INTO telefono (num, oper, dueno) VALUES
('3001112233', 'Claro', 1001),
('3205556677', 'Movistar', 1002),
('3109998877', 'Tigo', 1003),
('3124445566', 'Claro', 1004),
('3017778899', 'WOM', 1005);
GO

-- =======================
-- VALIDACIÓN
-- =======================

SELECT * FROM profesion;
SELECT * FROM persona;
SELECT * FROM estudios;
SELECT * FROM telefono;
GO
