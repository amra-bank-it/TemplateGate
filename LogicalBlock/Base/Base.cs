using Provider.Model;

namespace Provider.Interface
{
    internal class BusinessProcess : IBusinessProcess
    {

        Response IBusinessProcess.Check<T>(T inData)
        {
            throw new System.NotImplementedException();
        }


        Response IBusinessProcess.Pay<T>(T inData)
        {
            throw new System.NotImplementedException();
        }


        Response IBusinessProcess.State<T>(T inData)
        {
            throw new System.NotImplementedException();
        }
    }
}