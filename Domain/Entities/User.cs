using Domain.Commom;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public double MonthlySalary { get; set; }
        public double MonthlyExpenses { get; set; }
    }
}
