using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

public partial class VerifyCode : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVerifyCode1(73, 34);
    }

    public void GetVerifyCode1(int weight, int height, int fontsize = 16)
    {
        int codeW = weight;
        int codeH = height;
        int fontSize = fontsize;
        string chkCode = string.Empty;
        //颜色列表，用于验证码、噪线、噪点 
        Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
        //字体列表，用于验证码 
        string[] font = { "Times New Roman" };
        //验证码的字符集，去掉了一些容易混淆的字符 
        char[] character = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
        Random rnd = new Random();
        //生成验证码字符串 
        for (int i = 0; i < 4; i++)
        {
            chkCode += character[rnd.Next(character.Length)];
        }
        //写入Session
        HttpContext.Current.Session["session_verifycode"] = chkCode.ToLower();
        //创建画布
        Bitmap bmp = new Bitmap(codeW, codeH);
        Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        //画噪线 
        for (int i = 0; i < 3; i++)
        {
            int x1 = rnd.Next(codeW);
            int y1 = rnd.Next(codeH);
            int x2 = rnd.Next(codeW);
            int y2 = rnd.Next(codeH);
            Color clr = color[rnd.Next(color.Length)];
            g.DrawLine(new Pen(clr), x1, y1, x2, y2);
        }
        //画验证码字符串 
        for (int i = 0; i < chkCode.Length; i++)
        {
            string fnt = font[rnd.Next(font.Length)];
            Font ft = new Font(fnt, fontSize);
            Color clr = color[rnd.Next(color.Length)];
            g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, (float)0);
        }
        //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
        MemoryStream ms = new MemoryStream();
        try
        {
            bmp.Save(ms, ImageFormat.Png);

            Response.ClearContent();
            Response.ContentType = @"image/gif";
            Response.BinaryWrite(ms.ToArray());

        }
        catch (Exception)
        {
        }
        finally
        {
            g.Dispose();
            bmp.Dispose();
            Response.End();
        }
    }
}