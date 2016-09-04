using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace MarkerMetro.UITest.Framework
{
	public abstract class BaseScreen<T> : BaseControl<T> where T : class
	{
		protected BaseScreen(IApp app) : base(app)
		{
		}


		public T Screenshot(string title)
		{
			var shot = app.Screenshot(title);
			shot.CopyTo(TestContext.CurrentContext.Test.Name + " " + title + ".png", true);
			return this as T;
		}


	}
}

