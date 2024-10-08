﻿using DSS.Data.Models;
using DSS.Data.Repository;
using DSS.Data.Repositoty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data
{
    public class UnitOfWork
    {
        private Net1704_221_6_DSSContext _unitOfWorkContext;
        private ExtraDiamondRepository _extraDiamond;
        private CustomerRepository _customerRepository;
        private DiamondShellRepository _diamondshellRepository;
        private OrderRepository _order;
        private OrderDetailRepository _orderDetail;
        private ProductRepository _product;
        private MainDiamondRepository _mainDiamondRepository;
        public UnitOfWork()
        {
            _unitOfWorkContext ??= new Net1704_221_6_DSSContext();
        }
        public UnitOfWork(Net1704_221_6_DSSContext context)
        {
            _unitOfWorkContext = context;
        }
        public ExtraDiamondRepository ExtraDiamondRepository
        {
            get
            {
                return _extraDiamond ??= new Repository.ExtraDiamondRepository(_unitOfWorkContext);
            }
        }
        public MainDiamondRepository MainDiamondRepository
        {
            get
            {
                return _mainDiamondRepository ??= new Repository.MainDiamondRepository(_unitOfWorkContext);
            }
        }
        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customerRepository ??= new Repository.CustomerRepository(_unitOfWorkContext);
            }
        }
        public OrderRepository OrderRepository
        {
            get
            {
                return _order ??= new OrderRepository(_unitOfWorkContext);
            }
        }

        public OrderDetailRepository OrderDetailRepository
        {
            get
            {
                return _orderDetail ??= new OrderDetailRepository();
            }
        }
        public ProductRepository ProductRepository
        {
            get
            {
                return _product ??= new ProductRepository(_unitOfWorkContext);
            }
        }

        public DiamondShellRepository DiamondShellRepository 
        {
            get
            {
                return _diamondshellRepository ??= new Repository.DiamondShellRepository(_unitOfWorkContext);
            }
        }

        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = _unitOfWorkContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = await _unitOfWorkContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion
    }
}
