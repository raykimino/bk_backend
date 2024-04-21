using System.Security.Cryptography;
using System.Text;
using bk_backend.EFCore;
using bk_backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bk_backend.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly AppDbContext _dbContext;

    public class RegisterDto
    {
        public string UserName{ get; set; } = null!;
        public string Password{ get; set; } = null!;
        public int Type{ get; set; }
    }
    
    public class LoginDto
    {
        public string UserName{ get; set; } = null!;
        public string Password{ get; set; } = null!;
    }
    
    public class UserListDto
    {
        public required Guid UserId { get; set; } 
        public string? UserName { get; set; }
        public int UserType { get; set; }
    }
    
    private string ToMd5(string input)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] bytes = Encoding.Default.GetBytes(input);
        byte[] encryptedData = md5.ComputeHash(bytes);
        return Convert.ToBase64String(encryptedData);
    }

    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var isUserExist = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == registerDto.UserName);
        if (isUserExist != null)
        {
            return Ok("user is exist");
        }

        string enCodePwd = ToMd5(registerDto.Password);
        var newUser = new User()
        {
            UserId = new Guid(),
            UserName = registerDto.UserName,
            Password = enCodePwd,
            UserType = registerDto.Type
        };
        
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        var isUserExist = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
        if (isUserExist == null)
        {
            return Ok("userName or pwd incorrect");
        }

        if (ToMd5(loginDto.Password) != isUserExist.Password)
        {
            return Ok("userName or pwd incorrect");
        }

        return Ok();
    }

    [HttpGet("userList")]
    public async Task<ActionResult> GetUserList()
    {
        return Ok(await _dbContext.Users.Select(x => new UserListDto
        {
            UserId = x.UserId,
            UserName = x.UserName,
            UserType = x.UserType
        }).ToListAsync());
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
        if (user !=null)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        return Ok("user not exist");
    }
}