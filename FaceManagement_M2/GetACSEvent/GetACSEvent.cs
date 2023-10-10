using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Globalization;

using GetACSEvent.Language;

namespace GetACSEvent
{
    public partial class GetACSEvent : Form
    {
        public int m_UserID = -1;
        private string CsTemp = null;
        private int m_lLogNum = 0;
        private string MinorType = null;
        private string MajorType = null;
        public int m_lGetAcsEventHandle = -1;
        Thread m_pDisplayListThread = null;

        public List<LoginDetail> lstLoginDetail;

        public GetACSEvent()
        {
            InitializeComponent();
            CHCNetSDK.NET_DVR_Init();
            CHCNetSDK.NET_DVR_SetLogToFile(3, "./SdkLog/", true);
            comboBoxLanguage.SelectedIndex = 0;

            lstLoginDetail = new List<LoginDetail>
            {
                new LoginDetail("192.168.10.12", "admin", "lynd4Cam", "8000"),
                new LoginDetail("192.168.10.13", "admin", "lynd4Cam", "8000"),
                new LoginDetail("192.168.10.14", "admin", "lynd4Cam", "8000")
            };
        }

        private void Login(string deviceAdd, string userName, string password, string port)
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AddDevice deviceAdd = new AddDevice();
            deviceAdd.ShowDialog();
            m_UserID = deviceAdd.m_iUserID;
            deviceAdd.Dispose();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoginDetail now = lstLoginDetail[0];
            Login(now.DeviceAddress, now.UserName, now.Password, now.Port);
            if (m_UserID < 0) return;

            listViewEvent.Items.Clear();

            m_lLogNum = 0;
            CHCNetSDK.NET_DVR_ACS_EVENT_COND struCond = new CHCNetSDK.NET_DVR_ACS_EVENT_COND();
            struCond.Init();
            struCond.dwSize =(uint)Marshal.SizeOf(struCond);
            struCond.dwMajor = 2;
            struCond.dwMinor = 0;

            struCond.struStartTime.dwYear = dateTimeStart.Value.Year;
            struCond.struStartTime.dwMonth = dateTimeStart.Value.Month;
            struCond.struStartTime.dwDay = dateTimeStart.Value.Day;
            struCond.struStartTime.dwHour = dateTimeStart.Value.Hour;
            struCond.struStartTime.dwMinute = dateTimeStart.Value.Minute;
            struCond.struStartTime.dwSecond = dateTimeStart.Value.Second;

            struCond.struEndTime.dwYear = dateTimeEnd.Value.Year;
            struCond.struEndTime.dwMonth = dateTimeEnd.Value.Month;
            struCond.struEndTime.dwDay = dateTimeEnd.Value.Day;
            struCond.struEndTime.dwHour = dateTimeEnd.Value.Hour;
            struCond.struEndTime.dwMinute = dateTimeEnd.Value.Minute;
            struCond.struEndTime.dwSecond = dateTimeEnd.Value.Second;

            struCond.byPicEnable = 0;
            //struCond.wInductiveEventType = 65535;

            //struCond.dwBeginSerialNo = 0;
            //struCond.dwEndSerialNo = 0;

            uint dwSize = struCond.dwSize;
            IntPtr ptrCond = Marshal.AllocHGlobal((int)dwSize);
            Marshal.StructureToPtr(struCond, ptrCond, false);

            m_lGetAcsEventHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(m_UserID, CHCNetSDK.NET_DVR_GET_ACS_EVENT, ptrCond, (int)dwSize, null, IntPtr.Zero);
            if (-1 == m_lGetAcsEventHandle)
            {
                Marshal.FreeHGlobal(ptrCond);
                MessageBox.Show("NET_DVR_StartRemoteConfig FAIL, ERROR CODE" + CHCNetSDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                return;
            }

            m_pDisplayListThread = new Thread(ProcessEvent);
            m_pDisplayListThread.Start();
            Marshal.FreeHGlobal(ptrCond);
        }

