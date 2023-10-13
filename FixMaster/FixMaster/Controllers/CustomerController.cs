using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FixMaster.DTO;
using FixMaster.Interfaces;
using FixMaster.Models;
using FixMaster.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FixMaster.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase 
	{
        private readonly ICustomerInterface _customerService;
        private readonly IConfiguration _configuration;

        public CustomerController(ICustomerInterface customerService, IConfiguration configuration)
        {
            _customerService = customerService;
            _configuration = configuration;
        }

        // POST api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerClass customer)
        {
            await _customerService.CreateCustomer(customer);
            return Ok(customer);
        }

        // PUT api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] CustomerClass customer)
        {
            await _customerService.UpdateCustomer(id, customer);
            return Ok(customer);
        }

        // DELETE api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _customerService.DeleteCustomer(id);
            return Ok();
        }

        // GET api/customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomers();
            return Ok(customers);
        }

        // GET api/customers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        // POST api/customers/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginCustomer([FromBody] LoginDto loginDto)
        {
            var customer = await _customerService.LoginCustomerAsync(loginDto.Email);
            if (customer == null)
            {
                return Unauthorized("Invalid email address or password");
            }
            var token = GenerateJwtToken(customer);


            return Ok(new { token });
        }
        private string GenerateJwtToken(CustomerClass model)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, model.email),
                new Claim(ClaimTypes.Name, model.id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }

}

