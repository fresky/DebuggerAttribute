using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace DebuggerAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            User developer = new User
                {
                    Name = "fresky",
                    Title = "Developer",
                    Email = "dawei.xu@gmail.com",
                    Address = Convert.ToBase64String(Encoding.ASCII.GetBytes("China")),
                };
            developer.AddSkill("C#");
            developer.AddSkill("C++");
            developer.AddSkill("Jave");

            Console.WriteLine(developer.GetBussinessCard());
            Console.ReadKey();
        }


    }

    [DebuggerTypeProxy(typeof(UserProxy))]
    [DebuggerDisplay("{Name} has {GetSkills().Count} skills!")]
    public class User
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        private readonly List<string> m_Skills = new List<string>();

        [DebuggerStepThrough]
        public string GetBussinessCard()
        {
            return string.Format("BussinessCard:\r\n{0}", getUserInfo());
        }

        public void AddSkill(string skill)
        {
            m_Skills.Add(skill);
        }

        private string getUserInfo()
        {
            return string.Format("Skills: {0}\r\n{1}", getSkills(), getContacInfo());
        }

        [DebuggerHidden]
        private string getContacInfo()
        {
            return string.Format("Name: {0}\r\nPleae contact:\r\n{1}", Name, getAddress());
        }


        private string getAddress()
        {
            return string.Format("Email: {0}\r\nAddress:{1}", Email, Address);
        }

        private string getSkills()
        {
            return string.Join(", ", m_Skills);
        }

        public ReadOnlyCollection<string> GetSkills()
        {
            return m_Skills.AsReadOnly();
        }

        [Conditional("DEBUG")]
        static void MessageFromDebugger(string message)
        {
            Console.WriteLine("-----------DEBUG INFO START-------------");
            Console.WriteLine("I was called from the debugger evaluator: {0}", message);
            Console.WriteLine("-----------DEBUG INFO STOP--------------");
        }
    }

    public class UserProxy
    {
        public string Name { get; set; }
        public string Address { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public ReadOnlyCollection<string> Skills { get; set; }
        
        public UserProxy(User u)
        {
            this.Name = string.Format("{0}--{1}", u.Name, u.Title);
            this.Email = u.Email;
            this.Address = GetPlainAddress(u.Address);
            this.Skills = u.GetSkills();
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Email { get; set; }

        private string GetPlainAddress(string pwd)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(pwd));
        }
    }
}
