using Microsoft.AspNetCore.Mvc;
using SvinefarmenUnitTest.Model;
using SvinefarmenUnitTest.Interface;

namespace SvinefarmenUnitTest.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LightController : Controller
    {
        ILight _light;

        public LightController(ILight light)
        {
            _light= light;
        }
		[HttpPost]
		public async Task<IActionResult> CreateLightLog([FromBody] Model.LightLogRequest logEntry)
		{
			try
			{
                //Change variable name
				Lightlog createdAuthor = await _light.CreateLightLog(logEntry);
				if (createdAuthor is not null)
				{
					return Ok(createdAuthor);
				}
				return NotFound();

			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		

        [HttpDelete]
        public async Task<IActionResult> DeleteLightLog([FromRoute] int logId)
        {
            try
            {
                Lightlog deletedLightLog = await _light.DeleteLightLog(logId);
                if (deletedLightLog is not null)
                {
                    return Ok(deletedLightLog);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

		

        [HttpGet]
        public async Task<IActionResult> GetAllLightLogs()
        {
            try
            {
                List<Lightlog> lightLogList = await _light.GetAllLightLogs();
                if (lightLogList.Count > 0)
                {
                    return Ok(lightLogList);
                }
                return NoContent();

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

		[HttpGet("GetLevelOfLight")]
		public async Task<IActionResult> GetLevelOfLight()
		{
			try
			{
				Lightlog levelOfLight = await _light.GetLevelOfLight();
				if (levelOfLight is not null)
				{
					return Ok(levelOfLight);
				}
				return NotFound();

			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
        // Der kan kun være et argument med "fromBody", den her metode er ikke kritisk, så du kan springe den over.
        // men ellers ville man kunne lave en model der både indeholder startTime og endTime, og så bruge den i body.
		//[HttpGet]
  //      public async Task<IActionResult> GetLightLogByTime([FromBody] DateTime startTime, [FromBody] DateTime endTime)
  //      {
  //          try
  //          {
  //              List<LightModel> lightLogs= await _light.GetLightLogByTime(startTime,endTime);
  //              if (lightLogs is null)
  //              {
  //                  return NotFound();
  //              }
  //              if (lightLogs.Count > 0)
  //              {
  //                  return Ok(lightLogs);
  //              }
  //              return NoContent();

  //          }
  //          catch (Exception ex)
  //          {
  //              return Problem(ex.Message);
  //              throw;
  //          }
  //      }

        [HttpPut("logId")]
        public async Task<IActionResult> UpdateLightLog([FromBody] Lightlog lightLog, [FromRoute] int logId)
        {
            if (logId <= 0 || lightLog is null)
            {
                return BadRequest();
            }
            try
            {
                Lightlog updatedLightLog = await _light.UpdateLightLog(lightLog, logId);
                if (updatedLightLog is not null)
                {
                    return Ok(updatedLightLog);
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateLightStatus([FromBody] Lightlog logEntry)
        {
            if (logEntry is null)
            {
                return BadRequest();
            }
            try
            {
                Lightlog updatedLightLog = await _light.UpdateLightStatus(logEntry);
                if (updatedLightLog is not null)
                {
                    return Ok(updatedLightLog);
                }
                return NotFound();


			}
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

	}
}
