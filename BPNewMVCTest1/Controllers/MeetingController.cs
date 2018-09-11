using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BPNewMVCTest1.Models;
using BPNewMVCTest1.ViewModels;
using BPNewMVCTest1Service.HttpService;
using BPNewMVCTest1Service.TokenDTService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BPNewMVCTest1.Controllers
{
    public class MeetingController : BaseController
    {
        private ITokenDTService _tokenDTService;
        public IConfiguration _configuration;
        private IHttpService _httpService;
        public MeetingController(ITokenDTService tokenDTService, IConfiguration configuration, IHttpService httpService)
                : base(tokenDTService, configuration)
        {
            _tokenDTService = tokenDTService;
            _configuration = configuration;
            _httpService = httpService;
        }
        // GET: Meetings
        public async Task<ActionResult> ViewMeetings()
        {
            HttpClient client = _httpService.GetHttpClientInstance();
            HttpResponseMessage response = await client.GetAsync(_httpService.GetBaseURL() + "meeting");

            List<MeetingModel> meetingList = new List<MeetingModel>();
            if (response.IsSuccessStatusCode)
            {
                meetingList = await response.Content.ReadAsAsync<List<MeetingModel>>();
            }

            return View(meetingList);
        }

        // GET: Meeting
        public ActionResult Index()
        {
            return View();
        }

        // GET: Meeting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Meeting/Create
        public async Task<ActionResult> Create()
        {

            HttpClient client = _httpService.GetHttpClientInstance();
            HttpResponseMessage response = await client.GetAsync(_httpService.GetBaseURL() + "meeting/CreateMeetingGet");

            var meetingViewModel = new MeetingViewModel();
            if (response.IsSuccessStatusCode)
            {
                meetingViewModel = await response.Content.ReadAsAsync<MeetingViewModel>();
            }

            
            return View("CreateMeeting", meetingViewModel);
        }

        // POST: Meeting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MeetingViewModel meetingViewModel)
        {
            try
            {
                // TODO: Add insert logic here

                HttpClient client = _httpService.GetHttpClientInstance();
                HttpResponseMessage response = await client.PostAsJsonAsync(_httpService.GetBaseURL() + "meeting", meetingViewModel);

                if (response.IsSuccessStatusCode)
                {
                    //TODO : Log status code
                }

                return RedirectToAction(nameof(ViewMeetings));
            }
            catch
            {
                return View();
            }
        }

        // GET: Meeting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Meeting/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Meeting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Meeting/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}