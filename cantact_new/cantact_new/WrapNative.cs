using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace cantact_new
{
    /// <summary>
    /// 키보드 플래그
    /// </summary>
    [Flags]

    public enum KeyFlag
    {
        /// <summary>
        /// 키 누름
        /// </summary>
        KE_DOWN = 0,
        /// <summary>
        /// 확장 키
        /// </summary>
        KE__EXTENDEDKEY = 1,
        /// <summary>
        /// 키 복귀
        /// </summary>
        KE_UP =2
    }
    /// <summary>
    /// 마우스 플래그
    /// </summary>
    [Flags]
    
    public enum MouseFlag
    {
        /// <summary>
        /// 마우스 이동
        /// </summary>
        ME_MOVE = 1,
        /// <summary>
        /// 마우스 왼쪽버튼 누름
        /// </summary>
        ME_LEFTDOWN = 2,
        /// <summary>
        /// 마우스 왼쪽버튼 복귀
        /// </summary>
        ME_LEFTUP = 4,
        /// <summary>
        /// 마우스 오른쪽버튼 누름
        /// </summary>
        ME_RIGHTDOWN = 8,
        /// <summary>
        /// 마우스 오른쪽버튼 복귀
        /// </summary>
        ME_RIGHTUP = 0x10,
        /// <summary>
        /// 마우스 휠 누름
        /// </summary>
        ME_MIDDLEDOWN = 0x20,
        /// <summary>
        /// 마우스 휠 복귀
        /// </summary>
        ME_MIDDLEUP = 0x40,
        /// <summary>
        /// 마우스 휠 (미구현)
        /// </summary>
        ME_WHEEL = 0x800,
        /// <summary>
        /// 마우스 절대이동 (미구현)
        /// </summary>
        ME_ABSOULUTE = 8000
    }
    /// <summary>
    /// Win32 API를 이용한 래핑 클래스
    /// </summary>
    public static class WrapNative
    {
     
        [DllImport("user32.dll")]
        static extern void keybd_event(byte vk, byte scan, int flags, int extra);

        [DllImport("user32.dll")]
        static extern void mouse_event(int flag, int dx, int dy, int buttons, int extra);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point point);

        [DllImport("user32.dll")]
        static extern int SetCursorPos(int x, int y);
        /// <summary>
        /// 키 누름
        /// </summary>
        /// <param name="Keycode">키</param>
        public static void KeyDown(int Keycode)
        {
            keybd_event((byte)Keycode, 0, (int)KeyFlag.KE_DOWN, 0);
        }
        /// <summary>
        /// 키 복귀
        /// </summary>
        /// <param name="Keycode">키</param>
        public static void KeyUp(int Keycode)
        {
            keybd_event((byte)Keycode, 0, (int)KeyFlag.KE_UP, 0);
        }
        /// <summary>
        /// 마우스 좌표를 바꿈
        /// </summary>
        /// <param name="x">x좌표</param>
        /// <param name="y">y좌표</param>
        public static void Move(int x, int y)
        {
            SetCursorPos(x, y);
        }
        /// <summary>
        /// 마우스 좌표를 바꿈
        /// </summary>
        /// <param name="pt">포인트</param>
        public static void Move(Point pt)
        {
            SetCursorPos(pt.X, pt.Y);
        }
        /// <summary>
        /// 마우스 왼쪽버튼 누름
        /// </summary>
        public static void LeftDown()
        {
            mouse_event((int)MouseFlag.ME_LEFTDOWN, 0, 0, 0, 0);
        }
        /// <summary>
        /// 마우스 왼쪽버튼 복귀
        /// </summary>
        public static void LeftUp()
        {
            mouse_event((int)MouseFlag.ME_LEFTUP, 0, 0, 0, 0);
        }
        /// <summary>
        /// 마우스 오른쪽버튼 누름
        /// </summary>
        public static void RightDown()
        {
            mouse_event((int)MouseFlag.ME_RIGHTDOWN, 0, 0, 0, 0);
        }
        /// <summary>
        /// 마우스 오른쪽버튼 복귀
        /// </summary>
        public static void RightUp()
        {
            mouse_event((int)MouseFlag.ME_RIGHTUP, 0, 0, 0, 0);
        }
        /// <summary>
        /// 마우스 휠 누름
        /// </summary>
        public static void MiddleDown()
        {
            mouse_event((int)MouseFlag.ME_MIDDLEDOWN, 0, 0, 0, 0);
        }
        /// <summary>
        /// 마우스 휠 복귀
        /// </summary>
        public static void MiddleUp()
        {
            mouse_event((int)MouseFlag.ME_MIDDLEUP, 0, 0, 0, 0);
        }

        // 이 밑으로 마우스 휠 관련 매소드 작성해주시면 됩니다.
    }
}
