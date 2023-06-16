/*
 * Description: This file contains the getters and setters for all info for a singular class. 
 */

using System;

public class Grades
{
    private string className;
    private char grade;
    private string gradeCheck;
    private int credit;
    private double classGPA;

    public Grades(string className, char grade, string gradeCheck, int credit, double classGPA)
    {
        this.className = className;
        this.grade = grade;
        this.gradeCheck = gradeCheck;
        this.credit = credit;
        SetClassGPA(classGPA);
    }

    //getter methods
    public string GetClassName()
    {
        return className;
    }

    public char GetGrade()
    {
        return grade;
    }

    public string GetGradeCheck()
    {
        return gradeCheck;
    }

    public int GetCredit()
    {
        return credit;
    }

    public double GetClassGPA()
    {
        return classGPA;
    }

    //setter methods
    public void SetGrade(char grade)
    {
        this.grade = char.ToUpper(grade);
    }

    public void SetGradeCheck(string gradeCheck)
    {
        this.gradeCheck = gradeCheck.ToUpper();
    }

    public void SetCredit(int credit)
    {
        this.credit = credit;
    }

    public void SetClassGPA(double classGPA)
    {
        switch (GetGrade())
        {
            case 'A':
                classGPA = 4.00;
                break;

            case 'B':
                classGPA = 3.00;
                break;

            case 'C':
                classGPA = 2.00;
                break;

            case 'D':
                classGPA = 1.00;
                break;

            default:
                break;
        }

        if (GetGrade() != 'E')
        {
            if (string.Equals("plus", GetGradeCheck(), StringComparison.OrdinalIgnoreCase))
            {
                classGPA += 0.33;
            }
          else if (string.Equals("minus", GetGradeCheck(), StringComparison.OrdinalIgnoreCase))
            {
                classGPA -= 0.33;
            }
        }

        this.classGPA = classGPA;
    }

    //method to get the complete result 
    public override string ToString()
    {
        string str = "";
        string gc = "";

        if (!string.Equals("none", gradeCheck, StringComparison.OrdinalIgnoreCase))
        {
            gc = gradeCheck;
        }

        str = "Class Name: " + className
            + "\n     Grade: " + grade + " " + gc
            + "\n    Credit: " + credit
            + "\n Class GPA: " + classGPA.ToString("0.00") + "\n\n";
        return str;
    }
}
