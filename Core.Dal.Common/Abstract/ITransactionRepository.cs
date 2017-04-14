using Npgsql;

namespace Core.Dal.Common.Abstract
{
    public interface ITransactionRepository
    {
        NpgsqlTransaction BeginTransaction();
    }
}
