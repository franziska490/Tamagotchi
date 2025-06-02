-- Datenbank erstellen und verwenden
CREATE DATABASE IF NOT EXISTS tamagotchidb;
USE tamagotchidb;

-- Tabelle für Benutzer
CREATE TABLE users (
    userid INT AUTO_INCREMENT PRIMARY KEY,
    username TEXT NOT NULL,
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

-- Benutzer einfügen
INSERT INTO users (username, password, role) VALUES 
('laura', '1234', 'user'),       -- Achtung: Passwort sollte gehasht werden!
('admin01', 'adminpw', 'admin');

-- Haustiere einfügen (mit Bildpfad)
INSERT INTO pets (name, hunger, energy, mood, ownerid, imagepath) VALUES 
('Fluffy', 80, 90, 85, 1, '/Assets/seal_happy.png'),     
('Spike', 60, 70, 75, 2, '/Assets/penguin_happy.png'),   

-- Beispielaktionen für Fluffy (petid = 1)
INSERT INTO actions (petid, actiontype) VALUES 
(1, 'hunger'),
(1, 'energy'),
(1, 'mood');

-- Beispielaktionen für Spike (petid = 2)
INSERT INTO actions (petid, actiontype) VALUES 
(2, 'hunger'),
(2, 'mood');
