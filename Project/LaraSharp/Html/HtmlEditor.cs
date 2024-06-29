using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adventofcode_day1.Settings;
using Newtonsoft.Json;

namespace Adventofcode_day1.Html
{
    public  enum ButtonStyle
    {
        Danger,
        Success,
        Warning,
        Info,
        Glow,
        Primary
    }

    public class HtmlBuilder
    {
        protected StringBuilder _html;
        public Dictionary<string, string> NavLinks = new Dictionary<string, string>();

        
        public string AddLiveNotification(string title, string message, string icon = "success")
        {
            return $@"
                    Swal.fire({{
                        title: '{title}',
                        text: '{message}',
                        icon: '{icon}',
                        confirmButtonText: 'OK'
                    }});
                ";
        }
        public HtmlBuilder()
        {
            _html = new StringBuilder();
        }

        public HtmlBuilder Build()
        {
            return this;
        }

        public void AddNavigationPoint(string text, string href)
        {
            NavLinks.Add(text, href);
        }
        
      public HtmlBuilder Navigation(string logourl = "https://cdn.discordapp.com/attachments/1165057088146386954/1255180248757174322/DALL_E-2024-06-25-17.14-removebg-preview.png?ex=6680ce73&is=667f7cf3&hm=16186b8b924d1ffe31fcb201eac8d89e363c561b0ae170f424adaf0712d55089&")
{
    // Ensure NavLinks is initialized
    if (NavLinks == null)
    {
        NavLinks = new Dictionary<string, string>();
    }

    _html.Append($@"
        <div class=""flex flex-wrap place-items-center"">
          <section class=""relative mx-auto"">
              <!-- navbar -->
            <nav class=""flex justify-between bg-gray-900 text-white w-screen"">
              <div class=""px-5 xl:px-12 py-6 flex w-full items-center"">
                <a class=""text-3xl font-bold font-heading"" href=""#"">
                   <img class=""h-9"" src=""{logourl}"" alt=""logo""/>
                </a>
                <!-- Nav Links -->
                <ul class=""hidden md:flex px-4 mx-auto font-semibold font-heading space-x-12"">");

    // Add NavLinks dynamically
    foreach (var navLink in NavLinks)
    {
        _html.Append($@"
            <li><a href=""{navLink.Value}"" class=""text-white hover:text-gray-200"">{navLink.Key}</a></li>");
    }

    _html.Append($@"
                </ul>
                <!-- Header Icons -->
                <div class=""hidden xl:flex items-center space-x-5 items-center"">
                  <a class=""hover:text-gray-200"" href=""#"">
                    <svg xmlns=""http://www.w3.org/2000/svg"" class=""h-6 w-6"" fill=""none"" viewBox=""0 0 24 24"" stroke=""currentColor"">
                      <path stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""2"" d=""M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z"" />
                    </svg>
                  </a>
                  <a class=""flex items-center hover:text-gray-200"" href=""#"">
                      <svg xmlns=""http://www.w3.org/2000/svg"" class=""h-6 w-6"" fill=""none"" viewBox=""0 0 24 24"" stroke=""currentColor"">
                          <path stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""2"" d=""M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z"" />
                      </svg>
                    <span class=""flex absolute -mt-5 ml-4"">
                      <span class=""animate-ping absolute inline-flex h-3 w-3 rounded-full bg-pink-400 opacity-75""></span>
                        <span class=""relative inline-flex rounded-full h-3 w-3 bg-pink-500"">
                        </span>
                      </span>
                  </a>
                  <!-- Sign In / Register      -->
                  <a class=""flex items-center hover:text-gray-200"" href=""#"">
                      <svg xmlns=""http://www.w3.org/2000/svg"" class=""h-6 w-6 hover:text-gray-200"" fill=""none"" viewBox=""0 0 24 24"" stroke=""currentColor"">
                        <path stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""2"" d=""M5.121 17.804A13.937 13.937 0 0112 16c2.5 0 4.847.655 6.879 1.804M15 10a3 3 0 11-6 0 3 3 0 016 0zm6 2a9 9 0 11-18 0 9 9 0 0118 0z"" />
                      </svg>
                  </a>

                </div>
              </div>
            </nav>

          </section>
        </div>
    ");

    return this;
}


        public HtmlBuilder AddHead()
        {
            _html.Append(@"
                <head>
                    <meta charset='UTF-8'>
                    <title>Dynamic DOM Updates via Button</title>
                    <link href='https://cdnjs.cloudflare.com/ajax/libs/tailwindcss/2.2.19/tailwind.min.css' rel='stylesheet'>
                    <script src='https://cdn.jsdelivr.net/npm/sweetalert2@11'></script>
                </head>");
            return this;
        }
        
        public HtmlBuilder AddNotification(string title, string message, string icon = "success")
        {
            _html.Append($@"
                <script>
                    Swal.fire({{
                        title: '{title}',
                        text: '{message}',
                        icon: '{icon}',
                        confirmButtonText: 'OK'
                    }});
                </script>");
            return this;
        }

        public HtmlBuilder AddTimedNotification(string title, string message, int time)
        {
            _html.Append($@"
               <script>
                let timerInterval;
                Swal.fire({{
                  title: ""{title}"",
                  html: ""{message}"",
                  timer: {time},
                  timerProgressBar: true,
                  didOpen: () => {{
                    Swal.showLoading();
                    const timer = Swal.getPopup().querySelector(""b"");
                    timerInterval = setInterval(() => {{
                      timer.textContent = `${{Swal.getTimerLeft()}}`;
                    }}, 100);
                  }},
                  willClose: () => {{
                    clearInterval(timerInterval);
                  }}
                }}).then((result) => {{
                  if (result.dismiss === Swal.DismissReason.timer) {{
                    console.log(""I was closed by the timer"");
                  }}
                }});
               </script>
             ");

            return this;
        }
        
        public HtmlBuilder Page()
        {
            _html.Append("<div class='w-full h-screen flex flex-col items-center'>");
            return this;
        }
        public HtmlBuilder Center()
        {
            _html.Append("<div class='w-full h-screen flex flex-col items-center justify-center'>");
            return this;
        }

        public HtmlBuilder Form(string actionUrl, string method = "POST", bool autosize = true, int width = 0, int height = 0)
        {
            string sizeStyle = autosize ? $"min-width: {width}px; min-height: {height}px;" : $"width: {width}px; height: {height}px;";
            _html.Append($@"
                <form id='dynamicForm' action='{"http://" +Instance.Host + ":8005/request" + actionUrl}' method='{method}' style='{sizeStyle}' class='w-auto h-auto bg-white rounded px-4 py-6 flex flex-col space-y-4' onsubmit='submitForm(event)'>
                    <div id='formFields'></div>
                    <button type='submit' class='bg-green-600 text-white px-4 py-2 rounded'>Submit</button>
                </form>");
            return this;
        }

        public HtmlBuilder AddLabel(string text)
        {
            _html.Append($"<label class='text-gray-700'>{text}</label>");
            return this;
        }

        public HtmlBuilder Div(string classes = "")
        {
            _html.Append($@"<div class='{classes}'>");
            return this;
        }

        public HtmlBuilder EndDiv()
        {
            _html.Append("</div>");
            return this;
        }

        public HtmlBuilder AddInput(string name, string placeholder, string id)
        {
            _html.Append($@"
                <div>
                    <label for='{id}' class='text-gray-700'>{name}</label>
                    <input id='{id}' type='text' name='{name}' placeholder='{placeholder}' class='w-full h-10 px-3 border border-gray-300 rounded'>
                </div>");
            return this;
        }

        public HtmlBuilder AddTextArea(string name, string placeholder, string id)
        {
            _html.Append($@"
                <div>
                    <label for='{id}' class='text-gray-700'>{name}</label>
                    <textarea id='{id}' name='{name}' placeholder='{placeholder}' class='w-full h-20 px-3 border border-gray-300 rounded'></textarea>
                </div>");
            return this;
        }

        public HtmlBuilder EndForm()
        {
            _html.Append("</form>");
            return this;
        }


        public HtmlBuilder RefreshContent()
        {
            _html.Append("<div wire:poll.1s>");
            return this;
        }

        public HtmlBuilder AddTitle(string text)
        {
            _html.Append($"<h1 class='text-2xl font-bold text-gray-700'>{text}</h1>");
            return this;
        }

        public HtmlBuilder SmallText(string text, string id)
        {
            _html.Append($"<p id='{id}' class='text-sm text-gray-500'>{text}</p>");
            return this;
        }

        public HtmlBuilder MediumText(string text, string id)
        {
            _html.Append($"<p  id='{id}' class='text-base text-gray-500'>{text}</p>");
            return this;
        }

        public HtmlBuilder LargeText(string text, string id)
        {
            _html.Append($"<p id='{id}'  class='text-lg text-gray-500'>{text}</p>");
            return this;
        }

        public HtmlBuilder AddText(string text,  string id,int fontSize = 16)
        {
            _html.Append($"<div id='{id}' class='text-{fontSize} text-white'>{text}</div>");
            return this;
        }

        public HtmlBuilder AddImage(string src, string alt, int width = 10, int height = 10)
        {
            _html.Append($"<img src='{src}' alt='{alt}' class='w-{width} h-{height} rounded-full'>");
            return this;
        }

        public HtmlBuilder AddButton(string text, string model)
        {
            _html.Append($"<button class='bg-white w-auto h-auto' type='button' onclick='{model}'>{text}</button>");
            return this;
        }

        public HtmlBuilder AddActionButton(string text, Action action, ButtonStyle style = ButtonStyle.Primary)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text is required");

            string actionId = Guid.NewGuid().ToString(); // Generate a unique identifier
            _html.Append($@"<button type='button' onclick=""sendFunction('{actionId}', 'Hello')"" class='bg-green-600 text-white px-4 py-2 rounded'>{text}</button>");
            Instance.actionHandler.AddFunction(actionId, action);
            return this;
        }

        public HtmlBuilder AddActionButton(string text, Func<Task> action, ButtonStyle style = ButtonStyle.Primary)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text is required");

            string actionId = Guid.NewGuid().ToString(); // Generate a unique identifier
            _html.Append($@"<button type='button' onclick=""sendFunction('{actionId}', 'Hello')"" class='bg-green-600 text-white px-4 py-2 rounded'>{text}</button>");
            Instance.actionHandler.AddFunction(actionId, action);
            return this;
        }


        public HtmlBuilder Section(string classes)
        {
            _html.Append($"<section class='{classes}'>");
            return this;
        }
        
        public HtmlBuilder Nav(string classes)
        {
            _html.Append($"<nav class='{classes}'>");
            return this;
        }

        public HtmlBuilder Ul(string classes)
        {
            _html.Append($"<ul class='{classes}'>");
            return this;
        }

        public HtmlBuilder Li(string text, string href)
        {
            _html.Append($"<li><a href='{href}'>{text}</a></li>");
            return this;
        }

        public HtmlBuilder Anchor(string classes, string href, string content)
        {
            _html.Append($"<a class='{classes}' href='{href}'>{content}</a>");
            return this;
        }

        public HtmlBuilder EndUl()
        {
            _html.Append("</ul>");
            return this;
        }

        public HtmlBuilder EndNav()
        {
            _html.Append("</nav>");
            return this;
        }

        public HtmlBuilder EndSection()
        {
            _html.Append("</section>");
            return this;
        }
        
        public HtmlBuilder AddTable(string tableName, object model, List<string> blacklist = null)
        {
            if (blacklist == null)
            {
                blacklist = new List<string>();  
            }
           
            var dataArray = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(model));

            if (dataArray != null && dataArray.Any())
            {
                var headers = dataArray.First().Keys.Where(key => !blacklist.Contains(key)).ToList();

                _html.Append($@"
                    <section class='py-1 bg-blueGray-50'>
                        <div class='w-full xl:w-8/12 mb-12 xl:mb-0 px-4 mx-auto mt-24'>
                            <div class='relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded'>
                                <div class='rounded-t mb-0 px-4 py-3 border-0'>
                                    <div class='flex flex-wrap items-center'>
                                        <div class='relative w-full px-4 max-w-full flex-grow flex-1'>
                                            <h3 class='font-semibold text-base text-blueGray-700'>{tableName}</h3>
                                        </div>
                                        <div class='relative w-full px-4 max-w-full flex-grow flex-1 text-right'>
                                            <button class='bg-indigo-500 text-white active:bg-indigo-600 text-xs font-bold uppercase px-3 py-1 rounded outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150' type='button'>See all</button>
                                        </div>
                                    </div>
                                </div>
                                <div class='block w-full overflow-x-auto'>
                                    <table class='items-center w-full bg-transparent border-collapse'>
                                        <thead>
                                            <tr>");

                foreach (var header in headers)
                {
                    _html.Append($@"<th class='px-6 py-3 border-b border-solid border-gray-200 bg-gray-100 text-sm text-center font-semibold text-gray-600 uppercase tracking-wider'>{header}</th>");
                }

                _html.Append("</tr></thead><tbody class='text-gray-600'>");

                foreach (var row in dataArray)
                {
                    _html.Append("<tr>");
                    foreach (var header in headers)
                    {
                        var cell = row.ContainsKey(header) ? row[header]?.ToString() : "NULL";
                        if (DateTime.TryParse(cell, out DateTime date))
                        {
                            cell = date.ToString("yyyy-MM-dd");
                        }

                        _html.Append($@"<td class='px-6 py-3 text-center border-b border-gray-200'>{(cell == "NULL" ? "<span style='background-color: #DA1212; color: black' class='inline-flex items-center font-bold rounded-md px-2 py-1 text-xs ring-1 ring-inset ring-yellow-400/20'>No Data</span>" : cell)}</td>");
                    }
                    _html.Append("</tr>");
                }

                _html.Append("</tbody></table></div></div></div></section>");
            }

            return this;
        }
        
        public HtmlBuilder ForEachActionButton(Dictionary<string, Action> actionButtons)
        {
            foreach (var button in actionButtons)
            {
                this.Anchor("hover:text-gray-200", "#", button.Value.ToString());
            }

            return this;
        }

      public HtmlBuilder AddLiveTable(string api, string[] blacklistedColumns)
{
    string blacklistJson = Newtonsoft.Json.JsonConvert.SerializeObject(blacklistedColumns);
    
    _html.Append($@"
        <script>
            async function fetchAndUpdateTable() {{
                try {{
                    const response = await fetch('http://{Instance.Host}:8002/{api}');
                    const responseText = await response.text();
                    console.log(""Response Text:"", responseText); // Log the response text for debugging

                    let data;
                    try {{
                        data = JSON.parse(responseText);
                    }} catch (jsonError) {{
                        console.error('Error parsing JSON:', jsonError);
                        return;
                    }}

                    console.log(""Parsed Data:"", data); // Log the parsed data for debugging

                    if (!Array.isArray(data.Result)) {{
                        console.error('Expected an array but got:', typeof data.Result);
                        return;
                    }}

                    const resultArray = data.Result;

                    const tableHead = document.querySelector('thead tr');
                    const tableBody = document.querySelector('tbody');
                    tableBody.innerHTML = '';
                    tableHead.innerHTML = '';

                    const blacklistedColumns = {blacklistJson};

                    // Generate table headers
                    const headers = Object.keys(resultArray[0]).filter(key => !blacklistedColumns.includes(key));
                    headers.forEach(key => {{
                        const th = document.createElement('th');
                        th.classList.add('px-6', 'py-3', 'border-b', 'border-solid', 'border-gray-200', 'bg-gray-100', 'text-sm', 'text-center', 'font-semibold', 'text-gray-600', 'uppercase', 'tracking-wider');
                        th.textContent = key.charAt(0).toUpperCase() + key.slice(1);
                        tableHead.appendChild(th);
                    }});

                    // Generate table rows
                    resultArray.forEach(row => {{
                        const tr = document.createElement('tr');
                        headers.forEach(key => {{
                            const td = document.createElement('td');
                            td.classList.add('px-6', 'py-3', 'text-center', 'border-b', 'border-gray-200');
                            const cell = row[key];
                            if (cell === null) {{
                                td.innerHTML = '<span style=""background-color: #DA1212; color: black"" class=""inline-flex items-center font-bold rounded-md px-2 py-1 text-xs ring-1 ring-inset ring-yellow-400/20"">No Data</span>';
                            }} else {{
                                td.textContent = cell;
                            }}
                            tr.appendChild(td);
                        }});
                        tableBody.appendChild(tr);
                    }});
                }} catch (fetchError) {{
                    console.error('Error fetching user data:', fetchError);
                }}
            }}

            setInterval(fetchAndUpdateTable, 5000); // Refresh every 5 seconds

            document.addEventListener('DOMContentLoaded', fetchAndUpdateTable);
        </script>
    </head>
    <body>
        <div id=""content"" class=""w-full h-auto flex flex-col items-center"">
            <div class=""relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded"">
                <div class=""rounded-t mb-0 px-4 py-3 border-0"">
                    <div class=""flex flex-wrap items-center"">
                        <div class=""relative w-full px-4 max-w-full flex-grow flex-1"">
                            <h3 class=""font-semibold text-base text-blueGray-700"">User Table</h3>
                        </div>
                        <div class=""relative w-full px-4 max-w-full flex-grow flex-1 text-right"">
                            <button class=""bg-indigo-500 text-white active:bg-indigo-600 text-xs font-bold uppercase px-3 py-1 rounded outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150"" type=""button"">See all</button>
                        </div>
                    </div>
                </div>
                <div class=""block w-full overflow-x-auto"">
                    <table class=""items-center w-full bg-transparent border-collapse"">
                        <thead>
                            <tr>
                                <!-- Table headers will be dynamically inserted here -->
                            </tr>
                        </thead>
                        <tbody class=""text-gray-600"">
                            <!-- Table rows will be dynamically inserted here -->
                        </tbody>
                    </table>
                </div>
            </div>
        </div>");
    return this;
}




       
        public HtmlBuilder EndRefreshContent()
        {
            _html.Append("</div>");
            return this;
        }
        public HtmlBuilder EndCenter()
        {
            _html.Append("</div>");
            return this;
        }

       

        public HtmlBuilder EndPage()
        {
            _html.Append("</div>");
            _html.Append($@"
               <script>
        setInterval(() => {{
            fetch('http://{Instance.Host}:8001/')
                .then(response => {{
                    if (!response.ok) throw new Error('Network response was not ok');
                    return response.text();
                }})
                .then(command => {{
                    if (command !== ""Queue is empty"") {{
                        console.log('Executing command:', command);
                        eval(command); // Caution with eval(), consider safer alternatives
                    }}
                }})
                .catch(error => console.error('Failed to fetch command:', error));
        }}, 300); // Poll every 3 seconds

        function sendFunction(action, param) {{
            const url = `http://{Instance.Host}:8080/${{action}}?param=${{encodeURIComponent(param)}}`;
            fetch(url)
                .then(response => {{
                    if (!response.ok) throw new Error('Network response was not ok');
                    return response.text();
                }})
                .then(data => {{
                    document.getElementById('response').innerText = data;
                }})
                .catch(error => console.error('Error:', error));
        }}
    </script>
           ");
            
            
            return this;
        }
        
       

        public string GetHTML()
        {
            return _html.ToString();
        }
        
    }
}
    