        public void ProcessEvent()
        {
            int dwStatus = 0;
            Boolean Flag = true;
            CHCNetSDK.NET_DVR_ACS_EVENT_CFG struCFG = new CHCNetSDK.NET_DVR_ACS_EVENT_CFG();
            //struCFG.init();
            struCFG.dwSize = (uint)Marshal.SizeOf(struCFG);
            int dwOutBuffSize = (int)struCFG.dwSize;
            while (Flag)
            {
                dwStatus = CHCNetSDK.NET_DVR_GetNextRemoteConfig(m_lGetAcsEventHandle, ref struCFG, dwOutBuffSize);
                switch (dwStatus)
                {
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_SUCCESS://成功读取到数据，处理完本次数据后需调用next
                        this.Text += " x";
                        ProcessAcsEvent(ref struCFG, ref Flag);
                        break;
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:
                        Thread.Sleep(200);
                        break;
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_FAILED:
                        CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetAcsEventHandle);
                        MessageBox.Show("NET_SDK_GET_NEXT_STATUS_FAILED" + CHCNetSDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                        Flag = false;
                        break;
                    case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_FINISH:
                        CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetAcsEventHandle);
                        Flag = false;
                        break;
                    default:
                        MessageBox.Show("NET_SDK_GET_NEXT_STATUS_UNKOWN" + CHCNetSDK.NET_DVR_GetLastError().ToString(), "Error", MessageBoxButtons.OK);
                        Flag = false;
                        CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetAcsEventHandle);
                        break;
                }
            }
        }

        
        public delegate void ShowCardListThread(CHCNetSDK.NET_DVR_ACS_EVENT_CFG struCFG);

        public void ShowCardList(CHCNetSDK.NET_DVR_ACS_EVENT_CFG struCFG)
        {
            if (this.InvokeRequired)
            {
                Delegate delegateProc = new ShowCardListThread(AddAcsEventToList);
                this.BeginInvoke(delegateProc, struCFG);
            }
            else 
            {
                AddAcsEventToList(struCFG);
            }

        }

        private void ProcessAcsEvent(ref CHCNetSDK.NET_DVR_ACS_EVENT_CFG struCFG,ref bool flag)
        {
            try
            {
                ShowCardList(struCFG);
            }
            catch
            {
                MessageBox.Show("AddAcsEventToList Failed", "Error", MessageBoxButtons.OK);
                flag = false;
            }
        }
        private Boolean StrToByteArray(ref byte[] destination, string data)
        {
            if (data != "")
            {
                byte[] source = System.Text.Encoding.Default.GetBytes(data);
                if (source.Length > destination.Length)
                {
                    MessageBox.Show("The length of num is exceeding");
                    return false;
                }
                else
                {
                    for (int i = 0; i < source.Length; ++i)
                    {
                        destination[i] = source[i];
                    }
                    return true;
                }
            }
            return true;
        }

        private string GetStrLogTime(ref CHCNetSDK.NET_DVR_TIME time)
        {
            string res = time.dwYear.ToString() + ":" + time.dwMonth.ToString() + ":"
                + time.dwDay.ToString() + ":" + time.dwHour.ToString() + ":" + time.dwMinute.ToString()
                + ":" + time.dwSecond.ToString();
            return res;
        }


        private void AddAcsEventToList(CHCNetSDK.NET_DVR_ACS_EVENT_CFG struEventCfg)
        {
            //this.listViewEvent.BeginUpdate();
            ListViewItem Item = new ListViewItem();
            Item.Text = (++m_lLogNum).ToString();

            string LogTime = GetStrLogTime(ref struEventCfg.struTime);
            Item.SubItems.Add(LogTime);

            
            CsTemp = struEventCfg.struAcsEventInfo.dwCardReaderNo.ToString();
            Item.SubItems.Add(CsTemp);

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.dwDoorNo.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.dwVerifyNo.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.dwAlarmInNo.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.dwAlarmOutNo.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.dwCaseSensorNo.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.dwRs485No.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.dwMultiCardGroupNo.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.wAccessChannel.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.byDeviceNo.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.dwEmployeeNo.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.byDistractControlNo.ToString());

            Item.SubItems.Add(struEventCfg.struAcsEventInfo.wLocalControllerID.ToString());
          
            this.listViewEvent.Items.Add(Item);
            //this.listViewEvent.EndUpdate();
        }

        private void ProcessPicData(ref CHCNetSDK.NET_DVR_ACS_EVENT_CFG struEventCfg, string path)
        {
            if (struEventCfg.dwPicDataLen > 0 && struEventCfg.pPicData != IntPtr.Zero)
            {

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    int iLen = (int)struEventCfg.dwPicDataLen;
                    byte[] by = new byte[iLen];
                    Marshal.Copy(struEventCfg.pPicData, by, 0, iLen);
                    fs.Write(by, 0, iLen);
                    fs.Close();
                }
            }
        }

        private void GetACSEvent_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_UserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout_V30(m_UserID);
                m_UserID = -1;
            }
            CHCNetSDK.NET_DVR_Cleanup();
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLanguage.Text != null)
            {
                MultiLanguage.SetDefaultLanguage(comboBoxLanguage.Text);
                foreach (Form form in Application.OpenForms)
                {
                    MultiLanguage.LoadLanguage(form);
                }


                if (comboBoxLanguage.Text == "English")
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                }
                else if (comboBoxLanguage.Text == "Chinese")
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                }
            }
        }

    }
}
