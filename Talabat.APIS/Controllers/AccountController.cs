﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Talabat.APIS.Dtos;
using Talabat.APIS.Errors;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIS.Controllers
{
    
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
           _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            
            var user=await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));   
            
            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token="This will be Token",

            });
        }

        [HttpPost("register")]
        
        public async Task <ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new AppUser()
            {
                DisplayName = model.DispalyName,
                Email = model.EmailAddress,
                UserName = model.EmailAddress.Split('@')[0],
                PhoneNumber = model.PhoneNumber,


            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token="THIS Will be there"
            });
        }
    }
}
