CREATE TABLE dbo.BookingRequest
	(
	Id int IDENTITY(1,1) PRIMARY KEY,
	CurrentStatus int NOT NULL,
	UserDetailId int NOT NULL,
	OrganisationId int NOT NULL,
	Notes nvarchar(200) NOT NULL,
	PatientId int NOT NULL,
	DateRequested datetime NOT NULL,
    BedId int NOT NULL,
    ClinicalInformation bit NOT NULL
	)  ON [PRIMARY]

GO
CREATE TABLE dbo.Patient
	(
	Id int IDENTITY(1,1) PRIMARY KEY,
	PasId nvarchar(20) NOT NULL,
	Age int NOT NULL,
	Gender int NOT NULL
	)  ON [PRIMARY]

GO
ALTER TABLE dbo.BookingRequest ADD CONSTRAINT
	FK_BookingRequest_UserDetails FOREIGN KEY
	(
	UserDetailId
	) REFERENCES dbo.UserDetails
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  NO ACTION

GO
ALTER TABLE dbo.BookingRequest ADD CONSTRAINT
	FK_BookingRequest_Patient FOREIGN KEY
	(
	PatientId
	) REFERENCES dbo.Patient
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  NO ACTION

GO
ALTER TABLE dbo.BookingRequest ADD CONSTRAINT
	FK_BookingRequest_Bed FOREIGN KEY
	(
	BedId
	) REFERENCES dbo.Bed
	(
	Id
	) ON UPDATE  NO ACTION
	 ON DELETE  NO ACTION