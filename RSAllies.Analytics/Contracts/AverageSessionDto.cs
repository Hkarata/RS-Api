using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAllies.Analytics.Contracts
{
    public class AverageSessionDto
    {
        public string VenueName { get; set; } = string.Empty;
        public double AverageSessions { get; set; }
    }
}
