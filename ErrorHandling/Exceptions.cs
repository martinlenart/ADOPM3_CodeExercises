using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandling
{
    public enum ErrorSeverity { managable, fatal }
    public class ExplosionException : Exception
    {
        public int ButtonPressed { get; private set; }
        public ErrorSeverity Severity { get; private set; }

        public ExplosionException(string message, int buttonPressed, ErrorSeverity severity) : base(message)
        {
            ButtonPressed = buttonPressed;
            Severity = severity;
        }
    }
}
