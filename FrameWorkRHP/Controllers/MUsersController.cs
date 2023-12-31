using FrameworkRHP.Core.Models.EF;
using FrameworkRHP.Services.Interfaces;
using FrameworkRHP.Services.Interfaces.GenericInterface;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorkRHP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MUsersController : ControllerBase
    {
        public readonly IGenericService<Muser> _MUserService;
        public MUsersController(IGenericService<Muser> MUserService)
        {
            _MUserService = MUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var MUserList = await _MUserService.GetAllData();
                if (MUserList == null)
                {
                    return NotFound();
                }
                return Ok(MUserList);
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
                var MUser = await _MUserService.GetDataById(paramIntRoleId);

                if (MUser != null)
                {
                    return Ok(MUser);
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
        public async Task<IActionResult> Create(Muser paramMUserModel)
        {
            try
            {
                var isMUserCreated = await _MUserService.CreateData(paramMUserModel);

                if (isMUserCreated)
                {
                    return Ok(isMUserCreated);
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
        public async Task<IActionResult> Update(Muser paramMUserModel)
        {
            try
            {
                if (paramMUserModel != null)
                {
                    var isMUserCreated = await _MUserService.UpdateData(paramMUserModel);
                    if (isMUserCreated)
                    {
                        return Ok(isMUserCreated);
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
                var isMUserCreated = await _MUserService.DeleteData(paramIntRoleId);

                if (isMUserCreated)
                {
                    return Ok(isMUserCreated);
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
