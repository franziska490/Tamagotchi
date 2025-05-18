Create database if not exists tamagotchidb;
use tamagotchidb;

Create table users (
    userid int auto_increment primary key,
    username text not null,
    password text not null, -- Todo: password hash
    role enum('admin', 'user') not null 
);

Create table pets(
    petid int auto_increment primary key,
    name text not null,
    hunger int not null,
    energy int not null,
    mood int not null,
    ownerid int, foreign Key(ownerid) references users(userid)
);

Create table actions(
    actionid int auto_increment primary key,
    petid int,
    actiontype enum('hunger','energy','mood') not null,
    lastperform timestamp default CURRENT_TIMESTAMP,
    foreign key (petid) references pets(petid)
);

-- not null: darf nicht leer sein, eingabe verpflichtend
-- enum für rollenverteilung, aufzählungstypen für die verschiedenen werten