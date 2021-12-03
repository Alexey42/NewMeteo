using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMeteoServer
{
    public class ServerRequestForms
    {
        public class AuthRequestForm
        {
            public string Name { get; set; }
            public string Password { get; set; }
            public string Type { get; set; }
        }

        public class MapRequestForm
        {
            public string Name { get; set; }

            public byte[] Bytes { get; set; }

            public double[,] Values { get; set; }

            //public string Type { get; set; }
        }
    }
}