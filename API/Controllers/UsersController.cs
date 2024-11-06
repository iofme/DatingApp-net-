using System;
using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUser(){
        var users = await userRepository.GetMemberAsync();
        
        //var result = mapper.Map<IEnumerable<MemberDto>>(users);

        // var result = new List<MemberDto>();

        // foreach (var u in users){
        //     var userToReturn = new MemberDto {
        //         Id  = u.Id,
        //         UserName = u.UserName,
        //         /// TODO
        //     };

        //     result.Add(userToReturn);
        // }

        return Ok(users);
    }
    
    [HttpGet("{username}")] // /api/users/2
    public async Task<ActionResult<MemberDto>> GetUser(string username){
        var user = await userRepository.GetMemberAsync(username);

        if(user == null) return NotFound();

        return user; 
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto){
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(username == null) return BadRequest("No username found in token");

        var user = await userRepository.GetUserByUsernameAsync(username);

        if(user == null) return BadRequest("Could not find user");

        mapper.Map(memberUpdateDto, user);

        if (await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
    }
}
