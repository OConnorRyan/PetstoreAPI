using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace MONITOR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SaleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select SALEID,SALENUMBER,PONUMBER,ORDERNUMBER from sale";

            System.Data.DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DatabaseConnection");

            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close(); 
                }
            }

            return new JsonResult(table);
        }
    }
}
