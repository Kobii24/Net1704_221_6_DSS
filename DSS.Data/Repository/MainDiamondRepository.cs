using DSS.Data.Base;
using DSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Repository
{
    public class MainDiamondRepository : GenericRepository<MainDiamond>
    {

        public MainDiamondRepository()
        {

        }
        public MainDiamondRepository(Net1704_221_6_DSSContext context) => _context = context;
    }
}
