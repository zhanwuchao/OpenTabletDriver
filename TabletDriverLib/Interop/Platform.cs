using System;
using NativeLib;
using TabletDriverLib.Interop.Cursor;
using TabletDriverLib.Interop.Display;
using TabletDriverLib.Interop.Keyboard;
using TabletDriverPlugin;

namespace TabletDriverLib.Interop
{
    public static class Platform
    {
        private static ICursorHandler _cursorHandler;
        public static ICursorHandler CursorHandler
        {
            get
            {
                if (_cursorHandler == null)
                {
                    if (PlatformInfo.IsWindows)
                        _cursorHandler = new WindowsCursorHandler();
                    else if (PlatformInfo.IsLinux)
                        _cursorHandler = new EvdevCursorHandler();
                    else if (PlatformInfo.IsOSX)
                        _cursorHandler = new MacOSCursorHandler();
                    else
                    {
                        Log.Write("CursorHandler", $"Failed to create a cursor handler for this platform ({Environment.OSVersion.Platform}).", true);
                        return null;
                    }
                }
                return _cursorHandler;
            }
        }

        private static IKeyboardHandler _keyboardHandler;
        public static IKeyboardHandler KeyboardHandler
        {
            get
            {
                if (_keyboardHandler == null)
                {
                    if (PlatformInfo.IsWindows)
                        _keyboardHandler = new WindowsKeyboardHandler();
                    else if (PlatformInfo.IsLinux)
                        _keyboardHandler = new EvdevKeyboardHandler();
                    else if (PlatformInfo.IsOSX)
                        _keyboardHandler = null;
                    else
                    {
                        Log.Write("KeyboardHandler", $"Failed to create a keyboard handler for this platform ({Environment.OSVersion.Platform}).", true);
                        return null;
                    }
                }
                return _keyboardHandler;
            }
        }

        public static IVirtualScreen VirtualScreen
        {
            get
            {
                if (PlatformInfo.IsWindows)
                    return new WindowsDisplay();
                else if (PlatformInfo.IsLinux)
                    return new XScreen();
                else if (PlatformInfo.IsOSX)
                    return new MacOSDisplay();

                Log.Write("Display Handler", $"Failed to create a display handler for this platform ({Environment.OSVersion.Platform}).", true);
                return null;
            }
        }
    }
}