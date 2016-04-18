﻿app.controller("calcCtrl", function ($scope) {

    /******* Scope variables *******/

    $scope.btnsDisabled = false;
    $scope.showError = false;
    $scope.result = "0";
    $scope.errorMsg = "";

    /******* Scope functions *******/

    $scope.btnClicked = function (param) {
        try {
            $scope.result = performCalculation(param, $scope.result);          
        }
        catch (err) {
            DealWithError(err.message);
        }
    }
    $scope.dismissError = HideError;

    /******* Private variables *******/

    var maxDigits = 10;

    var maxVal = 999999999;

    var calc = new Calculator(maxVal);

    var opPerformed = true;

    /******* Private functions *******/

    function HideError () {
        $scope.showError = false;
        $scope.errorMsg = "";
    }

    function DealWithError(msg) {
        $scope.result = "ERR";
        $scope.errorMsg = msg;
        $scope.showError = true;
        $scope.btnsDisabled = true;
    }

    function performCalculation(key, val) {

        var opGroup = key.charAt(0);
        var opType = key.charAt(1);
        var res;

        switch (opGroup) {
            case "D":
                if (opPerformed && opType != "I") {
                    res = modifyArg(opType, "0");
                    opPerformed = false;
                }
                else {
                    res = modifyArg(opType, val);
                }
                break;
            case "O":
                var arg = parseFloat(val);
                var numRes = calc.performOperation(arg, opType);
                res = resToString(numRes);
                opPerformed = true;
                break;
            case "C":
                var arg = parseFloat(val);
                var numRes = calc.getResult(arg, opType);
                res = resToString(numRes);
                opPerformed = true;
                break;
            case "R":
                $scope.btnsDisabled = false;
                HideError();
                res = "0";
                opPerformed = true;
                break;
            default:
                throw { message: "Invalid argument" };
        }

        return res;
    }

    function modifyArg(char, arg) {
        if (arg == "0") {
            if (char == ".")
                return "0.";
            if (char == "I")
                return "0";
            return char;
        }
        else if (char == "I") {
            if (arg.charAt(0) == "-")
                return arg.substring(1, arg.length);
            return "-" + arg;
        }
        else {
            var newArg = arg + char;
            if (!/^-?([1-9][0-9]*|0)(\.[0-9]*)?$/g.test(newArg))
                return arg;
            digits = newArg.length;
            if (newArg.indexOf(".") > 0)
                --digits;
            if (newArg.charAt(0) == "-")
                --digits;
            return (digits <= maxDigits) ? newArg : arg;
        }
    }

    function resToString(numRes) {
        var res = numRes.toString();
        var len = res.length;
        var limit = maxDigits;
        if (res.charAt(0) == "-")
            ++limit;
        if (len <= limit)
            return res;
        if (res.substring(0, limit).indexOf(".") > 0)
            ++limit;
        return parseFloat(res.substring(0, limit)).toString();
    }

});