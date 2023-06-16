/*
 * Description: This file is to ask the user which action they want to do
 * 				and to take all necessary info from the user. This is a GPA 
 * 				calculator so classes can be added and GPA will be calculated. 
 */

using System;
using System.IO;

public class GPA
{
    public static void Main(string[] args)
    {
        // Local variables
        // Input info
        char inputOpt = ' ';
        string inputLine;

        // Class and grade info
        string className = "";
        char grade = ' ';
        string gradeCheck = "";
        int credit = 0;
        double classGPA = 0.0;

        // Calculation Options
        CalculationOptions calOp = new CalculationOptions();

        try
        {
            var isr = new StreamReader(Console.OpenStandardInput());
            Console.SetIn(isr);
            var stdin = Console.In;

            do
            {
                PrintMenu();
                Console.Write("\nWhat action would you like to perform?\n");
                inputLine = stdin.ReadLine().Trim();
                if (inputLine == "")
                {
                    continue;
                }
                inputOpt = inputLine[0];
                inputOpt = char.ToUpper(inputOpt);

                switch (inputOpt)
                {
                    case 'A': // Add a new class and grade
                        Console.Write("Please enter the class information:\n");
                        Console.Write("Enter the class name without spacing:\n");
                        className = stdin.ReadLine().Trim();
                        className = className.ToUpper();
                        Console.Write("Enter the grade (A, B, C, D, E) without a plus or minus:\n");
                        grade = stdin.ReadLine().Trim()[0];
                        grade = char.ToUpper(grade);

                        // Has to be one of the given grades
                        while (grade != 'A' && grade != 'B' && grade != 'C' && grade != 'D' && grade != 'E')
                        {
                            Console.Write("Please enter a valid grade without plus or minus:\n");
                            grade = stdin.ReadLine().Trim()[0];
                            grade = char.ToUpper(grade);
                        }

                        // An E grade does not have a plus or minus 
                        if (grade != 'E')
                        {
                            Console.Write("Enter 'plus' or 'minus' or 'none' for the grade:\n");
                            gradeCheck = stdin.ReadLine().Trim();
                            gradeCheck = gradeCheck.ToUpper();
                        }

                        Console.Write("Enter the number of credits for this class:\n");
                        credit = int.Parse(stdin.ReadLine().Trim());

                        // Check if class exists or not
                        if (calOp.AddGrade(className, grade, gradeCheck, credit, classGPA))
                        {
                            Console.Write("Class added\n");
                        }
                        else
                        {
                            Console.Write("Duplicate class, class NOT added\n");
                        } // End of if statement
                        break;

                    case 'C': // Check GPA
                        Console.Write("\nYour total GPA: " + calOp.TotalGPA().ToString("0.00") + "\n\n");
                        break;

                    case 'L': // List all grades
                        Console.Write("\n" + calOp.ListGrades() + "\n");
                        break;

                    case 'Q': // Quit
                        break;

                    case 'R': // Remove a grade
                        Console.Write("Please enter the class name of the class you want to remove:\n");
                        className = stdin.ReadLine().Trim();

                        // If statement to check if it was removed
                        if (calOp.RemoveGrade(className))
                        {
                            Console.Write(className.ToUpper() + " was removed\n");
                        }
                        else
                        {
                            // The reason it wasn't removed is because it does not exist
                            Console.Write(className.ToUpper() + " was NOT removed\n");
                        } // End of if else statement
                        break;

                    case 'U': // Update a grade
                        Console.Write("Please enter the class name of the grade you want to update:\n");
                        className = stdin.ReadLine().Trim();

                        Console.Write("Please choose what you would like to update in your class:\n" +
                            "grade OR gradeCheck(plus, minus or none) OR credit\n");
                        var cin = stdin.ReadLine().Trim();

                        int decider = 0; // To tell me which of the three is going to be updated

                        // If the incorrect info is not given, repeat until it is given
                        while (!(cin.Equals("grade", StringComparison.OrdinalIgnoreCase) ||
                                 cin.Equals("gradeCheck", StringComparison.OrdinalIgnoreCase) ||
                                 cin.Equals("grade check", StringComparison.OrdinalIgnoreCase) ||
                                 cin.Equals("credit", StringComparison.OrdinalIgnoreCase)))
                        {
                            Console.Write("Please enter a valid option such as \n" +
                                "grade OR gradeCheck(plus, minus or none) OR credit:\n");
                            cin = stdin.ReadLine().Trim();
                        }

                        if (cin.Equals("grade", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Write("Enter the grade (A, B, C, D, E) without a plus or minus:\n");
                            grade = stdin.ReadLine().Trim()[0];

                            Console.Write("Please enter the gradeCheck(plus, minus or none)\n");
                            gradeCheck = stdin.ReadLine().Trim();
                            decider = 1;
                        }
                        else if (cin.Equals("gradeCheck", StringComparison.OrdinalIgnoreCase) ||
                                 cin.Equals("grade check", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Write("Please enter the gradeCheck(plus, minus or none)\n");
                            gradeCheck = stdin.ReadLine().Trim();
                            decider = 2;
                        }
                        else if (cin.Equals("credit", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Write("Enter the number of credits for this class:\n");
                            credit = int.Parse(stdin.ReadLine().Trim());
                            decider = 3;
                        }

                        // If statement to check if it was updated
                        if (calOp.UpdateGrade(className, grade, gradeCheck, credit, decider))
                        {
                            Console.Write(className.ToUpper() + " was updated\n");
                        }
                        else
                        {
                            // It was not updated because it did not exist or it was the same information
                            Console.Write(className.ToUpper() + " was NOT updated\n");
                        } // End of if else statement
                        break;

                    case '?': // Display help
                        PrintMenu();
                        break;

                    default:
                        Console.Write("Unknown action\n");
                        break;
                }

            } while (inputOpt != 'Q' || inputLine.Length != 1);
        }
        catch (IOException exception)
        {
            Console.Write("IO Exception\n");
        }
    }

    private static void PrintMenu()
    {
        // TODO Auto-generated method stub
        Console.WriteLine("Choose the option to calculate your GPA!");
        Console.Write("Choice\t\tAction\n------\t\t------\n" +
                      "A\t\tAdd a grade\n" +
                      "C\t\tCheck total GPA\n" +
                      "L\t\tList all grades\n" +
                      "Q\t\tQuit\n" +
                      "R\t\tRemove a grade\n" +
                      "U\t\tUpdate a grade\n" +
                      "?\t\tDisplay Help\n");
    }
}
