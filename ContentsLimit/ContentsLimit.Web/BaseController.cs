using ContentsLimit.Business;
using Microsoft.AspNetCore.Mvc;

namespace ContentsLimit.Web
{
	public abstract class BaseController : Controller
	{

		protected readonly DataContext Db;

		protected BaseController(DataContext db)
		{
			Db = db;
		}


		/// <summary>Use for sending a message to another screen via TempData.</summary>
		/// <param name="type"></param>
		/// <param name="message"></param>
		/// <param name="htmlEncoding">Default is true. Set to false when sending HTML content such as a hyperlink as part of the message.</param>
		protected void AddMessage(MessageType type, string message, bool htmlEncoding = true)
		{
			TempData["Message"] = message;
			TempData["MessageHtmlEncoding"] = htmlEncoding;
			string cssClass;
			switch (type)
			{
				case MessageType.Success:
					cssClass = "alert-success";
					break;
				case MessageType.Error:
					cssClass = "alert-danger";
					break;
				case MessageType.Information:
					cssClass = "alert-info";
					break;
				case MessageType.Warning:
					cssClass = "alert-warning";
					break;
				default:
					cssClass = "alert-danger";
					break;
			}
			TempData["MessageClass"] = cssClass;
		}
	}
}
