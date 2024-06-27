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
                                .div("animate-bounce")
                                    .AddImage("https://github.com/Djhiphouse/LaraSharp/blob/main/Project/LaraSharp/Icons/error.png?raw=true", "error", 24, 24)
                                .endDiv()
                
                                .AddText("Route no found", "error", 24)
                            .endDiv()
                        .EndCenter()
                    .EndPage()
                .GetHTML();
        }
    }
}