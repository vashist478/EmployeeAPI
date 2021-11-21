using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPIServer.Controllers
{
    [Route("[controller]")]

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //[HttpGet]
        //public string[] Get()
        //{
        //    return new string[] { "mango", "banana", "pear" };
        //}       

        AppDbContext _db;

        public ValuesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _db.Employees.ToList();
        }


        /// <summary>
        /// SK: Get specific employee record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return _db.Employees.Find(id);
        }

        /// <summary>
        /// SK: Add Employee new record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(Employee model)
        {
            try
            {
                _db.Employees.Add(model);
                _db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// SK: Update Employee record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest();
                }
                else
                {
                    _db.Employees.Update(model);
                    _db.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK);
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        /// <summary>
        /// SK: Delete employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _db.Employees.Find(id);
            if (data != null)
            {
                _db.Employees.Remove(data);
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();

        }
    }
}
