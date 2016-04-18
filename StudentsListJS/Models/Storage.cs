using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace StudentsListJS.Models
{
    public class Storage
    {
        public Storage()
        {
            Database.SetInitializer<StorageContext>(null);
        }

        public List<Student> GetStudents()
        {
            using (var db = new StorageContext())
            {
                return db.Students.Include("Group").ToList();
            }
        }

        public List<Group> GetGroups()
        {
            using (var db = new StorageContext())
            {
                return db.Groups.ToList();
            }
        }

        public void AddStudent(Student student)
        {
            using (var db = new StorageContext())
            {
                if (db.Groups.Where(gr => gr.IDGroup == student.IDGroup).Count() < 1)
                    throw new Exception("Student musi być przypisany do grupy");

                if (db.Students.Where(stud => stud.IndexNo.Equals(student.IndexNo)).Count() > 0)
                    throw new Exception("Indeks musi być unikalny");

                db.Students.Add(student);
                db.SaveChanges();
            }
        }

        public void AddGroup(Group group)
        {
            using (var db = new StorageContext())
            {
                if (db.Groups.Where(gr => gr.Name.Equals(group.Name)).Count() > 0)
                    throw new Exception("Grupa musi mieć unikalną nazwę");

                db.Groups.Add(group);
                db.SaveChanges();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (var db = new StorageContext())
            {
                if (db.Groups.Where(gr => gr.IDGroup == student.IDGroup).Count() < 1)
                    throw new Exception("Student musi być przypisany do grupy");

                if (db.Students.Where(stud => stud.IDStudent != student.IDStudent && stud.IndexNo.Equals(student.IndexNo)).Count() > 0)
                    throw new Exception("Indeks musi być unikalny");

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void UpdateGroup(Group group)
        {
            using (var db = new StorageContext())
            {
                if (db.Groups.Where(gr => gr.Name.Equals(group.Name) && gr.IDGroup != group.IDGroup).Count() > 0)
                    throw new Exception("Grupa musi mieć unikalną nazwę");

                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteStudent(Student student)
        {
            using (var db = new StorageContext())
            {
                Student todelete = db.Students.Find(student.IDStudent);
                db.Students.Remove(todelete);
                db.SaveChanges();
            }
        }

        public void DeleteGroup(Group group)
        {
            using (var db = new StorageContext())
            {
                Group todelete = db.Groups.Find(group.IDGroup);
                if (todelete.Students.Count() > 0)
                    throw new Exception("Grupa posiada przypisanych studentów");

                db.Groups.Remove(todelete);
                db.SaveChanges();
            }
        }
    }
}