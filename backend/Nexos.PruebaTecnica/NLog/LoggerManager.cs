using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using NLog;

namespace Nexos.PruebaTecnica.NLog
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogError(object obj)
        {
            var jsonStringResult = JsonConvert.SerializeObject(obj);
            logger.Error(string.Format("[Data]: {0}", jsonStringResult));
        }


        public void LogInfo(ModelStateDictionary modelState)
        {
            foreach (var item in modelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    logger.Info(item.Key + " - " + error.ErrorMessage);
                }
            }
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
