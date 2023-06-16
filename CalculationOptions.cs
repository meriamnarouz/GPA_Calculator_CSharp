/*
 * Description: This file is for all the helper methods to add grades, get total GPA,
 * 				remove a grade, update a grade, or list all grades. It is where the 
 * 				actual setting up is done. 
 */

using System;
using System.Collections.Generic;

public class CalculationOptions
{
    List<Grades> gradeList; //list of grades

    public CalculationOptions()
    {
        gradeList = new List<Grades>();
    }

    //method adds the class and its grade if it is not a duplicate class
    public bool AddGrade(string className, char grade, string gradeCheck, int credit, double classGPA)
    {
	//for loop to check if it is already an existing class
        foreach (Grades g in gradeList)
        {
            if (g.GetClassName().Equals(className, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }

	//if it is not an existing class then add it
        Grades newGrade = new Grades(className, grade, gradeCheck, credit, classGPA);
        newGrade.SetClassGPA(classGPA);

        gradeList.Add(newGrade);
        return true;
    }

    //list all information of every class if there are classes in the list
    public string ListGrades()
    {
        if (gradeList.Count > 0)
        {
            string result = "";
            foreach (Grades g in gradeList)
            {
                result += g.ToString();
            }
            return result;
        }
        else
        {
            return "\nNo grades listed!\n\n";
        }
    }

    //method to remove specific classes
    public bool RemoveGrade(string className)
    {
        for (int i = 0; i < gradeList.Count; i++)
        {
	    //remove by class name
            if (gradeList[i].GetClassName().Equals(className, StringComparison.OrdinalIgnoreCase))
            {
                gradeList.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    //method to update class information
    public bool UpdateGrade(string className, char grade, string gradeCheck, int credit, int decider)
    {
	//for loop to find the class that needs to be updated
        for (int i = 0; i < gradeList.Count; i++)
        {
            if (gradeList[i].GetClassName().Equals(className, StringComparison.OrdinalIgnoreCase))
            {
		//update based on if it is grade or gradeCheck or credits
                if (decider == 1)
                {
		    //update the grade and gradeCheck
                    if (gradeList[i].GetGrade() == char.ToUpper(grade) || gradeList[i].GetGradeCheck().Equals(gradeCheck, StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                    else
                    {
                        gradeList[i].SetGrade(grade);
                        gradeList[i].SetGradeCheck(gradeCheck);
                        gradeList[i].SetClassGPA(0.0);
                        return true;
                    }
                }
                else if (decider == 2)
                {
		    //update the gradeCheck
                    if (gradeList[i].GetGradeCheck().Equals(gradeCheck, StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                    else
                    {
                        gradeList[i].SetGradeCheck(gradeCheck);
                        gradeList[i].SetClassGPA(0.0);
                        return true;
                    }
                }
                else if (decider == 3)
                {
		    //update the credits
                    if (gradeList[i].GetCredit() != credit)
                    {
                        gradeList[i].SetCredit(credit);
                        gradeList[i].SetClassGPA(0.0);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        return false;
    }

    //method to return GPA of all classes together
    public double TotalGPA()
    {
        double total = 0.0;

        foreach (Grades g in gradeList)
        {
            total += g.GetClassGPA();
        }

        if (gradeList.Count > 0)
        {
            total /= gradeList.Count;
        }

        return total;
    }
}