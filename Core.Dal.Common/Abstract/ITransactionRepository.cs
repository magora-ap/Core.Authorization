using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Core.Dal.Common.Abstract
{
    public interface ITransactionRepository
    {
        NpgsqlTransaction BeginTransaction();
    }
}
