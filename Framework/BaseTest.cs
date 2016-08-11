using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using System.Reflection;
using System.IO;

namespace MarkerMetro.UITest.Framework
{
    public abstract class BaseTest
    {
		protected enum AppLaunchModeOptions
		{
			EachTest,
			EachFixture,
			None
		}
		
        protected AndroidApp app;

		protected abstract string ApkName { get; }

		protected virtual AppLaunchModeOptions AppLaunchMode => AppLaunchModeOptions.EachTest;

		protected static string Decode(int[] array)
		{
			return string.Join("", array.Select(x => (char)x));
		}


		[SetUp]
        public virtual void BeforeEachTest()
        {
			if (AppLaunchMode == AppLaunchModeOptions.EachTest)
			{
				StartApplication();
			} 
        }

		[TestFixtureSetUp]
		public virtual void BeforeEachFixture()
		{
			if (AppLaunchMode == AppLaunchModeOptions.EachFixture)
			{
				StartApplication();
			}
		}

		protected void StartApplication()
		{
			string currentFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
			FileInfo fi = new FileInfo(currentFile);
			string dir = fi.Directory.Parent.Parent.Parent.FullName;

			app = ConfigureApp
				.Android
				.ApkFile(Path.Combine(dir, ApkName))
				.EnableLocalScreenshots()
				.StartApp();
		}





        
    }
}
