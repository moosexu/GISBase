using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using System.Reflection;


namespace BaseLibs
{
    public enum CursorType
    {
        Pointer=0,
        Hand=1,
        Pan=2,
        Default=3,
    }

    public class CursorFactory
    {
        //引用Windows API函数
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string filename);
        private object m_Hooker = null;

        public CursorFactory(object pHooker)
        {
            m_Hooker = pHooker;
        }

        private CursorType m_CursorType;

        public CursorType CursorType
        {
            get { return m_CursorType; }
            set
            {
                m_CursorType = value; 
                switch(m_CursorType)
                {
                    case CursorType.Pan:
                        (m_Hooker as Office2007RibbonForm).Cursor =Cursors.Hand;// GetCursor(CursorType.Pan);
                        break;
                    case CursorType.Pointer:
                        (m_Hooker as Office2007RibbonForm).Cursor = Cursors.Arrow;
                        break;
                    case CursorType.Default:
                        (m_Hooker as Office2007RibbonForm).Cursor = Cursors.Default;
                        break;
                    default:
                        break;
                }
            }
        }


        public Cursor GetCursor(CursorType pCursorType)
        {
            string sImageName = null;
            switch(pCursorType)
            {
                case CursorType.Hand:
                    break;
                case CursorType.Pan:
                    sImageName = "Pan.png";
                    break;
                case CursorType.Pointer:
                    break;
                default:
                    break;
            }
            string sCursorPath=Application.StartupPath + "\\" + sImageName;
            System.Windows.Forms.Cursor cursor = new System.Windows.Forms.Cursor(Cursor.Current.Handle);
            IntPtr cursorhandle = LoadCursorFromFile(sCursorPath);
            cursor.GetType().InvokeMember("handle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, cursor, new object[] { cursorhandle });
            return cursor;
        }
    }
}
