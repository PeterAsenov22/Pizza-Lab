namespace PizzaLab.WebAPI.Helpers.Logging
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Logging;
    using System;

    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly IApplicationBuilder _appBuilder;

        public DbLoggerProvider(
            Func<string, LogLevel, bool> filter,
            IApplicationBuilder appBuilder)
        {
            this._filter = filter;
            this._appBuilder = appBuilder;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(categoryName, _filter, _appBuilder);
        }

        public void Dispose()
        {

        }
    }
}
