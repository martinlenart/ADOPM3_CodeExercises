using System;
namespace ADOPM3_02_18a
{
	public class csServiceLogger
	{
		public List<(int, string)> CodeLog = new List<(int, string)>();
		public csServiceLogger(csFactoryMontor fm)
		{
			fm.AlarmStatus += LogACode;
		}

		void LogACode(int StatusCode)
		{
			CodeLog.Add((StatusCode, (StatusCode) switch
			{
				0 => $"All Good",
				1 => $"Easy Level",
				2 => $"Moderate Level",
				3 => $"Critical Level",
				_ => $"Unknown Level"
			}));
		}

	}
}

