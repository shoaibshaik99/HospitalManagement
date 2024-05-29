Create Table Hospital.Patients
(
Patient_Id int primary key identity(1,1),
Is_Trash bit,
First_Name varchar(50),
Last_Name varchar(50),
Number bigint,
Email varchar(50),
ImageLink varchar(50),
DOB DateTime,
Age int,
Gender VARCHAR(10) NOT NULL,
Contact_Address varchar(100),
Issue varchar(50)
)

--Drop Table Hospital.Patients;