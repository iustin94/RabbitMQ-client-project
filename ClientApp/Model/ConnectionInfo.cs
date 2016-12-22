﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class ConnectionInfo
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string VirtualHost { get; private set; }
        public int Port { get; private set; }
        public ConnectionInfo(string Username, string Password, string Virtualhost, int Port)
        {
            //TO-DO check credentials before setting them

            this.Username = Username;
            this.Password = Password;
            this.VirtualHost = Virtualhost;
            this.Port = Port;
        }
    }
}