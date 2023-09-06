using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pro.Data;
using pro.DTOs;
using pro.DTOs.Inside;
using pro.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pro.Controllers.checking
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly Context _context;

        public SkillController(Context context)
        {
            _context = context;
        }

        [HttpPost("CreateSkill")]
        public async Task<ActionResult<Skill>> CreateSkill(SkillDto skillDto)
        {
            var newSkill = new Skill
            {
                SkillType = skillDto.SkillType,
                SkillName = skillDto.SkillName,
            };

            // Add the new skill to the context
            _context.Skills.Add(newSkill);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the newly created skill
            return Ok(newSkill);
        }
    }
}
