//----------------------------------------
using IBP.SDKGatewayLibrary;
using System.Collections;
using Provider.Model;

namespace Provider
{
    public static class DebugMode
    {
        public static bool ON;
    }
    public  class GatewayCore : GatewayCoreBase
    {

        public override void InitGateway(Hashtable settings)
        {
            // TODO: Выполнить инициализацию шлюза.
            GlobalContainer.ReadContext(settings);

            switch (GlobalContainer.settFields.TraceLog)
            {
                case 0:  break;
                case 1:  break;
                default: break;
            }        

        }    


        /// <summary>
        /// Выполнение стадий проверки у поставщика
        /// </summary>
        /// <param name="context"></param>
        public override void CheckAccount(ref Context context)
        {
            
            GlobalContainer.ReadContext(context);
            GlobalContainer.WriteContext(ref context);
        }
       

        public override void Process(ref Context context)
        {

        }


        /// <summary>
        /// Проверить состояние платежа.
        /// </summary>
        /// <param name="context">
        /// Контектс ядра.
        /// </param>
        public override void CheckProcessStatus(ref Context context)
        {

        }

        /// <summary>
        /// Отозвать платеж.
        /// </summary>
        /// <param name="context">
        /// Контекст ядра.
        /// </param>
        public override void RecallPayment(ref Context context)
        {
            // TODO: Получить из контектса ядра необходимые аргументы и проверить состояние платежа.
            // Например:
            // string paySystemNumber = ((int)context["PaymentContext.Payment.Serial"]).ToString();
            // Result result = gatewayApi.Recall(paySystemNumber);
            //
            // switch (result.AcceptStatus)
            // {
            //     case AcceptStatus.PayStatusAbandoning:
            //         // Платеж отзывается.
            //         context.Status = State.Recalling;
            //         context.Description = e.UnsuccessfulResponse.AcceptNote;
            //         return;
            //     default:
            //         // Неудачный отзыв.
            //         context.Status = State.Finalized;
            //         context.Description = e.UnsuccessfulResponse.AcceptNote;
            //         return;
            // }

            // Если не произошло исключений, то сообщить об успехе операции.
            //

            //context.Status = State.Rejected;
        }
        public override void CheckRecallStatus(ref Context context)
        {
            //CheckProcessStatus(ref context);
        }
        public override void Dispose() { }
        public override Hashtable SaveSettings()
        {
            return null;
        }

        public override bool CanRecallPayment(ref Context context)
        {

            return false;
        }

    }

}
namespace ComplexLogger
{
    public enum L_Mode
    {
        TestPlatform
            , Release
            , debugFULL
    }
    public static class mLogger
    {
        public static L_Mode mode;

        public static void changeMode(L_Mode inmode)
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