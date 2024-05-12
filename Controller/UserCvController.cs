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
        public Guid userId { get; set; }
        public string? userRealName { get; set; }
        public string? userGender { get; set; }
        public string? userBirthDay { get; set; }
        public string? userPolicyFace { get; set; }
        public string? userPhone { get; set; }
        public string? userEmail { get; set; }
        public string? userBirthPlace { get; set; }
        public string? aimWorkPlace { get; set; }
        public string? aimJob { get; set; }
        public string? aimJobPosition { get; set; }
        public string? jobSeekingStatus { get; set; }
        public string? salaryStart { get; set; }
        public string? salaryEnd { get; set; }
        public string? startWorkTime { get; set; }
        public string? workType { get; set; } 
        public string? userSkill { get; set; }
        public string? userAchievement { get; set; }
        public string? userProjectExp { get; set; }
        public string? userSchoolName { get; set; }
        public string? userMajor { get; set; }
        public string? userDegree { get; set; }
        
    }
    
    [HttpGet("GetUserCvById")]
    public async Task<ActionResult> GetUserCvById(Guid id)
    {
        var userCv = await _appDbContext.Set<UserCv>().Where(u => u.UserId == id).FirstOrDefaultAsync();
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
            UserId = userCvDto.userId,
            UserRealName = userCvDto.userRealName,
            UserGender = userCvDto.userGender,
            UserBirthDay = userCvDto.userBirthDay,
            UserPolicyFace = userCvDto.userPolicyFace,
            UserPhone = userCvDto.userPhone,
            UserEmail = userCvDto.userEmail,
            UserBirthPlace = userCvDto.userBirthPlace,
            AimWorkPlace = userCvDto.aimWorkPlace,
            AimJob = userCvDto.aimJob,
            AimJobPosition = userCvDto.aimJobPosition,
            JobSeekingStatus = userCvDto.jobSeekingStatus,
            SalaryStart = userCvDto.salaryStart,
            SalaryEnd = userCvDto.salaryEnd,
            StartWorkTime  = userCvDto.startWorkTime,
            WorkType  = userCvDto.workType,
            UserSkill = userCvDto.userSkill,
            UserAchievement = userCvDto.userAchievement,
            UserProjectExp = userCvDto.userProjectExp,
            UserSchoolName = userCvDto.userSchoolName,
            UserMajor = userCvDto.userMajor,
            UserDegree = userCvDto.userDegree
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
        _appDbContext.Entry(getUserCv).State = EntityState.Detached;
        _appDbContext.Set<UserCv>().Update(userCv);
        await _appDbContext.SaveChangesAsync();
        return Ok("success update");
    }
}