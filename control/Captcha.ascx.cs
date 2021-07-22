﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class control_Captcha : System.Web.UI.UserControl
{
    int iHeight = 50;
    int iWidth = 160;
    Random oRandom = new Random();
    int[] aBackgroundNoiseColor = new int[] { 150, 150, 150 };
    int[] aTextColor = new int[] { 0, 0, 0 };
    int[] aFontEmSizes = new int[] { 15, 12, 20, 22, 18 };

    string[] aFontNames = new string[]
{
 "Comic Sans MS",
 "Arial",
 "Times New Roman",
 "Georgia",
 "Verdana",
 "Geneva"
};
    FontStyle[] aFontStyles = new FontStyle[]
    {
 FontStyle.Bold,
 FontStyle.Italic,
 FontStyle.Regular,
 FontStyle.Strikeout,
 FontStyle.Underline
    };
    HatchStyle[] aHatchStyles = new HatchStyle[]
    {
 HatchStyle.BackwardDiagonal, HatchStyle.Cross,
    HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
 HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
    HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
 HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid,
    HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
 HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard,
    HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
 HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal,
    HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
 HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal,
    HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
 HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard,
    HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
 HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis,
    HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
 HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
    };

    private string sCaptchaText = "";
    private string answer = "";
    public string captchaImage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Random oRandom = new Random();


        //int iNumber = oRandom.Next(100000, 999999);
        sCaptchaText = Session["captcha"].ToString();

        //answer = iNumber.ToString();
        //if (string.IsNullOrEmpty(Request.Form["captchatxt"]))
        //{
        //     = answer;
        //}
        DrawImage();


    }

    public void DrawImage()
    {
        //Creates an output Bitmap
        Bitmap oOutputBitmap = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
        Graphics oGraphics = Graphics.FromImage(oOutputBitmap);
        oGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

        //Create a Drawing area
        RectangleF oRectangleF = new RectangleF(0, 0, iWidth, iHeight);
        Brush oBrush = default(Brush);

        //Draw background (Lighter colors RGB 100 to 255)
        oBrush = new HatchBrush(aHatchStyles[oRandom.Next
            (aHatchStyles.Length - 1)], Color.FromArgb((oRandom.Next(100, 255)),
            (oRandom.Next(100, 255)), (oRandom.Next(100, 255))), Color.White);
        oGraphics.FillRectangle(oBrush, oRectangleF);

        System.Drawing.Drawing2D.Matrix oMatrix = new System.Drawing.Drawing2D.Matrix();
        int i = 0;
        for (i = 0; i <= sCaptchaText.Length - 1; i++)
        {
            oMatrix.Reset();
            int iChars = sCaptchaText.Length;
            int x = iWidth / (iChars + 1) * i;
            int y = iHeight / 2;

            //Rotate text Random
            oMatrix.RotateAt(oRandom.Next(-20, 20), new PointF(x, y));
            oGraphics.Transform = oMatrix;

            //Draw the letters with Random Font Type, Size and Color
            oGraphics.DrawString
            (
            //Text
            sCaptchaText.Substring(i, 1),
            //Random Font Name and Style
            new Font(aFontNames[oRandom.Next(aFontNames.Length - 1)],
               aFontEmSizes[oRandom.Next(aFontEmSizes.Length - 1)],
               aFontStyles[oRandom.Next(aFontStyles.Length - 1)]),
            //Random Color (Darker colors RGB 0 to 100)
            new SolidBrush(Color.FromArgb(oRandom.Next(0, 100),
               oRandom.Next(0, 100), oRandom.Next(0, 100))),
            x,
            oRandom.Next(0, 15)
            );
            oGraphics.ResetTransform();
        }

        MemoryStream oMemoryStream = new MemoryStream();
        oOutputBitmap.Save(oMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
        byte[] oBytes = oMemoryStream.GetBuffer();
        captchaImage = "data:image/png;base64," + Convert.ToBase64String(oBytes, 0, oBytes.Length);
        oOutputBitmap.Dispose();
        oMemoryStream.Close();

    }
    public bool ValidateCaptcha()
    {
        //Response.Write(Session["captcha"].ToString());
        //Response.Write("~~");
        //Response.Write(Request.Form["captchatxt"]);
        if (Session["captcha"].ToString() == Request.Form["captchatxt"])
        {
            Session["captcha"] = sCaptchaText;
            return true;
        }
        else
        {
            Session["captcha"] = sCaptchaText;
            return false;
        }

    }
}