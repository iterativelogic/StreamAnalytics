using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamAnalytics.System.Constants
{
  public class StringStreamType
  {
    public static Guid ToolNumber => Guid.Parse("2456321B-BF5B-49E6-AA6B-EC36244311D9");
    public static Guid JobNumber => Guid.Parse("DB626AA1-00CA-43FE-BA6D-312E597E138F");
    public static Guid PartNumber => Guid.Parse("325FED61-A8CA-4F4D-89DD-255D3110DCD3");
    public static Guid ProcessCode => Guid.Parse("BE96163F-8EB5-40CC-A2A8-BDBC996D6055");
    public static Guid LotNumber => Guid.Parse("C565D2FD-BA97-4F9C-B4A9-B35E6DF2ACFE");
    public static Guid BatchNumber => Guid.Parse("E6C2567B-830B-4534-AA1A-2B0950AA052B");
    public static Guid OperationNumber => Guid.Parse("529C88CA-B8B5-4FF4-850C-9D06F9988C50");
    public static Guid OperationCode => Guid.Parse("ED6A495C-30A9-406F-9F09-3DBD5BE5DBC9");
    public static Guid Other => Guid.Parse("BD914966-673F-4959-9DA5-A6684BD1869E");
  }
}
