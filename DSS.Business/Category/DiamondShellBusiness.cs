using DSS.Business.Base;
using DSS.Common;
using DSS.Data;
using DSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Business.Category
{
    public interface IDiamondShellBussiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> Create(DiamondShell diamondShell);
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(DiamondShell diamondShell);

        Task<IBusinessResult> Update(DiamondShell diamondShell);
        Task<IBusinessResult> DeleteById(int id);
        Task<int> SaveAll();
    }
    public class DiamondShellBusiness : IDiamondShellBussiness
    {
        private readonly UnitOfWork _unitOfWork;
        public DiamondShellBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> Create(DiamondShell diamondShell)
        {
            try
            {
                int result = await _unitOfWork.DiamondShellRepository.CreateAsync(diamondShell);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                var diamondShells = await _unitOfWork.DiamondShellRepository.GetByIdAsync(id);
                if (diamondShells != null)
                {
                    var result = await _unitOfWork.DiamondShellRepository.RemoveAsync(diamondShells);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var diamondShells = await _unitOfWork.DiamondShellRepository.GetAllAsync();

                if (diamondShells == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, diamondShells);
                }

            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                #region Business rule
                #endregion
                var diamondShells = await _unitOfWork.DiamondShellRepository.GetByIdAsync(id);

                if (diamondShells == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, diamondShells);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(DiamondShell diamondShell)
        {
            try
            {
                _unitOfWork.DiamondShellRepository.PrepareCreate(diamondShell);
                int result = await _unitOfWork.DiamondShellRepository.SaveAsync();
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<int> SaveAll()
        {
            return await _unitOfWork.DiamondShellRepository.SaveAsync();
        }

        public async Task<IBusinessResult> Update(DiamondShell diamondShell)
        {
            try
            {
                int result = await _unitOfWork.DiamondShellRepository.UpdateAsync(diamondShell);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
