using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using pro.Models;
using pro.DTOs;
using pro.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using pro.Helper;
using pro.DTOs.Inside;
using Microsoft.EntityFrameworkCore;
using pro.Data;
using System.Linq;

namespace pro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        private readonly IManageImage _iManageImage;
        private readonly UserManager<User> _userManager;
        private readonly Context context;

        public ResumeController(IManageImage iManageImage, UserManager<User> userManager, Context context)
        {
            _iManageImage = iManageImage;
            _userManager = userManager;
            this.context = context;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<Resume>> UploadResume(IFormFile file)
        {
            try
            {
                // Get the authenticated user's UserId from the ClaimsPrincipal
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Check if the user exists in the database
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Upload the file and get the file name and path
                var fileName = await _iManageImage.UploadFile(file); // Use _iManageImage here
                var filePath = Common.GetFilePath(fileName); // Get the file path using your Common class

                // Create a new Resume entity
                var newResume = new Resume
                {
                    UserId = userId,
                    FileName = fileName,
                    FilePath = filePath,



                };

                // Associate the new Resume with the User
                user.Resumes = new List<Resume> { newResume };
                // Save changes to the database
                await _userManager.UpdateAsync(user);

                return Ok(newResume);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                return BadRequest("Error uploading resume.");
            }
        }
       



    }


}


