using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
	class MachineNameFetcher
	{
		private static event EventHandler _machineNameChanged;
		private byte[] _machineNameBuffer = new byte[104857600];

		public MachineNameFetcher()
		{
			_machineNameChanged += OnMachineNameChanged;
			Console.WriteLine("Machine fetcher registered successfully.");
		}

		private void OnMachineNameChanged(object sender, EventArgs e) {}

		public string MachineName { get { return Environment.MachineName; } }

		protected void ChangeMachineName(string newName)
		{
			if (_machineNameChanged != null)
				_machineNameChanged(this, EventArgs.Empty);
		}
	}

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
			string machineName = new MachineNameFetcher().MachineName;
            ViewData["Message"] = "This is where kittens live: " + machineName;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact me please. And bring kittens.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
