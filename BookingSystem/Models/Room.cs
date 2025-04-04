namespace BookingSystem.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
      //  public int HotelId { get; set; }
        public bool AvailabilityStatus { get; set; } // True for Available
                                                     // public Hotel Hotel { get; set; }
                                                     // Navigation Property: A room belongs to a hotel
        //public Hotel Hotel { get; set; }

        // Navigation Property: A room can have multiple bookings
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
