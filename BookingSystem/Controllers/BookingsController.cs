using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Data;
using BookingSystem.Models;

namespace BookingSystem.Controllers
{
    public class BookingsController : Controller
    {
        private readonly BookingSystemContext _context;

        public BookingsController(BookingSystemContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookingSystemContext = _context.Booking.Include(b => b.Guest).Include(b => b.Hotel).Include(b => b.Room);
            return View(await bookingSystemContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Guest)
                .Include(b => b.Hotel)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "FullName");
            ViewData["HotelId"] = new SelectList(_context.Hotel, "HotelId", "HotelName");
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomType");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelId,RoomId,GuestId,CheckInDate,CheckOutDate")] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "FullName", booking.GuestId);
                ViewData["HotelId"] = new SelectList(_context.Hotel, "HotelId", "HotelName", booking.HotelId);
                ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomType", booking.RoomId);
                return View(booking);
            }

            // ✅ Check if room is available
            var existingBooking = await _context.Booking
                .Where(b => b.RoomId == booking.RoomId &&
                            b.CheckInDate < booking.CheckOutDate &&
                            booking.CheckInDate < b.CheckOutDate)
                .FirstOrDefaultAsync();

            if (existingBooking != null)
            {
                ModelState.AddModelError("", "The selected room is already booked for these dates.");
                ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "FullName", booking.GuestId);
                ViewData["HotelId"] = new SelectList(_context.Hotel, "HotelId", "HotelName", booking.HotelId);
                ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomType", booking.RoomId);
                return View(booking);
            }

            // ✅ If room is available, proceed with booking
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "GuestId", booking.GuestId);
            ViewData["HotelId"] = new SelectList(_context.Hotel, "HotelId", "HotelName", booking.HotelId);
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,HotelId,RoomId,GuestId,CheckInDate,CheckOutDate")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "GuestId", booking.GuestId);
            ViewData["HotelId"] = new SelectList(_context.Hotel, "HotelId", "HotelName", booking.HotelId);
            ViewData["RoomId"] = new SelectList(_context.Room, "RoomId", "RoomId", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Guest)
                .Include(b => b.Hotel)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingId == id);
        }
    }
}
