using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Queries.GetUserById
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public double MonthlySalary { get; set; }
        public double MonthlyExpenses { get; set; }
    }
}
