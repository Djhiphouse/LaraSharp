using Adventofcode_day1.Html;

namespace Adventofcode_day1.Views
{
    public class ErrorView
    {
        public static HtmlBuilder htmlBuilder;
        public static void Initialize()
        {
            htmlBuilder = new HtmlBuilder();
            htmlBuilder
                .Build()
                    .AddHead()
                        .Page()
                            .Div("w-full h-screen font-bold flex flex-col justify-center items-center bg-black")
                                .Div("animate-bounce")
                                    .AddImage("https://github.com/Djhiphouse/LaraSharp/blob/main/Project/LaraSharp/Icons/error.png?raw=true", "error", 24, 24)
                                .EndDiv()
                
                                .AddText("Route no found", "error", 24)
                            .EndDiv()
                        .EndCenter()
                    .EndPage()
                .GetHTML();
        }
    }
}