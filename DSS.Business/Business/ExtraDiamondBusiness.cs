using DSS.Business.Base;
using DSS.Common;
using DSS.Data;
using DSS.Data.Dao;
using DSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Business.Business
{
    public interface IExtraDiamondBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> Create(ExtraDiamond extraDiamond);
        Task<IBusinessResult> GetById(int code);
        Task<IBusinessResult> Save(ExtraDiamond extraDiamond);

        Task<IBusinessResult> Update(ExtraDiamond extraDiamond);
        Task<IBusinessResult> DeleteById(int code);
    }
    public class ExtraDiamondBusiness : IExtraDiamondBusiness
    {
        //private readonly ExtraDiamondDAO _DAO;
        private readonly UnitOfWork _unitOfWork;
        public ExtraDiamondBusiness()
        {
            //_DAO = new ExtraDiamondDAO();
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> Create(ExtraDiamond extraDiamond)
        {
            try
            {
                int result = await _unitOfWork.ExtraDiamondRepository.CreateAsync(extraDiamond);
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

        public async Task<IBusinessResult> DeleteById(int code)
        {
            try
            {
                var extraDiamond = await _unitOfWork.ExtraDiamondRepository.GetByIdAsync(code);
                if (extraDiamond != null)
                {
                    var result = await _unitOfWork.ExtraDiamondRepository.RemoveAsync(extraDiamond);
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
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var extraDiamonds = await _unitOfWork.ExtraDiamondRepository.GetAllAsync();

                if (extraDiamonds == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, extraDiamonds);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<int> SaveAll()
        {
            return await _unitOfWork.ExtraDiamondRepository.SaveAsync();
        }
        public async Task<IBusinessResult> GetById(int code)
        {
            try
            {
                #region Business rule
                #endregion

                var extraDiamond = await _unitOfWork.ExtraDiamondRepository.GetByIdAsync(code);

                if (extraDiamond == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, extraDiamond);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(ExtraDiamond extraDiamond)
        {
            try
            {
                //int result = await _unitOfWork.ExtraDiamondRepository.CreateAsync(extraDiamond);
                _unitOfWork.ExtraDiamondRepository.PrepareCreate(extraDiamond);
                int result = await _unitOfWork.ExtraDiamondRepository.SaveAsync();
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

        public async Task<IBusinessResult> Update(ExtraDiamond extraDiamond)
        {
            try
            {
                int result = await _unitOfWork.ExtraDiamondRepository.UpdateAsync(extraDiamond);
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
