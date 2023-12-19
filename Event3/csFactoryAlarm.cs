using System;
namespace ADOPM3_02_18a
{
	public class csFactoryAlarm
	{
        public event EventHandler<(int, string)> AlarmDetail = null;


        public csFactoryAlarm(csFactoryMontor fm)
		{
            fm.AlarmStatus += AlarmResponseSMS;

        }

        //The functions that execution is delegated to
        public void AlarmResponseSMS(object sender, int priority)
        {
            //Kod till att skicka SMS
            Console.WriteLine(theResponse("SMS Alarm", priority));

            //Fire a new event
            AlarmDetail?.Invoke(this, (priority, theResponse("SMS Alarm", priority)));
        }

        static string theResponse(string Company, int priority) =>
            (priority) switch
            {
                0 => $"{Company} All Good",
                1 => $"{Company} Easy Level",
                2 => $"{Company} Moderate Level",
                3 => $"{Company} Critical Level",
                _ => $"{Company} Unknown Level"
            };
    }
}

