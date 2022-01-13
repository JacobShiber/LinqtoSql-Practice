using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hospital.Controllers.API
{
    public class NurseController : ApiController
    {
        static string connectionString = "Data Source=DESKTOP-76KPC67;Initial Catalog=HospitalDB;Integrated Security=True;Pooling=False";

        NursesContextDataContext dataContext = new NursesContextDataContext(connectionString);
        // GET: api/Nurse
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(new { Massage = "Success!", Nurses = dataContext.Nurses.ToList() });
            }
            catch(SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Nurse/5
        public IHttpActionResult GetById(int id)
        {
            try
            {
                Nurse expectedNurse = dataContext.Nurses.First(nurse => nurse.Id == id);

                return Ok(new { Massage = "Success!", Nurse = expectedNurse });
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Nurse
        public IHttpActionResult AddNurse([FromBody]Nurse newNurse)
        {
            try
            {
                dataContext.Nurses.InsertOnSubmit(newNurse);
                dataContext.SubmitChanges();

                return Ok(new { Massage = "Success! new nurse been added" });
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Nurse/5
        [HttpPut]
        public IHttpActionResult EditNurse(int id, [FromBody]Nurse editedNurse)
        {
            try
            {
                Nurse expectedNurse = dataContext.Nurses.Single(nurse => nurse.Id == id);
                expectedNurse.FirstName = editedNurse.FirstName;
                expectedNurse.LastName = editedNurse.LastName;
                expectedNurse.Salary = editedNurse.Salary;
                expectedNurse.WorkHours = editedNurse.WorkHours;
                expectedNurse.BirthDate = editedNurse.BirthDate;

                dataContext.SubmitChanges();

                return Ok(new { Massage = "Success! nurse been edited" });
                                     
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Nurse/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                dataContext.Nurses.DeleteOnSubmit(dataContext.Nurses.First(nurse => nurse.Id == id));
                dataContext.SubmitChanges();

                return Ok(new { Massage = "Success! nurse been deleted" });
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
