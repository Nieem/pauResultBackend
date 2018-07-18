using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PauFacultyPortal.ViewModel
{
    public class ResponseModel
    {
        public ResponseModel(object data = null, bool isSuccess = false, string messege = "", Exception exception = null)
        {

            this.Data = data;
            this.Exception = exception;
            this.Message = messege;
            this.Exception = exception;
        }

        public Exception Exception { get; set; }

        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public object Data { get; set; }
    }
}
