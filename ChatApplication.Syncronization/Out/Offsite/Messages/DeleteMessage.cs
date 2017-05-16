using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Logging.JsonNet;
using ChatApplication.Syncronization.Contracts.Commands;
using ChatApplication.Syncronization.Mock;
using log4net;

namespace ChatApplication.Syncronization.Out.Offsite
{
    public class DeleteMessage : ICommand
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MessageRecord _message;

        public DeleteMessage(MessageRecord messageRecord)
        {
            _message = messageRecord;
        }

        public void Execute()
        {
            try
            {
                MockSync.Sync();
            }
            catch (TimeoutException ex)
            {
                _log.Error($"function=[{MethodBase.GetCurrentMethod().Name}]; exception=[{ex.GetBaseException().Message}]; message=[{JsonLogging.Serialize(_message)}];");
                Retry();
            }
            catch (Exception ex)
            {
                _log.Error($"function=[{MethodBase.GetCurrentMethod().Name}]; exception=[{ex.GetBaseException().Message}]; message=[{JsonLogging.Serialize(_message)}];");
                Undo();
            }
        }

        public void Retry()
        {
            _log.Warn($"function=[{MethodBase.GetCurrentMethod().Name}]; warn=[\"Retrying\"]; message=[{JsonLogging.Serialize(_message)}];");
            Execute();
        }

        public void Undo()
        {
            _log.Error($"function=[{MethodBase.GetCurrentMethod().Name}]; error=[\"Failed to Create Message\"]; message=[{JsonLogging.Serialize(_message)}]");
        }
    }
}
