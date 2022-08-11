using IBP.SDKGatewayLibrary;
using InterFaceEkassir.Setting.LoggerPlace;
using System;

namespace InterFaceEkassir.Setting.LoggerPlace
{
    public enum informationPrint
    {
        Trace
            , Release
    }
    public enum sourcePrint
    {
        Ekassir
            , Console
    }
}

namespace ComplexLogger
{
    public static class MeLogger
    {
        private static sourcePrint mode;
        private static informationPrint informationPrint;

        public static void Init(informationPrint _informationPrint, sourcePrint _inmode = sourcePrint.Ekassir)
        {
            mode = _inmode;
            informationPrint = _informationPrint;
        }
        public static void WriteMessage(string text, informationPrint typeInfo = informationPrint.Trace)
        {
            if (informationPrint == informationPrint.Release && typeInfo == informationPrint.Trace)
                return;

            switch (mode)
            {
                case sourcePrint.Ekassir:
                    {
                        Logger.Instance.WriteMessage(text, 1); return;

                        break;
                    }

                case sourcePrint.Console:
                    {
                        Console.WriteLine(text);
                        break;
                    }

            }
        }
    }
}
