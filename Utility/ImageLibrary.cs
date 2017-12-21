using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace Utility
{
    namespace Utility
    {
        public class ImageLibrary
        {
            private resimg _resimg = new resimg();
            /// <summary>
            /// resimg对象
            /// </summary>
            public class resimg
            {
                public resimg()
                {
                    imgmd5 = "";
                    imgsize = 0;
                    imgwidth = 0;
                    imghidth = 0;
                    imgname = "";
                }
                public string imgmd5 { get; set; }
                public int imgsize { get; set; }
                public int imgwidth { get; set; }
                public int imghidth { get; set; }
                public string imgname { get; set; }

            }
            public resimg resimgINFO { get { return _resimg; } }
            /// <summary>
            /// 获取图片编码信息
            /// </summary>
            /// <param name="mimeType">MIME类型 如：image/jpeg</param>
            /// <returns></returns>
            public static ImageCodecInfo GetEncoderInfo(string mimeType)
            {
                ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

                foreach (ImageCodecInfo encoder in encoders)
                {
                    if (encoder.MimeType == mimeType)
                    {
                        return encoder;
                    }
                }

                return null;
            }
            /// <summary>
            /// 获取缩放的正方形图片
            /// </summary>
            /// <param name="image">原始图片对象</param>
            /// <param name="size">尺寸</param>
            /// <returns></returns>
            public static Image GetZoomSquareImage(Image image, int size)
            {
                int width, height;

                double d;

                if (image.Height > image.Width)
                {
                    height = size;
                    d = (double)(size / (double)image.Height);
                    width = (int)(image.Width * d);
                }
                else
                {
                    width = size;
                    d = (double)(size / (double)image.Width);
                    height = (int)(image.Height * d);
                }

                Image.GetThumbnailImageAbort call = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                return image.GetThumbnailImage(width, height, call, new IntPtr());

            }
            /// <summary>
            /// 获取定宽的图片 高自动缩放
            /// </summary>
            /// <param name="image">原始图片对象</param>
            /// <param name="size">尺寸</param>
            /// <returns></returns>
            public static Image GetAbsoluteWidthImage(Image image, int size)
            {
                int width, height;

                width = size;
                height = image.Height * size / image.Width;

                Image.GetThumbnailImageAbort call = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                return image.GetThumbnailImage(width, height, call, new IntPtr());
            }
            /// <summary>
            /// 缩微图回调函数
            /// </summary>
            /// <returns></returns>
            private static bool ThumbnailCallback()
            {
                return false;
            }

            #region 通用的缩图方法~~  Cut

            #region Mode
            /// <summary>
            /// 缩图模式
            /// </summary>
            public enum MakeImageMode
            {
                /// <summary>
                /// 定宽和高 可能变形 
                /// </summary>
                AbsoluteWidthHeight,
                /// <summary>
                /// 定宽 高按比例
                /// </summary>
                AbsoluteWidth,
                /// <summary>
                /// 定高 宽按比例
                /// </summary>
                AbsoluteHeight,
                /// <summary>
                /// 剪成宽高~不变形
                /// </summary>
                CutWidthHeight,
                /// <summary>
                /// 缩成宽高~不变形
                /// </summary>
                ZoomWidthHeight,
                /// <summary>
                /// 缩成正方型 补白背景
                /// </summary>
                ZoomSquare,
            }
            #endregion

            /// <summary>
            /// 通用的缩图方法~~ 
            /// </summary>
            /// <param name="image">原图</param>
            /// <param name="width">新宽</param>
            /// <param name="height">新高</param>
            /// <param name="mode">模式</param>
            public static Image MakeImage(Image image, int width, int height, MakeImageMode mode)
            {
                if (width <= 0)
                    width = height;
                if (height <= 0)
                    height = width;
                if (width <= 0 && height <= 0)
                {
                    width = 100;
                    height = 100;
                }

                Rectangle dect = new Rectangle(0, 0, width, height);
                Rectangle src = new Rectangle(0, 0, image.Width, image.Height);

                switch (mode)
                {
                    case MakeImageMode.AbsoluteWidthHeight: //定宽和高 可能变形 
                        break;

                    case MakeImageMode.AbsoluteWidth:       //指定宽，高按比例                  
                        dect.Height = height = image.Height * width / image.Width;
                        break;

                    case MakeImageMode.AbsoluteHeight:      //指定高，宽按比例
                        dect.Width = width = image.Width * height / image.Height;
                        break;

                    case MakeImageMode.CutWidthHeight:      //指定高宽裁减（不变形）                
                        if ((double)image.Width / (double)image.Height > (double)dect.Width / (double)dect.Height)
                        {
                            src.Width = image.Height * dect.Width / dect.Height;
                            src.X = (image.Width - src.Width) / 2;
                        }
                        else
                        {
                            src.Height = image.Width * height / dect.Width;
                            src.Y = (image.Height - src.Height) / 2;
                        }
                        break;

                    case MakeImageMode.ZoomWidthHeight: //缩成~宽~高~~内~

                        break;

                    case MakeImageMode.ZoomSquare:          //正方可以上上面的方法剪~~这里缩~不剪
                        if (image.Height > image.Width)
                        {
                            dect.Width = image.Width * height / image.Height;
                            dect.X = (width - dect.Width) / 2;
                        }
                        else
                        {
                            dect.Height = image.Height * width / image.Width;
                            dect.Y = (height - dect.Height) / 2;
                        }
                        break;

                    default:
                        break;
                }

                //新建一个bmp图片
                Image bitmap = new System.Drawing.Bitmap(width, height);

                //新建一个画板
                Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以 白色背景 填充
                g.Clear(Color.White);

                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(image, dect, src, GraphicsUnit.Pixel);

                return bitmap;
            }

            /// <summary>
            /// 按原图产生指定的截图
            /// </summary>
            /// <param name="oderimgurl">原图路径</param>
            /// <param name="width">指定宽</param>
            /// <param name="height"></param>
            /// <param name="mode"></param>
            /// <param name="newimgurl">新图路径</param>
            /// <returns>截图后的文件名</returns>
            public bool getnewimage(string oderimgurl, int width, int height, MakeImageMode mode, string newimgurl, string Extension)
            {
                bool status = true;
                System.Drawing.Image img;//定义一个图片对象
                System.Drawing.Image newimg;
                System.Drawing.Image temp;
                Image bitmapnew;
                MemoryStream ms = new MemoryStream();

                img = System.Drawing.Image.FromFile(oderimgurl);//写入对象
                newimg = ImageLibrary.MakeImage(img, width, height, mode);//切图产生了一个新的图片对象
                _resimg.imgwidth = newimg.Width;//图片宽
                _resimg.imghidth = newimg.Height;//图片高
                temp = img;//引用旧图
                temp.Dispose();//释放旧图
                img = newimg;//把新图放到img
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                bitmapnew = ((Image)new Bitmap(ms));//文件流转换为图片对象
                img.Dispose();//释放
                byte[] bytes = ms.ToArray();
                int imgsize = int.Parse(ms.Length.ToString());
                string newimgname = ImageLibrary.md5(bytes);//文件流加密md5
                string newimgfileurl = newimgurl + newimgname + Extension;
                try
                {
                    bitmapnew.Save(newimgfileurl, System.Drawing.Imaging.ImageFormat.Jpeg);
                    status = true;
                }
                catch (System.Exception e)
                {
                    status = false;
                    throw e;
                }
                finally
                {
                    temp.Dispose();
                    img.Dispose();
                    ms.Close();
                    ms.Dispose();
                    bitmapnew.Dispose();
                }
                _resimg.imgmd5 = newimgname;
                _resimg.imgname = newimgname + Extension;
                _resimg.imgsize = imgsize / 1024;

                return status;
            }
            /// <summary>
            /// 缩微图回调函数
            /// </summary>
            /// <param name="file"></param>
            /// <returns></returns>
            public static string md5(byte[] file)
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(file);
                byte[] hash = md5.Hash;
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {

                    sb.Append(string.Format("{0:X2}", b));
                }
                return sb.ToString();
            }

            #endregion
        }
    }

}
