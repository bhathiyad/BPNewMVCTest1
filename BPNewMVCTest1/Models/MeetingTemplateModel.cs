using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPNewMVCTest1.Models
{
    public class MeetingTemplateModel
    {
        public int MeetingTemplateId { get; set; }

        public string TemplateName { get; set; }

        public string Address { get; set; }

        public string Venue { get; set; }

        public string GeoLocation { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public void SetGeoLocation()
        {
            //string[] splits = GeoLocation.Split(DELIMITER);

            //this.Latitude = double.Parse(splits[0], CultureInfo.InvariantCulture);
            //this.Longitude = double.Parse(splits[1], CultureInfo.InvariantCulture);
        }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public byte Status { get; set; }
    }
}
