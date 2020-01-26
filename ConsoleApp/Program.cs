using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace ConsoleApp
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Button b = new Button();

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(b);

            EventDescriptorCollection events = TypeDescriptor.GetEvents(b);

            foreach (PropertyDescriptor pd in props)
                Console.WriteLine(pd.DisplayName);

            foreach (EventDescriptor ed in events)
                Console.WriteLine(ed.Name);

            Console.ReadLine();
        }
    }
}