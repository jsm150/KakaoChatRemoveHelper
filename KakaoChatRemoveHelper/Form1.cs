using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static WinApi.Winapi;

namespace KakaoChatRemoveHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyProc += HookProc;
            SetHook();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnHook();
        }

        private static IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            var key = Marshal.ReadInt32(lParam);

            if (code < 0 || wParam != (IntPtr)WM_KEYDOWN || key != (int)VKeys.VK_DELETE)
                return CallNextHookEx(Hhook, code, (int)wParam, lParam);

            var chatBoard = FindKakaoChatBoard();
            if (chatBoard == IntPtr.Zero)
                return CallNextHookEx(Hhook, code, (int)wParam, lParam);

            MouseRightClick();
            DeleteChat(chatBoard);
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
            var _proc = Process.GetProcessesByName("KakaoTalk")[0];
            var main = _proc.MainWindowHandle;
            return FindWindowEx(main, IntPtr.Zero, "EVA_VH_ListControl_Dblclk", null);
        }

        private static bool DeleteChat(IntPtr chatBoard)
        {
            PostMessage(chatBoard, (IntPtr)0x07E9, (IntPtr)0x76, (IntPtr)0xD378C20);
            var deletePopUp = SearchWindow("EVA_Window_Dblclk", "");

            PostMessage(deletePopUp, (IntPtr)0x0201, (IntPtr)0x1, (IntPtr)0x0AE0042);
            PostMessage(deletePopUp, (IntPtr)0x0202, IntPtr.Zero, (IntPtr)0x0AE0042);
            return true;
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
                GetWindowRect(basic, out var Rect);
                if ((Rect.Width, Rect.Height) == (width, height))
                    return basic;

                if (basic == IntPtr.Zero)
                    return IntPtr.Zero;
            }
        }
    }
}