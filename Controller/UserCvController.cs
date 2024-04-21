using bk_backend.EFCore;
using bk_backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bk_backend.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserCvController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public UserCvController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public class UserCvDto
    {
        public Guid UserId { get; set; }
        public required string UserRealName { get; set; }
        public required string UserPhone { get; set; }
        public required string UserEmail { get; set; }
        public required string UserAddress { get; set; }
        public required string UserSchoolName { get; set; }
        public required string UserDegree { get; set; }
        public required string UserMajor { get; set; } 
        public required string UserAbility { get; set; }
        public required string UserProjectExp { get; set; }
        public required string UserAchievement { get; set; }
    }
    
    [HttpGet("GetUserCvById")]
    public async Task<ActionResult<UserCv>> GetUserCvById(Guid id)
    {
        var userCv = await _appDbContext.Set<UserCv>().Where(u => u.UserCvId == id).FirstOrDefaultAsync();
        if (userCv == null)
        {
            return Ok($"{id} is not found");
        }

        return Ok(userCv);
    }

    [HttpGet("GetAllUserCv")]
    public async Task<ActionResult<List<UserCv>>> GetAllUserCv()
    {
        List<UserCv> userCvs = await _appDbContext.Set<UserCv>().ToListAsync();
        if (userCvs.Count < 1)
        {
            return Ok("failed find UserCv");
        }

        return Ok(userCvs);
    }

    [HttpPost("InsertUserCv")]
    public async Task<ActionResult> InsertUserCv(UserCvDto userCvDto)
    {
        UserCv userCv = new UserCv()
        {
            UserCvId = new Guid(),
            UserRealName = userCvDto.UserRealName,   
            UserPhone = userCvDto.UserPhone,
            UserEmail = userCvDto.UserEmail,  
            UserAddress = userCvDto.UserAddress,    
            UserSchoolName = userCvDto.UserSchoolName,  
            UserDegree = userCvDto.UserDegree, 
            UserMajor = userCvDto.UserMajor,  
            UserAbility = userCvDto.UserAbility,    
            UserProjectExp = userCvDto.UserProjectExp,
            UserAchievement = userCvDto.UserAchievement     
        };
        await _appDbContext.Set<UserCv>().AddAsync(userCv);
        await _appDbContext.SaveChangesAsync();
        return Ok("success insert");
    }

    [HttpDelete("DeleteUserCvById")]
    public async Task<ActionResult> DeleteUserCvById(Guid id)
    {
        var userCv = await _appDbContext.Set<UserCv>().Where(u => u.UserCvId == id).FirstOrDefaultAsync();
        if (userCv == null)
        {
            return Ok($"{id} is not found");
        }

        _appDbContext.Set<UserCv>().Remove(userCv);
        await _appDbContext.SaveChangesAsync();
        return Ok("success delete");
    }

    [HttpPut("UpdateUserCv")]
    public async Task<ActionResult> UpdateUserCv(UserCv userCv)
    {
        var getUserCv = await _appDbContext.Set<UserCv>().Where(u => u.UserCvId == userCv.UserCvId).FirstOrDefaultAsync();
        if (getUserCv == null)
        {
            return Ok($"{userCv.UserCvId} is not found");
        }

        _appDbContext.Set<UserCv>().Update(userCv);
        await _appDbContext.SaveChangesAsync();
        return Ok("success update");
    }
}