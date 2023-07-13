using System;

namespace SimpleAds.data
{
    public class Ad
    {
        public int Id { get; set; }
        public string? ListerName { get; set; }

        public DateTime ListingDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Listing { get; set; }

        public bool Delete { get; set; }
    }
}