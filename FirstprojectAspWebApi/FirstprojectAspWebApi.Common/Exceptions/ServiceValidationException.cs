using System;

namespace FirstprojectAspWebApi.Exceptions
{
    public class ServiceValidationException:FirstProjectAspWebapiException
    {
        public ServiceValidationException():base("Service Validation Exception")
        {

        }
        public ServiceValidationException(string msg):base(msg)
        {

        }
        public ServiceValidationException(int statusCode,string msg):base(statusCode,msg)
        {

        }
        public ServiceValidationException(string msg,Exception innerException):base(msg, innerException)
        {

        }
        public ServiceValidationException(int statusCode,string msg,Exception innerException):base(statusCode,msg,innerException)
        {

        }
    }
}
