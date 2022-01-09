using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamAnalytics.System.Constants
{
  public class NumericalStreamTypes
  {
    public static readonly Guid Continuous = new Guid("000D02BA-4C5B-4B60-BB35-FEA9C7BA0BFA");
    public static readonly Guid Discreet = new Guid("EEC98B7B-2A18-4C3E-A366-BBE728F9E058");
    public static readonly Guid Threshold = new Guid("0A8651CD-EE30-4D50-8C4B-3004BE273D3A");
  }
}
