Create database if not exists tamagotchidb;
use tamagotchidb;

-- Tabelle für Benutzer
Create table users (
    userid int auto_increment primary key,
    username text not null,
    password text not null, -- Todo: password hash
    role enum('admin', 'user') not null 
);

-- Tabelle für Benutzer
Create table pets(
    petid int auto_increment primary key,
    name text not null,
    hunger int not null,
    energy int not null,
    mood int not null,
    ownerid int, foreign Key(ownerid) references users(userid)
);

-- Tabelle für durchgeführte Aktionen
Create table actions(
    actionid int auto_increment primary key,
    petid int,
    actiontype enum('hunger','energy','mood') not null,
    lastperform timestamp default CURRENT_TIMESTAMP,
    foreign key (petid) references pets(petid)
);

-- not null: darf nicht leer sein, eingabe verpflichtend
-- enum für rollenverteilung, aufzählungstypen für die verschiedenen werten

-- Benutzer einfügen
INSERT INTO users (username, password, role) VALUES 
('laura', '1234', 'user'), -- Achtung: Passwort sollte gehasht sein
('admin01', 'adminpw', 'admin');

-- Haustiere einfügen (jeweils einem Benutzer zugeordnet)
INSERT INTO pets (name, hunger, energy, mood, ownerid) VALUES 
('Fluffy', 80, 90, 85, 1),  -- Haustier für laura
('Spike', 60, 70, 75, 2);   -- Haustier für admin01

-- Beispielaktionen für Fluffy (Pet von laura, petid = 1)
INSERT INTO actions (petid, actiontype) VALUES 
(1, 'hunger'),
(1, 'energy'),
(1, 'mood');

-- Beispielaktionen für Spike (Pet von admin01, petid = 2)
INSERT INTO actions (petid, actiontype) VALUES 
(2, 'hunger'),
(2, 'mood');
