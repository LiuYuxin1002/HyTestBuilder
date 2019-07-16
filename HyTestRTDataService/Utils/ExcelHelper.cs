using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace HyTestRTDataService.Utils
{
    public class ExcelHelper
    {
        /// <summary>
        /// 手动选择Excel，导入为DataTable
        /// </summary>
        /// <returns>点击取消返回null</returns>
        public static DataTable SelectExcelToDataTable()
        {
            
            DataTable dt = null;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Filter = "Excel文件|*.xlsx";
                openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return null;
                ExcelHelper excelHelper = new ExcelHelper();
                dt = PathToDataTable(openFileDialog.FileName);    //正式转datatable
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }

            return dt;
        }

        private static DataTable PathToDataTable(string fileName)
        {
            string sheetName = null;
            bool isFirstRowColumn = true;

            IWorkbook workbook = null;
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                //判断单元数据类型
                                String cellValue = null;
                                if (cell.CellType == CellType.Numeric)
                                {
                                    cellValue = cell.NumericCellValue.ToString();
                                }
                                else if (cell.CellType == CellType.String)
                                {
                                    cellValue = cell.StringCellValue;
                                }

                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        bool isnull = true;
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                            if (!string.IsNullOrEmpty(dataRow[j].ToString().Trim()))
                            {
                                isnull = false;
                            }
                        }
                        if (!isnull)
                        {
                            data.Rows.Add(dataRow);
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //TODO: 保存的文件无法使用
        /// <summary>
        /// 将DataTable转为Excel文件并保存到所选路径
        /// </summary>
        public static void DataTableToExcel(DataTable dt) 
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Filter = "Excel文件|*.xls";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string FileName = dialog.FileName;
            if (File.Exists(FileName))                                //存在则删除
            {
                //log.Info("文件已存在，将被覆盖");
                System.IO.File.Delete(FileName);
            }
            System.IO.FileStream objFileStream;
            System.IO.StreamWriter objStreamWriter;
            string strLine = "";
            objFileStream = new System.IO.FileStream(FileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            objStreamWriter = new System.IO.StreamWriter(objFileStream, Encoding.Unicode);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                strLine = strLine + dt.Columns[i].Caption.ToString() + Convert.ToChar(9);      //写列标题
            }
            objStreamWriter.WriteLine(strLine);
            strLine = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    try
                    {
                        if (dt.Rows[i].ItemArray[j] == null)
                            strLine = strLine + " " + Convert.ToChar(9);                        //写内容
                        else
                        {
                            string rowstr = "";
                            rowstr = dt.Rows[i].ItemArray[j].ToString();
                            if (rowstr.IndexOf("\r\n") > 0)
                                rowstr = rowstr.Replace("\r\n", " ");
                            if (rowstr.IndexOf("\t") > 0)
                                rowstr = rowstr.Replace("\t", " ");
                            strLine = strLine + rowstr + Convert.ToChar(9);
                        }
                    }
                    catch (IndexOutOfRangeException e)//防止超出范围
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";
            }
            /**关闭其他流文件**/
            objStreamWriter.Close();
            objFileStream.Close();
        }
    }
}
