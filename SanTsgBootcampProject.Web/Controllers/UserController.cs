using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SanTsgBootcampProject.Application.Interfaces;
using SanTsgBootcampProject.Data.Repositories.Interfaces;
using SanTsgBootcampProject.Domain.SharedConstants;
using SanTsgBootcampProject.Domain.Users;
using System.Threading.Tasks;

namespace SanTsgBootcampProject.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUnitOfWork unitOfWork, IUserService userService, ILogger<UserController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        public IActionResult Index()
        {
            var users = _unitOfWork.Users.GetAll();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation("New user will be created");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateUser(user);
                _logger.LogInformation("User Succesfully added");
                return RedirectToAction("Index");
            }
            _logger.LogInformation("User couldn't be created. Page reloded");
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
                _logger.LogInformation("Attempt to edit to deleted account with id {0}", user.Id);
                return RedirectToAction("Index", "User");
            }
            _logger.LogInformation("Attempt to edit user with id {0}", user.Id);
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
                _logger.LogInformation("user with id {0} edited", user.Id);
                return RedirectToAction("Index");
            }
            _logger.LogInformation("user with id {0} couldn't edited because of model error", user.Id);
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
                _logger.LogInformation("Attempt to delete already deleted user with id {0}", user.Id);
                return RedirectToAction("Index", "User");
            }
            _logger.LogInformation("Attempt to delete a user with id {0}", user.Id);
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
            _logger.LogInformation("user with id {0} deleted", user.Id);
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult ChanceStatus(int? id)
        {
            User user = _unitOfWork.Users.Get(id);

            if (user == null)
                return NotFound();

            if (user.Status == Status.Deleted)
            {
                TempData["WarningMessage"] = "Cannot chance Status because it is deleted";
                _logger.LogInformation("Attempt to chance status already deleted user with id {0}", user.Id);
                return RedirectToAction("Index", "User");
            }

            user.Status = _unitOfWork.Users.ChanceCurrentStatus(user);
            _unitOfWork.Save();
            _logger.LogInformation("user with id {0} status change", user.Id);
            return RedirectToAction("Index", "User");
        }

    }
}
