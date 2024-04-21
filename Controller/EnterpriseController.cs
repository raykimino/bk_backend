using System.Text.Json.Nodes;
using bk_backend.EFCore;
using bk_backend.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bk_backend.Controller;

[Route("api")]
[ApiController]
public class EnterpriseController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public EnterpriseController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public class EnterpriseDto
    {
        public required string EnterpriseName { get; set; }
        public required string EnterpriseAddress { get; set; }
        public required string EnterpriseIndustry { get; set; }
        public required string EnterpriseAreasInvolved { get; set; }
        public required string EnterpriseNature { get; set; }
        public required string EnterpriseArea { get; set; }
    }

    [HttpGet("GetAllEnterprise")]
    public async Task<ActionResult<List<Enterprise>>> GetAllEnterprise()
    {
        List<Enterprise> enterprises = await _appDbContext.Set<Enterprise>().ToListAsync();
        if (enterprises.Count < 1)
        {
            return Ok("false found enterprises");
        }
        return enterprises;
    }

    [HttpGet("GetEnterprise")]
    public async Task<ActionResult<Enterprise>> GetEnterprise(Guid id)
    {
        var enterprise = await _appDbContext.Set<Enterprise>().Where(e => e.EnterpriseInfoId == id).FirstOrDefaultAsync();
        if (enterprise == null)
        {
            return Ok("id is not found!");
        }
        return Ok(enterprise);
    }

    [HttpPost("InsertEnterprise")]
    public async Task<ActionResult> InsertEnterprise(EnterpriseDto enterpriseDto)
    {
        Enterprise enterprise = new Enterprise()
        {
            EnterpriseInfoId = new Guid(),
            EnterpriseNature = enterpriseDto.EnterpriseNature,
            EnterpriseName = enterpriseDto.EnterpriseName,
            EnterpriseAddress = enterpriseDto.EnterpriseAddress,
            EnterpriseArea = enterpriseDto.EnterpriseArea,
            EnterpriseAreasInvolved = enterpriseDto.EnterpriseAreasInvolved,
            EnterpriseIndustry = enterpriseDto.EnterpriseIndustry,

        };
        var addEnterprise = await _appDbContext.Set<Enterprise>().AddAsync(enterprise);
        var flag = await _appDbContext.SaveChangesAsync();
        if (flag != -1)
        {
            return BadRequest("failed insert");
        }
        return Ok("success insert");
    }

    [HttpDelete("DeleteEnterprise")]
    public async Task<ActionResult> DeleteEnterprise(Guid id)
    {
        var enterprise = await _appDbContext.Set<Enterprise>().Where(e => e.EnterpriseInfoId == id).FirstOrDefaultAsync();
        if (enterprise == null)
        {
            return Ok($"{id} is not found");
        }

        _appDbContext.Set<Enterprise>().Remove(enterprise);
        var flag = await _appDbContext.SaveChangesAsync();
        if (flag != -1)
        {
            return BadRequest("failed delete");
        }
        return Ok("success delete");
    }
}