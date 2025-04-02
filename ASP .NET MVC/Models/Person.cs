using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_.NET_MVC.Models
{
    public class Person
    {
        public Person() { }

        public Person(int id, string firstName, string lastName, GenderType gender, DateTime dateOfBirth, string phoneNumber, string birthPlace, bool isGraduated)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            BirthPlace = birthPlace;
            IsGraduated = isGraduated;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }  
        public DateTime DateOfBirth  { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }
        public bool IsGraduated { get; set; }
        public enum GenderType {
            Female = 0,
            Male = 1
        }
        public int GetAge(){
            return DateTime.Now.Year - DateOfBirth.Year;
        }

    }
}