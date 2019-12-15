using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StandardTemplate.utils
{
    class WordHelper
    {
        private _Application wordApp = null;
        private _Document wordDoc = null;
        object miss = System.Reflection.Missing.Value;
        public _Application Application
        {
            get
            {
                return wordApp;
            }
            set
            {
                wordApp = value;
            }
        }
        public _Document Document
        {
            get
            {
                return wordDoc;
            }
            set
            {
                wordDoc = value;
            }
        }

        //通过模板创建新文档
        public void CreateNewDocument(string filePath)
        {
            killWinWordProcess();
            wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            wordApp.Visible = false;
            object missing = System.Reflection.Missing.Value;
            object templateName = filePath;
            wordDoc = wordApp.Documents.Open(ref templateName, ref missing,
              ref missing, ref missing, ref missing, ref missing, ref missing,
              ref missing, ref missing, ref missing, ref missing, ref missing,
              ref missing, ref missing, ref missing, ref missing);
        }
        //保存新文件
        public void SaveDocument(string filePath)
        {
            object fileName = filePath;
            object format = WdSaveFormat.wdFormatDocument;//保存格式
                                                          //object miss =System.Reflection.Missing.Value;
           try
           {
                wordDoc.SaveAs(ref fileName, ref miss, ref miss,
                                 ref miss, ref miss, ref miss, ref miss,
                                 ref miss, ref miss, ref miss, ref miss,
                                 ref miss, ref miss, ref miss, ref miss,
                                 ref miss);
            }
           catch (System.Exception ex)
           {
                MessageBox.Show(ex.Message);
           }
            //关闭wordDoc，wordApp对象
            object SaveChanges = WdSaveOptions.wdSaveChanges;
            object OriginalFormat = WdOriginalFormat.wdOriginalDocumentFormat;
            object RouteDocument = false;
            wordDoc.Close(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
            wordApp.Quit(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
        }
        //在书签处插入值
        public bool InserttextValue(Dictionary<string, string> dict)
        {
            object replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
            foreach (string key in dict.Keys)
            {
                wordApp.Selection.Find.Replacement.ClearFormatting();
                wordApp.Selection.Find.ClearFormatting();
                wordApp.Selection.Find.Text = key;//需要被替换的文本
                wordApp.Selection.Find.Replacement.Text = dict[key];//替换文本 

                //执行替换操作
                wordApp.Selection.Find.Execute(ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref replace, ref miss, ref miss, ref miss, ref miss);
            }
            return true;
        }
        
        public bool Insertpicture(Dictionary<string, string> dictval)
        {
            object replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceOne;
            foreach (string key in dictval.Keys)
            {
                wordApp.Selection.Find.Replacement.ClearFormatting();
                wordApp.Selection.Find.ClearFormatting();
                wordApp.Selection.Find.Text = key;//需要被替换的文本
                object testx = key;
                //wordApp.Selection.Find.Replacement.Text = dict[key];//替换文本 
                object MissingValue = Type.Missing;
                //执行替换操作
                bool ts = wordApp.Selection.Find.Execute(ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue, ref MissingValue, "", ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue);
                object Anchor = wordApp.Selection.Range;
                //oDoc.InlineShapes.AddPicture(picfileName, ref LinkToFile, ref SaveWithDocument, ref Anchor);
                object left = 0;
                object top = 0;
                object width = 400;
                object height = 200;
                Anchor = wordApp.Selection.Range;
                wordDoc.InlineShapes.AddPicture(dictval[key], false, ref miss, ref Anchor);
                wordApp.Selection.Find.Replacement.ClearFormatting();
                wordApp.Selection.Find.ClearFormatting();
                //wordDoc.Shapes.AddPicture(dictval[key], false, true, ref left, ref top, ref width, ref height, ref Anchor);
            }
            return true;
        }
        public bool Inserttable(Dictionary<string, string> dictval, double[] numtestpre, double[] 量杯读数, int[] testtime, double[] leakage)
        {
            object replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
            foreach (string key in dictval.Keys)
            {
                wordApp.Selection.Find.Replacement.ClearFormatting();
                wordApp.Selection.Find.ClearFormatting();
                wordApp.Selection.Find.Text = key;//需要被替换的文本
                object MissingValue = Type.Missing;
                //执行替换操作
                bool ts = wordApp.Selection.Find.Execute(ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue, ref MissingValue, "", ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue, ref MissingValue);
                Range Anchor = wordApp.Selection.Range;
                Microsoft.Office.Interop.Word.Table newTable = wordDoc.Tables.AddOld(Anchor, 1 + numtestpre.Count(), 5);

                newTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleThickThinLargeGap;
                newTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                newTable.Columns[1].Width = 64f;
                newTable.Columns[2].Width = 90f;
                newTable.Columns[3].Width = 80f;
                newTable.Columns[4].Width = 80f;
                newTable.Columns[5].Width = 93f;

                newTable.Cell(1, 1).Range.Text = "实验次数";
                newTable.Cell(1, 1).Range.Bold = 2;//设置单元格中字体为粗体
                newTable.Cell(1, 2).Range.Text = "测试压力(MPa)";
                newTable.Cell(1, 2).Range.Bold = 2;//设置单元格中字体为粗体
                newTable.Cell(1, 3).Range.Text = "量杯读数(ml)";
                newTable.Cell(1, 3).Range.Bold = 2;//设置单元格中字体为粗体
                newTable.Cell(1, 4).Range.Text = "测试时间(s)";
                newTable.Cell(1, 4).Range.Bold = 2;//设置单元格中字体为粗体
                newTable.Cell(1, 5).Range.Text = "泄漏量(mL/min)";
                newTable.Cell(1, 5).Range.Bold = 2;//设置单元格中字体为粗体
                wordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
                wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中

                for (int i = 2; i <= 1 + numtestpre.Count(); i++)
                {
                    newTable.Cell(i, 1).Range.Text = (i - 1).ToString();
                    newTable.Cell(i, 1).Range.Bold = 2;//设置单元格中字体为粗体
                    newTable.Cell(i, 2).Range.Text = numtestpre[i - 2].ToString();
                    newTable.Cell(i, 2).Range.Bold = 2;//设置单元格中字体为粗体
                    newTable.Cell(i, 3).Range.Text = 量杯读数[i - 2].ToString();
                    newTable.Cell(i, 3).Range.Bold = 2;//设置单元格中字体为粗体
                    newTable.Cell(i, 4).Range.Text = testtime[i - 2].ToString();
                    newTable.Cell(i, 4).Range.Bold = 2;//设置单元格中字体为粗体
                    newTable.Cell(i, 5).Range.Text = leakage[i - 2].ToString();
                    newTable.Cell(i, 5).Range.Bold = 2;//设置单元格中字体为粗体                    
                }

                wordApp.Selection.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//垂直居中
                wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中

            }
            return true;
        }

        public void killWinWordProcess()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("WINWORD");
            foreach (System.Diagnostics.Process process in processes)
            {
                bool b = process.MainWindowTitle == "";
                if (process.MainWindowTitle == "")
                {
                    process.Kill();
                }
            }
        }
    }
}
