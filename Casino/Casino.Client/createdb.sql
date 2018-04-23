Use master
GO

select * from sys.databases;

drop database pizzastoredb;

create DATABASE casinodb;

use casinodb
Go

create schema casinodb

create TABLE casinodb.Users
    (
        ID int primary key not null identity(1,1),
        UserID bigint not null,
        Username varchar(max) not null,
        Name varchar(max) not null,
        Age int not null,
        Email varchar(max) not null,
        UserPocketID int foreign key
        REFERENCES casinodb.Pockets(PocketID)
    )

create table casinodb.Pockets
    (
        PocketID int primary key not null identity(1,1),
        Cash money not null,
        Coins int not null,
        ChipsID int foreign key
        REFERENCES casinodb.Chips(ChipsID)
    )

create table casinodb.Chips
    (
        ChipsID int primary key not null identity(1,1),
        Amount int not null,
        Type varchar(25) not null
    )
GO


select * from casinodb.Chips;
select * from casinodb.Pockets;
select * from casinodb.Users;

insert into casinodb.Chips(Amount, Type)
    values (250, 'green');

insert into casinodb.Pockets(Cash, Coins, ChipsID)
    values(250, 100, 2);

    select * from casinodb.Pockets as cp
    inner join casinodb.Chips as cc on cc.ChipsID = cp.ChipsID;

alter table casinodb.Users
    add Password varchar(max) not null;