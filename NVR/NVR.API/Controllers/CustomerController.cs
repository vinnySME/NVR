using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NVR.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NVR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly string dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\{"Customers.txt"}");
        private ResponseData responseData = new ResponseData();

        public CustomerController()
        {
        }

        [HttpGet]
        public ActionResult<ResponseData> Get()
        {
            try
            {
                var JSON = System.IO.File.ReadAllText(dbFilePath);
                var customers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(JSON);

                responseData.success = true;
                responseData.data = customers;
                responseData.code = 200;
                responseData.message = "Get customers successfully !!";
                return Ok(responseData);
            }
            catch (Exception ex)
            {
                responseData.message = ex.Message;
                return Ok(responseData);
            }

            
        }

        [HttpPost]
        public ActionResult<ResponseData> Post(Customer customer)
        {
            try
            {
                var JSON = System.IO.File.ReadAllText(dbFilePath);
                var customers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(JSON);
                if (customers == null) customers = new List<Customer>();
                customers.Add(customer);
                
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(customers);
                System.IO.File.WriteAllText(dbFilePath, jsonString);

                responseData.success = true;
                responseData.code = 200;
                responseData.message = "Customer added successfully !!";
                responseData.data = customer.Email;
                return Ok(responseData);
            }
            catch (Exception ex)
            {
                responseData.message = ex.Message;
                return Ok(responseData);
            }
        }

    }
}
