using Microsoft.AspNetCore.Mvc;
using MeuProjeto.Application.DTOs;
using MeuProjeto.Application.UseCases;

namespace MeuProjeto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ICreateUserUseCase _createUserUseCase;
    private readonly ILoginUseCase _loginUseCase;

    public AuthController(ICreateUserUseCase createUserUseCase, ILoginUseCase loginUseCase)
    {
        _createUserUseCase = createUserUseCase;
        _loginUseCase = loginUseCase;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        var response = await _createUserUseCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(Register), new { id = response.Id }, response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _loginUseCase.ExecuteAsync(request);
        return Ok(response);
    }
}
