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
    public interface IMainDiamondBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> Create(MainDiamond mainDiamond);
        Task<IBusinessResult> GetById(int id);

        Task<IBusinessResult> Save(MainDiamond mainDiamond);
        Task<IBusinessResult> Update(MainDiamond mainDiamond);
        Task<IBusinessResult> DeleteById(int id);
    }
    public class MainDiamondBusiness : IMainDiamondBusiness
    {
        public readonly UnitOfWork _unitOfWork;
        public MainDiamondBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IBusinessResult> Create(MainDiamond mainDiamond)
        {
            try
            {
                int result = await _unitOfWork.MainDiamondRepository.CreateAsync(mainDiamond);
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


                var mainDiamond = await _unitOfWork.MainDiamondRepository.GetByIdAsync(id);
                if (mainDiamond != null)
                {
                    var result = await _unitOfWork.MainDiamondRepository.RemoveAsync(mainDiamond);
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
            }catch(Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
            }






        public async Task<IBusinessResult> GetAll()
        {
            try
            {


                var mainDiamond = await _unitOfWork.MainDiamondRepository.GetAllAsync();
                if (mainDiamond != null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, mainDiamond);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION,ex.Message);
            }
      }     
        

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var mainDiamonds = await _unitOfWork.MainDiamondRepository.GetByIdAsync(id);
                if(mainDiamonds != null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, mainDiamonds);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(MainDiamond mainDiamond)
        {
            try
            {
                _unitOfWork.MainDiamondRepository.CreateAsync(mainDiamond);
                int result = await _unitOfWork.MainDiamondRepository.SaveAsync();
                if(result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);

                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, mainDiamond);
                }
            }
            catch ( Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION,ex.Message);
            }
        }

        public Task<IBusinessResult> Update(MainDiamond mainDiamond)
        {
            throw new NotImplementedException();
        }
    }
        

       
    }

