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

select * from Hospital.Appointments
select * from Hospital.Patients
select * from Hospital.Doctors
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
