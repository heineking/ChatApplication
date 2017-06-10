using System;
using System.Reflection;
using ChatApplication.Logging.JsonNet;
using ChatApplication.Syncronization.Contracts.Commands;
using ChatApplication.Syncronization.Mock;
using log4net;

namespace ChatApplication.Syncronization.Out.Offsite.Comands
{
    public class Create<TEntity> : ICommand where TEntity : class
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly TEntity _entity;

        public Create(TEntity entity)
        {
            _entity = entity;
        }

        public void Execute()
        {
            try
            {
                _log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; type=[\"OffSite\"]");
                MockSync.Sync();
            }
            catch (TimeoutException ex)
            {
                _log.Error(
                    $"function=[{MethodBase.GetCurrentMethod().Name}]; exception=[{ex.GetBaseException().Message}]; message=[{JsonLogging.Serialize(_entity)}];");
                Retry();
            }
            catch (Exception ex)
            {
                _log.Error(
                    $"function=[{MethodBase.GetCurrentMethod().Name}]; exception=[{ex.GetBaseException().Message}]; message=[{JsonLogging.Serialize(_entity)}];");
                Undo();
            }
        }

        public void Retry()
        {
            _log.Warn(
                $"function=[{MethodBase.GetCurrentMethod().Name}]; warn=[\"Retrying\"]; message=[{JsonLogging.Serialize(_entity)}];");
            Execute();
        }

        public void Undo()
        {
            _log.Error(
                $"function=[{MethodBase.GetCurrentMethod().Name}]; error=[\"Failed to Create Message\"]; message=[{JsonLogging.Serialize(_entity)}]");
        }
    }
}
