using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class MasterSqlupgrade
    {
        public int IdSql { get; set; }
        public double VersionApi { get; set; }
        public string Sql { get; set; }
    }
}
