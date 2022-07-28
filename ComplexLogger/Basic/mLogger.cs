using IBP.SDKGatewayLibrary;
using System;


namespace ComplexLogger
{
    public enum L_Mode
    {
        TestPlatform
            ,Release
            ,debugFULL
    }
    public static class mLogger
    {
        public static L_Mode mode;

        public static void  changeMode(L_Mode inmode)
        {
            mode = inmode;
        }
        public static void WriteMessage(string text)
        {
            if (mode == L_Mode.debugFULL)
            {
                Logger.Instance.WriteMessage(text, 1); return;
            }
            Console.WriteLine(text);
        }
        public static void WriteMessageDBG(string text)
        {
            if (mode == L_Mode.debugFULL)
            {
                Logger.Instance.WriteMessage(text, 1); return;
            }
        }
    }
}