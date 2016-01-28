CREATE TABLE dbo.Ward
	(
	Id int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(50) NOT NULL,
	OrganisationId int NOT NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.Ward ADD CONSTRAINT
	FK_Ward_Location FOREIGN KEY
	(
	OrganisationId
	) REFERENCES dbo.Organisation
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  NO ACTION
GO

CREATE TABLE dbo.Bed
	(
	Id int IDENTITY(1,1) PRIMARY KEY,
	WardId int NOT NULL,
	Price decimal(10, 2) NOT NULL,
	Availability bit NOT NULL,
	Name nvarchar(50) NOT NULL,
  MinAge int NOT NULL,
	MaxAge int NOT NULL,
	Gender int NOT NULL,
	Tier int NOT NULL,
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.Bed ADD CONSTRAINT
	FK_Bed_Ward FOREIGN KEY
	(
	WardId
	) REFERENCES dbo.Ward
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  NO ACTION
GO