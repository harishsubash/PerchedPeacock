using System.Threading.Tasks;

namespace PerchedPeacock.Core
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}