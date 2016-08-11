using System;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MarkerMetro.UITest.Framework
{
	public static class AppExtensions
    {
        public static void TapAndEnterText(this IApp app, string marked, string text)
        {
            app.Tap(marked);
            app.EnterText(marked, text);
        }

        public static void TapAndEnterText(this IApp app, Func<AppQuery, AppQuery> query, string text)
        {
            app.Tap(query);
            app.EnterText(query, text);
        }


		public static bool Exists(this IApp app, Func<AppQuery, AppQuery> query)
		{
			return app.Query(query).Any();
        }

        public static void ScrollDownIntoView(this IApp app, string marked)
        {
            ScrollDownIntoView(app, x => x.Marked(marked), null);
        }

		public static void ScrollDownIntoView(this IApp app, Func<AppQuery, AppQuery> query)
		{
			app.ScrollDownIntoView(query, null);
		}


        public static void ScrollDownIntoView(this IApp app, Func<AppQuery, AppQuery> query, Func<AppQuery, AppQuery> withinQuery)
        {
            int loop = 0;
            while (!app.Exists(query))
            {
				app.ScrollDown(withinQuery);
                loop++;
                if (loop > 3) break;
            }
        }

    }
}
