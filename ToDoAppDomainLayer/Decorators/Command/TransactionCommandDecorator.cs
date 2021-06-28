using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppDomainLayer.Interfaces;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace ToDoAppDomainLayer.Decorators.Command
{
    class TransactionCommandDecorator<TParam, TOutput>
    {
        private ICommand<TParam, TOutput> Decoratee { get; set; }
        private ILogger<ErrorHandlingCommandDecorator<TParam, TOutput>> Logger { get; set; }
        public TransactionCommandDecorator(ICommand<TParam, TOutput> decoratee,
            ILogger<ErrorHandlingCommandDecorator<TParam, TOutput>> logger)
        {
            this.Decoratee = decoratee;
            this.Logger = logger;
        }
        public TOutput Execute(TParam param)
        {
            TOutput result;
            try
            {
                using TransactionScope scope = new TransactionScope();
                result = Decoratee.Execute(param);
                scope.Complete();
            }
            catch
            {
                Logger.LogError("Error during transaction has occured. Rolling back...");
                throw;
            }
            return result;
        }
    }
}
