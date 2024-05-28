create database HospitalDB;
use HospitalDB;

Create Schema Hospital;

CREATE TABLE Hospital.Doctors
(
    DoctorId INT PRIMARY KEY IDENTITY(1,1),
    IsTrash BIT NOT NULL DEFAULT 0,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    ContactNumber VARCHAR(15) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    ImageLink VARCHAR(255),
    DOB DATETIME NOT NULL,
    Age AS (DATEDIFF(YEAR, DOB, GETDATE()) - CASE 
                                                WHEN MONTH(DOB) > MONTH(GETDATE()) 
                                                  OR (MONTH(DOB) = MONTH(GETDATE()) AND DAY(DOB) > DAY(GETDATE())) 
                                                THEN 1 
                                                ELSE 0 
                                              END),
    Gender VARCHAR(10) NOT NULL,
    Contact_Address VARCHAR(100) NOT NULL,
    Qualification VARCHAR(50) NOT NULL,
    Specialization VARCHAR(50) NOT NULL,
    Years_Of_Experience INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

ALTER PROCEDURE sp_CreateDoctorProfile
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @ContactNumber VARCHAR(15),
    @Email VARCHAR(50),
    @ImageLink VARCHAR(255),
    @DOB DATETIME,
    @Gender VARCHAR(10),
    @Contact_Address VARCHAR(100),
    @Qualification VARCHAR(50),
    @Specialization VARCHAR(50),
    @Years_Of_Experience INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Hospital.Doctors
    (
        
        FirstName,
        LastName,
        ContactNumber,
        Email,
        ImageLink,
        DOB,
        Gender,
        Contact_Address,
        Qualification,
        Specialization,
        Years_Of_Experience
    )
    VALUES
    (
        @FirstName,
        @LastName,
        @ContactNumber,
        @Email,
        @ImageLink,
        @DOB,
        @Gender,
        @Contact_Address,
        @Qualification,
        @Specialization,
        @Years_Of_Experience
    );
END;

select * from Hospital.Doctors

exec sp_CreateDoctorProfile
    'John', -- FirstName: Sample first name
    'Doe', -- LastName: Sample last name
    '+1234567890', -- ContactNumber: Sample contact number
    'john.doe@example.com', -- Email: Sample email
    NULL, -- ImageLink: No image link provided
    '1990-01-01', -- DOB: Sample date of birth
    'Male', -- Gender: Sample gender
    '123 Main St, City, Country', -- Contact_Address: Sample contact address
    'MD', -- Qualification: Sample qualification
    'Cardiology', -- Specialization: Sample specialization
    10 -- Years_Of_Experience: Sample years of experience

INSERT INTO Hospital.Doctors (IsTrash, FirstName, LastName, ContactNumber, Email, ImageLink, DOB, Gender, Contact_Address, Qualification, Specialization, Years_Of_Experience)
VALUES
(
    0, -- IsTrash: Not marked as trash
    'John', -- FirstName: Sample first name
    'Doe', -- LastName: Sample last name
    '+1234567890', -- ContactNumber: Sample contact number
    'john.doe@example.com', -- Email: Sample email
    NULL, -- ImageLink: No image link provided
    '1990-01-01', -- DOB: Sample date of birth
    'Male', -- Gender: Sample gender
    '123 Main St, City, Country', -- Contact_Address: Sample contact address
    'MD', -- Qualification: Sample qualification
    'Cardiology', -- Specialization: Sample specialization
    10 -- Years_Of_Experience: Sample years of experience
);


Drop Table Hospital.Doctors;
Drop Schema Hospital;