using DSS.Business.Base;
using DSS.Common;
using DSS.Data;
using DSS.Data.DAO;
using DSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Business.Business
{
    public interface OrderDetailInterface
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int code);
        Task<IBusinessResult> Save(OrderDetail order);
        Task<IBusinessResult> Update(OrderDetail order);
        Task<IBusinessResult> Delete(int code);
    };

    public class OrderDetail_Business : OrderDetailInterface
    {
        //private OrderDetailDAO _DAO;
        private UnitOfWork _unitOfWork;

        public OrderDetail_Business()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IBusinessResult> Delete(int code)
        {
            try
            {
                var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(code);
                if (orderDetail != null)
                {
                    var result = await _unitOfWork.OrderDetailRepository.RemoveAsync(orderDetail);
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

        public async Task<int> SaveAll()
        {
            return await _unitOfWork.OrderDetailRepository.SaveAll();
        }
        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var orderDetails = await _unitOfWork.OrderDetailRepository.GetAllAsync();

                if (orderDetails == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetails);
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

                var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(code);

                if (orderDetail == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetail);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }

        }

        public async Task<IBusinessResult> Save(OrderDetail orderDetail)
        {
            try
            {
                //int result = await _unitOfWork.OrderDetailRepository.CreateAsync(orderDetail);
                _unitOfWork.OrderDetailRepository.PrepareCreate(orderDetail);
                int result = await _unitOfWork.OrderDetailRepository.SaveAsync();
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

        public async Task<IBusinessResult> Update(OrderDetail orderDetail)
        {
            try
            {
                int result = await _unitOfWork.OrderDetailRepository.UpdateAsync(orderDetail);
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

        public async Task<IBusinessResult> Create(OrderDetail orderDetail)
        {
            try
            {

                int result = await _unitOfWork.OrderDetailRepository.CreateAsync(orderDetail);
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
