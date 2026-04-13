using static System.Console;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StudentManagementSystem
{
    class Student
    {
        private static int studentCount;

        public string studentId { get; set; }
        public string name { get; set; }
        public int[] grades { get; set; }

        static Student()
        {
            studentCount = 0;
            WriteLine("Student Management System Initialized.");
        }

        public Student(string name, string studentId, int[] grades)
        {
            this.studentId = studentId;
            this.name = name;
            this.grades = grades;
            studentCount++;
        }

        //destructor used to remove any objects of the Student class from memory.
        ~Student()
        {
            WriteLine($"Student {studentId} has been deleted.");
        }

        //method used to calculate average grade of a student
        public double aveGrade()
        {
            int sumOfGrades = 0;

            foreach (int grade in grades)
            {
                sumOfGrades += grade;
            }

            return grades.Length > 0 ? (double)sumOfGrades / grades.Length : 0;
        }

        //use of regular expression to validate the format of the student ID
        public static bool ValidateStudentId(string studentId)
        {
            return Regex.IsMatch(studentId, @"^S\d{5}$");
        }
    }

    class Program
    {
        static List<Student> studentList = new List<Student>();

        static void Main()
        {
            
            int[] grades1 = { 90, 87, 85 };
            int[] grades2 = { 78, 93, 82 };

            studentList.Add(new Student("John Doe", "S12345", grades1));
            studentList.Add(new Student("Dante Rios", "S54321", grades2));

            WriteLine("Two initial students have been added.\n");

            bool run = true;
            int choice;

            do
            {
                Write("\nMenu:\n" +
                      "1. Add Student\n" +
                      "2. Remove Student\n" +
                      "3. Display All Students\n" +
                      "4. Search for Student\n" +
                      "5. Exit\n" +
                      "Choose an option: ");

                choice = Convert.ToInt32(ReadLine());

                switch (choice)
                {
                    case 1:
                        addStudent();
                        break;

                    case 2:
                        removeStudent();
                        break;

                    case 3:
                        displayStudents(studentList);
                        break;

                    case 4:
                        searchStudent();
                        break;

                    case 5:
                        run = false;
                        WriteLine("Exiting program...");
                        break;

                    default:
                        WriteLine("Invalid option! Try again.");
                        break;
                }

            } while (run);
        }

        static void addStudent()
        {
            //try-catch block used to handle invalid ID input
            try                         
            {
                bool validId = false;
                string studentId = "";

                //do...while loop used to allow user to enter the ID until a correct ID is entered
                do
                {
                    Write("Enter Student ID (Format: S12345): ");
                    studentId = ReadLine();

                    if (Student.ValidateStudentId(studentId))
                    {
                        validId = true;
                    }
                    else
                    {
                        WriteLine("Invalid Student ID format. Example: S12345");
                    }

                } while (!validId);

                Write("Enter Student Name: ");
                string name = ReadLine();

                Write("How many grades do you want to enter? ");
                int gradeCount = Convert.ToInt32(ReadLine());

                int[] grades = new int[gradeCount];

                for (int i = 0; i < grades.Length; i++)
                {
                    Write($"Enter grade {i + 1}: ");
                    grades[i] = Convert.ToInt32(ReadLine());
                }

                studentList.Add(new Student(name, studentId, grades));
                WriteLine("Student successfully added.");
            }
            catch (FormatException ex)
            {
                WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                WriteLine($"Unexpected Error: {ex.Message}");
            }
        }

        static void removeStudent()
        {
            Write("Enter Student ID to remove: ");
            string studentId = ReadLine();

            Student foundStudent = studentList.Find(s => s.studentId == studentId);

            if (foundStudent != null)
            {
                studentList.Remove(foundStudent);
                WriteLine("Student deleted.");
            }
            else
            {
                WriteLine("Student not found.");
            }
        }

        static void displayStudents(List<Student> studentList)
        {
            if (studentList.Count == 0)
            {
                WriteLine("No students found.");
                return;
            }

            WriteLine("\n--- Student List ---");

            //foreach loop used to display all students in memory
            foreach (Student s in studentList)
            {
                WriteLine($"ID: {s.studentId} | Name: {s.name} | Average: {s.aveGrade():F2}");
            }
        }

        static void searchStudent()
        {
            Write("Enter Student ID to search: ");
            string studentId = ReadLine();

            Student foundStudent = studentList.Find(s => s.studentId == studentId);

            if (foundStudent != null)
            {
                WriteLine($"\nStudent Found:\nID: {foundStudent.studentId} | Name: {foundStudent.name} | Average Grade: {foundStudent.aveGrade():F2}");
            }
            else
            {
                WriteLine("\nStudent not found.");
            }
        }
    }
}
