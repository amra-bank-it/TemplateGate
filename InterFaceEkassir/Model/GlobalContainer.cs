using ComplexLogger;
using IBP.SDKGatewayLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
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
            srvFields = new ServerFields();
            cliFields = new ClientFields();
            settFields = new SettingFields();
        }

        /// <summary>
        /// Чтения контекста и заполнение объектов в GlobalContainer
        /// </summary>
        /// <param name="inContext"></param>
        public static void ReadContext<T>(T inContext) where T : Hashtable
        {
            foreach(PropertyInfo propGlbCon in typeof(GlobalContainer).GetProperties())
            {
                object outerPropertyValue = propGlbCon.PropertyType.GetConstructor(new Type[] { }).Invoke(new object[] { });

                foreach (PropertyInfo propInnerObject in propGlbCon.PropertyType.GetProperties())
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

                    var TextValue = inContext[Name];

                    if (TextValue == null)
                        continue;


                    Object innerPropertyValue = Convert.ChangeType(TextValue, propInnerObject.PropertyType);

                    propInnerObject.SetValue(outerPropertyValue, innerPropertyValue, null);
                }
                propGlbCon.SetValue(typeof(GlobalContainer).GetProperties() , outerPropertyValue , null);
            }
        }

        /// <summary>
        /// Запись в контекст значения объектов из GlobalContainer
        /// </summary>
        /// <param name="inContext"></param>
        public static void WriteContext(ref Context inContext)
        {
            PropertyInfo[] propGC = typeof(GlobalContainer).GetProperties();
            foreach (PropertyInfo prop in propGC)
            {
                var currentType = prop.GetType();
                ObjectToContext(currentType, ref inContext);
            }
        }
        private static void ObjectToContext(Type inType, ref Context inContext)
        {
            PropertyInfo[] propCurrent = inType.GetProperties();

            foreach (PropertyInfo prop in propCurrent)
            {
                var Name = PaymentContext(prop.Name);
                var Value = prop.GetValue(prop);
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