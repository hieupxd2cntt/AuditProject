using System;
using Core.Common;

namespace Core.Base
{
    public abstract class ControllerBase : IDisposable
    {
        protected string ConnectionString { get; set; }

        protected ControllerBase()
        {
            ConnectionString = App.Configs.ConnectionString;
        }

        protected ControllerBase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Dispose()
        {
        }
    }
}
