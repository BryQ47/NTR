﻿function Calculator(limit) {

    var result = 0;
    var operation = "N";
    var resultLimit = limit;

    function internalReset() {
        result = 0;
        operation = "N";
    }

    function calculateResult(arg, op, percent) {
        switch (operation) {
            case "A":
                if (percent)
                    result *= (1 + arg / 100);
                else
                    result += arg;
                break;
            case "S":
                if (percent)
                    result *= (1 - arg / 100);
                else
                    result -= arg;
                break;
            case "M":
                if (percent)
                    arg /= 100;
                result *= arg;
                break;
            case "D":
                if (percent)
                    arg /= 100;
                result /= arg;
                break;
            case "R":
                result = Math.sqrt(arg);
                break;
            default:
                result = arg;
        }
        if (!isFinite(result)) {
            internalReset();
            throw { message: "Result is not a number" };
        }
        if (result > resultLimit) {
            internalReset();
            throw { message: "Result is too big" };
        }
        operation = op;
        return result;
    }

    this.performOperation = calculateResult;

    this.getResult = function (arg, type) {
        if (type == "P")
            return calculateResult(arg, "N", true);
        if (type == "R")
            operation = "R";
        return calculateResult(arg, "N");
    }
}