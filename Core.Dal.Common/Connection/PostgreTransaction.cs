using System;
using Npgsql;

namespace Core.Dal.Common.Connection
{
    public class PostgreTransaction : IDisposable
    {
        public PostgreTransaction(NpgsqlTransaction transaction)
        {
            Transaction = transaction;
        }

        private NpgsqlTransaction Transaction { get; }

        public void Dispose()
        {
            Transaction?.Dispose();
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Disposed;
        public event EventHandler Commited;
        public event EventHandler Rollbacked;

        public void Commit()
        {
            Transaction.Commit();
            Commited?.Invoke(this, EventArgs.Empty);
        }

        public void Rollback()
        {
            Transaction.Rollback();
            Rollbacked?.Invoke(this, EventArgs.Empty);
        }
    }
}