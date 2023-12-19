using System;
namespace ADOPM3_02_18a
{
	public class csLogEntry
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public override string ToString() => $"{StatusCode}, {Message}";
    }
	public class csServiceLogger
	{
		public List<csLogEntry> CodeLog = new List<csLogEntry>();


		public csServiceLogger(csFactoryAlarm fa)
		{
			fa.AlarmDetail += LogACode;
		}

		void LogACode(object sender, (int StatusCode, string Message) e)
		{
			CodeLog.Add(new csLogEntry()
				{
					StatusCode = e.StatusCode,
					Message = e.Message
				}
			);
		}

	}
}

