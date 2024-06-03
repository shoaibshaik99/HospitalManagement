CREATE TABLE Hospital.Appointments
(
    Appointment_Id INT PRIMARY KEY IDENTITY(1,1),
    Patient_Id INT FOREIGN KEY REFERENCES Hospital.Patients(Patient_Id) NOT NULL,
    Doctor_Id INT FOREIGN KEY REFERENCES Hospital.Doctors(DoctorId) NOT NULL,
    Appointment_Date DATE NOT NULL,
    Start_Time TIME NOT NULL,
    End_Time AS DATEADD(minute, 20, Start_Time),
    Concerns VARCHAR(MAX) NOT NULL
);

Alter PROCEDURE usp_CreateAppointment
    @Patient_Id INT,
    @Doctor_Id INT,
    @Appointment_Date DATE,
    @Start_Time TIME,
    @Concerns VARCHAR(MAX)
AS
BEGIN
    INSERT INTO Hospital.Appointments (Patient_Id, Doctor_Id, Appointment_Date, Start_Time, Concerns)
    VALUES (@Patient_Id, @Doctor_Id, @Appointment_Date, @Start_Time, @Concerns);
END;
select * from Hospital.Doctors
select * from Hospital.Patients
select * from Hospital.Appointments


UPDATE Hospital.Patients
    SET Last_Name = 'Shaik'
	WHERE
        Patient_Id = 5;

exec usp_CreateAppointment 1,1,'2024-05-30', '9:15', 'Cold'

CREATE PROCEDURE usp_GetAllAppointments
AS
BEGIN
    SELECT 
        Appointment_Id, 
        Patient_Id, 
        Doctor_Id, 
        Appointment_Date, 
        Start_Time, 
        End_Time, 
        Concerns
    FROM Hospital.Appointments;
END;
---------------------------------------------------------------------------------
Alter Proc usp_GetMyPatientsDetails @DoctorID int
As
Begin
	Select
	d.DoctorID,
	d.FirstName + ' ' + d.LastName As 'Doctor Name',
	d.ImageLink As 'Doctor Image',
	p.Patient_Id,
	p.First_Name + ' ' + p.Last_Name As 'Patient Name',
	p.ImageLink As 'Patient Image',
	p.Gender As 'Patient Gender',
	p.Age As 'Patient Age',
	a.Concerns
	From Hospital.Doctors d
	join Hospital.Appointments a on d.DoctorId = a.Doctor_ID
	join Hospital.Patients p on p.Patient_Id = a.Patient_ID
	where p.Is_Trash = 0 and d.IsTrash = 0 and d.DoctorId = @DoctorID
End

exec usp_GetMyPatientsDetails 3
---------------------------------------------------------------------------------
Create Proc usp_GetMyDoctorDetails @Patient_Id int
As
	SELECT
		p.Patient_Id As 'Patient ID',
		p.First_Name + ' ' + p.Last_Name As 'Patient Name',
        d.DoctorID,
        d.FirstName + ' ' + d.LastName AS 'Doctor Name',
        d.ImageLink AS 'Doctor Image',
		d.Age As 'Doctor Age',
		d.Gender,
		d.Qualification,
		d.Specialization,
		d.Years_Of_Experience
		From Hospital.Patients p
		join Hospital.Appointments a on p.Patient_Id = a.Patient_ID
		join Hospital.Doctors d on d.DoctorId = a.Doctor_ID
		where p.Is_Trash = 0 and d.IsTrash = 0 and d.Patient_Id = @Patient_Id
End

exec usp_GetMyDoctorDetails 4