using System;
namespace ADOPM3_02_18a
{
	public class csServiceLogger
	{
		public List<int> CodeLog = new List<int>();
		public csServiceLogger(csFactoryMontor fm)
		{
			fm.AlarmStatus += LogACode;
		}

		void LogACode(int StatusCode)
		{
			CodeLog.Add(StatusCode);
		}

	}
}

