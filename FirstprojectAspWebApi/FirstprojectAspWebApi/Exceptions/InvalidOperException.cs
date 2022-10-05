using System;

namespace FirstprojectAspWebApi.Exceptions
{
    public class InvalidOperException:FirstProjectAspWebapiException
    {
        public InvalidOperException():base("Invalid Operation Exception")
        {

        }
        public InvalidOperException(string msg):base(msg)
        {

        }
        public InvalidOperException(int statusCode,string msg):base(statusCode,msg)
        {

        }
        public InvalidOperException(string msg,Exception innerException):base(msg, innerException)
        {

        }
        public InvalidOperException(int statusCode,string msg,Exception innerException):base(statusCode, msg,innerException)
        {

        }
    }
}
