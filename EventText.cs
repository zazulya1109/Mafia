using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;

namespace WebApplication1
{
    public static class EventText
    {
        private static string serverPath = "EventText.txt";
        private static string localServerPath = "EventTextLocal.txt";
        private static Dictionary<EventType, string> description;

        static EventText()
        {
            description = new Dictionary<EventType, string>();
            string currentPath = serverPath;

            if (Startup.isLocal) 
                 currentPath = localServerPath;

            string[] fullText = File.ReadAllLines(currentPath);
            foreach (string line in fullText)
            {
                string[] elements = line.Split(" ");

                string text = GetText(elements);
                int typeIndex = int.Parse(elements[0]);

                description.Add((EventType)typeIndex, text);
            }
        }

        private static string GetText(string[] elements)
        {
            string result = "";
            for (int i = 1; i < elements.Length; i++)
            {
                result += elements[i] + " ";
            }
            return result;
        }
        
        public static string GetText(EventType type)
        {
            return description[type];
        }
    }
}
