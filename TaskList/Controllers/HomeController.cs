using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using TaskList.Business.Abstract;
using TaskList.Models.ViewModels;

namespace TaskList.Controllers
{
    public class HomeController : Controller
    {                                                               
        readonly ITaskService _taskService;                         
                                                                    
        public HomeController(ITaskService taskService)             
        {                                                           
            _taskService = taskService;                             
        }                                                           

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("LoginName") == null)
                return RedirectToAction("login", "account");

            DashboardContainer container = new DashboardContainer();
            container.UserTasks = _taskService.GetUsersTasks(new Guid(HttpContext.Session.GetString("LoginId")));
            container.AllTasks = _taskService.GetAllTasks();
            container.Reports = _taskService.GetReports();
            return View(container);
        }

      
    }

   
}
