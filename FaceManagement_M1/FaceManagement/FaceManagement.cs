using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace FaceManagement
{
    public partial class FaceManagement : Form
    {
        public List<LoginDetail> lstLoginDetail;
        public LoginDetail hrDeviceLoginDetail;
        public FaceManagement()
        {
            InitializeComponent();
            CHCNetSDK.NET_DVR_Init();
            CHCNetSDK.NET_DVR_SetLogToFile(3, "./SdkLog/", true);
            hrDeviceLoginDetail = new LoginDetail("192.168.10.12", "admin", "lynd4Cam", "8000");

            lstLoginDetail = new List<LoginDetail>
            {
                new LoginDetail("192.168.10.13", "admin", "lynd4Cam", "8000"),
                new LoginDetail("192.168.10.14", "admin", "lynd4Cam", "8000")
            };
        }

        private int m_UserID = -1;
        private int m_lGetFaceCfgHandle = -1;
        private int m_lSetFaceCfgHandle = -1;
        private int m_lCapFaceCfgHandle = -1;

        public Int32 m_lGetCardCfgHandle = -1;
        public Int32 m_lSetCardCfgHandle = -1;
        public Int32 m_lDelCardCfgHandle = -1;
        public string faceImagePath = "c:\\Temp\\";
  
      
     
        private void ProcessSetFaceData(ref CHCNetSDK.NET_DVR_FACE_STATUS struStatus ,ref bool flag)
        {
            switch(struStatus.byRecvStatus)
            {
                case 1:
                    MessageBox.Show("SetFaceDataSuccessful", "Succeed", MessageBoxButtons.OK);
                    break;
                default:
                    flag = false;
                    MessageBox.Show("NET_SDK_SET_Face_DATA_FAILED" + struStatus.byRecvStatus.ToString(), "ERROR", MessageBoxButtons.OK);
                    break;
            }
            
        }

        private void ReadFaceData(ref CHCNetSDK.NET_DVR_FACE_RECORD struRecord,string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                MessageBox.Show("The face picture does not exist!");
                return;
            }
            FileStream fs = new FileStream(imagePath, FileMode.OpenOrCreate);
            if (0 == fs.Length)
            {
                MessageBox.Show("The face picture is 0k,please input another picture!");
                return;
            }
            if (200 * 1024 < fs.Length)
            {
                MessageBox.Show("The face picture is larger than 200k,please input another picture!");
                return;
            }
            try
            {
                int.TryParse(fs.Length.ToString(),out struRecord.dwFaceLen);
                int iLen = struRecord.dwFaceLen;
                byte[] by = new byte[iLen];
                struRecord.pFaceBuffer = Marshal.AllocHGlobal(iLen);
                fs.Read(by, 0, iLen);
                Marshal.Copy(by, 0, struRecord.pFaceBuffer, iLen);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Read Face Data failed");
                fs.Close();
                return;
            }
        }
    
        private DataTable ReadCSVData(string filePath)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("CSV file not found!");
                    return dataTable;
                }

                // Read CSV data and load into the DataTable
                using (TextFieldParser parser = new TextFieldParser(filePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(","); // Assuming comma-separated CSV file

                    // Read the first line as header (column names)
                    string[] headers = parser.ReadFields();

                    // Add columns to the DataTable based on header values
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }

                    // Read the remaining lines as data rows
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        dataTable.Rows.Add(fields);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while reading CSV data: " + ex.Message);
            }

            return dataTable;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    DataTable dataTable = ReadCSVData(selectedFilePath);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //  Going to each device and updating the list

            foreach (LoginDetail loginDetail in lstLoginDetail)
            {
                //Login("10.2.12.10", "admin", "lynd4Cam", "8000");
                Login(loginDetail.DeviceAddress, loginDetail.UserName, loginDetail.Password, loginDetail.Port);
                if (m_UserID < 0) continue;
                // Iterate through each row in the DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Skip the last empty row (if any)
                    if (!row.IsNewRow)
                    {
                        // Process data for each row
                        string empId = row.Cells["EmpId"].Value?.ToString();
                        string empName = row.Cells["EmpName"].Value?.ToString();
                        string userType = row.Cells["UserType"].Value?.ToString();
                        string floorNo = row.Cells["FloorNo"].Value?.ToString();
                        string roomNo = row.Cells["RoomNo"].Value?.ToString();
                        string cardNo = row.Cells["CardNo"].Value?.ToString();
                        DateTime startTime = Convert.ToDateTime(row.Cells["StartTime"].Value);
                        DateTime endTime = Convert.ToDateTime(row.Cells["EndTime"].Value);

                        // setting the card Information
                        SetCard(cardNo, empId, empName, startTime, endTime);
                        // setting the face Information
                        SetFaceInfo("1", cardNo, faceImagePath + "FacePicture" + cardNo + ".jpg");
                    }
                }

                MessageBox.Show("Employee List updated for device : " + " Destination ", "Succeed", MessageBoxButtons.OK);
            }
        }


        private void SetFaceInfo(string cardReaderNo,string cardNo,string imagePath)
        {

            CHCNetSDK.NET_DVR_FACE_COND struCond = new CHCNetSDK.NET_DVR_FACE_COND();
            struCond.init();
            struCond.dwSize = Marshal.SizeOf(struCond);
            struCond.dwFaceNum = 1;
            int.TryParse(cardReaderNo, out struCond.dwEnableReaderNo);
            byte[] byTemp = System.Text.Encoding.UTF8.GetBytes(cardNo);
            for (int i = 0; i < byTemp.Length; i++)
            {
                struCond.byCardNo[i] = byTemp[i];
            }

            int dwInBufferSize = struCond.dwSize;
            IntPtr ptrstruCond = Marshal.AllocHGlobal(dwInBufferSize);
            Marshal.StructureToPtr(struCond, ptrstruCond, false);

            //this.Text += m_UserID.ToString();

            m_lSetFaceCfgHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(m_UserID, CHCNetSDK.NET_DVR_SET_FACE, ptrstruCond, dwInBufferSize, null, IntPtr.Zero);


            if (-1 == m_lSetFaceCfgHandle)
            {
                Marshal.FreeHGlobal(ptrstruCond);
                MessageBox.Show("NET_DVR_SET_FACE_FAIL, ERROR CODE" + CHCNetSDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                return;
            }

            CHCNetSDK.NET_DVR_FACE_RECORD struRecord = new CHCNetSDK.NET_DVR_FACE_RECORD();
            struRecord.init();
            struRecord.dwSize = Marshal.SizeOf(struRecord);

            byte[] byRecordNo = System.Text.Encoding.UTF8.GetBytes(cardNo);
            for (int i = 0; i < byRecordNo.Length; i++)
            {
                struRecord.byCardNo[i] = byRecordNo[i];
            }

            ReadFaceData(ref struRecord, imagePath);
            int dwInBuffSize = Marshal.SizeOf(struRecord);
            int dwStatus = 0;

            CHCNetSDK.NET_DVR_FACE_STATUS struStatus = new CHCNetSDK.NET_DVR_FACE_STATUS();
            struStatus.init();
            struStatus.dwSize = Marshal.SizeOf(struStatus);
            int dwOutBuffSize = struStatus.dwSize;
            IntPtr ptrOutDataLen = Marshal.AllocHGlobal(sizeof(int));
            bool Flag = true;
            while (Flag)
            {
                dwStatus = CHCNetSDK.NET_DVR_SendWithRecvRemoteConfig(m_lSetFaceCfgHandle, ref struRecord, dwInBuffSize, ref struStatus, dwOutBuffSize, ptrOutDataLen);
                switch (dwStatus)
                {
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_SUCCESS://成功读取到数据，处理完本次数据后需调用next
                        ProcessSetFaceData(ref struStatus, ref Flag);
                        break;
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:
                        break;
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_FAILED:
                        CHCNetSDK.NET_DVR_StopRemoteConfig(m_lSetFaceCfgHandle);
                        MessageBox.Show("NET_SDK_GET_NEXT_STATUS_FAILED" + CHCNetSDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                        Flag = false;
                        break;
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_FINISH:
                        CHCNetSDK.NET_DVR_StopRemoteConfig(m_lSetFaceCfgHandle);
                        Flag = false;
                        break;
                    default:
                        MessageBox.Show("NET_SDK_GET_NEXT_STATUS_UNKOWN" + CHCNetSDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                        Flag = false;
                        CHCNetSDK.NET_DVR_StopRemoteConfig(m_lSetFaceCfgHandle);
                        break;
                }
            }

            Marshal.FreeHGlobal(ptrstruCond);
            Marshal.FreeHGlobal(ptrOutDataLen);
        }


        private void SendCardData(string cardNo,string empNo,string empName,DateTime startTime,DateTime endTime)
        {
            CardManagement.CHCNetSDK.NET_DVR_CARD_RECORD struData = new CardManagement.CHCNetSDK.NET_DVR_CARD_RECORD();
            struData.Init();
            struData.dwSize = (uint)Marshal.SizeOf(struData);
            struData.byCardType = 1;
            byte[] byTempCardNo = new byte[CHCNetSDK.ACS_CARD_NO_LEN];
            byTempCardNo = System.Text.Encoding.UTF8.GetBytes(cardNo);
            for (int i = 0; i < byTempCardNo.Length; i++)
            {
                struData.byCardNo[i] = byTempCardNo[i];
            }
            ushort.TryParse(cardNo, out struData.wCardRightPlan[0]);
            uint.TryParse(empNo, out struData.dwEmployeeNo);
            byte[] byTempName = new byte[CardManagement.CHCNetSDK.NAME_LEN];
            byTempName = System.Text.Encoding.Default.GetBytes(empName);
            for (int i = 0; i < byTempName.Length; i++)
            {
                struData.byName[i] = byTempName[i];
            }
            struData.struValid.byEnable = 1;
            struData.struValid.struBeginTime.wYear = Convert.ToUInt16(startTime.Year);
            struData.struValid.struBeginTime.byMonth = Convert.ToByte(startTime.Month);
            struData.struValid.struBeginTime.byDay = Convert.ToByte(startTime.Day);
            struData.struValid.struBeginTime.byHour = Convert.ToByte(startTime.Hour);
            struData.struValid.struBeginTime.byMinute = Convert.ToByte(startTime.Minute);
            struData.struValid.struBeginTime.bySecond = Convert.ToByte(startTime.Second);
            struData.struValid.struEndTime.wYear = Convert.ToUInt16(endTime.Year);
            struData.struValid.struEndTime.byMonth = Convert.ToByte(endTime.Month);
            struData.struValid.struEndTime.byDay = Convert.ToByte(endTime.Day);
            struData.struValid.struEndTime.byHour = Convert.ToByte(endTime.Hour);
            struData.struValid.struEndTime.byMinute = Convert.ToByte(endTime.Minute);
            struData.struValid.struEndTime.bySecond = Convert.ToByte(endTime.Second);
            struData.byDoorRight[0] = 1;
            struData.wCardRightPlan[0] = 1;
            IntPtr ptrStruData = Marshal.AllocHGlobal((int)struData.dwSize);
            Marshal.StructureToPtr(struData, ptrStruData, false);
            Marshal.StructureToPtr(struData, ptrStruData, false);

            CardManagement.CHCNetSDK.NET_DVR_CARD_STATUS struStatus = new CardManagement.CHCNetSDK.NET_DVR_CARD_STATUS();
            struStatus.Init();
            struStatus.dwSize = (uint)Marshal.SizeOf(struStatus);
            IntPtr ptrdwState = Marshal.AllocHGlobal((int)struStatus.dwSize);
            Marshal.StructureToPtr(struStatus, ptrdwState, false);

            int dwState = (int)CardManagement.CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS;
            uint dwReturned = 0;
            while (true)
            {
                dwState = CardManagement.CHCNetSDK.NET_DVR_SendWithRecvRemoteConfig(m_lSetCardCfgHandle, ptrStruData, struData.dwSize, ptrdwState, struStatus.dwSize, ref dwReturned);
                struStatus = (CardManagement.CHCNetSDK.NET_DVR_CARD_STATUS)Marshal.PtrToStructure(ptrdwState, typeof(CardManagement.CHCNetSDK.NET_DVR_CARD_STATUS));
                if (dwState == (int)CardManagement.CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT)
                {
                    Thread.Sleep(10);
                    continue;
                }
                else if (dwState == (int)CardManagement.CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED)
                {
                    MessageBox.Show("NET_DVR_SET_CARD fail error: " + CardManagement.CHCNetSDK.NET_DVR_GetLastError());
                }
                else if (dwState == (int)CardManagement.CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS)
                {
                    if (struStatus.dwErrorCode != 0)
                    {
                        MessageBox.Show("NET_DVR_SET_CARD success but errorCode:" + struStatus.dwErrorCode);
                    }
                    else
                    {
                        MessageBox.Show("NET_DVR_SET_CARD success");
                    }
                }
                else if (dwState == (int)CardManagement.CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH)
                {
                    MessageBox.Show("NET_DVR_SET_CARD finish");
                    break;
                }
                else if (dwState == (int)CardManagement.CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION)
                {
                    MessageBox.Show("NET_DVR_SET_CARD exception error: " + CardManagement.CHCNetSDK.NET_DVR_GetLastError());
                    break;
                }
                else
                {
                    MessageBox.Show("unknown status error: " + CardManagement.CHCNetSDK.NET_DVR_GetLastError());
                    break;
                }
            }
            CardManagement.CHCNetSDK.NET_DVR_StopRemoteConfig(m_lSetCardCfgHandle);
            m_lSetCardCfgHandle = -1;
            Marshal.FreeHGlobal(ptrStruData);
            Marshal.FreeHGlobal(ptrdwState);
            return;
        }

        private void SetCard(string cardNo, string empNo, string empName, DateTime startTime, DateTime endTime)
        {
            if (m_lSetCardCfgHandle != -1)
            {
                if (CardManagement.CHCNetSDK.NET_DVR_StopRemoteConfig(m_lSetCardCfgHandle))
                {
                    m_lSetCardCfgHandle = -1;
                }
            }

            CardManagement.CHCNetSDK.NET_DVR_CARD_COND struCond = new CardManagement.CHCNetSDK.NET_DVR_CARD_COND();
            struCond.Init();
            struCond.dwSize = (uint)Marshal.SizeOf(struCond);
            struCond.dwCardNum = 1;
            IntPtr ptrStruCond = Marshal.AllocHGlobal((int)struCond.dwSize);
            Marshal.StructureToPtr(struCond, ptrStruCond, false);

            m_lSetCardCfgHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(m_UserID, CardManagement.CHCNetSDK.NET_DVR_SET_CARD, ptrStruCond, (int)struCond.dwSize, null, IntPtr.Zero);
            if (m_lSetCardCfgHandle < 0)
            {
                MessageBox.Show("NET_DVR_SET_CARD error:" + CHCNetSDK.NET_DVR_GetLastError());
                Marshal.FreeHGlobal(ptrStruCond);
                return;
            }
            else
            {
                SendCardData(cardNo, empNo, empName, startTime, endTime);
                Marshal.FreeHGlobal(ptrStruCond);
            }
        }

        private void Login(string deviceAdd,string userName,string password,string port)
        {
            CHCNetSDK.NET_DVR_USER_LOGIN_INFO struLoginInfo = new CHCNetSDK.NET_DVR_USER_LOGIN_INFO();
            CHCNetSDK.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40 = new CHCNetSDK.NET_DVR_DEVICEINFO_V40();
            struDeviceInfoV40.struDeviceV30.sSerialNumber = new byte[CHCNetSDK.SERIALNO_LEN];

            struLoginInfo.sDeviceAddress = deviceAdd;
            struLoginInfo.sUserName = userName;
            struLoginInfo.sPassword = password;
            ushort.TryParse(port, out struLoginInfo.wPort);

            int lUserID = -1;
            lUserID = CHCNetSDK.NET_DVR_Login_V40(ref struLoginInfo, ref struDeviceInfoV40);
            if (lUserID >= 0)
            {
                m_UserID = lUserID;
                MessageBox.Show("Login Successful");
            }
            else
            {
                m_UserID = -1;
                uint nErr = CHCNetSDK.NET_DVR_GetLastError();
                if (nErr == CHCNetSDK.NET_DVR_PASSWORD_ERROR)
                {
                    MessageBox.Show("user name or password error!");
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        string strTemp1 = string.Format("Left {0} try opportunity", struDeviceInfoV40.byRetryLoginTime);
                        MessageBox.Show(strTemp1);
                    }
                }
                else if (nErr == CHCNetSDK.NET_DVR_USER_LOCKED)
                {
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        string strTemp1 = string.Format("user is locked, the remaining lock time is {0}", struDeviceInfoV40.dwSurplusLockTime);
                        MessageBox.Show(strTemp1);
                    }
                }
                else
                {
                    MessageBox.Show("net error or dvr is busy!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Login(loginDetail.DeviceAddress, loginDetail.UserName, loginDetail.Password, lo3_ginDetail.Port);
            //Login("192.168.10.13", "admin", "lynd4Cam", "8000");
            
            Login(hrDeviceLoginDetail.DeviceAddress, hrDeviceLoginDetail.UserName, hrDeviceLoginDetail.Password, hrDeviceLoginDetail.Port);
            if (m_UserID < 0) return;

            // Iterate through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Skip the last empty row (if any)
                if (!row.IsNewRow)
                {
                    // Process data for each row
                    string empId = row.Cells["EmpId"].Value?.ToString();
                    string empName = row.Cells["EmpName"].Value?.ToString();
                    string userType = row.Cells["UserType"].Value?.ToString();
                    string floorNo = row.Cells["FloorNo"].Value?.ToString();
                    string roomNo = row.Cells["RoomNo"].Value?.ToString();
                    string cardNo = row.Cells["CardNo"].Value?.ToString();
                    DateTime startTime = Convert.ToDateTime(row.Cells["StartTime"].Value);
                    DateTime endTime = Convert.ToDateTime(row.Cells["EndTime"].Value);
                   
                    // setting the card Information
                    SetCard(cardNo, empId, empName, startTime, endTime);
                   
                }
            }
            MessageBox.Show("Employee List updated for  HR device : ", "Succeed", MessageBoxButtons.OK);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // Login("192.168.10.13", "admin", "lynd4Cam", "8000");
            Login(hrDeviceLoginDetail.DeviceAddress, hrDeviceLoginDetail.UserName, hrDeviceLoginDetail.Password, hrDeviceLoginDetail.Port);
            if (m_UserID < 0) return;

            // Iterate through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Skip the last empty row (if any)
                if (!row.IsNewRow)
                {
                    // Process data for each row
                    //string empId = row.Cells["EmpId"].Value?.ToString();
                    //string empName = row.Cells["EmpName"].Value?.ToString();
                    //string userType = row.Cells["UserType"].Value?.ToString();
                    //string floorNo = row.Cells["FloorNo"].Value?.ToString();
                    //string roomNo = row.Cells["RoomNo"].Value?.ToString();
                    string cardNo = row.Cells["CardNo"].Value?.ToString();
                    //string startTime = row.Cells["StartTime"].Value?.ToString();
                    //string endTime = row.Cells["EndTime"].Value?.ToString();

                    GetImagesFromHR(cardNo);
                }
            }
            MessageBox.Show("Employee List updated for  HR device : ", "Succeed", MessageBoxButtons.OK);
        }

        private void GetImagesFromHR(string cardNo)
        {
            if (m_lGetFaceCfgHandle != -1)
            {
                CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                m_lGetFaceCfgHandle = -1;
            }

           
            CHCNetSDK.NET_DVR_FACE_COND struCond = new CHCNetSDK.NET_DVR_FACE_COND();
            struCond.init();
            struCond.dwSize = Marshal.SizeOf(struCond);
            int dwSize = struCond.dwSize;
            if ("1".ToString() == "")
            {
                struCond.dwEnableReaderNo = 0;
            }
            else
            {
                int.TryParse("1".ToString(), out struCond.dwEnableReaderNo);
            }
            struCond.dwFaceNum = 1;//人脸数量是1
            byte[] byTemp = System.Text.Encoding.UTF8.GetBytes(cardNo);
            for (int i = 0; i < byTemp.Length; i++)
            {
                struCond.byCardNo[i] = byTemp[i];
            }

            IntPtr ptrStruCond = Marshal.AllocHGlobal(dwSize);
            Marshal.StructureToPtr(struCond, ptrStruCond, false);

            m_lGetFaceCfgHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(m_UserID, CHCNetSDK.NET_DVR_GET_FACE, ptrStruCond, dwSize, null, IntPtr.Zero);
            if (m_lGetFaceCfgHandle == -1)
            {
                Marshal.FreeHGlobal(ptrStruCond);
                MessageBox.Show("NET_DVR_GET_FACE_FAIL, ERROR CODE" + CHCNetSDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                return;
            }

            bool Flag = true;
            int dwStatus = 0;

            CHCNetSDK.NET_DVR_FACE_RECORD struRecord = new CHCNetSDK.NET_DVR_FACE_RECORD();
            struRecord.init();
            struRecord.dwSize = Marshal.SizeOf(struRecord);
            int dwOutBuffSize = struRecord.dwSize;
            while (Flag)
            {
                dwStatus = CHCNetSDK.NET_DVR_GetNextRemoteConfig(m_lGetFaceCfgHandle, ref struRecord, dwOutBuffSize);
                switch (dwStatus)
                {
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_SUCCESS://成功读取到数据，处理完本次数据后需调用next
                        ProcessFaceData(ref struRecord, ref Flag, cardNo);
                        break;
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:
                        break;
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_FAILED:
                        CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                        MessageBox.Show("NET_SDK_GET_NEXT_STATUS_FAILED" + CHCNetSDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                        Flag = false;
                        break;
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_FINISH:
                        MessageBox.Show("NET_SDK_GET_NEXT_STATUS_FINISH", "Tips", MessageBoxButtons.OK);
                        CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                        Flag = false;
                        break;
                    default:
                        MessageBox.Show("NET_SDK_GET_STATUS_UNKOWN" + CHCNetSDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                        Flag = false;
                        CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                        break;
                }
            }

            Marshal.FreeHGlobal(ptrStruCond);
        }

        private void ProcessFaceData(ref CHCNetSDK.NET_DVR_FACE_RECORD struRecord, ref Boolean Flag, string cardNo)
        {
            string strpath = null;
            //DateTime dt = DateTime.Now;
            strpath = faceImagePath + string.Format("FacePicture" + cardNo + ".jpg");

            if (0 == struRecord.dwFaceLen)
            {
                return;
            }

            try
            {
                using (FileStream fs = new FileStream(strpath, FileMode.OpenOrCreate))
                {
                    int FaceLen = struRecord.dwFaceLen;
                    byte[] by = new byte[FaceLen];
                    Marshal.Copy(struRecord.pFaceBuffer, by, 0, FaceLen);
                    fs.Write(by, 0, FaceLen);
                    fs.Close();
                }
            }
            catch
            {
                Flag = false;
                CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                MessageBox.Show("ProcessFingerData failed", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
