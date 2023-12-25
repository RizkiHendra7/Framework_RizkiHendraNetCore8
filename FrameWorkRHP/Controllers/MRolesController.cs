using FrameworkRHP.Core.Models.EF;
using FrameworkRHP.Services.Interfaces.GenericInterface;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorkRHP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MRolesController : ControllerBase
    {
        public readonly IGenericService<Mrole> _MroleService;
        public MRolesController(IGenericService<Mrole> MroleService)
        {
            _MroleService = MroleService;
        }
         
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var MRoleList = await _MroleService.GetAllData();
                if (MRoleList == null)
                {
                    return NotFound();
                }
                return Ok(MRoleList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }
         
        [HttpGet("{paramIntRoleId}")]
        public async Task<IActionResult> GetById(int paramIntRoleId)
        {
            try
            { 
                var MRole = await _MroleService.GetDataById(paramIntRoleId);

                if (MRole != null)
                {
                    return Ok(MRole);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         
        [HttpPost]
        public async Task<IActionResult> Create(Mrole paramMRoleModel)
        {
            try
            {
                var isMroleCreated = await _MroleService.CreateData(paramMRoleModel);

                if (isMroleCreated)
                {
                    return Ok(isMroleCreated);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         
        [HttpPut]
        public async Task<IActionResult> Update(Mrole paramMRoleModel)
        {
            try
            { 
                if (paramMRoleModel != null)
                {
                    var isMroleCreated = await _MroleService.UpdateData(paramMRoleModel);
                    if (isMroleCreated)
                    {
                        return Ok(isMroleCreated);
                    }
                    return BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         
        [HttpDelete("{paramIntRoleId}")]
        public async Task<IActionResult> Delete(int paramIntRoleId)
        {
            try
            { 
                var isMroleCreated = await _MroleService.DeleteData(paramIntRoleId);

                if (isMroleCreated)
                {
                    return Ok(isMroleCreated);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

} 
