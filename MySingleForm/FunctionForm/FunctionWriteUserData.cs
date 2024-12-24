﻿using SimpleReader.Resource;
using RFIDReaderAPI.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RFIDReaderAPI;
using SimpleReaderDemo.Helper;

namespace SimpleReaderDemo.MySingleForm.TestForm.FunctionForm
{
    public partial class FunctionWriteUserData : BaseOption
    {
        IAsynchronousMessage contextForm = null;
        Int32 AntNum = 0;                          // 所选的天线编号
        String addAntParam = string.Empty;         //新增9-24天线配置参数

        String[] EPCdata = new String[] { "", "", "" }; // EPC,TID,UserData   ,当前选择的标签数据

        public FunctionWriteUserData()
        {
            InitializeComponent();
        }

        public FunctionWriteUserData(string readerID, String[] epcData, Int32 antNum, String AddAntParam, IAsynchronousMessage contextForm)
            : this()
        {
            this.ConnID = readerID;
            this.EPCdata = epcData;
            this.contextForm = contextForm;
            this.AntNum = antNum;
            this.addAntParam = AddAntParam;
            Init();
        }

      
        public void Init()
        {
            this.tb_SelectEPC.Text = EPCdata[0];
            this.tb_SelectTID.Text = EPCdata[1];
            InitReaderProerty();
        }

        private void FunctionWriteUserData_Load(object sender, EventArgs e)
        {
            this.Width = 500;
            this.Height = 300;
        }

        private void tb_WriteEPCData_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Handled)
            {
                tb_WriteEPCLength.Text = (tb_WriteUserData.Text.Length % 4 == 0 ? tb_WriteUserData.Text.Length / 4 : tb_WriteUserData.Text.Length / 4 + 1).ToString();
            }
        }

        #region 通用方法
        /// <summary>
        /// 写用户数据
        /// </summary>
        private void WriteUserData()
        {
            var antno = RFIDReader._Tag6C.GeteAntennaNo(AntNum + "");
            string pwd = tb_AccessPwd.Text.Trim();
            string selectEPC = tb_SelectEPC.Text.Trim();
            string selectTID = tb_SelectTID.Text.Trim();
            string writeData = this.tb_WriteUserData.Text.Trim();
            string result = "-1";
            if (string.IsNullOrEmpty(writeData))
            {
                MessageBox.Show("Write Data is null!");
                return;
            }
            else if (string.IsNullOrEmpty(pwd))
            {
                if (string.IsNullOrEmpty(selectTID))
                {
                   
                    result = RFIDReader._Tag6C.WriteUserData(ConnID, (eAntennaNo)AntNum, writeData, 0,matchType:eMatchCode.EPC,matchCode: selectEPC,matchWordStartIndex: 0);
                }
                else
                {
                    result = RFIDReader._Tag6C.WriteUserData(ConnID, (eAntennaNo)AntNum, writeData, 0, matchType: eMatchCode.TID, matchCode: selectTID, matchWordStartIndex: 0);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(tb_SelectTID.Text.Trim()))
                {
                    result = RFIDReader._Tag6C.WriteUserData(ConnID, (eAntennaNo)AntNum, writeData, 0, matchType: eMatchCode.EPC, matchCode: selectEPC, matchWordStartIndex: 0,accessPassword:pwd);
                }
                else
                {
                    result = RFIDReader._Tag6C.WriteUserData(ConnID, (eAntennaNo)AntNum, writeData, 0, matchType: eMatchCode.TID, matchCode: selectTID, matchWordStartIndex: 0, accessPassword: pwd);
                }
            }
            ShowMessage(result);
        }

        #endregion

        private void btn_Write_Click(object sender, EventArgs e)
        {
            WriteUserData();
        }
    }
}
