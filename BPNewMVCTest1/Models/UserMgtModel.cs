using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPNewMVCTest1.Models
{
    public class UserMgtModel
    {
    }

    public class UserReadModel
    {
        public int CustomUserId { get; set; }
        public string UserName { get; set; }
        public byte ReadStatus { get; set; }
    }
}
