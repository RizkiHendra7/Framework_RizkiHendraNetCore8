﻿using FrameworkRHP.Core.Models.EF;
using FrameworkRHP.Infrastructure.UOW;
using FrameworkRHP.Services.Interfaces.GenericInterface;

namespace FrameworkRHP.Services.ServicesImplement
{
    public class MUserService : IGenericService<Muser>
    {
        public IUnitOfWork _unitOfWork;

        public MUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateData(Muser ParamModels)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MUsers.InsertAsync(ParamModels);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteData(int ParamIntId)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MUsers.DeleteAsync(ParamIntId);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public Task<IEnumerable<Muser>> GetAllActiveData()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Muser>> GetAllData()
        { 
            return await _unitOfWork.MUsers.GetAllAsync();
        } 
         
        public async Task<Muser> GetDataById(int ParamIntId)
        {
            var MUserData =   await _unitOfWork.MUsers.GetByIdAsync(Convert.ToInt32(ParamIntId));
            MUserData = MUserData == null ? new Muser() : MUserData;
            return MUserData;
        }

        public async Task<bool> UpdateData(Muser ParamModels)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MUsers.UpdateAsync(ParamModels);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }

}
