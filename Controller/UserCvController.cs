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
        public string? UserRealName { get; set; }
        public string? UserGender { get; set; }
        public string? UserBirthDay { get; set; }
        public string? UserPolicyFace { get; set; }
        public string? UserPhone { get; set; }
        public string? UserEmail { get; set; }
        public string? UserBirthPlace { get; set; }
        public string? AimWorkPlace { get; set; }
        public string? AimJob { get; set; }
        public string? AimJobPosition { get; set; }
        public string? JobSeekingStatus { get; set; }
        public string? SalaryStart { get; set; }
        public string? SalaryEnd { get; set; }
        public string? StartWorkTime { get; set; }
        public string? WorkType { get; set; } 
        public string? UserSkill { get; set; }
        public string? UserAchievement { get; set; }
        public string? UserProjectExp { get; set; }
        public string? UserSchoolName { get; set; }
        public string? UserMajor { get; set; }
        public string? UserDegree { get; set; }
        
    }
    
    [HttpGet("GetUserCvById")]
    public async Task<ActionResult<UserCv>> GetUserCvById(Guid id)
    {
        var userCv = await _appDbContext.Set<UserCv>().Where(u => u.UserCvId == id).FirstOrDefaultAsync();
        if (userCv == null)
        {
            return Ok();
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

    [HttpPost]
    public async Task<ActionResult> InsertUserCv(UserCvDto userCvDto)
    {
        UserCv userCv = new UserCv()
        {
            UserCvId = Guid.NewGuid(),
            UserId = userCvDto.UserId,
            UserRealName = userCvDto.UserRealName,
            UserGender = userCvDto.UserGender,
            UserBirthDay = userCvDto.UserBirthDay,
            UserPolicyFace = userCvDto.UserPolicyFace,
            UserPhone = userCvDto.UserPhone,
            UserEmail = userCvDto.UserEmail,
            UserBirthPlace = userCvDto.UserBirthPlace,
            AimWorkPlace = userCvDto.AimWorkPlace,
            AimJob = userCvDto.AimJob,
            AimJobPosition = userCvDto.AimJobPosition,
            JobSeekingStatus = userCvDto.JobSeekingStatus,
            SalaryStart = userCvDto.SalaryStart,
            SalaryEnd = userCvDto.SalaryEnd,
            StartWorkTime  = userCvDto.StartWorkTime,
            WorkType  = userCvDto.WorkType,
            UserSkill = userCvDto.UserSkill,
            UserAchievement = userCvDto.UserAchievement,
            UserProjectExp = userCvDto.UserProjectExp,
            UserSchoolName = userCvDto.UserSchoolName,
            UserMajor = userCvDto.UserMajor,
            UserDegree = userCvDto.UserDegree
        };
        await _appDbContext.Set<UserCv>().AddAsync(userCv);
        await _appDbContext.SaveChangesAsync();
        return Ok("success");
    }

    [HttpDelete]
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

    [HttpPut]
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