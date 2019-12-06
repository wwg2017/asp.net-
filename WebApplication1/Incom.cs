using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Incom
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Income { get; set; }
        public int Type { get; set; }
    }
    public class IncomView
    {
        public string Name { get; set; }
        public string Income { get; set; }
        public string IncomeA { get; set; }
        public string IncomeB { get; set; }
        public string IncomeC { get; set; }
    }
}