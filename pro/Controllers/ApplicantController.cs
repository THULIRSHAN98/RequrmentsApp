using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using pro.Models;
using pro.DTOs;
using pro.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace pro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly Context _context;//database access
        private readonly UserManager<User> _userManager;//register user

        public ApplicantController(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("app")]
        public async Task<ActionResult<Applicant>> CreateApplicantForUser(ApplicantDTO applicantDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                if (user.Applicant != null)
                {
                    return BadRequest("User already has an associated Applicant.");
                }

                var newApplicant = new Applicant
                {
                    UserId = userId,
                    Title = applicantDto.Title,
                    Dob = applicantDto.Dob,
                    Gender = applicantDto.Gender,
                    PhoneNo = applicantDto.PhoneNo,
                    Email = applicantDto.Email,
                    Address = applicantDto.Address,
                    Street = applicantDto.Street,
                    City = applicantDto.City,
                    State = applicantDto.State,
                    Zip = applicantDto.Zip,
                    Country = applicantDto.Country,
                };

                user.Applicant = newApplicant;

                await _context.SaveChangesAsync();

                return Ok(newApplicant);
            }
            catch (Exception ex)
            {
                // Log the exception if needed

                // Set HTTP status code to 500 (Internal Server Error)
                return StatusCode(500, "Duplicate entry");
            }
        }
    }
}
