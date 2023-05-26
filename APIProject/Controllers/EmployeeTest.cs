using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIProject.Controllers
{

    public class EmployeeTest : ApiController
    {
        [System.Web.Http.Route("api/GetEmpDetails")]
        public HttpResponseMessage GetEmpDetails(int EmpID)
        {
            try
            {
                string query = @"select ea.ID EMPAssetID,a.name AssetType,ea.FromDate,ea.ToDate,ea.Remarks,e.AssetName,e.id AssetID
                    from dbo.EmployeeAssets ea,dbo.Mst_Assets e,Mst_AssetType a,dbo.Mst_employees em
                    where ea.assetid=e.id and e.assettypeid=a.id and ea.deleted=0 and em.id=ea.empid and em.id=" + EmpID;
                DataTable table = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Assetmgt"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                    return Request.CreateResponse(HttpStatusCode.OK, table);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
