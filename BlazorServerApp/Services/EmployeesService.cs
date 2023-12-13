using System;
using Microsoft.AspNetCore.Mvc;
using BlazorServerApp.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Security.AccessControl;

namespace BlazorServerApp.Services
{
    public class EmployeesService
    {
        private readonly AppDBContext _dbContext;

        public EmployeesService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void addEmployee(EmployeeVM employeeVM)
        {
            Employee employee = new Employee() { FullName = employeeVM.FullName, Department = employeeVM.Department, Salary = employeeVM.Salary };
            _dbContext.Add(employee);
            _dbContext.SaveChanges();
        }
        public Employee? updateEmployee(string id, EmployeeVM employeeVM)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                employee.FullName = employeeVM.FullName;
                employee.Department = employeeVM.Department;
                employee.Salary = employeeVM.Salary;

                _dbContext.Employees.Update(employee);
                _dbContext.SaveChanges();
            }
            return employee;
        }

        public List<Employee> getAllEmployees()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee? getByID(string id)
        {
            return _dbContext.Employees.FirstOrDefault(x => x.Id == id);
        }

        public void deleteEmployee(string id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return;
            }
            _dbContext.Remove(employee);
            _dbContext.SaveChanges();
        } 
    }
}