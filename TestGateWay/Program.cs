using ComplexLogger;
using IBP.SDKGatewayLibrary;
using Provider;
using BusinessEkassir_sett;
using Provider.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using Provider.Exceptions;

namespace TestGateWay
{

    class Program
    {

        static void Main(string[] args)
        {
            for (int ttt = 100000; ttt < 100010; ttt++)
            {

                try
                {
                    mLogger.changeMode(L_Mode.TestPlatform);
                    mLogger.WriteMessage("Платеж:"+ ttt);
                    //User us = new User();
                    //us.oo();
                    //return;


                    mLogger.WriteMessage("нач:" + DateTime.Now.ToString());

                    GatewayCore gateway = new GatewayCore();
                    Context context = new Context();
                    Exception exception = new Exception();
                    context.Add(GlobalContainer.PaymentContext("PointName"), "TEST");
                    context.Add(GlobalContainer.PaymentContext("Value"), 11.00m);
                    context.Add(GlobalContainer.PaymentContext("Id"), Guid.NewGuid());
                    context.Add(GlobalContainer.PaymentContext("Serial"), ttt);
                    context.Add(GlobalContainer.PaymentContext("ServerTime"), DateTime.Now);
                    context.Add(GlobalContainer.PaymentContext("Account"), "1518000000001");
                    //context.Add(contextServices.PaymentContext("DebtElev"), "0");
                    //context.Add(contextServices.PaymentContext("typeBlocked"), "О");
                    //context.Add(contextServices.PaymentContext("Purpose"), "Блокируем потому что так надо было (тесты)");

                    Hashtable RFR = new Hashtable();
                    RFR.Add("TraceLog", "0");
                    RFR.Add("SentryUrl", @"https://932e9e7e53db416ca2502fb326160a2c@sentry.asar.studio/17");

                    //Тестируем работу модуля настройки шлюза
                    SettingManager settingM = new SettingManager();
                    settingM.InitSettingManadger("");

                    //Тестируем работу модуля инициализации шлюза
                    gateway.InitGateway(RFR);

                    //Тестируем работу проведения платежа
                    gateway.CheckAccount(ref context);


                    int rer = 0;
    
                    mLogger.WriteMessage("кон:" + DateTime.Now.ToString());

                }
                catch (Exception e)
                {
                    int rr = 0;
                }
            }
            Console.ReadKey();
        }
    }
}