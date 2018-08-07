using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPNewMVCTest1.Models
{
    public class AuthTokenModel
    {
        public string Id { get; set; }
        public string Auth_Token { get; set; }
        public string Expires_In { get; set; }
    }
}
