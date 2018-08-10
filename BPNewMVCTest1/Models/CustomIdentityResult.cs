using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPNewMVCTest1.Models
{
    public class CustomIdentityResult
    {
        [JsonProperty]  // Causes the protected setter to be called on deserialization.
        public bool Succeeded { get; protected set; }
        public List<IdentityError> Errors { get; }
    }
}
