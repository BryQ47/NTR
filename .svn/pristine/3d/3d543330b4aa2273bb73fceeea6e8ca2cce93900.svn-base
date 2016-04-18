using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsList.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using StudentsList.Commands;

namespace StudentsList.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private Storage storage = new Storage();
        public event PropertyChangedEventHandler PropertyChanged;

        public List<Student> Students
        {
            get
            {
                if (_selectedGroup != null)
                    return storage.getStudentsOfGroup(_selectedGroup.GroupId);
                return storage.getStudents();
            }
        }

        public List<Group> Groups
        {
            get
            {
                return storage.getGroups();
            }
        }

        private Group _selectedGroup;
        public Group SelectedGroup
        {
            get { return _selectedGroup; }
            set 
            { 
                if (value == _selectedGroup)
                    return;
                _selectedGroup = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Students"));
            }
        }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                if (value == _selectedStudent)
                    return;
                _selectedStudent = value;

                if (value == null)
                {
                    StudentLastName = "";
                    StudentFirstName = "";
                    StudentIndexNo = "";
                }
                else
                {
                    StudentFirstName = _selectedStudent.FirstName;
                    StudentLastName = _selectedStudent.LastName;
                    StudentIndexNo = _selectedStudent.IndexNo;
                }

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StudentFirstName"));
                    PropertyChanged(this, new PropertyChangedEventArgs("StudentLastName"));
                    PropertyChanged(this, new PropertyChangedEventArgs("StudentIndexNo"));
                }
                    
            }
        }

        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentIndexNo { get; set; }

        private string _errorMsg = "";
        public string ErrorMsg 
        {
            get
            {
                return _errorMsg;
            }
            set
            {
                _errorMsg = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ErrorMsg"));
            }
        }

        private ButtonCommand saveCmd;
        private ButtonCommand newCmd;
        private ButtonCommand delCmd;
        public ICommand SaveStudentCommand { get { return saveCmd; } }
        public ICommand NewStudentCommand { get { return newCmd; } }
        public ICommand DeleteStudentCommand { get { return delCmd; } }

        public MainWindowViewModel()
        {
            saveCmd = new ButtonCommand(SaveStudent, CanSaveStudent);
            newCmd = new ButtonCommand(AddStudent, CanAddStudent);
            delCmd = new ButtonCommand(DeleteStudent, CanDeleteStudent);
        }

        private void SaveStudent()
        {
            try
            {
                _selectedStudent.FirstName = StudentFirstName;
                _selectedStudent.LastName = StudentLastName;
                _selectedStudent.IndexNo = StudentIndexNo;
                storage.updateStudent(_selectedStudent);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Students"));
            }
            catch (Exception ex)
            {
                HandleCriticalError(ex.Message);
            }
        }

        private void AddStudent()
        {
            try
            {
                storage.createStudent(StudentFirstName, StudentLastName, StudentIndexNo, _selectedGroup.GroupId);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Students"));
            }
            catch (Exception ex)
            {
                HandleCriticalError(ex.Message);
            }

        }


        private void DeleteStudent()
        {
            try
            {
                storage.deleteStudent(_selectedStudent);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Students"));
            }
            catch (Exception ex)
            {
                HandleCriticalError(ex.Message);
            }
        }

        private bool CanSaveStudent()
        {
            return (_selectedStudent != null) && !(_selectedStudent.FirstName.Equals(StudentFirstName) && _selectedStudent.LastName.Equals(StudentLastName) && _selectedStudent.IndexNo.Equals(StudentIndexNo));
        }

        private bool CanDeleteStudent()
        {
            return (_selectedStudent != null);
        }

        private bool CanAddStudent()
        {
            return !(String.IsNullOrEmpty(StudentFirstName) || String.IsNullOrEmpty(StudentLastName) || (_selectedGroup == null));
        }

        private void HandleCriticalError(string msgtxt)
        {
            ErrorMsg = "Błąd: " + msgtxt;
            SelectedGroup = null;
            SelectedStudent = null;
        }
    }
}
