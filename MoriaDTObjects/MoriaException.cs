using System;

namespace MoriaDTObjects
{
    public class MoriaException: Exception
    {
        public MoriaException(string message, string username = ""): base(message)
        {
            Username = username;
        }

        public string Username
        {
            get;
            set;
        }
    }
}
