using Domain.Models;
using Domain.ViewModels;
using System.Collections.Generic;

namespace Service
{
    public interface IEmployeeService
    {
        // Get All Records
        List<Domain.ViewModels.GetAllEmployeeData> GetAll();

        Task<bool> Insert(EmployeeVM EmployeeVM);

        Task<bool> Update(EmployeeVM employee);
     
        Task<bool> Delete(int id);

       
    }
}
