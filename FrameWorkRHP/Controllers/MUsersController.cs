using FrameworkRHP.Core.Models.EF;
using FrameworkRHP.Infrastructure.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FrameWorkRHP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MUsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MUsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productDetailsList = await _unitOfWork.MUsers.GetAllAsync();
            if (productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }


        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var productDetails = await _unitOfWork.MUsers.GetMuserByIdAsync(Convert.ToInt32(userId));

            if (productDetails != null)
            {
                return Ok(productDetails);
            }
            else
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="ParamMUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Muser ParamMUser)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MUsers.InsertAsync(ParamMUser);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return Ok(true);
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return BadRequest();
            }              
        }

        /// <summary>
        /// Update the product
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Muser ParamMUser)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MUsers.UpdateAsync(ParamMUser);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return Ok(true);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return BadRequest(ex.Message);
            } 
        }


        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteProduct(int userId)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MUsers.DeleteAsync(userId);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return Ok(true);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return BadRequest(ex.Message);
            }
        }
         
    }
}
