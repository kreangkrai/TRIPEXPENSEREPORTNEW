using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTREPORT
{
    public class DataModel
    {
        public string trip {  get; set; }
        public string emp_id { get; set; }
        public List<Data> datas { get; set; }
    }

    public class Data {
        public DateTime start {  get; set; }
        public DateTime end { get; set; }
        public string zipcode { get; set; }
        public int province { get; set; }
        public int a_1_4 { get; set; }
        public int a_4_8 { get; set; }
        public int a_8 { get; set; }
    }
}
