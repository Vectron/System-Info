using System;
using System.Windows.Forms;

namespace System_Info.Controllers
{
	public interface IController : IDisposable
	{
		Panel GetPanel();
	}
}