using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;

namespace BJCBCPOS_Model
{
    /// <summary>
    /// Keyboard utility class use window api 
    /// </summary>
    public class KeyboardApi
    {
        /// <summary>
        /// load keyboard layout with specific locale
        /// </summary>
        /// <param name="pwszKLID"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "LoadKeyboardLayout",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern uint LoadKeyboardLayout(StringBuilder pwszKLID, uint flags);

        /// <summary>
        /// get current keyboard layout
        /// </summary>
        /// <param name="idThread"></param>
        /// <returns></returns>
        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "GetKeyboardLayout",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern uint GetKeyboardLayout(uint idThread);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hkl"></param>
        /// <param name="Flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "ActivateKeyboardLayout",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern uint ActivateKeyboardLayout(uint hkl, uint Flags);

        /// <summary>
        /// get current keyboard state (key down, key up, toggle)
        /// </summary>
        /// <param name="lpKeyState"></param>
        /// <returns></returns>
        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "GetKeyState",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern short GetKeyState(int vKeycode);

        /// <summary>
        /// get current keyboard state (key down, key up, toggle)
        /// </summary>
        /// <param name="lpKeyState"></param>
        /// <returns></returns>
        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "GetKeyboardState",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        /// <summary>
        /// set current keyboad state (key down, key up, toggle)
        /// </summary>
        /// <param name="lpKeyState"></param>
        /// <returns></returns>
        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "SetKeyboardState",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern bool SetKeyboardState(byte[] lpKeyState);

        /// <summary>
        /// get virtual key code
        /// </summary>
        /// <param name="uCode"></param>
        /// <param name="uMapType"></param>
        /// <param name="hkl"></param>
        /// <returns></returns>
        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "MapVirtualKeyEx",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern uint MapVirtualKeyEx(int uCode, uint uMapType, uint hkl);

        /// <summary>
        /// get unicode text from specific code and key state
        /// </summary>
        /// <param name="wVirtKey"></param>
        /// <param name="wScanCode"></param>
        /// <param name="lpKeyState"></param>
        /// <param name="pwszBuff"></param>
        /// <param name="cchBuff"></param>
        /// <param name="wFlags"></param>
        /// <param name="hkl"></param>
        /// <returns></returns>
        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "ToUnicodeEx",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern int ToUnicodeEx(uint wVirtKey, int wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, uint hkl);

