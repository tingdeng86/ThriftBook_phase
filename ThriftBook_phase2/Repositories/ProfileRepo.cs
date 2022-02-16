using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;

namespace ThriftBook_phase2.Repositories
{
    public class ProfileRepo
    {
        ApplicationDbContext _context;

        public ProfileRepo(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
