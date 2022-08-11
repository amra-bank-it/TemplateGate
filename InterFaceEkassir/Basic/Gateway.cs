//----------------------------------------
using IBP.SDKGatewayLibrary;
using System.Collections;
using Provider.Model;
using ComplexLogger;
using System.Reflection;
using InterFaceEkassir.Setting.LoggerPlace;

namespace Provider
{
    public class GatewayCore : GatewayCoreBase
    {

        public override void InitGateway(Hashtable settings)
        {
            // TODO: Выполнить инициализацию шлюза.
            GlobalContainer.ContextToGlobalContainer(settings);

            switch (GlobalContainer.settFields.TraceLog)
            {
                case 1: MeLogger.Init(informationPrint.Trace); break;
                default: MeLogger.Init(informationPrint.Release); break;
            }
        }

        public void InitGateway(Hashtable settings, sourcePrint prints = sourcePrint.Ekassir)
        {
            // TODO: Выполнить инициализацию шлюза.
            MeLogger.Init(informationPrint.Trace, prints);
            GlobalContainer.ContextToGlobalContainer(settings);

            switch (GlobalContainer.settFields.TraceLog)
            {
                case 1: MeLogger.Init(informationPrint.Trace, prints); break;
                default: MeLogger.Init(informationPrint.Release, prints); break;
            }
        }

        /// <summary>
        /// Выполнение стадий проверки у поставщика
        /// </summary>
        /// <param name="context"></param>
        public override void CheckAccount(ref Context context)
        {

            Tracing.Context(context, MethodBase.GetCurrentMethod().Name);

            GlobalContainer.ContextToGlobalContainer(context);

            TracingAccInRealese(MethodBase.GetCurrentMethod().Name);




            try
            {
                
            }
            catch (Exception err)
            {
                context.Description = "" + err.ToString();
                context.Status = State.AccountNotExists;
                TracingAccInRealese(MethodBase.GetCurrentMethod().Name, context);
                GlobalContainer.WriteContext(ref context);
                return;
            }

            GlobalContainer.WriteContext(ref context);
            context.Description = "OK";
            context.Status = State.AccountExists;
            TracingAccInRealese(MethodBase.GetCurrentMethod().Name, context);
        }

        private static void TracingAccInRealese(string typeMethod)
        {
            MeLogger.WriteMessage($"Стадия Проверки Аккаунта {GlobalContainer.srvFields.Account}", informationPrint.Release);
        }

        private static void TracingAccInRealese(string typeMethod, Context context)
        {
            MeLogger.WriteMessage($"Результат Стадии Проверки Аккаунта {GlobalContainer.srvFields.Account}:{context.Status.ToString()}/Описание:{context.Description}", informationPrint.Release);
        }

        public override void Process(ref Context context)
        {
            Tracing.Context(context, "Process");

            GlobalContainer.ContextToGlobalContainer(context);

            TracingAccInRealese(MethodBase.GetCurrentMethod().Name);

            
            try
            {
                
            }
            catch (Exception err)
            {
                context.Description = "" + err.ToString();
                context.Status = State.Rejected;
                TracingAccInRealese(MethodBase.GetCurrentMethod().Name, context);
                GlobalContainer.WriteContext(ref context);
                return;
            }


            GlobalContainer.WriteContext(ref context);
            context.Description = "OK";
            context.Status = State.Finalized;
            TracingAccInRealese(MethodBase.GetCurrentMethod().Name, context);

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