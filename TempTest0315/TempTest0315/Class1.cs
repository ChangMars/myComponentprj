using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempTest0315
{
    class Class1
    {
     
        private StringBuilder sb;
        public string SaveData(string crtno,string actno,string cardte,string upop,string name)
        {
            sb = new StringBuilder();
            sb.Append(" insert into test0315 (crtno,actno,cardte,name,upop,upddate) ");
            sb.Append(" values ");
            sb.Append(" ( ");
            sb.Append(" '"+crtno+ "','" + actno + "','" + cardte + "', ");
            sb.Append(" '" + name + "','" + upop + "','" + ChDte() + "' ");
            sb.Append(" ) ");
            return sb.ToString() ;
        }
        public string SaveData0316(string crtno,string price)
        {
            sb = new StringBuilder();
            sb.Append(" insert into test0316 (crtno,price) ");
            sb.Append(" values ");
            sb.Append(" ( ");
            sb.Append(" '" + crtno + "','"+ price + "' ");
            sb.Append(" ) ");
            return sb.ToString();
        }
        public string GetTest0315()
        {
            return "select * from Test0315";
        }
        public string ChDte()
        {
            //西元年 轉 民國年
            string sampleDate = DateTime.Now.ToString("yyyy-MM-dd");
            string [] date = sampleDate.Split('-');
            int Intyyyy =Convert.ToInt32(date[0])-1911;      
            return Intyyyy.ToString()+ date[1]+ date[2];
        }
    }
}
