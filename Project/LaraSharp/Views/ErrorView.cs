using Adventofcode_day1.Html;

namespace Adventofcode_day1.Views
{
    public class ErrorView
    {
        public static HtmlBuilder htmlBuilder;
        public static void Initialize()
        {
            htmlBuilder = new HtmlBuilder();
            htmlBuilder = new HtmlBuilder();
            htmlBuilder
                .Build()
                    .AddHead()
                        .Page()
                            .div("w-full h-screen font-bold flex flex-col justify-center items-center bg-black")
                                .AddText("Error 404", "error", 42)
                                .AddText("Route no found", "error", 24)
                            .endDiv()
                        .EndCenter()
                    .EndPage()
                .GetHTML();
        }
    }
}