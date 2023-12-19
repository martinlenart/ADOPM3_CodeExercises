using System;
namespace ADOPM3_02_18a
{
	public class csFactoryMontor
	{
		public event EventHandler<int> AlarmStatus = null;

		public int StatusCode { get; set; }
        public bool CheckStatus()
		{
			//kod to check the factory floor if any machine signals an error
			//..
			AlarmStatus?.Invoke(this, StatusCode);
			return StatusCode == 0;
		}
		public csFactoryMontor()
		{
		}
	}
}

