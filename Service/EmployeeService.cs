﻿using Domain.Models;
using Domain.ViewModels;
using Repository;
using System.Linq.Expressions;

namespace Service
{
    public class EmployeeService : IEmployeeService

    {
        private readonly AppDbContect _dbContext;
        private readonly IRepository<Employee> _repository;

        public EmployeeService(Repository.AppDbContect dbContext, IRepository<Employee> repository)
        {
            this._dbContext = dbContext;
            this._repository = repository;
        }

        public List<GetAllEmployeeData> GetAll()
        {
            return this._repository.GetAll();
        }

        public async Task<bool> Delete(int id)
        {
            Employee employeeData = await _repository.Get(id);
            return await _repository.Delete(employeeData);
           // throw new NotImplementedException();
        }

        public Task<bool> Insert(EmployeeVM employee)
        {
            DateTime minAllowedDate = DateTime.Today.AddYears(-18);

            if (employee.BirthDate > minAllowedDate)
            {
                throw new ArgumentException("Birth date must be at least 18 years ago.");

            }
            else
            {

                Employee employeeData = new()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Gender = employee.Gender,
                    MaritalStatus = employee.MaritalStatus,
                    BirthDate = employee.BirthDate,
                    Hobbies = employee.Hobbies,
                    Salary = employee.Salary,
                    Address = employee.Address,
                    CountryId = employee.CountryId,
                    StateId = employee.StateId,
                    CityId = employee.CityId,
                    ZipCode = employee.ZipCode,
                    Password = employee.Password,
                    ImageName = employee.Files?.FileName,
                };
                return _repository.Insert(employeeData);
            }
        }
        async Task<bool> IEmployeeService.Update(EmployeeVM employee)
        {
            DateTime minAllowedDate = DateTime.Today.AddYears(-18);

            if (employee.BirthDate > minAllowedDate)
            {
                throw new ArgumentException("Birth date must be at least 18 years ago.");

            }
            else
            {
                var employeevalue =  _dbContext.Employee.Find(employee.Id);
                if (employeevalue != null)
                {
                    employeevalue.FirstName = employee.FirstName;
                    employeevalue.LastName = employee.LastName;
                    employeevalue.Email = employee.Email;
                    employeevalue.Gender = employee.Gender;
                    employeevalue.Hobbies = employee.Hobbies;
                    employeevalue.MaritalStatus = employee.MaritalStatus;
                    employeevalue.BirthDate = employee.BirthDate;
                    employeevalue.Salary = employee.Salary;
                    employeevalue.Address = employee.Address;
                    employeevalue.CityId = employee.CityId;
                    employeevalue.StateId = employee.StateId;
                    employeevalue.CountryId = employee.CountryId;
                    employeevalue.ZipCode = employee.ZipCode;
                    employeevalue.Password = employee.Password;
                    employeevalue.ImageName = employee.Files?.FileName;
                }
                var result =await _repository.Update(employeevalue);
                return result;
            }

        }
    }
}