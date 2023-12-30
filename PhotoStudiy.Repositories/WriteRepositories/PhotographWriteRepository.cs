using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context.Contracts.Models;
using PhotoStudiy.Repositories.Anchors;
using PhotoStudiy.Repositories.Contracts.WriteRepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.WriteRepositories
{
    public class PhotographWriteRepository : BaseWriteRepository<Photogragh>, IPhotographWriteRepository, IRepositoryAnchor
    {
        public PhotographWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {

        }
    }
}
