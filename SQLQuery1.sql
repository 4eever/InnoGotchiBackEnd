use Innogotchi
use db_aa70e1_innogotchi

SELECT * FROM Users
SELECT * FROM Farms
SELECT * FROM UserFarms
SELECT * FROM Innogotchis
SELECT * FROM DeadInnogotchis
SELECT * FROM Roles
SELECT * FROM BodyParts
SELECT * FROM InnogotchiBodyParts



DBCC CHECKIDENT (Users, RESEED, 0);
DBCC CHECKIDENT (Farms, RESEED, 0);
DBCC CHECKIDENT (UserFarms, RESEED, 0);
DBCC CHECKIDENT (Innogotchis, RESEED,    0);
DBCC CHECKIDENT (DeadInnogotchis, RESEED, 0);

DELETE FROM Users;
DELETE FROM Farms;
DELETE FROM UserFarms;
DELETE FROM Innogotchis;
DELETE FROM DeadInnogotchis;
DELETE FROM BodyParts;
DELETE FROM InnogotchiBodyParts;

DBCC CHECKIDENT ('Users', RESEED, 0);
DBCC CHECKIDENT ('Farms', RESEED, 0);
DBCC CHECKIDENT ('Innogotchis', RESEED, 0);
DBCC CHECKIDENT ('DeadInnogotchis', RESEED, 0);
DBCC CHECKIDENT ('InnogotchiBodyParts', RESEED, 0);

DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Farms;
DROP TABLE IF EXISTS UserFarms;
DROP TABLE IF EXISTS Innogotchis;
DROP TABLE IF EXISTS DeadInnogotchis;
DROP TABLE IF EXISTS Roles;
DROP TABLE IF EXISTS BodyParts;
DROP TABLE IF EXISTS InnogotchiBodyParts;

drop database Innogotchi

SELECT 
    session_id, 
    login_name, 
    host_name, 
    program_name
FROM 
    sys.dm_exec_sessions
WHERE 
    database_id = DB_ID('Innogotchi');

