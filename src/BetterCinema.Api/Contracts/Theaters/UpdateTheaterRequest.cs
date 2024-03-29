﻿namespace BetterCinema.Api.Contracts.Theaters
{
    public class UpdateTheaterRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsConfirmed { get; set; }
    }
}
