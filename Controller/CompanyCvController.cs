using bk_backend.EFCore;
using bk_backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bk_backend.Controller;

[ApiController]
[Route("api/[Controller]")]
public class CompanyCvController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public CompanyCvController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetListCvByUserId")]
    public async Task<ActionResult> GetListCvByUserId(Guid userId)
    {
        List<CompanyCv> companyCvs = await _dbContext.CompanyCvs.Where(cc => cc.UserId == userId).ToListAsync();
        if (companyCvs.Count < 1)
        {
            return Ok("你未投递过简历");
        }

        return Ok(companyCvs);
    }
    
    [HttpGet("GetListCvByCompanyId")]
    public async Task<ActionResult> GetListCvByCompanyId(Guid companyId)
    {
        List<CompanyCv> companyCvs = await _dbContext.CompanyCvs.Where(cc => cc.CompanyId == companyId).ToListAsync();
        if (companyCvs.Count < 1)
        {
            return Ok("该企业未有学生投递简历");
        }

        return Ok(companyCvs);
    }
    
    
}