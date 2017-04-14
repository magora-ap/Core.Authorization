using System;
using Npgsql;

namespace Core.Dal.Common.Connection
{
    /// <summary>
    /// Class for work with postgreSql connection pool
    /// </summary>
    /// Initial author Sergey Sushenko
    public class PostgreConnection : IDisposable
    {
        public NpgsqlConnection ConnectionDb { get; private set; }
        private PostgreTransaction TransactionDb { get; set; }
        private bool IsOpened { get; set; }
        public PostgreConnection(string connectionString)
        {
            ConnectionDb = new NpgsqlConnection(connectionString);
        }

        public PostgreTransaction BeginTransaction()
        {
            Open();
            TransactionDb = new PostgreTransaction(ConnectionDb.BeginTransaction());
            TransactionDb.Disposed += TransactionDb_Disposed;
            TransactionDb.Commited += TransactionDb_Commited;
            TransactionDb.Rollbacked += TransactionDb_Rollbacked;
            return TransactionDb;
        }

        private void TransactionDb_Rollbacked(object sender, EventArgs e)
        {
            TransactionDb = null;
            Release();
        }

        private void TransactionDb_Commited(object sender, EventArgs e)
        {
            TransactionDb = null;
            Release();
        }

        private void TransactionDb_Disposed(object sender, EventArgs e)
        {
            TransactionDb = null;
            Release();
        }

        public void Open()
        {
            if (IsOpened) return;
            ConnectionDb.Open();
            IsOpened = true;
        }

        public void Release()
        {
            if ((TransactionDb!= null) || !IsOpened) return;
            ConnectionDb.Close();
            IsOpened = false;
        }

        public void Dispose()
        {
            ConnectionDb?.Dispose();
            TransactionDb?.Dispose();
        }
    }
}
