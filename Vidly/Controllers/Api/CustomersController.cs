using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //Get /api /customers
        public IHttpActionResult GetCustomers()
        {
            return Ok(_context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>));
        }

        //Get /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customerindb = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customerindb == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customerindb));
        }

        //Post /api /customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //Put /api /customers/1
        [HttpPut]
        public IHttpActionResult Edit(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerindb = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customerindb == null)
                return NotFound();

            Mapper.Map(customerDto, customerindb);
            _context.SaveChanges();
            return Ok();

        }
        //Delete /api /customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerindb = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customerindb == null)
                return NotFound();

            _context.Customers.Remove(customerindb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
