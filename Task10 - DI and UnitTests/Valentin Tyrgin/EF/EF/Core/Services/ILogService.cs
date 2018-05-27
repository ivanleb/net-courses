namespace EF.Core.Services
{
    public interface ILogService
    {
        void Info(string msg);
        void Error(string msg);
        void Warn(string msg);
    }
}