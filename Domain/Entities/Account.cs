using Domain.Commom;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Account : AuditableEntity
    {
        public int UserID { get; set; }
        public double Balance { get; set; }
    }
}
