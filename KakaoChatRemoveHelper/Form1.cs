using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KakaoChatRemoveHelper.Properties;
using static WinApi.Winapi;

namespace KakaoChatRemoveHelper
{
    public partial class Form1 : Form
    {
        private static bool _IskeySetMode;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyProc += kakaoChatRemove.HookProc;
            KeyProc += KeySetting;
            SetHook();
            txt_KeyState.Text = new KeysConverter().ConvertToInvariantString(kakaoChatRemove.DeleteKey);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnHook();
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Minimized:
                    notifyIcon1.Visible = true;
                    Hide();
                    break;
                case FormWindowState.Normal:
                    notifyIcon1.Visible = false;
                    ShowInTaskbar = true;
                    break;
                case FormWindowState.Maximized:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FormClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetFormSize_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void btn_KeySetting_Click(object sender, EventArgs e)
        {
            _IskeySetMode = true;
            lbl_KeyStateMessage.Text = @"설정할 키를 눌러주세요!";
        }

        private IntPtr KeySetting(int code, IntPtr wParam, IntPtr lParam)
        {
            if (!_IskeySetMode || code < 0 || wParam != (IntPtr) WM_KEYDOWN)
                return CallNextHookEx(Hhook, code, (int) wParam, lParam);

            kakaoChatRemove.DeleteKey = Marshal.ReadInt32(lParam);
            Settings.Default.KeySetting = kakaoChatRemove.DeleteKey;
            Settings.Default.Save();
            txt_KeyState.Text = new KeysConverter().ConvertToInvariantString(kakaoChatRemove.DeleteKey);
            lbl_KeyStateMessage.Text = @"설정이 완료되었습니다.";
            _IskeySetMode = false;
            return (IntPtr) 1;
        }
    }
}