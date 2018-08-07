using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPNewMVCTest1.Models
{
    public class MeetingModel
    {
        public int MeetingId { get; set; }
        public DateTime? Date { get; set; }
        public string Subject { get; set; }
        public byte Status { get; set; }
        public CategoryModel CategoryModel { get; set; }
        public SubCategoryModel SubCategoryModel { get; set; }

        private string diplayStatus;

        public string DisplayStatus
        {
            get
            {
                switch (this.Status)
                {
                    case 0:
                        return "Pending";
                    case 1:
                        return "Next";
                    case 2:
                        return "--";
                    case 3:
                        return "--";
                    case 4:
                        return "Scheduled";
                    case 5:
                        return "Last";
                    default:
                        return "--";
                }
            }


            set { diplayStatus = value; }
        }

        private string _displayApproval;

        public string DisplayApproval
        {
            get
            {
                switch (this.Status)
                {
                    case 0:
                        return "Pending";
                    default:
                        return "Confirmed";
                }
            }
            set { _displayApproval = value; }
        }


    }
}
