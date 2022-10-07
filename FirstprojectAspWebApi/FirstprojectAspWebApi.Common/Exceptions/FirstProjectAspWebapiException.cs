using System;

namespace FirstprojectAspWebApi.Exceptions
{
    public class FirstProjectAspWebapiException:Exception
    {
        public int ErrorCode { get; set; }
        public FirstProjectAspWebapiException():base("First Project ASP Web API Exception")
        {

        }
        public FirstProjectAspWebapiException(string msg):base(msg)
        {

        }
        public FirstProjectAspWebapiException(int statusCode,string msg):base(msg)
        {
            ErrorCode = statusCode;
        }
        public FirstProjectAspWebapiException(string msg,Exception innerException):base(msg, innerException)
        {

        }
        public FirstProjectAspWebapiException(int statusCode,string msg,Exception innerException):base(msg, innerException)
        {
            ErrorCode=statusCode;
        }

    }
}
