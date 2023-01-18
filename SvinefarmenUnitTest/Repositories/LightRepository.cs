using Microsoft.EntityFrameworkCore;
using Npgsql;
using SvinefarmenUnitTest.Interface;
using SvinefarmenUnitTest.Model;

namespace SvinefarmenUnitTest.Repository
{
    public class LightRepository : ILight
	{
		private readonly ThePigFarmContext _thePigFarmContext;

        public LightRepository(ThePigFarmContext thePigFarmContext)
        {
			_thePigFarmContext= thePigFarmContext;
        }

		//For this to work we need to kreate a primary key in the DB -
		//that should actually have been done no matter what
		public async Task<Lightlog> CreateLightLog(LightLogRequest light)
		{
			Lightlog newLightLog = new()
			{
				Leveloflight = light.Leveloflight,
				Timeoflog = DateTime.UtcNow,
				Lightlevelinstable = light.Lightlevelinstable
			};
			_thePigFarmContext.Lightlogs.Add(newLightLog);
			await _thePigFarmContext.SaveChangesAsync();
			return newLightLog;
		}

		//Returns a 404 - research why
		public async Task<Lightlog> DeleteLightLog(int logId)
		{
            Lightlog foundLightModel = await _thePigFarmContext.Lightlogs.FirstOrDefaultAsync(x => x.Id == logId);
			if (foundLightModel != null)
			{
                _thePigFarmContext.Lightlogs.Remove(foundLightModel);
				await _thePigFarmContext.SaveChangesAsync();
			}
			return foundLightModel;
		}

		//Works PERFECT
		public async Task<List<Lightlog>> GetAllLightLogs()
		{
			return await _thePigFarmContext.Lightlogs.ToListAsync();
		}

        //Works PERFECT
        public async Task<Lightlog> GetLevelOfLight()
		{
			return await _thePigFarmContext.Lightlogs.OrderByDescending(x => x.Timeoflog).FirstAsync();
			
		}

		public async Task<List<Lightlog>> GetLightLogByTime(DateTime startTime, DateTime endTime)
		{
			return await _thePigFarmContext.Lightlogs.Where(x => x.Timeoflog > startTime && x.Timeoflog < endTime).ToListAsync();
		}

		public Task<Lightlog> SetLevelOfLight(Lightlog light)
		{
			throw new NotImplementedException();
		}

		//Does not brake, and it does return Something, but not what was intended
		public async Task<Lightlog> UpdateLightLog(Lightlog logEntry, int logId)
		{
            Lightlog updateLog = await _thePigFarmContext.Lightlogs.FirstOrDefaultAsync(x => x.Id == logId);
			if (updateLog is not null) {
				updateLog.Leveloflight = logEntry.Leveloflight;
				updateLog.Timeoflog= logEntry.Timeoflog;
				updateLog.Lightlevelinstable = logEntry.Lightlevelinstable;
				await _thePigFarmContext.SaveChangesAsync();
			}
			return updateLog;
		}

		//gives response  status 400
		public async Task<Lightlog> UpdateLightStatus(Lightlog light)
		{
            Lightlog newestLog = await _thePigFarmContext.Lightlogs.OrderByDescending(x => x.Timeoflog).FirstAsync();
			if (newestLog is not null) {
				newestLog.Leveloflight = light.Leveloflight;
				newestLog.Timeoflog = light.Timeoflog;
				newestLog.Lightlevelinstable = light.Lightlevelinstable;
				await _thePigFarmContext.SaveChangesAsync();
			}
			return newestLog;
		}
	}
}
