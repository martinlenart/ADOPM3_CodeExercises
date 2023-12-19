using System;
namespace ADOPM3_02_18a
{
	public class csServiceMan
	{
		public csServiceMan(csFactoryMontor fm)
		{
            fm.AlarmStatus += SendServiceMan;
        }

        public void SendServiceMan(object sender, int priority)
        {
            if (priority >= 3)
            {
                Console.WriteLine("Service man sent");
            }
            else
            {
                Console.WriteLine("No need for service, relax");
            }
        }

    }
}

