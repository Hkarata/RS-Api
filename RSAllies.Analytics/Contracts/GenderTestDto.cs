using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAllies.Analytics.Contracts
{
    public class GenderTestDto
    {
        public string GenderType { get; set; } = string.Empty;
        public int Passed { get; set; }
        public int Failed { get; set; }
    }
}
