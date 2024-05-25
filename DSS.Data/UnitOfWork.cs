using DSS.Data.Models;
using DSS.Data.Repository;
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
        public UnitOfWork()
        {
            
        }
        public ExtraDiamondRepository ExtraDiamondRepository
        {
            get
            {
                return _extraDiamond ??= new Repository.ExtraDiamondRepository();
            }
        }
    }
}
