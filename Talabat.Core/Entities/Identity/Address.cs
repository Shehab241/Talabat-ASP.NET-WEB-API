﻿namespace Talabat.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string AppUserId { get; set; }
        public AppUser User { get; set; }
    }
}