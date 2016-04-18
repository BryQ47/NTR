using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsList.Model
{
    class Storage
    {
        public List<Student> getStudents()
        {
            using (var db = new StorageContext())
            {
                return db.Students.ToList();
            }
        }

        public List<Student> getStudentsOfGroup(int groupId)
        {

            using (var db = new StorageContext())
            {
                var query =
                    from student in db.Students
                    where student.Group.GroupId == groupId
                    select student;
                return query.ToList();
            }
        }

        public List<Group> getGroups()
        {
            using (var db = new StorageContext())
                return db.Groups.ToList();
        }

        public void createStudent(string firstName, string lastName, string indexNo, int groupId)
        {
            using (var db = new StorageContext())
            {
                var group = db.Groups.Find(groupId);
                var student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    IndexNo = indexNo,
                    Group = group
                };
                db.Students.Add(student);
                db.SaveChanges();
            }
        }

        public void updateStudent(Student st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Students.Include("Group").ToList().Find(s => s.StudentId == st.StudentId);

                if (original != null)
                {
                    original.FirstName = st.FirstName;
                    original.LastName = st.LastName;
                    original.IndexNo = st.IndexNo;
                    db.SaveChanges();
                }
            }
        }

        public void deleteStudent(Student st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Students.Find(st.StudentId);
                if (original != null)
                {
                    db.Students.Remove(original);
                    db.SaveChanges();
                }
            }
        }

    }
}
