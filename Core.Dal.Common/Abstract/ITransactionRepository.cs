using Core.Dal.Common.Connection;

namespace Core.Dal.Common.Abstract
{
    public interface ITransactionRepository
    {
        PostgreTransaction BeginTransaction();
    }
}
