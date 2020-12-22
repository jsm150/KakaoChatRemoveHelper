using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using KakaoChatRemoveHelper.Properties;
using static WinApi.Winapi;

namespace KakaoChatRemoveHelper
{
    public static class kakaoChatRemove
    {
        private static bool _IsBindChatBoard;
        private static IntPtr _chatBoard;
        public static int Setupkey { get; set; } = Settings.Default.KeySetting;

        public static IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            var key = Marshal.ReadInt32(lParam);

            if (code < 0 || wParam != (IntPtr) WM_KEYDOWN || key != Setupkey)
                return CallNextHookEx(Hhook, code, (int) wParam, lParam);

            if (!_IsBindChatBoard)
                _chatBoard = FindKakaoChatBoard();

            if (_chatBoard == IntPtr.Zero)
                return CallNextHookEx(Hhook, code, (int) wParam, lParam);

            MouseRightClick();
            Thread.Sleep(5);
            DeleteChat(_chatBoard);
            return (IntPtr) 1;
        }

        private static void MouseRightClick()
        {
            mouse_event(0x8, 0, 0, 0, 0);
            Thread.Sleep(5);
            mouse_event(0x10, 0, 0, 0, 0);
        }

        private static IntPtr FindKakaoChatBoard()
        {
            var proc = Process.GetProcessesByName("KakaoTalk")[0];
            var main = proc.MainWindowHandle;
            return FindWindowEx(main, IntPtr.Zero, "EVA_VH_ListControl_Dblclk", null);
        }

        private static async Task DeleteChat(IntPtr chatBoard)
        {
            PostMessage(chatBoard, (IntPtr) 0x07E9, (IntPtr) 0x76, (IntPtr) 0xD378C20);
            var deletePopUp = await Task.Run(() => SearchPopUp("EVA_Window_Dblclk", "", (305, 199)));
            if (deletePopUp == IntPtr.Zero)
            {
                _IsBindChatBoard = false;
                return;
            }

            _IsBindChatBoard = true;

            PostMessage(deletePopUp, (IntPtr) 0x0201, (IntPtr) 0x1, (IntPtr) 0x0AE0042);
            Thread.Sleep(5);
            PostMessage(deletePopUp, (IntPtr) 0x0202, IntPtr.Zero, (IntPtr) 0x0AE0042);

            var errorPopUp = await Task.Run(() => SearchPopUp("EVA_Window_Dblclk", "", (230, 112), (237, 112)));
            if (errorPopUp == IntPtr.Zero)
                return;

            PostMessage(errorPopUp, (IntPtr) 0x0201, (IntPtr) 0x1, (IntPtr) 0x0570066);
            Thread.Sleep(5);
            PostMessage(errorPopUp, (IntPtr) 0x0202, IntPtr.Zero, (IntPtr) 0x0570066);
        }

        private static IntPtr SearchPopUp(string @class, string caption, params (int width, int height)[] size)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < 500)
            {
                var a = SearchPopUpLogic(@class, caption, size);
                if (a != IntPtr.Zero)
                {
                    sw.Stop();
                    return a;
                }
            }

            sw.Stop();
            return IntPtr.Zero;
        }

        private static IntPtr SearchPopUpLogic(string @class, string caption, (int width, int height)[] size)
        {
            var basic = IntPtr.Zero;
            while (true)
            {
                var a = FindWindowEx(IntPtr.Zero, basic, @class, caption);
                basic = a == IntPtr.Zero ? GetWindow(basic, 2) : a;

                GetWindowRect(basic, out var rect);
                if (size.Any(t => (rect.Width, rect.Height) == t))
                    return basic;

                if (basic == IntPtr.Zero)
                    return IntPtr.Zero;
            }
        }
    }
}