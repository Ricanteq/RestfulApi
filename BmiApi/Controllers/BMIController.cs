using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BmiApi.Models;
using BmiApi.Services.BMICalculatorService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BmiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BMIController : ControllerBase
    {
        private readonly BMICalculatorService _bmiCalculatorService;
        public BMIController(BMICalculatorService bmiCalculatorService)
        {
            _bmiCalculatorService = bmiCalculatorService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name)) return BadRequest("Name cannot be empty");
            if (user.Age == null) return BadRequest("Age cannot be empty");
            if (user.Weight == null) return BadRequest("Weight cannot be empty");
            if (user.Height == null) return BadRequest("Height cannot be empty");

            var userResponse = await _bmiCalculatorService.CreateUserCalculateBmi(user);

            return Ok(userResponse);
        }
        
        
        
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
          
            var users = await _bmiCalculatorService.GetUsers();
            return Ok(users);
        }
      
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User updatedUser)
        {
            var userToUpdate = _bmiCalculatorService.GetUserById(id);
            if (userToUpdate == null) return NotFound();

            userToUpdate.Name = updatedUser.Name ?? userToUpdate.Name;
            userToUpdate.Age = updatedUser.Age;
            userToUpdate.Weight = updatedUser.Weight;
            userToUpdate.Height = updatedUser.Height;
            userToUpdate.BMI = _bmiCalculatorService.CalculateBMI(userToUpdate.Weight, userToUpdate.Height);

            return Ok(userToUpdate);
        }

        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = _bmiCalculatorService.GetUserById(id);
            if (userToDelete == null) return NotFound();

            await _bmiCalculatorService.DeleteUser(userToDelete);

            return NoContent();
        }

       
    }
}
