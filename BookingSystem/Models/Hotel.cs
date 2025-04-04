using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Please enter the hotel name.")]
        public string HotelName { get; set; }

        [Required(ErrorMessage = "Please enter the hotel location.")]
        public string Location { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [Url(ErrorMessage = "Please enter a valid image URL.")]
        public string ImageUrl { get; set; }

        // Navigation Property: A hotel can have multiple rooms
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
