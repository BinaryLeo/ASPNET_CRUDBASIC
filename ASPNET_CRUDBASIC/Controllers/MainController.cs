using Microsoft.AspNetCore.Mvc;
using ASPNET_CRUDBASIC.Data;
using ASPNET_CRUDBASIC.Models;
namespace ASPNET_CRUDBASIC.Controllers
{
    public class MainController : Controller

    {
        readonly ContactData _contactData = new();
        public IActionResult List()
        {
            var contactList = _contactData.ListUsers();
            return View(contactList);
        }
        public IActionResult SaveView()
        {
            return View();
        }
      
        [HttpPost]
        public IActionResult SaveView(ContactModel selectedUser)
        {
            if (!ModelState.IsValid)
                return View();
            
            var (StatusResult, ErrorMessage) = _contactData.SaveUser(selectedUser);
            bool statusResponse = StatusResult;
            string errorMessage = ErrorMessage;
            try
            {

                if (statusResponse)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["ErrorMessage"] = "Error: " + errorMessage;
                    return View();
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error: " + errorMessage;
                return View();
            }
        }
        public IActionResult Edit(int UserId)
        {
            var selectedUser = _contactData.GetUserById(UserId);
            return View(selectedUser);
        }
        [HttpPost]
        public IActionResult Edit(ContactModel selectedUser)
        {
            if (!ModelState.IsValid)
                return View();
            var StatusResult = _contactData.UpdateUser(selectedUser);
            if(StatusResult)
                return RedirectToAction("List");
            else
             return View();
        }
        public IActionResult DeleteView(int UserId)
        {
            var selectedUser = _contactData.GetUserById(UserId);
            return View(selectedUser);
        }
        [HttpPost]
        public IActionResult DeleteView(ContactModel selectedUser)
        {
            var StatusResult = _contactData.DeleteUser(selectedUser.UserId);
            if (StatusResult)
                return RedirectToAction("List");
            else
                return View();
        }
    }

}
