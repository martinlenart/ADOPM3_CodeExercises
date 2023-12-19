using System;
namespace ADOPM3_02_18a
{
	public class csFactoryMontor
	{
		public Action<int> AlarmStatus { get; set; } = null;

		public int StatusCode { get; set; }
        public bool CheckStatus()
		{
			//kod to check the factory floor if any machine signals an error
			//..
            AlarmStatus?.Invoke(StatusCode);

			return StatusCode == 0;
		}
		public csFactoryMontor()
		{
		}
	}
}

