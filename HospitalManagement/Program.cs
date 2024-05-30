using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;

namespace HospitalManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IDoctorRepo,DoctorRepo>();
            builder.Services.AddTransient<IDoctorBusiness, DoctorBusiness>();
            builder.Services.AddTransient<IPatientRepo, PatientRepo>();
            builder.Services.AddTransient<IPatientBusiness, PatientBusiness>();
            builder.Services.AddTransient<IAppointmentRepo, AppointmentRepo>();
            builder.Services.AddTransient<IAppointmentBusiness, AppointmentBusiness>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}