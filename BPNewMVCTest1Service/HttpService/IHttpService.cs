using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BPNewMVCTest1Service.HttpService
{
    public interface IHttpService
    {
        HttpClient GetHttpClientInstance();
        string GetBaseURL();
    }
}
