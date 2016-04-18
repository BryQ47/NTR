app.controller("studentsCtrl", ['$scope', '$http', 'ngTableParams',
    function ($scope, $http, NgTableParams) {

        var self = this;

        /******* Tables *******/

        $scope.studentsTable = new NgTableParams();
        $scope.groupsTable = new NgTableParams();


        /******* Scope variables *******/

        $scope.students = [];
        $scope.groups = [];
        $scope.groupNames = [];
        $scope.showError = false;
        $scope.errorMsg = "";
        $scope.selectedStudent = {};
        $scope.selectedGroup = {};

        /******* Scope functions *******/

        $scope.dismissError = HideErrorMsg;

        $scope.selectStudent = function (id) {
            var students = $scope.students;
            var nstud = students.length;
            for (var i = 0; i < nstud; ++i) {
                var stud = students[i];
                if (stud.IDStudent === id) {
                    var s = {};
                    s.BirthDate = new Date(stud.BirthDate);
                    s.BirthPlace = stud.BirthPlace,
                    s.FirstName = stud.FirstName,
                    s.IDGroup = stud.IDGroup,
                    s.IDStudent = stud.IDStudent,
                    s.IndexNo = stud.IndexNo,
                    s.LastName = stud.LastName,
                    s.Stamp = stud.Stamp
                    $scope.selectedStudent = s;
                }
            }
            $scope.studentForm.$setPristine();
        }

        $scope.selectGroup = function (id) {
            var groups = $scope.groups;
            var ngroup = groups.length;
            for (var i = 0; i < ngroup; ++i) {
                var gr = groups[i];
                if (gr.IDGroup === id) {
                    var g = {};
                    g.Name = gr.Name,
                    g.IDGroup = gr.IDGroup,
                    g.Stamp = gr.Stamp
                    $scope.selectedGroup = g;
                }
            }
            $scope.groupForm.$setPristine();
        }

        $scope.studentCreate = createStudent;
        $scope.studentUpdate = updateStudent;
        $scope.studentDelete = deleteStudent;

        $scope.groupCreate = createGroup;
        $scope.groupUpdate = updateGroup;
        $scope.groupDelete = deleteGroup;

        $scope.refreshLists = getStudents;

        /******* Initialization *******/

        getStudents();

        /******* Fucntions *******/

        function HideErrorMsg() {
            $scope.showError = false;
            $scope.errorMsg = "";
        }


        function loadStudents(newStudents, newGroups) {

            $scope.students = newStudents;
            //$scope.groups = newGroups;
            $scope.selectedStudent = {};

            ngroup = newGroups.length;

            var gnames = [];
            for (var k = 0; k < ngroup; ++k)
                gnames.push(newGroups[k].Name);

            $scope.groupNames = gnames;

            var page = $scope.studentsTable.page();

            $scope.studentsTable.settings({
                dataset: newStudents
            });

            $scope.studentsTable.page(page);

        }

        function loadGroups(newGroups) {

            $scope.groups = newGroups;
            $scope.selectedGroup = {};

            ngroup = newGroups.length;

            var page = $scope.groupsTable.page();

            $scope.groupsTable.settings({
                dataset: newGroups
            });

            $scope.groupsTable.page(page);

        }

        function successCalback(response) {
            HideErrorMsg();
            loadStudents(response.data.students, response.data.groups);
            loadGroups(response.data.groups);
            if (response.data.error) {
                $scope.errorMsg = response.data.error;
                $scope.showError = true;
            }
        }

        function errorCallback(response) {
            $scope.errorMsg = "Błąd serwera";
            $scope.showError = true;
        }

        function getStudents() {
            $http({
                method: 'GET',
                url: '/api/Students'
            }).then(successCalback, errorCallback);
        }

        function createStudent() {
            var newStudent = {};
            newStudent.FirstName = $scope.selectedStudent.FirstName;
            newStudent.LastName = $scope.selectedStudent.LastName;
            newStudent.BirthPlace = $scope.selectedStudent.BirthPlace;
            newStudent.BirthDate = $scope.selectedStudent.BirthDate;
            newStudent.IndexNo = $scope.selectedStudent.IndexNo;
            newStudent.IDGroup = $scope.selectedStudent.IDGroup;

            $http({
                method: 'POST',
                url: '/api/Students/POST',
                data: JSON.stringify(newStudent)
            }).then(successCalback, errorCallback);
        }

        function updateStudent() {
            $http({
                method: 'POST',
                url: '/api/Students/PUT',
                data: JSON.stringify($scope.selectedStudent)
            }).then(successCalback, errorCallback);
        }

        function deleteStudent() {
            $http({
                method: 'POST',
                url: '/api/Students/DELETE',
                data: JSON.stringify($scope.selectedStudent)
            }).then(successCalback, errorCallback);
        }

        function createGroup() {
            var newGroup = {};
            newGroup.Name = $scope.selectedGroup.Name;

            $http({
                method: 'POST',
                url: '/api/Groups/POST',
                data: JSON.stringify(newGroup)
            }).then(successCalback, errorCallback);
        }

        function updateGroup() {
            $http({
                method: 'POST',
                url: '/api/Groups/PUT',
                data: JSON.stringify($scope.selectedGroup)
            }).then(successCalback, errorCallback);
        }

        function deleteGroup() {
            $http({
                method: 'POST',
                url: '/api/Groups/DELETE',
                data: JSON.stringify($scope.selectedGroup)
            }).then(successCalback, errorCallback);
        }


}]);