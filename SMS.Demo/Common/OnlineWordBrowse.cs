using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Word = Microsoft.Office.Interop.Word;

namespace SMS.Demo.Common
{
    public class OnlineWordBrowse
    {
        /// <summary>
        /// word转成html
        /// </summary>
        /// <param name="wordFileName">word的路径+文件名</param>
        /// <returns>html的路径+文件名</returns>
        public static string WordToHtml(object wordFileName,object serviceName)
        {
            //在此处放置用户代码以初始化页面
            Word.ApplicationClass word = new Word.ApplicationClass();
            Type wordType = word.GetType();
            Word.Documents docs = word.Documents;
            //打开文件
            Type docsType = docs.GetType();
            Word.Document doc = (Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[]
            { wordFileName, true, true });
            //转换格式，另存为
            Type docType = doc.GetType();
            string wordSaveFileName = wordFileName.ToString();
            //设置文件名--与word文件名相同
            int LastPoint = wordSaveFileName.LastIndexOf('.');
            int LastSlash = wordSaveFileName.LastIndexOf("\\");
            int FileNameLength = LastPoint - LastSlash;
            string FileName = wordSaveFileName.Substring(LastSlash + 1, FileNameLength) + "html";
            //int FileNameLength = wordSaveFileName.Length - LastPoint;
            //设置路径+文件名
            string strSaveFileName = serviceName + FileName;
            object saveFileName = (object)strSaveFileName;
            //将word文件保存位html文件
            docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[]
            { saveFileName, Word.WdSaveFormat.wdFormatFilteredHTML });
            docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, null);
            //退出 Word
            wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);
            return FileName.ToString();
        }
    }

}
