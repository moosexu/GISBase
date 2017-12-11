using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace BaseLibs
{
    class DBClass
    {
        OleDbConnection m_OleDbConnection = null;
        string m_ErrorInfo = "";
        public DBClass()
        {
            m_OleDbConnection = new OleDbConnection();
        }
        ~DBClass()
        {
            Close();
        }
        public bool OpenDB(string strDBPath)//�����ݿ�
        {
            try
            {
                string strConn = "Provider=Microsoft.jet.oledb.4.0;Data source=" + strDBPath + ";";
                m_OleDbConnection.ConnectionString = strConn;
                m_OleDbConnection.Open();
                return true;
            }
            catch (Exception Ex)
            {
                m_ErrorInfo = Ex.Message;
            }
            return false;
        }

        public OleDbDataReader ReadDB(string strSql)//ִ�����ݿ�SQL������DataReader����
        {
            if (m_OleDbConnection.State != System.Data.ConnectionState.Open) return null;
            try
            {
                OleDbCommand pOleDbCommand = new OleDbCommand();
                pOleDbCommand.Connection = m_OleDbConnection;
                pOleDbCommand.CommandText = strSql;
                OleDbDataReader pOleDbDataReader = pOleDbCommand.ExecuteReader();
                return pOleDbDataReader;
            }
            catch (Exception Ex)
            {
                m_ErrorInfo = Ex.Message;
            }
            return null;

        }

        public bool Close()//�ر����ݿ�
        {
            try
            {
                m_OleDbConnection.Close();
                return true;
            }
            catch (Exception Ex)
            {
                m_ErrorInfo = Ex.Message;
            }
            return false; ;
            
        }
    }
}
