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
		
        protected IApp app;
		Platform platform;


		protected virtual string ApkPath { get { throw new MissingFieldException(); } }

		protected virtual string BundlePath { get { throw new MissingFieldException(); } }

		protected virtual AppLaunchModeOptions AppLaunchMode { get { return AppLaunchModeOptions.EachTest;} }

		protected BaseTest(Platform platform)
		{
			this.platform = platform;
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
			if (platform == Platform.Android)
			{
				app = ConfigureApp
					.Android
					.ApkFile(ApkPath)
					.EnableLocalScreenshots()
					.StartApp();					

			}
			else {

				app = ConfigureApp
					.iOS
					.AppBundle(BundlePath)
					.EnableLocalScreenshots()
					.StartApp();
			}


		}

    }
}
