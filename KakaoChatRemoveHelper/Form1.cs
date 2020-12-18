﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using KakaoChatRemoveHelper.Properties;
using static WinApi.Winapi;

namespace KakaoChatRemoveHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private static int _setKey = Settings.Default.KeySetting;
        private static bool _IskeySetMode = false;
        private static bool _IsBindChatBoard = false;
        private static IntPtr _chatBoard;

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnHook();
        }

        private static IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            var key = Marshal.ReadInt32(lParam);

            if (code < 0 || wParam != (IntPtr)WM_KEYDOWN || key != _setKey)
                return CallNextHookEx(Hhook, code, (int)wParam, lParam);

            if (!_IsBindChatBoard)
                _chatBoard = FindKakaoChatBoard();

            if (_chatBoard == IntPtr.Zero)
                return CallNextHookEx(Hhook, code, (int)wParam, lParam);

            MouseRightClick();
            DeleteChat(_chatBoard);
            return (IntPtr)1;
        }

        private static void MouseRightClick()
        {
            mouse_event(0x8, 0, 0, 0, 0);
            Thread.Sleep(10);
            mouse_event(0x10, 0, 0, 0, 0);
        }

        private static IntPtr FindKakaoChatBoard()
        {
            var proc = Process.GetProcessesByName("KakaoTalk")[0];
            var main = proc.MainWindowHandle;
            return FindWindowEx(main, IntPtr.Zero, "EVA_VH_ListControl_Dblclk", null);
        }

        private static void DeleteChat(IntPtr chatBoard)
        {
            PostMessage(chatBoard, (IntPtr)0x07E9, (IntPtr)0x76, (IntPtr)0xD378C20);
            var deletePopUp = SearchWindow("EVA_Window_Dblclk", "");
            if (deletePopUp == IntPtr.Zero)
            {
                _IsBindChatBoard = false;
                return;
            }

            PostMessage(deletePopUp, (IntPtr)0x0201, (IntPtr)0x1, (IntPtr)0x0AE0042);
            PostMessage(deletePopUp, (IntPtr)0x0202, IntPtr.Zero, (IntPtr)0x0AE0042);
            _IsBindChatBoard = true;
        }

        private static IntPtr SearchWindow(string @class, string caption)
        {
            Thread.Sleep(20);
            const int width = 305;
            const int height = 199;
            var basic = IntPtr.Zero;
            while (true)
            {
                var a = FindWindowEx(IntPtr.Zero, basic, @class, caption);
                basic = a switch
                {
                    var x when x == IntPtr.Zero => GetWindow(basic, 2),
                    _ => a
                };
                GetWindowRect(basic, out var rect);
                if ((rect.Width, rect.Height) == (width, height))
                    return basic;

                if (basic == IntPtr.Zero)
                    return IntPtr.Zero;
            }
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
            if (!_IskeySetMode || code < 0 || wParam != (IntPtr)WM_KEYDOWN)
                return CallNextHookEx(Hhook, code, (int)wParam, lParam);

            _setKey = Marshal.ReadInt32(lParam);
            Settings.Default.KeySetting = _setKey;
            Settings.Default.Save();
            txt_KeyState.Text = new KeysConverter().ConvertToInvariantString(_setKey);
            lbl_KeyStateMessage.Text = @"설정이 완료되었습니다.";
            _IskeySetMode = false;
            return (IntPtr) 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyProc += HookProc;
            KeyProc += KeySetting;
            SetHook();
            txt_KeyState.Text = new KeysConverter().ConvertToInvariantString(_setKey);
        }
    }
}