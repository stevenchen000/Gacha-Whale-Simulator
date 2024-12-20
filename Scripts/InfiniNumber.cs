using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class InfiniNumber : Resource
{
    [Export] private Array<int> number;
    [Export] private bool negative = false;

    public InfiniNumber()
    {
        number = new Array<int>();
    }

    public InfiniNumber(Array<int> number)
    {
        this.number = new Array<int>();
        this.number.AddRange(number);
    }

    public string GetNumberAsString(){
        string result = "";
        for(int i = number.Count-1; i >= 0; i--)
        {
            int num = number[i];
            if (i == number.Count - 1)
            { //First digits don't have leading 0s
                result += num.ToString();
            }
            else
            {// all other sections have leading 0s
                result += num.ToString("000");
            }

            if (i != 0)
            {//comma after every section except last one
                result += ",";
            }
        }
        return result;
    }

    /*****************
     * Addition
     * *************/

    public static InfiniNumber operator +(InfiniNumber a, InfiniNumber b)
    {
        int sizeA = a.number.Count;
        int sizeB = b.number.Count;
        InfiniNumber result = null;
        if(sizeA > sizeB)
        {
            result = (InfiniNumber)a.Duplicate(true);
            AddBToA(result, b);
        }
        else
        {
            result = (InfiniNumber)b.Duplicate(true);
            AddBToA(result, a);
        }

        return result;
    }

    private static void AddBToA(InfiniNumber a, InfiniNumber b)
    {
        var numA = a.number;
        var numB = b.number;

        for(int i = 0; i < numB.Count; i++)
        {
            numA[i] += numB[i];
            if(numA[i] >= 1000)
            {
                numA[i] -= 1000;
                if(numA.Count > i + 1)
                {
                    numA[i + 1] += 1;
                }
                else
                {
                    numA.Add(1);
                }
            }
        }
    }

    /****************
     * Subtraction
     * *************/
    public static InfiniNumber operator -(InfiniNumber a, InfiniNumber b)
    {
        int sizeA = a.number.Count;
        int sizeB = b.number.Count;
        InfiniNumber result = null;
        if (sizeA > sizeB)
        {
            result = (InfiniNumber)a.Duplicate(true);
            AddBToA(result, b);
        }
        else
        {
            result = (InfiniNumber)b.Duplicate(true);
            AddBToA(result, a);
        }

        return result;
    }
    /// <summary>
    /// A must be bigger than B
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static InfiniNumber SubtractBFromA(InfiniNumber a, InfiniNumber b, bool negative)
    {
        var numsA = a.number;
        var numsB = b.number;
        var numsResult = new Array<int>();
        numsResult.AddRange(numsA);

        for(int i = 0; i < numsB.Count; i++)
        {
            int temp = numsResult[i];
            temp -= numsB[i];
            if(temp < 0)
            {
                temp += 1000;
                numsResult[i + 1] -= 1;
            }
            numsResult[i] = temp;
        }
        for(int i = numsB.Count; i < numsA.Count; i++)
        {
            int temp = numsResult[i];
            if(temp < 0)
            {
                temp += 1000;
                numsResult[i + 1] -= 1;
                numsResult[i] = temp;
            }
            else
            {
                break;
            }
        }

        var result = new InfiniNumber(numsResult);
        result.negative = negative;

        return result;
    }





    /*************
     * Multiplication
     * ***********/

    public static InfiniNumber operator *(InfiniNumber a, InfiniNumber b)
    {
        int sizeA = a.number.Count;
        int sizeB = b.number.Count;
        InfiniNumber result = new InfiniNumber();

        for(int i = 0; i < sizeB; i++)
        {
            int bDigits = b.number[i];
            var temp = MultiplyAByNumAndShift(a, bDigits, i);
            result += temp;
        }
        result.negative = a.negative ^ b.negative;

        return result;
    }

    private static InfiniNumber MultiplyAByNumAndShift(InfiniNumber a, int b, int arrayShifts)
    {
        var resultArray = new Array<int>();
        var numA = a.number;

        for (int i = 0; i < numA.Count; i++)
        {
            resultArray.Add(numA[i] * b);
        }
        int j = 0;
        while(j < resultArray.Count)
        {
            if(resultArray[j] >= 1000)
            {
                int div = resultArray[j] / 1000;
                int rem = resultArray[j] % 1000;
                resultArray[j] = rem;

                if(j == resultArray.Count - 1)
                {
                    resultArray.Add(0);
                }
                resultArray[j + 1] += div;
            }

            j++;
        }

        for(int i = 0; i < arrayShifts; i++)
        {
            resultArray.Insert(0, 0);
        }

        return new InfiniNumber(resultArray);
    }

    private void RemoveStartingZeros(InfiniNumber num)
    {
        var nums = num.number;
        int size = nums.Count;
        int index = size - 1;

        while(index >= 0)
        {
            if(nums[index] == 0)
            {
                nums.RemoveAt(index);
                index--;
            }
            else
            {
                break;
            }
        }

    }

    /******************
     * < Operator
     * **************/

    public static bool operator >(InfiniNumber a, InfiniNumber b)
    {
        int sizeA = a.number.Count;
        int sizeB = b.number.Count;

        bool result = false;

        if(a.negative && !b.negative)
        {
            result = false;
        }else if(!a.negative && b.negative)
        {
            result = true;
        }
        else if(sizeA > sizeB)
        {
            result = true;
        }else if(sizeA < sizeB)
        {
            result = false;
        }
        else
        {
            for(int i = 0; i < sizeA; i++)
            {
                int tempA = a.number[i];
                int tempB = b.number[i];
                if (tempA > tempB)
                {
                    result = true;
                    break;
                }else if(tempB > tempA)
                {
                    result = false;
                    break;
                }
            }
        }

        if (a.negative && b.negative) result = !result;

        return result;
    }

    public static bool operator <(InfiniNumber a, InfiniNumber b)
    {
        return b > a;
    }

    public static bool operator ==(InfiniNumber a, InfiniNumber b)
    {
        var numsA = a.number;
        var numsB = b.number;
        int sizeA = numsA.Count;
        int sizeB = numsB.Count;

        bool result = true;

        if(sizeA != sizeB)
        {
            result = false;
        }
        else if(a.negative != b.negative)
        {
            result = false;
        }
        else
        {
            for(int i = 0; i < sizeA; i++)
            {
                int tempA = numsA[i];
                int tempB = numsB[i];
                if(tempA != tempB)
                {
                    result = false;
                    break;
                }
            }
        }

        return result;
    }

    public static bool operator !=(InfiniNumber a, InfiniNumber b)
    {
        return !(a == b);
    }
}
