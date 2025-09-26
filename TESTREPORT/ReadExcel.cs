using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using OfficeOpenXml;
namespace TESTREPORT
{
    public static class ReadExcel
    {
        public static List<DataModel> Read()
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            List<DataModel> datasmodel = new List<DataModel>();
            // ตัวอย่างการหา path ของโฟลเดอร์ปัจจุบัน
            string currentDirectory = Directory.GetCurrentDirectory();

            string excelFolder = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", ".."));
            // สร้าง path ไปยังไฟล์ Excel ในโฟลเดอร์ปัจจุบัน
            string filePath = Path.Combine(excelFolder, "excel\\testcases.xlsx");

            Console.WriteLine($"Current Directory: {currentDirectory}");
            Console.WriteLine($"Excel File Path: {filePath}");

            // ตรวจสอบว่าไฟล์มีอยู่จริง
            if (!File.Exists(filePath))
            {
                Console.WriteLine("ไม่พบไฟล์ Excel ในโฟลเดอร์ปัจจุบัน!");
                return null;
            }

            // อ่านไฟล์ Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                for ( int i=0; i < package.Workbook.Worksheets.Count - 1;i++)
                {
                    //Console.WriteLine($"กำลังอ่าน Worksheet: {package.Workbook.Worksheets[i].Name}");

                    int rowCount = package.Workbook.Worksheets[i].Dimension?.Rows ?? 0;
                    int colCount = package.Workbook.Worksheets[i].Dimension?.Columns ?? 0;

                    if (rowCount == 0 || colCount == 0)
                    {
                        Console.WriteLine("Worksheet นี้ว่างเปล่า!");
                        continue;
                    }
                    DataModel datamodel = new DataModel();
                    List<Data> datas = new List<Data>();
                    for (int row = 2; row <= rowCount; row++)
                    {
                        Data data = new Data();
                        
                        for (int col = 1; col <= colCount; col++)
                        {

                            var cellValue = package.Workbook.Worksheets[i].Cells[row, col].Value?.ToString() ?? "";
                            if (row == 2 && col == 13)
                            {
                                datamodel.trip = cellValue;
                            }

                            if (row == 2 && col == 14)
                            {
                                datamodel.emp_id = cellValue;
                            }

                            if (col == 2)
                            {
                                data.start = Convert.ToDateTime(cellValue);
                            }
                            if (col == 3)
                            {
                                data.end = Convert.ToDateTime(cellValue);
                            }
                            if (col == 5)
                            {
                                data.zipcode = cellValue;
                            }
                            if (col == 8)
                            {
                                data.province = Int32.Parse(cellValue);
                            }
                            if (col == 9)
                            {
                                data.a_1_4 = Int32.Parse(cellValue);
                            }
                            if (col == 10)
                            {
                                data.a_4_8 = Int32.Parse(cellValue);
                            }
                            if (col == 11)
                            {
                                data.a_8 = Int32.Parse(cellValue);
                            }
                        }
                        datas.Add(data);
                    }
                    datamodel.datas = datas;
                    datasmodel.Add(datamodel);
                }
               
            }
            return datasmodel;
        }
    }
}