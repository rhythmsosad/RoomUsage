using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class Class1
    {
        public string name { get; set; }
        public string surname { get; set; }

        public int salary { get; set; }

        public List<Class1> GetName()
        {
            return new List<Class1>() {
                new Class1() {  name = "name1", surname = "surname1" },
                new Class1() {  name = "name1", surname = "surname1" },
                new Class1() {  name = "name1", surname = "surname1" },
                new Class1() {  name = "name2", surname = "surname2" },
                new Class1() {  name = "name2", surname = "surname2" },
                new Class1() {  name = "name2", surname = "surname2" },
                new Class1() {  name = "name2", surname = "surname2" },
                new Class1() {  name = "name2", surname = "surname2" },
                new Class1() {  name = "name3", surname = "surname3" },
                new Class1() {  name = "name3", surname = "surname3" }
            };
        }

        public List<Class1> GetSalary()
        {
            return new List<Class1>()
            {
                new Class1() { name = "name1", salary = 25000 },
                new Class1() { name = "name2", salary = 30000 },
                new Class1() { name = "name3", salary = 40000 }
            };
        }

    }
}