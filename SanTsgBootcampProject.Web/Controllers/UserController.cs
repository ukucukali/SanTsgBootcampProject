using Microsoft.AspNetCore.Mvc;
using SanTsgBootcampProject.Data;
using SanTsgBootcampProject.Domain.SharedConstants;
using SanTsgBootcampProject.Domain.Users;
using System.Linq;

namespace SanTsgBootcampProject.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id.GetValueOrDefault() == 0)
                return NotFound();

            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();
            if (user.Status == Status.Deleted)
            {
                TempData["WarningMessage"] = "It is already deleted";
                return RedirectToAction("Index", "User");
            }

            user.Status = Status.Deleted;
            _context.Users.Update(user);
            _context.SaveChanges();

            return View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();

            user.Status = Status.Deleted;
            _context.Users.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult ChanceStatus(int? id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();

            if (user.Status == Status.Deleted)
            {
                TempData["WarningMessage"] = "Cannot chance Status because it is deleted";
                return RedirectToAction("Index", "User");
            }

            user.Status = user.Status == Status.Active ? Status.Deactive : Status.Active;
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "User");
        }

    }
}
