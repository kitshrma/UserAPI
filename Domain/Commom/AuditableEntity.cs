using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commom
{
    public abstract class AuditableEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
