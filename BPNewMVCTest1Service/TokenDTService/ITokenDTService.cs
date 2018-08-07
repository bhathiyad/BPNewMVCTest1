using System;
using System.Collections.Generic;
using System.Text;

namespace BPNewMVCTest1Service.TokenDTService
{
    public interface ITokenDTService
    {
        string GetToken();
        void SetToken(string token);
        string GetURL();
        void SetURL(string url);
        
    }
}
