using Microsoft.AspNetCore.Mvc;
using SanTsgBootcampProject.Data.Repositories.Interfaces;
using SanTsgBootcampProject.Domain.SharedConstants;
using SanTsgBootcampProject.Domain.Users;

namespace SanTsgBootcampProject.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            var users = _unitOfWork.Users.GetAll();
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
                _unitOfWork.Users.Add(user);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id.GetValueOrDefault() == 0)
                return NotFound();

            var user = _unitOfWork.Users.Get(id);

            if (user.Status == Status.Deleted)
            {
                TempData["WarningMessage"] = "You Cannot Edit Deleted Accounts";
                return RedirectToAction("Index", "User");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                User editedUser = _unitOfWork.Users.Get(user.Id);
                editedUser.Username = user.Username;
                editedUser.Password = user.Password;
                editedUser.EmailAddress = user.EmailAddress;
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var user = _unitOfWork.Users.Get(id);

            if (user == null)
                return NotFound();
            //if status already deleted then just returs message and redirect to the main page
            if (user.Status == Status.Deleted)
            {
                TempData["WarningMessage"] = "It is already deleted";
                return RedirectToAction("Index", "User");
            }

            return View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            var user = _unitOfWork.Users.Get(id);

            if (user == null)
                return NotFound();

            User editedUser = _unitOfWork.Users.Get(user.Id);
            editedUser.Status = Status.Deleted;
            _unitOfWork.Save();

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult ChanceStatus(int? id)
        {
            var user = _unitOfWork.Users.Get(id);

            if (user == null)
                return NotFound();

            if (user.Status == Status.Deleted)
            {
                TempData["WarningMessage"] = "Cannot chance Status because it is deleted";
                return RedirectToAction("Index", "User");
            }

            User editedUser = _unitOfWork.Users.Get(user.Id);
            editedUser.Status = _unitOfWork.Users.ChanceCurrentStatus(user);
            _unitOfWork.Save();
            return RedirectToAction("Index", "User");
        }

    }
}
