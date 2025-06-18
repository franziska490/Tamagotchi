-- Datenbank erstellen und verwenden
CREATE DATABASE IF NOT EXISTS tamagotchidb;
USE tamagotchidb;

-- Tabelle für Benutzer
CREATE TABLE users (
    userid INT AUTO_INCREMENT PRIMARY KEY,
    username TEXT NOT NULL UNIQUE,
    password TEXT NOT NULL, -- TODO: Passwort Hash
    role ENUM('admin', 'user') NOT NULL 
);

-- Tabelle für Haustiere (neu mit imagepath)
CREATE TABLE pets (
    petid INT AUTO_INCREMENT PRIMARY KEY,
    name TEXT NOT NULL,
    hunger INT NOT NULL,
    energy INT NOT NULL,
    mood INT NOT NULL,
    ownerid INT,
    imagepath TEXT,
    FOREIGN KEY(ownerid) REFERENCES users(userid)
);

-- Tabelle für Aktionen
CREATE TABLE actions (
    actionid INT AUTO_INCREMENT PRIMARY KEY,
    petid INT,
    actiontype ENUM('hunger', 'energy', 'mood') NOT NULL,
    lastperform TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (petid) REFERENCES pets(petid)
);