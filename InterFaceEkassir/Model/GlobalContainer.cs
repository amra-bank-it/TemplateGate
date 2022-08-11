using ComplexLogger;
using IBP.SDKGatewayLibrary;
using System;
using System.Collections;
using System.Reflection;


namespace Provider.Model
{
    public static class GlobalContainer
    {
        //Стандартные параметры сервера
        public static ServerFields srvFields { get; set; }
        public static ClientFields cliFields { get; set; }
        public static SettingFields settFields { get; set; }


        /// <summary>
        /// Очистка объектов контенейра
        /// </summary>
        public static void ClearContext()
        {
            MeLogger.WriteMessage("Очистка глобального контекста ");
            srvFields = new ServerFields();
            cliFields = new ClientFields();
            settFields = new SettingFields();
        }

        /// <summary>
        /// Чтения контекста и заполнение объектов в GlobalContainer
        /// </summary>
        /// <param name="inContext"></param>
        public static void ContextToGlobalContainer<T>(T inContext) where T : Hashtable
        {
            MeLogger.WriteMessage("Конвертация из контекста Екассира -> Глобальный контекст");

            var GlbConAll = typeof(GlobalContainer).GetProperties();

            //Обходим каждое свойство в классе GlobalContainer
            foreach (PropertyInfo propGlbCon in GlbConAll)
            {
                //ссылка на статичное свойство имеющая тип созданного экземпляра, либо нулл 
                var refPropGlbCon = propGlbCon.GetValue(propGlbCon);

                var typePropGlbCon = propGlbCon.PropertyType;

                //Если свойство равно нулл то создаем экземпляр объекта в статичном свойстве, для последующего заполнения, иначе передаем уже созданный экземпляр
                object outerPropertyValue = GetOuterPropertyValue(refPropGlbCon, typePropGlbCon);


                //Обходим каждое свойство, вложенного свойства класса GlobalContainer
                foreach (PropertyInfo propInnerObject in typePropGlbCon.GetProperties())
                {
                    object Name = null;


                    if (typeof(T) == typeof(Hashtable))
                    {
                        Name = propInnerObject.Name;
                    }
                    else
                    {
                        Name = PaymentContext(propInnerObject.Name);
                    }


                    var TextValue = inContext[Name.ToString()];

                    if (TextValue == null)
                        continue;




                    Object innerPropertyValue = null;
                    try
                    {
                        innerPropertyValue = Convert.ChangeType(TextValue, propInnerObject.PropertyType);
                    }
                    catch (Exception err)
                    {
                        MeLogger.WriteMessage($"???ERORR-Convert.ChangeType:ContextName:{Name} Value:{TextValue.ToString()}. {err.ToString()}");
                    }

                    propInnerObject.SetValue(outerPropertyValue, innerPropertyValue, null);
                }

                if (outerPropertyValue == null)
                    continue;
                propGlbCon.SetValue(typeof(GlobalContainer).GetProperties(), outerPropertyValue, null);
            }
        }


        private static object GetOuterPropertyValue(object refPropGlbCon, Type typePropGlbCon)
        {
            return refPropGlbCon == null ? typePropGlbCon.GetConstructor(new Type[] { }).Invoke(new object[] { }) : refPropGlbCon;
        }

        /// <summary>
        /// Запись в контекст значения объектов из GlobalContainer
        /// </summary>
        /// <param name="inContext"></param>
        public static void WriteContext(ref Context inContext)
        {
            MeLogger.WriteMessage("Конвертация из контекста Глобальный контекст -> Екассир");

            PropertyInfo[] propGC = typeof(GlobalContainer).GetProperties();
            foreach (PropertyInfo prop in propGC)
            {
                var refPropGlbCon = prop.GetValue(prop);

                var currentType = refPropGlbCon.GetType();
                ObjectToContext(currentType, ref inContext, refPropGlbCon);
            }
        }
        private static void ObjectToContext(Type inType, ref Context inContext, object refPropGlbCon)
        {
            PropertyInfo[] propCurrent = inType.GetProperties();

            foreach (PropertyInfo prop in propCurrent)
            {
                var Name = PaymentContext(prop.Name);
                var Value = prop.GetValue(refPropGlbCon);
                inContext[Name] = Value;
            }
        }

        public static string PaymentContext(string field)
        {

            if ("Account,Value,Id,Serial,Number,Total,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment." + field;
            else if ("Fee,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment.Comission";
            else if ("latitude,longitude,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Point[\"" + field + "\"]";
            else if ("PointName,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Point.Name";
            else if ("PointSerial,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Point.Serial";
            else if ("RealAccount,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Point.Account.RealAccount";
            else if ("AccountABS,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Point.Account";
            else if ("ServiceAlias,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment.Service.Alias";
            else if ("ServiceCount,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment.Service.Count";
            else if ("Service,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment.Service";
            else if ("AccountName,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Account.Name";
            else if ("ServerTime,".IndexOf(field + ",") >= 0)
                return "PaymentContext.Payment.ServerTime";
            else
                return "PaymentContext.Payment[\"" + field + "\"]";
        }
    }

}