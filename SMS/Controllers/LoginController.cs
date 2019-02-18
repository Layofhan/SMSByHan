using SMS.Data.Services;
using SMS.Demo.Contracts;
using SMS.Entity;
using SMS.Entity.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public ILoginContract LoginContract { get; set; }
        private static readonly Regex a = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private static readonly Regex b = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        // GET: Login
        [AllowAnonymousAttribute]
        public ActionResult Index()
        {
            var userAgent = HttpContext.Request.UserAgent;
            if ((a.IsMatch(userAgent) || b.IsMatch(userAgent.Substring(0, 4))))
            {
                return Redirect("http://www.baidu.com");
            }
            else
            {
                return View();
            }
        }
        /// <summary>
        /// 登入操作
        /// </summary>
        /// <param name="Name">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns></returns>
        [AllowAnonymousAttribute]
        public ActionResult Login(string Name,string PassWord,string CheckCode,bool isChace)
        {
            if (CheckCode.ToLower().Equals(Session["validate"].ToString()))
            {
                return LoginContract.Login(Name, PassWord, isChace).ToMvcJson();
            }
            return DataProcess.Failure("验证码不正确,请重新输入!").ToMvcJson();
        }
        [AllowAnonymousAttribute]
        //获取4位验证码
        public FileContentResult GetCode()
        {
            //string code = GetRndStr();
            string code = GetNumAndStr(4);
            byte[] imageByte;
            using (Bitmap img = CreateImages(code, "ch"))
            {
                imageByte = BitmapToByte(img);
                Session["validate"] = code.ToLower();
            }
            return File(imageByte, "image/gif");
        }
        public static byte[] BitmapToByte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 数字随机数
        /// </summary>
        /// <returns></returns>
        private string GetRndNum()
        {
            string code = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                code += random.Next(9);
            }
            return code;
        }
        /// <summary>
        /// 英文随机
        /// </summary>
        /// <returns></returns>
        private string GetRndStr()
        {
            string Vchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] VcArray = Vchar.Split(',');
            string checkCode = string.Empty;
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                int t = rand.Next(VcArray.Length);
                checkCode += VcArray[t];
            }
            return checkCode;
        }
        /// <summary>
        /// 中文随机
        /// </summary>
        /// <returns></returns>
        private string GetRndCh()
        {
            System.Text.Encoding gb = System.Text.Encoding.Default;//获取GB2312编码页（表）
            object[] bytes = CreateRegionCode(4);//生4个随机中文汉字编码
            string[] str = new string[4];
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                //根据汉字编码的字节数组解码出中文汉字
                str[i] = gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[])));
                sb.Append(str[i].ToString());
            }
            return sb.ToString();
        }
        /// <summary>
        /// 产生随机中文字符
        /// </summary>
        /// <param name="strlength"></param>
        /// <returns></returns>
        private static object[] CreateRegionCode(int strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            Random rnd = new Random();
            object[] bytes = new object[strlength];

            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();
                //区位码第2位
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //区位码第3位
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机种子
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //区位码第4位
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();
                //定义两个字节变量存储产生的随机汉字区位码
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);

                //将两个字节变量存储在字节数组中
                byte[] str_r = new byte[] { byte1, byte2 };

                //将产生的一个汉字的字节数组放入object数组中
                bytes.SetValue(str_r, i);
            }
            return bytes;
        }

        public string GetNumAndStr(int length)
        {
            string str = string.Empty;
            string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p" +
            ",q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q" +
            ",R,S,T,U,V,W,X,Y,Z";

            string[] VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组
            string[] num = new string[length];

            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数

            Random rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同
            for (int i = 1; i < length + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(61);

                temp = t;
                str += VcArray[t];
                // num[i - 1] = VcArray[t]; 
            }
            return str;
        }
        /// <summary>
        /// 画图片的背景图+干扰线 
        /// </summary>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        private Bitmap CreateImages(string checkCode, string type)
        {
            int step = 0;
            if (type == "ch")
            {
                step = 5;//中文字符，边界值做大
            }
            int iwidth = (int)(checkCode.Length * (13 + step));
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 22);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);//清除背景色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };//定义随机颜色
            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            Random rand = new Random();

            for (int i = 0; i < 50; i++)
            {
                int x1 = rand.Next(image.Width);
                int x2 = rand.Next(image.Width);
                int y1 = rand.Next(image.Height);
                int y2 = rand.Next(image.Height);
                g.DrawLine(new Pen(Color.LightGray, 1), x1, y1, x2, y2);//根据坐标画线
            }

            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(5);

                Font f = new System.Drawing.Font(font[findex], 10, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                int ii = 4;
                if ((i + 1) % 2 == 0)
                {
                    ii = 2;
                }
                g.DrawString(checkCode.Substring(i, 1), f, b, 3 + (i * (12 + step)), ii);

            }
            g.DrawRectangle(new Pen(Color.Silver, 0), 0, 0, image.Width - 1, image.Height - 1);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            return image;
        }
    }
}