using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using PauFacultyPortal.Service;
using PauFacultyPortal.ViewModel;
using PauFacultyPortal.ViewModel.Dashboard;


namespace PauFacultyPortal.Server.Controllers
{
    public class DashboardController:ApiController
    {
        DashboardService service = new DashboardService();

        [HttpGet]
        public IHttpActionResult Get(string LoginId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                List<DashboardViewModel> models = service.GetDashboardProfileInfo(LoginId);
                response = new ResponseModel(models, true, "", null);
            }
            catch (Exception exception)
            {

                response = new ResponseModel(null, false, "Error Found", exception);
            }

            return Ok(response);
        }
    }
}