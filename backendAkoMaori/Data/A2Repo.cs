using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using A2.Models;
using A2.Dtos;
using Microsoft.EntityFrameworkCore;

namespace A2.Data
{
    public class A2Repo : IA2Repo
    {
        private readonly A2DbContext _dbContext;

        public A2Repo(A2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }


        public void AddUser(User u)
        {
            _dbContext.Users.Add(u);
            _dbContext.SaveChanges();
        }


        public User GetUser(string name)
        {
            return _dbContext.Users.FirstOrDefault(e => e.UserName == name);
        }
        public bool UsernameTaken(String username)
        {
            return _dbContext.Users.Any(i => i.UserName == username);
        }



        public bool ValidLogin(string username, string password)
        {
            User c = _dbContext.Users.FirstOrDefault(e => e.UserName == username && e.Password == password);
            if (c == null)
                return false;
            else
                return true;

        }
        public bool ValidOrganizer(string org, string Password)
        {
            Organizer c = _dbContext.Organizers.FirstOrDefault(e => e.Name == org && e.Password == Password);
            if (c == null)
                return false;
            else
                return true;

        }

        public bool FindProduct(int id)
        {
            Product c = _dbContext.Products.FirstOrDefault(e => e.Id == id);
            if (c == null)
                return false;
            else
                return true;

        }

        public void InsertEvent(Event ev)
        {
            _dbContext.Events.Add(ev);
            _dbContext.SaveChanges();
        }

        public int GetNumEvents()
        {
            return _dbContext.Events.Count();
        }

        public Event GetEvent(int id)
        {
            return _dbContext.Events.FirstOrDefault(e => e.Id == id);
        }

    }
}
