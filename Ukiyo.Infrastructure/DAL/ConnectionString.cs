using System;
using System.Reflection;

namespace Ukiyo.Infrastructure.DAL
{
    public class ConnectionString
    {
        public ConnectionString()
            : this("localhost", 0, Assembly.GetEntryAssembly()?.GetName().Name)
        {
        }

        public ConnectionString(
            string host,
            ushort port,
            string dbname,
            string user = null,
            string password = null)
        {
            ValidateArguments(host, dbname);
            Host = host;
            Port = port;
            DbName = dbname;
            User = user;
            Password = password;
        }

        public string Host { get; set; }

        public ushort Port { get; set; }

        public string DbName { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            var str1 = $"Server={Host};Database={DbName};";
            if (Port > 0)
                str1 += $"Port={Port};";
            string str2;
            if (User == null)
            {
                str2 = str1 + "Integrated Security=true;";
            }
            else
            {
                str2 = str1 + $"User Id={User};";
                if (Password != null)
                    str2 += $"Password={Password};";
            }

            return str2;
        }

        private void ValidateArguments(string host, string dbname)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host), $"{nameof(host)} is null.");
            if (dbname == null)
                throw new ArgumentNullException(nameof(dbname), $"{nameof(dbname)} is null.");
            if (host.Trim() == string.Empty)
                throw new ArgumentException($"{nameof(host)} is empty.", nameof(host));
            if (dbname.Trim() == string.Empty)
                throw new ArgumentException($"{nameof(dbname)} is empty.", nameof(dbname));
        }
    }
}