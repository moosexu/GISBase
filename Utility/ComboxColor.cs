using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Seismography.UserControls
{
    /// <summary>
    /// 重写了Combox，使其能够带颜色下拉
    /// </summary>
    class ComboxColor : System.Windows.Forms.ComboBox
    {
        public ComboxColor()
        {
            //以下两句是关键的；
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        //重写函数
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            try
            {
                //显示图片
                Image image = (Image)Items[e.Index];
                System.Drawing.Rectangle rect = e.Bounds;
                e.Graphics.DrawImage(image, rect);
            }
            catch
            {
            }

            finally
            {
                base.OnDrawItem(e);
            }
        }
    }
}