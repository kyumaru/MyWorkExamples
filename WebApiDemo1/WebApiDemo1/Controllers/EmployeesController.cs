using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDBAccess;

namespace WebApiDemo1.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<employee> Get()
        {
            using (employeeDBEntities entities = new employeeDBEntities())
            {
                return entities.employees.ToList();
            }
        }


        //public employee Get(int id)
        //{
        //    using (employeeDBEntities entities = new employeeDBEntities())
        //    {
        //        return entities.employees.FirstOrDefault(e=>e.ID==id);
        //    }
        //}

        //notice how HttpResponseMessage type encapsulates an http message and entity
        public HttpResponseMessage Get(int id)
        {
            using (employeeDBEntities entities = new employeeDBEntities())
            {
                var entity = entities.employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                   return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, 
                        "employee with id = "+ entity.ID.ToString() + " not found");

                }
            }
        }
        //the purpuse of post in web api is receiving a new object to create a new db record
        //public void Post([FromBody]employee e)
        //{
        //    //using a db context
        //    using (employeeDBEntities entities = new employeeDBEntities())
        //    {
        //        entities.employees.Add(e);
        //        entities.SaveChanges();
        //    }
        //}

        //by a RESTfull constraint Post method shoul return httpStatus 204 item created 
        public HttpResponseMessage Post([FromBody]employee e)
        {
            try
            {
                //using a db context
                using (employeeDBEntities entities = new employeeDBEntities())
                {
                    entities.employees.Add(e);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, e);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + e.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (employeeDBEntities entities = new employeeDBEntities())
                {
                    var entity = entities.employees.FirstOrDefault(e => e.ID == id);

                    if (entity != null)
                    {
                        entities.employees.Remove(entity);
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "employee ID " + id + " deleted");

                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "error kyu");
                    }
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }

        public HttpResponseMessage Put([FromBody]employee e, int id)
        {
            try
            {
                //using a db context
                using (employeeDBEntities entities = new employeeDBEntities())
                {
                    var entity = entities.employees.FirstOrDefault(emp => emp.ID == id);

                    if (entity != null)
                    {
                        entity.name = e.name;
                        entity.salary = e.salary;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, "employee ID " + id + " updated");

                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "error kyu");
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}