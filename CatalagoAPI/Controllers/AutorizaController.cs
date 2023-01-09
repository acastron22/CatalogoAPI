﻿using CatalagoAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatalagoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutorizaController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;


    public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        return "AutorizaController :: Acessado em : " + DateTime.Now.ToLongDateString();
    }

    [HttpPost("Register")]
    public async Task<ActionResult> RegisterUser([FromBody]UsuarioDTO model)
    {
        //if (ModelState.IsValid)
        //{
        //    return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
        //}

        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _signInManager.SignInAsync(user, false);
        return Ok(GeraToken(model));
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UsuarioDTO userInfo)
    {
        // verifica se o modelo é valido
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(e=> e.Errors));
        }

        // verifica as credenciais do usuário e retorna um valor
        var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return Ok(GeraToken(userInfo));
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Login Inválido");
            return BadRequest(ModelState);
        }
    }

    private UsuarioTokenDTO GeraToken(UsuarioDTO userInfo)
    {
        // Defina declaraçõs do usuário
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
            new Claim ("meuPet", "pipoca"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //Gera uma chave com base em um algoritmo simétrico
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

        // Gera a assinatura digital do token usando o algoritmo HMAC e a chave privada
        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Tempo de expi~ração do token.
        var expiracao = _configuration["TokenConfiguration:ExpireHours"];
        var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

        // classe que representa um token JWT e gera o token
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["TokenConfiguration:Issuer"],
            audience: _configuration["TokenConfiguration:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credenciais);

        // Retoprna os dados com o token e informações
        return new UsuarioTokenDTO()
        {
            Authenticated= true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Message = "Token JWT OK"
        };

    }

    
}
