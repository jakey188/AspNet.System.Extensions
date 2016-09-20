using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApplication1
{
    public  class TableToListTest
    {
        private Stopwatch sw = new Stopwatch();
        public  void Test()
        {
            int row = 2;
            var dt = CrateDataTable(row);
            ReflectionToList(dt,row);
            EmitToList(dt,row);

            int row1 = 200;
            var dt1 = CrateDataTable(row1);
            ReflectionToList(dt1,row1);
            EmitToList(dt1,row1);
        }

        void EmitToList(DataTable dt,int row)
        {
            sw.Restart();
            var list = dt.ToEntities<User>(true);
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
            Console.WriteLine("Emit转换耗时【row"+ row + "】" + time);
        }

        void ReflectionToList(DataTable dt,int row)
        {
            sw.Restart();
            var list = dt.ToEntities<User>(false);
            sw.Stop();
            var time = sw.ElapsedMilliseconds;
            Console.WriteLine("反射转换耗时【row" + row + "】" + time);
        }


        private DataTable CrateDataTable(int rows)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name1");
            dt.Columns.Add("Name2");
            dt.Columns.Add("Name3");
            dt.Columns.Add("Name4");
            dt.Columns.Add("Name5");
            dt.Columns.Add("Name6");
            dt.Columns.Add("Name7");
            dt.Columns.Add("Name8");
            dt.Columns.Add("Name9");
            dt.Columns.Add("Name10");

            for (var i = 0; i < rows; i++)
            {
                var row = dt.NewRow();
                row["Id"] = null;
                row["Name1"] = "数据"+i;
                row["Name2"] = "数据" + i;
                row["Name3"] = "";
                row["Name4"] = "数据" + i;
                row["Name5"] = "数据" + i;
                row["Name6"] = "数据" + i;
                row["Name7"] = "数据" + i;
                row["Name8"] = "数据" + i;
                row["Name9"] = "数据" + i;
                row["Name10"] = "数据" + i;
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
