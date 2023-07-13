using Microsoft.AspNetCore.Mvc;
using ControllerDemo.Models;

namespace ControllerDemo.Controllers
{
   //Controller
   [Controller]
   public class HomeController : Controller
   {
      //Action (method)
      [Route("/")]
      public ContentResult Home()
      {
         //return "Hello from Controller Index";
         //return new ContentResult() { Content = "Hello from Home page" , ContentType="text/json"};
         //return Content("Hello from Index");

         return Content("<h1>Welcome</h1> <h2>Hello from Index</h2>", "text/html");
      }

      [Route("book")]
      public IActionResult Index()
      {
         if (!Request.Query.ContainsKey("bookid"))
         {
            Response.StatusCode = 400;
            return Content("Book id must be supplied");
         }

         if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
         {
            Response.StatusCode = 400;
            return Content("Book id can't be empty");
         }

         int bookid = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
         if (bookid <= 0)
         {
            Response.StatusCode = 400;
            return Content("can't be <=0");
         }
         if (bookid > 1000)
         {
            Response.StatusCode = 400;
            return Content("Bookid should be > 1000");
         }
         //isLogging should be true
         if (Convert.ToBoolean(Request.Query["isloggedIn"]) == true)
         {
            Response.StatusCode = 401;
            return Content("user must be authenticated");
         }

         //return Content("All Okk...!!");
         return File("/files/sample.pdf", "application/pdf");
      }

      [Route("person")]
      public JsonResult Person()
      {
         Person person = new Person()
         { Id = Guid.NewGuid(), FirstName = "Aanchal", LastName = "Priya", Age = 25 };
         return Json(person);
      }

      [Route("file-download")]
      public VirtualFileResult FileDownload()
      {
         //it takes file from "wwwroot" folder present in project
         return new VirtualFileResult("/files/sample.pdf", "application/pdf");
      }

      [Route("file-download2")]
      public PhysicalFileResult FileDownload2()
      {
         //it takes file present in local machine
         return new PhysicalFileResult(@"D:\Dev\C# Training\ControllerDemo\wwwroot\files\sample.pdf", "application/pdf");
      }

      [Route("file-download3")]
      public FileContentResult FileDownload3()
      {
         byte[] bytes = System.IO.File.ReadAllBytes(@"D:\Dev\C# Training\ControllerDemo\wwwroot\files\sample.pdf");
         return File(bytes, "application/pdf");
      }

      [Route("about")]
      public string About()
      {
         return "About us page";
      }

      [Route("contact/{mobile:regex(^\\d{{10}}$)}")]
      public string Contact()
      {
         return "our Contact page";
      }
   }
}
