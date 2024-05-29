Create Table Hospital.Patients
(
	Patient_Id int primary key identity(1,1),
	Is_Trash bit not null default 0,
	First_Name varchar(50) not null,
	Last_Name varchar(50) not null,
	Number varchar(15) not null,
	Email varchar(50) not null,
	ImageLink varchar(255),
	DOB Date not null,
	Age AS (DATEDIFF(YEAR, DOB, GETDATE()) - CASE 
                                                WHEN MONTH(DOB) > MONTH(GETDATE()) 
                                                  OR (MONTH(DOB) = MONTH(GETDATE()) AND DAY(DOB) > DAY(GETDATE())) 
                                                THEN 1 
                                                ELSE 0 
                                              END),
	Gender VARCHAR(10) NOT NULL,
	Contact_Address varchar(100) not null,
	Concern varchar(max) not null
)

--CreatePatient
CREATE PROCEDURE usp_CreatePatient
    @First_Name varchar(50),
    @Last_Name varchar(50),
    @Number bigint,
    @Email varchar(50),
    @ImageLink varchar(50),
    @DOB date,
    @Gender varchar(10),
    @Contact_Address varchar(100),
    @Concern varchar(max)
AS
BEGIN
    INSERT INTO Hospital.Patients (Is_Trash, First_Name, Last_Name, Number, Email, ImageLink, DOB, Gender, Contact_Address, Concern)
    VALUES (0, @First_Name, @Last_Name, @Number, @Email, @ImageLink, @DOB, @Gender, @Contact_Address, @Concern);
END

--GetAlPatients
CREATE PROCEDURE usp_GetAllPatients
AS
BEGIN
    SELECT Patient_Id, First_Name, Last_Name, Number, Email, ImageLink, DOB, Gender, Contact_Address, Concern
    FROM Hospital.Patients
    WHERE Is_Trash = 0; -- Only retrieve non-deleted patients
END

--GetPatientById
CREATE PROCEDURE usp_GetPatientById
    @Patient_Id int
AS
BEGIN
    SELECT Patient_Id, First_Name, Last_Name, Number, Email, ImageLink, DOB, Gender, Contact_Address, Concern
    FROM Hospital.Patients
    WHERE Patient_Id = @Patient_Id
        --AND Is_Trash = 0; -- Ensure the patient is not deleted
END

--UpdatePatient
CREATE PROCEDURE usp_UpdatePatient
    @Patient_Id int,
    @First_Name varchar(50),
    @Last_Name varchar(50),
    @Number bigint,
    @Email varchar(50),
    @ImageLink varchar(50),
    @DOB date,
    @Gender varchar(10),
    @Contact_Address varchar(100),
    @Concern varchar(max)
AS
BEGIN
    UPDATE Hospital.Patients
    SET First_Name = @First_Name,
        Last_Name = @Last_Name,
        Number = @Number,
        Email = @Email,
        ImageLink = @ImageLink,
        DOB = @DOB,
        Gender = @Gender,
        Contact_Address = @Contact_Address,
        Concern = @Concern
    WHERE Patient_Id = @Patient_Id
        AND Is_Trash = 0; -- Ensure the patient is not deleted
END

--DeletePatient
CREATE PROCEDURE usp_DeletePatient
    @Patient_Id int
AS
BEGIN
    UPDATE Hospital.Patients
    SET Is_Trash = 1 -- Soft delete by setting Is_Trash to true
    WHERE Patient_Id = @Patient_Id;
END

--Sample CRUD

EXEC usp_CreatePatient 
    @First_Name = 'John',
    @Last_Name = 'Doe',
    @Number = '1234567890',
    @Email = 'john@example.com',
    @ImageLink = 'image.jpg',
    @DOB = '1990-01-01',
    @Gender = 'Male',
    @Contact_Address = '123 Street, City',
    @Concern = 'Fever and cough';


EXEC usp_UpdatePatient
    @Patient_Id = 1,
    @First_Name = 'Jane',
    @Last_Name = 'Doe',
    @Number = '9876543210',
    @Email = 'jane@example.com',
    @ImageLink = 'new_image.jpg',
    @DOB = '1988-05-10',
    @Gender = 'Female',
    @Contact_Address = '456 Road, Town',
    @Concern = 'Headache and fatigue';

EXEC usp_DeletePatient @Patient_Id = 1;

EXEC usp_GetAllPatients;

EXEC usp_GetPatientById @Patient_Id = 1;


--Drop Table Hospital.Patients;