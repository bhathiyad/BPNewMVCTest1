using BPNewMVCTest1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BPNewMVCTest1.ViewModels
{
    public class MeetingViewModel
    {
        public int CategoryId { get; set; }
        public int CustomUserId { get; set; }
        public int MeetingTemplateId { get; set; }
        public int SubCategoryId { get; set; }
        public MeetingViewModel()
        {
            this.CategoryList = new List<CategoryModel>();
            this.OrganizerList = new List<UserReadModel>();
            this.MeetingVenueList = new List<MeetingTemplateModel>();
        }

        [Display(Name = "Category")]
        public List<CategoryModel> CategoryList { get; set; }
        [Display(Name = "SubCategory")]
        public List<SubCategoryModel> SubCategoryList { get; set; }
        [Display(Name = "Date (DD/MMM/YY) ")]
        public DateTime MeetingDate { get; set; }
        [Display(Name = "Start Time(HH/MM)")]
        public string MeetingStartTime { get; set; }
        [Display(Name = "End Time(HH/MM)")]
        public string MeetingEndTime { get; set; }
        [Display(Name ="Organizer")]
        public List<UserReadModel> OrganizerList { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Select a Venue Template")]
        public List<MeetingTemplateModel> MeetingVenueList { get; set; }
        [Display(Name = "Meeting At")]
        public string MeetingAt { get; set; }
        [Display(Name = "Meeting Room Name")]
        public string MeetingRoomName { get; set; }
        [Display(Name = "Geo Location")]
        public string GeoLocation { get; set; }
    }
}
