using IBP.SDKGatewayLibrary;
using Provider;
using Provider.Model;
using System;
using System.Reflection;
using System.Linq;

namespace BusinessEkassir_sett
{
    /// <summary>
    /// Меенеджер настроек.
    /// </summary>
    /// <remarks>
    /// Сообщает ядру PaySystem Server список объектов, которые требуются шлюзу для выполнения различных операций.
    /// </remarks>
    public class SettingManager : SettingManagerBase
    {
        private string[] parametrGateway;
        private string[] argsCheckStage;
        private string[] argsProcessStage;
        private string[] argsCheckProcessStatusStage;

        private string[] argsSaveCheckStage;
        private string[] argsSaveProcessStage;
        private string[] argsSaveProcessStatusStage;

        /// <summary>
        /// Инициализация менеджера настроек.
        /// </summary>
        /// <param name="initString">
        /// Строка инициализации. Содержит значение атрибута initsettstringN настроек шлюза SDK.
        /// </param>
        /// <remarks>
        /// Этот метод вызывается ядром сразу после создания экземпляра класса. 
        /// </remarks>

        public override void InitSettingManadger(string initString)
        {
            //Logger.Instance.WriteMessage("Инициализация  Шлюза", 1);
            // TODO: Заполнить массив строк именами атрибутов конфигурации, необходимых для работы шлюза.
            // Доступ к метабазе екасира

            this.parametrGateway = GetFieldsObject(typeof(SettingFields));

            string[] cliFields = GetFieldsObject(typeof(ClientFields));
            string[] srvFields = GetFieldsObject(typeof(ServerFields));
            string[] cli2srvFields = cliFields.Concat(srvFields).ToArray();

            this.argsCheckStage = cli2srvFields;
            this.argsProcessStage = cli2srvFields;
            this.argsCheckProcessStatusStage = cli2srvFields;

            //
            this.argsSaveCheckStage = argsCheckStage;
            this.argsSaveProcessStage = argsProcessStage;
            this.argsSaveProcessStatusStage = argsCheckProcessStatusStage;
            //

        }


        /// <summary>
        /// Получить список атрибутов конфигурации, необходимых для работы шлюза.
        /// </summary>
        /// <returns>
        /// Список атрибутов конфигурации.
        /// </returns>
        public override string[] GetSettingsKey()
        {
            // TODO: Заполнить массив строк именами атрибутов конфигурации, необходимых для работы шлюза.
            //
            //Logger.Instance.WriteMessage("Получаем переменные для настройки шлюза.", 1);
            return this.parametrGateway;
        }

        /// <summary>
        /// Получить список аргументов контекста, необходимых для выполнения каждой из операции шлюза.
        /// </summary>
        /// <param name="operation">
        /// Операция, для которой запрашивается список аргументов контекста.
        /// </param>
        /// <returns>
        /// Список аргументов контекста для каждой из операции.
        /// </returns>
        public override string[] GetPaymentContextKeys(Operation operation)
        {
            string[] contextService;
            //Logger.Instance.WriteMessage("Получаем переменные для работы Контекста. Тип Операции:" + operation.ToString(), 1);

            switch (operation)
            {
                case Operation.CheckAccount:
                    contextService = this.argsCheckStage;
                    break;

                case Operation.Process:                                            
                    contextService = this.argsProcessStage;
                    break;

                case Operation.CheckProcessStatus:
                    contextService = this.argsCheckProcessStatusStage;
                    break;

                default:
                    return null;
            }


            return contextService;
        }

        /// <summary>
        /// Установить список аргументов контекста, в которых требуется сохранить данные после выполнения операции шлюза.
        /// </summary>
        /// <param name="operation">
        /// Операция, для которой требуется сохранение аргументов.
        /// </param>
        /// <returns>
        /// Список названий аргументов.
        /// </returns>
        public override string[] SetPaymentContextKeys(Operation operation)
        {
            // TODO: Используя параметр operation определить операцию, для которой требуется
            // сохранить список аргументов контекста в ядре, заполнить массив именами необходимых
            // аргументов и вернуть массив ядру.
            string[] contextService;

            //Logger.Instance.WriteMessage("Получаем переменные для  сохранение в Контекст. Тип Операции:" + operation.ToString(), 1);
            switch (operation)
            {
                case Operation.CheckAccount:                    

                    contextService = this.argsSaveCheckStage;
                    break;

                case Operation.Process:                    

                    contextService = this.argsSaveProcessStage;
                    break;

                case Operation.CheckProcessStatus:

                    contextService = this.argsSaveProcessStatusStage;
                    break;

                default:
                    return null;
            }



            return contextService;
        }

        public override string[] SaveSettingKey()
        {
            return null; //SetFilterContextKeys
        }

        public override string[] GetFilterContextKeys()
        {
            return null;
        }
        private string[] GetFieldsObject(Type inType)
        {
            string[] outTXT = new string[0];
            PropertyInfo[] propGC = inType.GetProperties();
            foreach (PropertyInfo prop in propGC)
            {
                var Name = prop.Name;
                Array.Resize(ref outTXT, outTXT.Length + 1);
                outTXT[outTXT.GetUpperBound(0)] = Name;
            }
            return outTXT;
        }

    }
}
