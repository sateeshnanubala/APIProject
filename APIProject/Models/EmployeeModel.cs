using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIProject.Models
{
    public class EmployeeModel
    {
        public int Employeeid { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Emailid { get; set; }
        public int Pno { get; set; }
        public String DOB { get; set; }
        public int Sal { get; set; }
    }
}
