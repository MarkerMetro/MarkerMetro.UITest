using System;
using NUnit.Framework;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace MarkerMetro.UITest.Framework
{
	public abstract class BaseControl<T> where T : class
	{
		protected AndroidApp app;

		protected BaseControl(AndroidApp app)
		{
			this.app = app;
		}

		protected Y CreateControl<Y>(bool waitForProgress = true, TimeSpan? timeout = null) where Y : class
		{
			return CreateScreen(() => (Y)Activator.CreateInstance(typeof(Y), app), waitForProgress, timeout);
		}

		protected Y CreateScreen<Y>(Func<Y> instantiation, bool waitForProgress = true, TimeSpan? timeout = null) where Y : class
		{
			var t = instantiation();
			WaitUntilReady(waitForProgress, timeout, t);
			return t;
		}

		protected Y CreateScreen<Y>(bool waitForProgress = true, TimeSpan? timeout = null) where Y : class
		{
			return CreateControl<Y>(waitForProgress, timeout);
		}

		protected Y CreateScreen<Y>(TimeSpan timeout) where Y : class
		{
			return CreateControl<Y>(true, timeout);
		}

		protected T CreateThis(bool waitForProgress = true)
		{
			return CreateControl<T>(waitForProgress);
		}

		protected void WaitUntilReady<Y>(bool waitForProgress, TimeSpan? timeout, Y t) where Y : class
		{
			if ((t as BaseControl<Y>).WaitForId != null)
				app.WaitForElement(WaitForId);

			if (waitForProgress)
			{
				if ((t as BaseControl<Y>).ProgressIndicatorId != null)
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
            return this as T;
        }

        public T WithBack(Action<T> func)
        {
            func(this as T);
            app.Back();
            return this as T;
        }

    }
}

