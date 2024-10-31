using System;
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
}
