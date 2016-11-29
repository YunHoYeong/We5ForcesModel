using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace We5ForcesModel
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }
        char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private void bunifuImageButton1_Click(object sender, EventArgs e) // Excel file 클릭했을시
        {
            WriteExcelData();
        }
        private void WriteExcelData()
        {
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Title = "Save REPORT File";
            saveFile.DefaultExt = "xlsx";
            saveFile.Filter = "Excel (*.xlsx)|*.xlsx";
            saveFile.FilterIndex = 0;
            saveFile.RestoreDirectory = true;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application excel = null;
                Microsoft.Office.Interop.Excel.Workbook wb = null;
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range Range2 = null;

                object missing = Type.Missing;

                Random Rnd = new Random();
                int RndNum = Rnd.Next();

                byte[] Template = Properties.Resources.Report;
                
                File.WriteAllBytes(Path.GetTempPath() + RndNum + ".xlsx", Template);

                try
                {
                    excel = new Microsoft.Office.Interop.Excel.Application();
                    excel.DisplayAlerts = false;

                    wb = excel.Workbooks.Open(Path.GetTempPath() + RndNum + ".xlsx",
                      missing, false, missing, missing, missing, true, missing, missing, true, missing, missing, missing, missing, missing);

                    // 원천기술 분석
                    ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets.get_Item("Page 5");
                    ws.PageSetup.Zoom = false;

                    int cntTechnologyLevel = mainFrm.CriticalTechnology1.Count;
                    
                    
                    for (int i = 0; i < cntTechnologyLevel; i++)
                    {
                        string[] temp = new string[] { mainFrm.CriticalTechnology1[i],
                                                        string.Empty,
                                                        mainFrm.CriticalTechnology2[i] + "%",
                                                        mainFrm.CriticalTechnology3[i] + "%",
                                                        mainFrm.CriticalTechnology4[i],
                                                        mainFrm.CriticalTechnology5[i],
                                                        mainFrm.CriticalTechnology6[i],
                                                        mainFrm.CriticalTechnology7[i],
                                                        mainFrm.CriticalTechnology8[i],
                                                        mainFrm.CriticalTechnology9[i]
                                                        };
                        Range2 = ws.Range["B" + (15 + i), "K" + (15 + i)];
                        Range2.Value = temp;
                    }

                    // 기술 수준 파악은 최대가 4개임
                    if(cntTechnologyLevel < 4)
                    {
                        for(int i = 4; i > cntTechnologyLevel; i--)
                        {
                            Range2 = ws.Range["B" + (15 + i - 1), "K" + (15 + i - 1)];
                            Range2.EntireRow.Delete();
                        }
                    }

                    int cntTechnology = mainFrm.OrigrinalTechnology1.Count;
                    // 원천기술 식별은 5개가 Default이며, 5개가 초과할 경우에는 Row를 Insert하고
                    if (cntTechnology > 5)
                    {
                        for(int i = 0; i < cntTechnology - 5; i++)
                        {
                            Range2 = ws.Range["B6", "K6"];
                            Range2.Insert();
                        }
                    }
                    else if(cntTechnology < 5) // 5개 보다 작을때는 그만큼 Row를 삭제해야함.
                    {
                        for (int i = 5; i > cntTechnology; i--)
                        {
                            Range2 = ws.Range["B" + (5 + i - 1), "K" + (5 + i - 1)];
                            Range2.EntireRow.Delete();
                        }
                    }
                    for(int i = 0; i < cntTechnology; i++)
                    {
                        string[] temp = new string[] { mainFrm.OrigrinalTechnology1[i], mainFrm.OrigrinalTechnology2[i] };

                        Range2 = ws.Range["B" + (5 + i), "K" + (5 + i)];
                        Range2.Value = temp;
                    }

                    wb.SaveAs(saveFile.FileName, missing, missing, missing, missing, missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);
                    wb.Close(false, missing, missing);
                    excel.Quit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());

                    wb.Close(false, missing, missing);
                    excel.Quit();

                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wb);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ws);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(Range2);

                    excel = null;
                    wb = null;
                    ws = null;
                    Range2 = null;
                }
            }
        }

        private void Report_Load(object sender, EventArgs e)
        {

        }
    }
}
