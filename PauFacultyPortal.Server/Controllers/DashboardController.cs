using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using PauFacultyPortal.Service;
using PauFacultyPortal.ViewModel;
using PauFacultyPortal.ViewModel.Dashboard;
using System.Security.Claims;

namespace PauFacultyPortal.Server.Controllers
{
    public class DashboardController:ApiController
    {
        DashboardService service = new DashboardService();

        [HttpGet]
        public List<DashboardViewModel> Get()
        {
            //ResponseModel response = new ResponseModel();
            //try
            //{
            var loginId = ((ClaimsIdentity)User.Identity).FindFirst("LoginID").Value; 
    
            List<DashboardViewModel> models = loginId==null? null: service.GetDashboardProfileInfo(loginId);

            //    response = new ResponseModel(models, true, "", null);
            //}
            //catch (Exception exception)
            //{

            //    response = new ResponseModel(null, false, "Error Found", exception);
            //}

            //return Ok(response);
            return models;
        }
    }
}