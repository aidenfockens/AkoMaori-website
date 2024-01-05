using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Models;
using A2.Dtos;

namespace A2.Data
{
    public interface IA2Repo
    {
        public void SaveChanges();
        public void AddUser(User u);
        public bool UsernameTaken(string username);
        public bool ValidLogin(string userName, string password);
        public bool ValidOrganizer(string Name, string Password);
        public bool FindProduct(int id);
        public void InsertEvent(Event ev);
        public int GetNumEvents();
        public User GetUser(string name);
        public Event GetEvent(int id);
    }
}