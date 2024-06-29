using Adventofcode_day1.Html;
using Adventofcode_day1.Settings;

namespace Adventofcode_day1.Views
{
    public class TutorialView
    {
        public static HtmlBuilder htmlBuilder = new HtmlBuilder();

        public static void TriggerAlert()
        {
            Instance.domdocument.AddJavascript(
                  htmlBuilder.AddLiveNotification("Tutorial Alert", "This is a Tutorial Alert")
                );
        }

        
        public static void Initialize()
        {
            htmlBuilder.AddNavigationPoint("Logs", "/logs");
            htmlBuilder.AddNavigationPoint("Welcome", "/welcome");
            htmlBuilder.AddNavigationPoint("ErrorPage", "/error");
            
            htmlBuilder
                .AddHead()
                .Navigation()
                    .Div("w-full h-screen bg-black")
                        .Page()
                            .Center()
                                .Div("animate-bounce")
                                    .AddText("This is a Tutorial View", "tuto_str", 24)
                                .EndDiv()
                                .AddActionButton("LIve Alert", TriggerAlert)
                            .EndCenter()
                        .EndPage()
                    .EndDiv()
                .GetHTML();
        }
    }
}