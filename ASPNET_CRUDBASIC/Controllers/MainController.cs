using Microsoft.AspNetCore.Mvc;
using ASPNET_CRUDBASIC.Data;
using ASPNET_CRUDBASIC.Models;
namespace ASPNET_CRUDBASIC.Controllers
{
    public class MainController : Controller

    {
        readonly ContactData _contactData = new ContactData();
        public IActionResult List()
        {
            //Contact List View
            var contactList = _contactData.ListUsers();
            return View(contactList);
        }
        public IActionResult SaveView()
        {
            // Save View
            return View();
        }
        [HttpPost]
        public IActionResult SaveView(ContactModel selectedUser)
        {
            try
            {
                var result = _contactData.SaveUser(selectedUser);
                bool statusResponse = result.StatusResult;
                string errorMessage = result.ErrorMessage;

                if (statusResponse)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while saving the user. Error: " + errorMessage;
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
                return View();
            }
        }

    }

}
