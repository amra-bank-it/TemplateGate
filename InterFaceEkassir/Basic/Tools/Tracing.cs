using ComplexLogger;
using IBP.SDKGatewayLibrary;
using Provider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEkassir.Basic.Tools
{
    public static class Tracing
    {
        public static void Context(Context context, string methodName)
        {
            if (GlobalContainer.settFields.TraceLog == 1)
            {
                MeLogger.WriteMessage($"{methodName}.Print Context");
                foreach (var one in context.Keys)
                {
                    object NameKey = one;
                    object ValueKey = "";
                    try
                    {
                        ValueKey = context[one] == null ? "" : context[one].ToString();
                    }
                    catch (Exception err)
                    {
                        ValueKey = "Не удалось получить значение из контекста";
                    }
                    MeLogger.WriteMessage("Key:" + NameKey);
                    MeLogger.WriteMessage("Value:" + ValueKey);
                }
            }
        }

    }
}
