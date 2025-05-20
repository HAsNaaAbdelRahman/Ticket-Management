using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ticket_management.BLL.Services.Contract;
using Ticket_Management.DAL.DTOs.Incoming;
using Ticket_Management.DAL.Model;
using Ticket_Management.Models;

namespace Ticket_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserService userService)
        {
            _logger = logger;
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginDto userLogin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(userLogin);
                }

                var user = _userService.Authenticate(userLogin.Email);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(userLogin.Email), "User not found. Try again or register.");
                    return View(userLogin);
                }

                var passwordHasher = new PasswordHasher<Customer>();
                var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, userLogin.Password);

                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    ModelState.AddModelError(nameof(userLogin.Password), "Invalid password. Try again.");
                    return View(userLogin);
                }

                if (user.Email == "admin@example.com")
                {
                    HttpContext.Session.SetString("IsAdmin", "true");
                    HttpContext.Session.SetInt32("AdminId", user.Id);
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else
                {
                    HttpContext.Session.SetString("IsAdmin", "false");
                    HttpContext.Session.SetInt32("CustomerId", user.Id);
                    return RedirectToAction("ClientDashboard", "Client");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login process.");
                ModelState.AddModelError(string.Empty, "Unexpected error occurred. Please try again later.");
                return View(userLogin);
            }
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(UserRegsiterDto newAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newAccount);
                }

                if (await _context.Customers.AnyAsync(c => c.Email == newAccount.Email))
                {
                    ModelState.AddModelError(nameof(newAccount.Email), "User with this email already exists.");
                    return View(newAccount);
                }

                var customer = new Customer
                {
                    FullName = newAccount.FullName,
                    Email = newAccount.Email,
                    MobileNumber = newAccount.MobileNumber,
                };
                var passwordHasher = new PasswordHasher<Customer>();
                customer.Password = passwordHasher.HashPassword(customer, newAccount.Password);

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error saving new customer");
                ModelState.AddModelError(string.Empty, "Unexpected error occurred. Please try again later.");
                return View(newAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during account creation.");
                ModelState.AddModelError(string.Empty, "Unexpected error occurred. Please try again later.");
                return View(newAccount);
            }
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
