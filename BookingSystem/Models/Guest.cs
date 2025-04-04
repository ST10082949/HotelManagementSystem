namespace BookingSystem.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation Property: A guest can make multiple bookings
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
