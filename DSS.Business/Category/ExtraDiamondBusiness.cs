using DSS.Business.Base;
using DSS.Common;
using DSS.Data.Dao;
using DSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Business.Category
{
    public interface IExtraDiamondBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int code);
        Task<IBusinessResult> Save(ExtraDiamond extraDiamond);
        Task<IBusinessResult> Update(ExtraDiamond extraDiamond);
        Task<IBusinessResult> DeleteById(int code);
    }
    public class ExtraDiamondBusiness : IExtraDiamondBusiness
    {
        private readonly ExtraDiamondDAO _DAO;
        public ExtraDiamondBusiness()
        {
            _DAO = new ExtraDiamondDAO();
        }
        public async Task<IBusinessResult> DeleteById(int code)
        {
            try
            {
                var extraDiamond = await _DAO.GetByIdAsync(code);
                if (extraDiamond != null)
                {
                    var result = await _DAO.RemoveAsync(extraDiamond);
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
                var extraDiamonds = await _DAO.GetAllAsync();

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

        public async Task<IBusinessResult> GetById(int code)
        {
            try
            {
                #region Business rule
                #endregion

                var extraDiamond = await _DAO.GetByIdAsync(code);

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
                int result = await _DAO.CreateAsync(extraDiamond);
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
                int result = await _DAO.UpdateAsync(extraDiamond);
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
