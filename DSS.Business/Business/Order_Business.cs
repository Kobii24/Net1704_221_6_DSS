using DSS.Business.Base;
using DSS.Common;
using DSS.Data;
using DSS.Data.DAO;
using DSS.Data.Models;
using DSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Business.Business
{
    public interface OrderInterface
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> Create(Order order);
        Task<IBusinessResult> GetById(int code);
        Task<IBusinessResult> Save(Order order);
        Task<IBusinessResult> Update(Order order);
        Task<IBusinessResult> Delete(int code);
    };

    public class Order_Business : OrderInterface
    {
        private UnitOfWork _unitOfWork ;
        private Net1704_221_6_DSSContext _DSSContext;

        public Order_Business()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> Delete(int code)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(code);
                if (order != null)
                {
                    var result = await _unitOfWork.OrderRepository.RemoveAsync(order);
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

        public async Task <int> SaveAll()
        {
            return await _unitOfWork.OrderRepository.SaveAll();
        }
        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _unitOfWork.OrderRepository.GetAll();
                var orders = await _unitOfWork.OrderRepository.GetAllAsync() ;

                if (orders == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else 
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orders);
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

                var order = await _unitOfWork.OrderRepository.GetByIdAsync(code);

                if (order == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, order );
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }

        }

        public async Task<IBusinessResult> Save(Order order)
        {
            try
            {
               // int result = await _unitOfWork.OrderRepository.CreateAsync(order);
               _unitOfWork.OrderRepository.PrepareCreate(order);
                int result = await _unitOfWork.OrderRepository.SaveAsync();
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

        public async Task<IBusinessResult> Update(Order order)
        {
            try
            {

                int result = await _unitOfWork.OrderRepository.UpdateAsync(order);
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

        public async Task<IBusinessResult> Create(Order order)
        {
            try
            {

                int result = await _unitOfWork.OrderRepository.CreateAsync(order);
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
