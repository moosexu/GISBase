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
    /// ��д��Combox��ʹ���ܹ�����ɫ����
    /// </summary>
    class ComboxColor : System.Windows.Forms.ComboBox
    {
        public ComboxColor()
        {
            //���������ǹؼ��ģ�
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        //��д����
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            try
            {
                //��ʾͼƬ
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