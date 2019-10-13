using System.Threading.Tasks;

namespace PerchedPeacock.Core
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}