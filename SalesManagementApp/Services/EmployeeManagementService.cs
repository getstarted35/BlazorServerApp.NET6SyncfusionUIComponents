using Microsoft.EntityFrameworkCore;
using SalesManagementApp.Data;
using SalesManagementApp.Entities;
using SalesManagementApp.Extensions;
using SalesManagementApp.Models;
using SalesManagementApp.Services.Contracts;

namespace SalesManagementApp.Services
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly SalesManagementDbContext salesManagemenetDbContext;

        public EmployeeManagementService(SalesManagementDbContext salesManagemenetDbContext)
        {
            this.salesManagemenetDbContext = salesManagemenetDbContext;
        }
        
        public async Task<Employee> AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                Employee employeeToAdd = employeeModel.Convert();

                var result = await this.salesManagemenetDbContext.Employees
                                             .AddAsync(employeeToAdd);
                await this.salesManagemenetDbContext.SaveChangesAsync();

                return result.Entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteEmployee(int id)
        {
            try
            {
                var employee = await this.salesManagemenetDbContext.Employees.FindAsync(id);
                if (employee != null)
                {
                    this.salesManagemenetDbContext.Employees.Remove(employee);
                    await this.salesManagemenetDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            
            {
                {
                    try
                    {
                        return await this.salesManagemenetDbContext.Employees.Convert();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }



        }
      
        public async Task<List<EmployeeJobTitle>> GetJobTitles()
        {
            try
            {
                return await this.salesManagemenetDbContext.EmployeeJobTitles.ToListAsync();
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        //vay ben belanın ta kendisiyim. Burda 1-2 harf yanlış girip iplement edince baya saç baş yoldum
        public async Task<List<ReportToModel>> GetReportToEmployees()
        {
            try
            {
                var employees = await (from e in this.salesManagemenetDbContext.Employees
                                       join j in this.salesManagemenetDbContext.EmployeeJobTitles
                                        on e.EmployeeJobTitleId equals j.EmployeeJobTitleId
                                       select new ReportToModel
                                       {
                                           ReportToEmpId = e.Id,
                                           ReportToName = e.FirstName + "" + e.LastName.Substring(0, 1).ToUpper() + "."
                                       }).ToListAsync();
                employees.Add(new ReportToModel { ReportToEmpId = null, ReportToName = "<None>" });
                return employees.OrderBy(o => o.ReportToEmpId).ToList();
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                var employeeToUpdate = await this.salesManagemenetDbContext.Employees.FindAsync(employeeModel.Id);
                if (employeeToUpdate != null)
                {
                    employeeToUpdate.FirstName = employeeModel.FirstName;
                    employeeToUpdate.LastName = employeeModel.LastName;
                    employeeToUpdate.ReportToEmpId = employeeModel.ReportToEmpId;
                    employeeToUpdate.DateOfBirth = employeeModel.DateOfBirth;
                    employeeToUpdate.ImagePath = employeeModel.ImagePath;
                    employeeToUpdate.Gender = employeeModel.Gender;
                    employeeToUpdate.Email = employeeModel.Email;
                    employeeToUpdate.EmployeeJobTitleId = employeeModel.EmployeeJobTitleId;

                    await this.salesManagemenetDbContext.SaveChangesAsync();

                }

            }
            catch
            {
                throw;
            }
        }
    }
}