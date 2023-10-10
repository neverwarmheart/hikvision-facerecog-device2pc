namespace GetACSEvent
{
    partial class GetACSEvent
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetACSEvent));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxMainType = new System.Windows.Forms.ComboBox();
            this.comboBoxSecondType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxEmployeeNo = new System.Windows.Forms.TextBox();
            this.listViewEvent = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 106);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(363, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 41);
            this.label1.TabIndex = 1;
            this.label1.Text = "Get ACS Event";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(22, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 57;
            this.label2.Text = "Major Type:";
            // 
            // comboBoxMainType
            // 
            this.comboBoxMainType.Enabled = false;
            this.comboBoxMainType.FormattingEnabled = true;
            this.comboBoxMainType.Items.AddRange(new object[] {
            "All",
            "Alarm",
            "Exception",
            "Operation",
            "Event"});
            this.comboBoxMainType.Location = new System.Drawing.Point(128, 229);
            this.comboBoxMainType.Name = "comboBoxMainType";
            this.comboBoxMainType.Size = new System.Drawing.Size(173, 22);
            this.comboBoxMainType.TabIndex = 59;
            this.comboBoxMainType.Text = "All";
            // 
            // comboBoxSecondType
            // 
            this.comboBoxSecondType.Enabled = false;
            this.comboBoxSecondType.FormattingEnabled = true;
            this.comboBoxSecondType.Items.AddRange(new object[] {
            "All",
            "4G_MOUDLE_OFFLINE",
            "4G_MOUDLE_ONLINE",
            "AC_OFF",
            "AC_RESUME",
            "ALARMIN_ARM",
            "ALARMIN_BROKEN_CIRCUIT",
            "ALARMIN_DISARM",
            "ALARMIN_EXCEPTION",
            "ALARMIN_RESUME",
            "ALARMIN_SHORT_CIRCUIT",
            "ALARMOUT_OFF",
            "ALARMOUT_ON",
            "ALWAYS_CLOSE_BEGIN",
            "ALWAYS_CLOSE_END",
            "ALWAYS_OPEN_BEGIN",
            "ALWAYS_OPEN_END",
            "ANTI_SNEAK_FAIL",
            "AUTH_PLAN_DORMANT_FAIL",
            "AUTO_COMPLEMENT_NUMBER",
            "AUTO_RENUMBER",
            "BATTERY_ELECTRIC_LOW",
            "BATTERY_ELECTRIC_RESUME",
            "BATTERY_RESUME",
            "CAMERA_NOT_CONNECT",
            "CAMERA_RESUME",
            "CAN_BUS_EXCEPTION",
            "CAN_BUS_RESUME",
            "CARD_AND_PSW_FAIL",
            "CARD_AND_PSW_OVER_TIME",
            "CARD_AND_PSW_PASS",
            "CARD_AND_PSW_TIMEOUT",
            "CARD_ENCRYPT_VERIFY_FAIL",
            "CARD_FINGERPRINT_PASSWD_VERIFY_FAIL",
            "CARD_FINGERPRINT_PASSWD_VERIFY_PASS",
            "CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT",
            "CARD_FINGERPRINT_VERIFY_FAIL",
            "CARD_FINGERPRINT_VERIFY_PASS",
            "CARD_FINGERPRINT_VERIFY_TIMEOUT",
            "CARD_INVALID_PERIOD",
            "CARD_MAX_AUTHENTICATE_FAIL",
            "CARD_NO_RIGHT",
            "CARD_OUT_OF_DATE",
            "CARD_PLATFORM_VERIFY",
            "CARD_READER_DESMANTLE_ALARM",
            "CARD_READER_DESMANTLE_RESUME",
            "CARD_READER_OFFLINE",
            "CARD_READER_RESUME",
            "CARD_RIGHT_INPUT",
            "CARD_RIGHT_OUTTPUT",
            "CASE_SENSOR_ALARM",
            "CASE_SENSOR_RESUME",
            "CHANNEL_CONTROLLER_DESMANTLE_ALARM",
            "CHANNEL_CONTROLLER_DESMANTLE_RESUME",
            "CHANNEL_CONTROLLER_FIRE_IMPORT_ALARM",
            "CHANNEL_CONTROLLER_FIRE_IMPORT_RESUME",
            "CHANNEL_CONTROLLER_OFF",
            "CHANNEL_CONTROLLER_RESUME",
            "CLIMBING_OVER_GATE",
            "COM_NOT_CONNECT",
            "COM_RESUME",
            "COMBINED_VERIFY_PASS",
            "COMBINED_VERIFY_TIMEOUT",
            "DEV_POWER_OFF",
            "DEV_POWER_ON",
            "DEVICE_NOT_AUTHORIZE",
            "DISTRACT_CONTROLLER_ALARM",
            "DISTRACT_CONTROLLER_OFFLINE",
            "DISTRACT_CONTROLLER_ONLINE",
            "DISTRACT_CONTROLLER_RESUME",
            "DOOR_BUTTON_PRESS",
            "DOOR_BUTTON_RELEASE",
            "DOOR_CLOSE_NORMAL",
            "DOOR_OPEN_ABNORMAL",
            "DOOR_OPEN_NORMAL",
            "DOOR_OPEN_OR_DORMANT_FAIL",
            "DOOR_OPEN_OR_DORMANT_LINKAGE_OPEN_FAIL",
            "DOOR_OPEN_OR_DORMANT_OPEN_FAIL",
            "DOOR_OPEN_TIMEOUT",
            "DOORBELL_RINGING",
            "DROP_ARM_BLOCK",
            "DROP_ARM_BLOCK_RESUME",
            "EMERGENCY_BUTTON_RESUME",
            "EMERGENCY_BUTTON_TRIGGER",
            "EMPLOYEE_NO_NOT_EXIST",
            "FACE_IMAGE_QUALITY_LOW",
            "FINGE_RPRINT_QUALITY_LOW",
            "FINGER_PRINT_MODULE_NOT_CONNECT",
            "FINGER_PRINT_MODULE_RESUME",
            "FINGERPRINT_COMPARE_FAIL",
            "FINGERPRINT_COMPARE_PASS",
            "FINGERPRINT_INEXISTENCE",
            "FINGERPRINT_PASSWD_VERIFY_FAIL",
            "FINGERPRINT_PASSWD_VERIFY_PASS",
            "FINGERPRINT_PASSWD_VERIFY_TIMEOUT",
            "FIRE_BUTTON_RESUME",
            "FIRE_BUTTON_TRIGGER",
            "FIRE_IMPORT_BROKEN_CIRCUIT",
            "FIRE_IMPORT_RESUME",
            "FLASH_ABNORMAL",
            "FORCE_ACCESS",
            "FREE_GATE_PASS_NOT_AUTH",
            "GATE_TEMPERATURE_OVERRUN",
            "HOST_DESMANTLE_ALARM",
            "HOST_DESMANTLE_RESUME",
            "ID_CARD_READER_NOT_CONNECT",
            "ID_CARD_READER_RESUME",
            "ILLEGAL_MESSAGE",
            "INDICATOR_LIGHT_OFF",
            "INDICATOR_LIGHT_RESUME",
            "INTERLOCK_DOOR_NOT_CLOSE",
            "INTRUSION_ALARM",
            "INVALID_CARD",
            "INVALID_MULTI_VERIFY_PERIOD",
            "IR_ADAPTOR_COMM_EXCEPTION",
            "IR_ADAPTOR_COMM_RESUME",
            "IR_EMITTER_EXCEPTION",
            "IR_EMITTER_RESUME",
            "LAMP_BOARD_COMM_EXCEPTION",
            "LAMP_BOARD_COMM_RESUME",
            "LEADER_CARD_OPEN_BEGIN",
            "LEADER_CARD_OPEN_END",
            "LEGAL_CARD_PASS",
            "LEGAL_EVENT_NEARLY_FULL",
            "LEGAL_MESSAGE",
            "LINKAGE_CAPTURE_PIC",
            "LOCAL_CONTROL_NET_BROKEN",
            "LOCAL_CONTROL_NET_RSUME",
            "LOCAL_CONTROL_OFFLINE",
            "LOCAL_CONTROL_RESUME",
            "LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN",
            "LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME",
            "LOCAL_FACE_MODELING_FAIL",
            "LOCAL_LOGIN_LOCK",
            "LOCAL_LOGIN_UNLOCK",
            "LOCAL_RESTORE_CFG",
            "LOCAL_UPGRADE",
            "LOCAL_USB_UPGRADE",
            "LOCK_CLOSE",
            "LOCK_OPEN",
            "LOW_BATTERY",
            "MAC_DETECT",
            "MAINTENANCE_BUTTON_RESUME",
            "MAINTENANCE_BUTTON_TRIGGER",
            "MASTER_RS485_LOOPNODE_BROKEN",
            "MASTER_RS485_LOOPNODE_RESUME",
            "MINOR_REMOTE_ARM",
            "MOD_GPRS_REPORT_PARAM",
            "MOD_NET_REPORT_CFG",
            "MOD_REPORT_GROUP_PARAM",
            "MOTOR_SENSOR_EXCEPTION",
            "MULTI_VERIFY_NEED_REMOTE_OPEN",
            "MULTI_VERIFY_REMOTE_RIGHT_FAIL",
            "MULTI_VERIFY_REPEAT_VERIFY",
            "MULTI_VERIFY_SUCCESS",
            "MULTI_VERIFY_SUPER_RIGHT_FAIL",
            "MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS",
            "MULTI_VERIFY_TIMEOUT",
            "NET_BROKEN",
            "NET_RESUME",
            "NORMAL_CFGFILE_INPUT",
            "NORMAL_CFGFILE_OUTTPUT",
            "NOT_BELONG_MULTI_GROUP",
            "NTP_CHECK_TIME",
            "OFFLINE_ECENT_NEARLY_FULL",
            "PASSING_TIMEOUT",
            "PASSWORD_MISMATCH",
            "PEOPLE_AND_ID_CARD_DEVICE_OFFLINE",
            "PEOPLE_AND_ID_CARD_DEVICE_ONLINE",
            "POS_END_ALARM",
            "POS_START_ALARM",
            "PRINTER_OFFLINE",
            "PRINTER_ONLINE",
            "PRINTER_OUT_OF_PAPER",
            "REMOTE_ACTUAL_GUARD",
            "REMOTE_ACTUAL_UNGUARD",
            "REMOTE_ALARMOUT_CLOSE_MAN",
            "REMOTE_ALARMOUT_OPEN_MAN",
            "REMOTE_ALWAYS_CLOSE",
            "REMOTE_ALWAYS_OPEN",
            "REMOTE_CAPTURE_PIC",
            "REMOTE_CFGFILE_INTPUT",
            "REMOTE_CFGFILE_OUTPUT",
            "REMOTE_CHECK_TIME",
            "REMOTE_CLEAR_CARD",
            "REMOTE_CLOSE_DOOR",
            "REMOTE_CONTROL_ALWAYS_OPEN_DOOR",
            "REMOTE_CONTROL_CLOSE_DOOR",
            "REMOTE_CONTROL_NOT_CODE_OPER_FAILED",
            "REMOTE_CONTROL_OPEN_DOOR",
            "REMOTE_DISARM",
            "REMOTE_HOUSEHOLD_CALL_LADDER",
            "REMOTE_LOGIN",
            "REMOTE_LOGOUT",
            "REMOTE_OPEN_DOOR",
            "REMOTE_REBOOT",
            "REMOTE_RESTORE_CFG",
            "REMOTE_UPGRADE",
            "REMOTE_VISITOR_CALL_LADDER",
            "REVERSE_ACCESS",
            "RS485_DEVICE_ABNORMAL",
            "RS485_DEVICE_REVERT",
            "SD_CARD_FULL",
            "SECURITY_MODULE_DESMANTLE_ALARM",
            "SECURITY_MODULE_DESMANTLE_RESUME",
            "SECURITY_MODULE_OFF",
            "SECURITY_MODULE_RESUME",
            "STAY_EVENT",
            "STRESS_ALARM",
            "SUBMARINEBACK_COMM_BREAK",
            "SUBMARINEBACK_COMM_RESUME",
            "SUBMARINEBACK_REPLY_FAIL",
            "TRAILING",
            "UNLOCK_PASSWORD_OPEN_DOOR",
            "VERIFY_MODE_MISMATCH",
            "WATCH_DOG_RESET"});
            this.comboBoxSecondType.Location = new System.Drawing.Point(128, 261);
            this.comboBoxSecondType.Name = "comboBoxSecondType";
            this.comboBoxSecondType.Size = new System.Drawing.Size(173, 22);
            this.comboBoxSecondType.TabIndex = 61;
            this.comboBoxSecondType.Text = "All";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(22, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 60;
            this.label3.Text = "Minor Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(373, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 64;
            this.label4.Text = "End Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(357, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 17);
            this.label5.TabIndex = 62;
            this.label5.Text = "Start Time:";
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(475, 229);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(173, 22);
            this.dateTimeStart.TabIndex = 66;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(475, 263);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(173, 22);
            this.dateTimeEnd.TabIndex = 67;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(905, 311);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 25);
            this.btnSearch.TabIndex = 68;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(708, 226);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 17);
            this.label8.TabIndex = 77;
            this.label8.Text = "Employee No:";
            // 
            // textBoxEmployeeNo
            // 
            this.textBoxEmployeeNo.Location = new System.Drawing.Point(822, 225);
            this.textBoxEmployeeNo.Name = "textBoxEmployeeNo";
            this.textBoxEmployeeNo.Size = new System.Drawing.Size(173, 22);
            this.textBoxEmployeeNo.TabIndex = 76;
            // 
            // listViewEvent
            // 
            this.listViewEvent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listViewEvent.FullRowSelect = true;
            this.listViewEvent.GridLines = true;
            this.listViewEvent.HideSelection = false;
            this.listViewEvent.Location = new System.Drawing.Point(11, 349);
            this.listViewEvent.Name = "listViewEvent";
            this.listViewEvent.Size = new System.Drawing.Size(984, 258);
            this.listViewEvent.TabIndex = 79;
            this.listViewEvent.UseCompatibleStateImageBehavior = false;
            this.listViewEvent.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No.";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Employee ID";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Employee Name";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Card No";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Time";
            this.columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Auth Location(IP)";
            this.columnHeader6.Width = 200;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(716, 264);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 17);
            this.label10.TabIndex = 82;
            this.label10.Text = "IP Address:";
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(822, 263);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(173, 22);
            this.textBoxIpAddress.TabIndex = 81;
            this.textBoxIpAddress.Text = "192.168.10.12";
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Items.AddRange(new object[] {
            "English",
            "Chinese"});
            this.comboBoxLanguage.Location = new System.Drawing.Point(906, 119);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(90, 22);
            this.comboBoxLanguage.TabIndex = 80;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.Enabled = false;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Location = new System.Drawing.Point(12, 112);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(103, 29);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "#";
            this.columnHeader7.Width = 100;
            // 
            // GetACSEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 611);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.textBoxIpAddress);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.listViewEvent);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxEmployeeNo);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dateTimeEnd);
            this.Controls.Add(this.dateTimeStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxSecondType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxMainType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GetACSEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACS Demo M2+";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetACSEvent_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxMainType;
        private System.Windows.Forms.ComboBox comboBoxSecondType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxEmployeeNo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ListView listViewEvent;
    }
}

