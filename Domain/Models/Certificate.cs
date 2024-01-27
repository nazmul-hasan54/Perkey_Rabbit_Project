using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public string? CertificateName { get; set; }
        public DateTime CertificateDate { get; set; }
        public int EmployeeId { get; set; }
    }
}
