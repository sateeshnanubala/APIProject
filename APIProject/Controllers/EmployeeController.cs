using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APIProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIProject.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase 
    {
        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int Employeeid, String FirstName, String LastName , String  Emailid , int Pno , String DOB , int Sal )
        {
            String valemp;
            //var res = "";
            try
            {
                valemp = ValedEmployee(Employeeid, FirstName, LastName, Emailid, Pno, DOB, Sal);
                if (valemp == "OK")
                {
                    return Ok(await GetEmployeeDetails(Employeeid, FirstName, LastName, Emailid, Pno, DOB, Sal));
                   // return Ok(await GetEmployeeDetails(Employeeid));
                }
                else
                {
                    return BadRequest("not valied data");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest("not valied data " + ex.Message);
            }

            //return  res ;
        }

        public string ValedEmployee(int Employeeid, String FirstName, String LastName, String Emailid, int Pno, string DOB, int Sal)
        {
            try
            {
                if (Employeeid == 0 && FirstName == "" && LastName == "" && Emailid == "" && Pno == 0 && DOB == "" && Sal == 0)
                {

                    if (Employeeid == 0)
                    {
                        return "Invalide Employee ID";
                    }
                    else if (FirstName == "")
                    {
                        return "Invalide First Name";
                    }
                    else if (LastName == "")
                    {
                        return "Invalide Last Name";
                    }
                    else if (Emailid == "")
                    {
                        return "Invalide Email";
                    }
                    else if (Pno == 0)
                    {
                        return "Invalide Pno";
                    }
                    else if (DOB == "")
                    {
                        return "Invalide First Name";
                    }
                    else if (Sal == 0)
                    {
                        return "Invalied Salary";
                    }
                        return "OK";
                }
                else
                {
                    return "In Valide Data";
                }
            }
            catch (Exception ex)
            {
                return "Error" + ex.Message;
            }
            
        }

            // GET api/<EmployeeController>/5
            [HttpGet("{id}")]
        public string GetEmployeeDetails(int Employeeid)
        {
            try
            {
                string query = @"select * from dbo.Employee  where EMPID=" + Employeeid;
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Assetmgt"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    
                    cmd.CommandType =  CommandType.Text;
                    da.Fill( table);
                }

                return "Sucesss";
            }
            catch (Exception ex)
            {
                return ex.Message; ;
            }

        } 


        [HttpGet("{id}")]
        // public string GetEmployeeDetails(int Employeeid, String FirstName, String LastName, String Emailid, int Pno, DateTime DOB, int Sal)
        public async Task<IEnumerable<EmployeeModel>> GetEmployeeDetails(int Employeeid, String FirstName, String LastName, String Emailid, int Pno, string DOB, int Sal)
        { 
            List<EmployeeModel> Emplist = new List<EmployeeModel>();
            try
            {
                string query = @"select * from dbo.Employee  where EMPID=" + Employeeid;
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Assetmgt"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    await cmd.ExecuteReaderAsync();
                    cmd.CommandType = CommandType.Text;
                    da.Fill( table);
                }
                 
             }
            catch (Exception ex)
             {
                   throw ex;
             }
            return  Emplist;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpDelete("TaXcal")]
        public double TaXcal(int Employeeid, int income)
        {
            try
            {
                double tax = CalculateTax(income);

                return tax ;
            }
            catch (Exception)
            { 
                throw;
            }

        }

        static double CalculateTax(double income)
        {
            double tax = 0;

            // Define tax brackets and rates
            double[] brackets = { 250000, 500000, 1000000, 10000000 };
            double[] rates = { 0, 5, 10, 20 };

            // Iterate through the brackets and calculate tax
            for (int i = 1; i < brackets.Length; i++)
            {
                if (income <= brackets[i])
                {
                    tax += (income - brackets[i - 1]) * rates[i - 1];
                    break;
                }
                else
                {
                    tax += (brackets[i] - brackets[i - 1]) * rates[i - 1];
                }
            }

            return tax;
        }
    }
}
