using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace We5ForcesModel
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Bunifu.Framework.License.Authenticate("yhy900211@yonsei.ac.kr", "WpcUQDUwLGaf0xSkbqkUlCZodlOTelD/2KY8EyBCp7s=");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(new mainFrm());
         //  Application.Run(new BublleChart1());
        }
    }
}
