using System.Linq;

namespace TestProject1
{
    public interface IEntityService
    {
        IQueryable<Entity> QueryAll();
    }
}