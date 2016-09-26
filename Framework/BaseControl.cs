using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;

namespace MarkerMetro.UITest.Framework
{
	public abstract class BaseControl<T> where T : class
	{
		protected IApp app;

		protected BaseControl(IApp app)
		{
			this.app = app;
		}

		protected AndroidApp AndroidApp
		{
			get
			{
				return app as AndroidApp;
			}
		}

		protected iOSApp iOSApp
		{
			get
			{
				return app as iOSApp;
			}
		}

		protected Y Create<Y>(bool waitForProgress = true, TimeSpan? timeout = null) where Y : class
		{
			return Create(() => (Y)Activator.CreateInstance(typeof(Y), app), waitForProgress, timeout);
		}

		protected Y Create<Y>(Func<Y> instantiation, bool waitForProgress = true, TimeSpan? timeout = null) where Y : class
		{
			var newScreen = instantiation();
			(newScreen as BaseControl<Y>).WaitUntilReady(waitForProgress, timeout);
			return newScreen;
		}

		protected Y Create<Y>(TimeSpan timeout) where Y : class
		{
			return Create<Y>(true, timeout);
		}

		protected T CreateThis(bool waitForProgress = true)
		{
			return Create<T>(waitForProgress);
		}

		public virtual void WaitUntilReady(bool waitForProgress, TimeSpan? timeout) 
		{
			if (WaitForId != null)
				app.WaitForElement(WaitForId);

			if (waitForProgress)
			{
				if (ProgressIndicatorId != null)
				{
					WaitForProgress(timeout);
				}
			}
		}

		protected void WaitForProgress(TimeSpan? timeout = null)
		{
			app.WaitForNoElement(x => x.Id(ProgressIndicatorId), timeout: timeout, postTimeout: TimeSpan.FromMilliseconds(500), retryFrequency: TimeSpan.FromMilliseconds(60));
		}

		public virtual string WaitForId { get { return null;}}
		public virtual string ProgressIndicatorId { get { return null; } }



        public T With(Action<T> func)
        {
            func(this as T);
            return CreateThis();
        }

        public T WithBack(Action<T> func)
        {
            func(this as T);
			return Back();
        }

		public T Back()
		{
			app.Back();
			return CreateThis();
		}

    }
}

