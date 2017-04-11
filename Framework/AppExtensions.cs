using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void ScrollDownIntoView(this IApp app, string marked, string markedWithin = null,
                                              ScrollStrategy strategy = ScrollStrategy.Auto,
                                              double swipePercentage = 0.67,
                                              int swipeSpeed = 500,
                                              bool withInertia = true)
        {
            ScrollDownIntoView(app, x => x.Marked(marked), markedWithin == null ? (Func<AppQuery, AppQuery>)null : x => x.Marked(markedWithin), strategy, swipePercentage, swipeSpeed, withInertia);
        }

        public static void ScrollDownIntoView(this IApp app, Func<AppQuery, AppQuery> query, Func<AppQuery, AppQuery> withinQuery = null,
                                              ScrollStrategy strategy = ScrollStrategy.Auto,
                                              double swipePercentage = 0.67,
                                              int swipeSpeed = 500,
                                              bool withInertia = true
                                             )
        {
            int loop = 0;
            while (!app.Exists(query))
            {
                app.ScrollDown(withinQuery, strategy, swipePercentage, swipeSpeed, withInertia);
                loop++;
                if (loop > 3) break;
            }
        }

        public static void ScrollUpIntoView(this IApp app, string marked, string markedWithin = null,
                                              ScrollStrategy strategy = ScrollStrategy.Auto,
                                              double swipePercentage = 0.67,
                                              int swipeSpeed = 500,
                                              bool withInertia = true)
        {
            ScrollUpIntoView(app, x => x.Marked(marked), markedWithin == null ? (Func<AppQuery, AppQuery>)null : x => x.Marked(markedWithin), strategy, swipePercentage, swipeSpeed, withInertia);
        }

        public static void ScrollUpIntoView(this IApp app, Func<AppQuery, AppQuery> query, Func<AppQuery, AppQuery> withinQuery = null,
                                              ScrollStrategy strategy = ScrollStrategy.Auto,
                                              double swipePercentage = 0.67,
                                              int swipeSpeed = 500,
                                              bool withInertia = true
                                             )
        {
            int loop = 0;
            while (!app.Exists(query))
            {
                app.ScrollUp(withinQuery, strategy, swipePercentage, swipeSpeed, withInertia);
                loop++;
                if (loop > 3) break;
            }
        }

    }
}
