using SvinefarmenUnitTest.Model;

namespace SvinefarmenUnitTest.Interface
{
    public interface ILight
    {
        Task<Lightlog> CreateLightLog(LightLogRequest light);
        Task<Lightlog> GetLevelOfLight();
        Task<List<Lightlog>> GetAllLightLogs();
        Task<List<Lightlog>> GetLightLogByTime(DateTime startTime, DateTime endTime);
        Task<Lightlog> SetLevelOfLight(Lightlog light);
        Task<Lightlog> UpdateLightLog(Lightlog logEntry, int logId);
        Task<Lightlog> UpdateLightStatus(Lightlog light);
        Task<Lightlog> DeleteLightLog(int logId);

    }
}
