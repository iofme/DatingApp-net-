using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.Controllers;

public class UsersController(DataContext context) : BaseApiController
{

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(){
        var users = await context.Users.ToListAsync();

        return users;
    }
    
    [Authorize]
    [HttpGet("{id:int}")] // /api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id){
        var user = await context.Users.FindAsync(id);
        
        if(user == null) return NotFound();

        return user;
    }
}