        /// <summary>
        /// send user input (all type of user input keyboard, mouse)
        /// </summary>
        /// <param name="nInputs"></param>
        /// <param name="pInputs"></param>
        /// <param name="cbSize"></param>
        /// <returns></returns>
        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "SendInput",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode,
            EntryPoint = "GetMessageExtraInfo",
            SetLastError = true,
            ThrowOnUnmappableChar = false)]
        static extern IntPtr GetMessageExtraInfo();

        [StructLayout(LayoutKind.Explicit)]
        struct INPUT
        {
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public int type;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        [Flags]
        enum InputType
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 2
        }

        [Flags]
        enum MOUSEEVENTF
        {
            MOVE = 0x0001, /* mouse move */
            LEFTDOWN = 0x0002, /* left button down */
            LEFTUP = 0x0004, /* left button up */
            RIGHTDOWN = 0x0008, /* right button down */
            RIGHTUP = 0x0010, /* right button up */
            MIDDLEDOWN = 0x0020, /* middle button down */
            MIDDLEUP = 0x0040, /* middle button up */
            XDOWN = 0x0080, /* x button down */
            XUP = 0x0100, /* x button down */
            WHEEL = 0x0800, /* wheel button rolled */
            MOVE_NOCOALESCE = 0x2000, /* do not coalesce mouse moves */
            VIRTUALDESK = 0x4000, /* map to entire virtual desktop */
            ABSOLUTE = 0x8000 /* absolute move */
        }

        [Flags]
        enum KEYEVENTF
        {
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,
            UNICODE = 0x0004,
            SCANCODE = 0x0008,
        }

        private const uint FLAG_ACTIVATE = 0x00000001;
        private const uint FLAG_SETFORPROCESS = 0x00000100;
        private const uint MAPTYPE_SCANCODE_TO_VIRTUAL = 1u;
        private const uint MAPTYPE_VIRTUAL_TO_CHAR = 2u;
        private const short VK_SHIFT = 0x10;
        private const short VK_CAPITAL = 0x14;
        private const short VK_NUMLOCK = 0x90;

        private readonly uint hkl;

        public KeyboardApi(CultureInfo cultureInfo)
        {
            string current_culture = GetKeyboardLayout(this.hkl).ToString("x8");
            string layoutName = cultureInfo.LCID.ToString("x8");

            string current_lang = current_culture.Substring(current_culture.Length - 4, 4);
            string layout_lang = layoutName.Substring(layoutName.Length - 4, 4);

            if (current_lang != layout_lang)
            {
                var pwszKlid = new StringBuilder(layoutName);
                this.hkl = LoadKeyboardLayout(pwszKlid, FLAG_ACTIVATE);

                Activate();
            }
        }

        public uint Handle
        {
            get
            {
                return this.hkl;
            }
        }

        public void Activate()
        {
            ActivateKeyboardLayout(this.hkl, FLAG_SETFORPROCESS);
        }

        /// <summary>
        /// get current character in current keyboard layout of specific scancode 
        /// </summary>
        /// <param name="scan_code">keyboard scan code to find character</param>
        /// <param name="isCapital">indicate currently in capital mode (is hold shift button or capslock is on)</param>
        /// <returns></returns>
        public string GetCharacter(short scan_code, bool isCapital)
        {
            StringBuilder character_text = new StringBuilder();
            uint virtual_code = MapVirtualKeyEx(scan_code, MAPTYPE_SCANCODE_TO_VIRTUAL, hkl);
            byte[] bKeyState = new byte[256];
            //bool bKeyStateStatus = GetKeyboardState(bKeyState);
            //if (!bKeyStateStatus)
            //    return "";
            if (isCapital)
            {
                // set byte of virtual code capital(20) to 1
                bKeyState[20] = 1;
            }
            int result = ToUnicodeEx(virtual_code, scan_code, bKeyState, character_text, 5, 0u, hkl);
            if (result == 1)
            {
                return character_text.ToString();
            }
            return "";
        }

        public bool GetNumLockKeyIsActive()
        {
            return (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
        }

        public bool GetCapLockKeyIsActive()
        {
            return (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        }

        /// <summary>
        /// send command simulate keyboard press and release key
        /// </summary>
        /// <param name="scancode">scan code key for simulate</param>
        public void SendInput(short scancode)
        {
            INPUT[] Inputs = new INPUT[2];
            INPUT input = new INPUT();
            input.type = 1;
            input.ki.wScan = scancode;
            input.ki.dwFlags = (int)KEYEVENTF.SCANCODE;
            input.ki.dwExtraInfo = GetMessageExtraInfo();
            Inputs[0] = input;
            INPUT input2 = new INPUT();
            input2.type = 1;
            input2.ki.wScan = scancode;
            input2.ki.dwFlags = (int)KEYEVENTF.SCANCODE | (int)KEYEVENTF.KEYUP | (int)KEYEVENTF.UNICODE;
            input2.ki.dwExtraInfo = GetMessageExtraInfo();
            Inputs[1] = input2;
            SendInput(2, Inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        /// <summary>
        /// send command simulate keyboard press and release key of extended key
        /// </summary>
        /// <param name="scancode">scan code key for simulate</param>
        public void SendInputExtended(short scancode)
        {
            INPUT[] Inputs = new INPUT[2];
            INPUT input = new INPUT();
            input.type = 1;
            input.ki.wScan = scancode;
            input.ki.dwFlags = (int)KEYEVENTF.SCANCODE | (int)KEYEVENTF.EXTENDEDKEY;
            input.ki.dwExtraInfo = GetMessageExtraInfo();
            Inputs[0] = input;
            INPUT input2 = new INPUT();
            input2.type = 1;
            input2.ki.wScan = scancode;
            input2.ki.dwFlags = (int)KEYEVENTF.SCANCODE | (int)KEYEVENTF.KEYUP | (int)KEYEVENTF.EXTENDEDKEY;
            input2.ki.dwExtraInfo = GetMessageExtraInfo();
            Inputs[1] = input2;
            SendInput(2, Inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        /// <summary>
        /// send command simulate keyboard press and holding
        /// </summary>
        /// <param name="scancode">scan code key for simulate</param>
        public void SendInputHold(short scancode)
        {
            INPUT[] Inputs = new INPUT[1];
            INPUT input = new INPUT();
            input.type = 1;
            input.ki.wScan = scancode;
            input.ki.dwFlags = (int)KEYEVENTF.SCANCODE;
            input.ki.dwExtraInfo = GetMessageExtraInfo();
            Inputs[0] = input;
            SendInput(1, Inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        /// <summary>
        /// send command simulate keyboard release key
        /// </summary>
        /// <param name="scancode">scan code key for simulate</param>
        public void SendInputUnHold(short scancode)
        {
            INPUT[] Inputs = new INPUT[1];
            INPUT input = new INPUT();
            input.type = 1;
            input.ki.wScan = scancode;
            input.ki.dwFlags = (int)KEYEVENTF.SCANCODE | (int)KEYEVENTF.KEYUP | (int)KEYEVENTF.UNICODE;
            input.ki.dwExtraInfo = GetMessageExtraInfo();
            Inputs[0] = input;
            SendInput(1, Inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        /// <summary>
        /// reset key state of all key in keyboard
        /// </summary>
        /// <returns></returns>
        public bool ResetKeyboardState()
        {
            return SetKeyboardState(new byte[256]);
        }
    }

    public struct key
    {
        public short scancode;
        public bool extended;

        public key(short code, bool extend)
        {
            scancode = code;
            extended = extend;
        }

        public override int GetHashCode()
        {
            return scancode.GetHashCode() + extended.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            return obj is key && this == (key)obj;
        }

        public static bool operator ==(key x, key y)
        {
            return x.scancode == y.scancode && x.extended == y.extended;
        }

        public static bool operator !=(key x, key y)
        {
            return x.scancode != y.scancode || x.extended != y.extended;
        }

        public static key ESC = new key(0x01, false);
        public static key Tilde = new key(0x29, false);
        public static key One = new key(0x02, false);
        public static key Two = new key(0x03, false);
        public static key Three = new key(0x04, false);
        public static key Four = new key(0x05, false);
        public static key Five = new key(0x06, false);
        public static key Six = new key(0x07, false);
        public static key Seven = new key(0x08, false);
        public static key Eight = new key(0x09, false);
        public static key Nine = new key(0x0A, false);
        public static key Zero = new key(0x0B, false);
        public static key Hyphen = new key(0x0C, false);
        public static key Equal = new key(0x0D, false);
        public static key BackSpace = new key(0x0E, false);
        public static key Tab = new key(0x0F, false);
        public static key q = new key(0x10, false);
        public static key w = new key(0x11, false);
        public static key e = new key(0x12, false);
        public static key r = new key(0x13, false);
        public static key t = new key(0x14, false);
        public static key y = new key(0x15, false);
        public static key u = new key(0x16, false);
        public static key i = new key(0x17, false);
        public static key o = new key(0x18, false);
        public static key p = new key(0x19, false);
        public static key OpenBracket = new key(0x1A, false);
        public static key CloseBracket = new key(0x1B, false);
        public static key BackSlash = new key(0x2B, false);
        public static key Delete = new key(0x53, true);
        public static key CapsLock = new key(0x3A, false);
        public static key a = new key(0x1E, false);
        public static key s = new key(0x1F, false);
        public static key d = new key(0x20, false);
        public static key f = new key(0x21, false);
        public static key g = new key(0x22, false);
        public static key h = new key(0x23, false);
        public static key j = new key(0x24, false);
        public static key k = new key(0x25, false);
        public static key l = new key(0x26, false);
        public static key SemiColon = new key(0x27, false);
        public static key SingleQuote = new key(0x28, false);
        public static key Enter = new key(0x1C, false);
        public static key LeftShift = new key(0x2A, false);
        public static key z = new key(0x2C, false);
        public static key x = new key(0x2D, false);
        public static key c = new key(0x2E, false);
        public static key v = new key(0x2F, false);
        public static key b = new key(0x30, false);
        public static key n = new key(0x31, false);
        public static key m = new key(0x32, false);
        public static key Comma = new key(0x33, false);
        public static key Dot = new key(0x34, false);
        public static key Slash = new key(0x35, false);
        public static key Up = new key(0x48, true);
        public static key RightShift = new key(0x36, false);
        public static key Control = new key(0x1D, false);
        public static key NumPad = new key(0xFD, false);
        public static key Alternate = new key(0x38, false);
        public static key SpaceBar = new key(0x39, false);
        public static key Left = new key(0x4B, true);
        public static key Down = new key(0x50, true);
        public static key Right = new key(0x4D, true);
        public static key SwitchLanguage = new key(0xFF, false);
        public static key HideKeyboard = new key(0xFE, false);
        public static key NUM_0 = new key(0x52, false);
        public static key NUM_1 = new key(0x4F, false);
        public static key NUM_2 = new key(0x50, false);
        public static key NUM_3 = new key(0x51, false);
        public static key NUM_4 = new key(0x4B, false);
        public static key NUM_5 = new key(0x4C, false);
        public static key NUM_6 = new key(0x4D, false);
        public static key NUM_7 = new key(0x47, false);
        public static key NUM_8 = new key(0x48, false);
        public static key NUM_9 = new key(0x49, false);
        public static key NUM_PLUS = new key(0x4E, false);
        public static key NUM_MINUS = new key(0x4A, false);
        public static key NUM_MUTIPLY = new key(0x37, false);
        public static key NUM_DIVIDE = new key(0x35, true);
        public static key NUM_DOT = new key(0x53, false);
        public static key NUMLOCK = new key(0x45, true);
    }
}
