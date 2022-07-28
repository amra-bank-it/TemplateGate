using Provider.Model;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Exceptions
{
    public class Errors
    {
        //public static void SentryException(Exception exception) 
        //{
        //    using (SentrySdk.Init(o =>
        //    {
        //        o.Dsn = GlobalContainer.settFields.SentryUrl;
        //        o.Debug = true;
        //        o.TracesSampleRate = 1.0;
        //    }))

        //        try
        //        {

        //            int rrrrrr = 0;

        //        }

        //        catch (Exception)
        //        {
        //            SentrySdk.CaptureException(new Exception("Возникла ошибка: " + exception.ToString()));
        //        }

        //    var transaction = SentrySdk.StartTransaction(

        //        "test-transaction-name",
        //        "test-transaction-operation"
        //        );

        //    var span = transaction.StartChild("test-child-operation");

        //    span.Finish();
        //    transaction.Finish();
        //}
    }
}
