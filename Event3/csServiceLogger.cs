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
		public csServiceLogger(csFactoryMontor fm)
		{
			fm.AlarmStatus += LogACode;
		}

		void LogACode(int StatusCode)
		{
			CodeLog.Add(new csLogEntry()
				{
					StatusCode = StatusCode,
					Message = (StatusCode) switch
					{
						0 => $"All Good",
						1 => $"Easy Level",
						2 => $"Moderate Level",
						3 => $"Critical Level",
						_ => $"Unknown Level"
					}
				}
			);
		}

	}
}

