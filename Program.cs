using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEtudiants
{
    public class Student
    {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(int studentNumber, string firstName, string lastName)
        {
            StudentNumber = studentNumber;
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public class Course
    {
        public int CourseNumber { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }

        public Course(int courseNumber, string courseCode, string courseTitle)
        {
            CourseNumber = courseNumber;
            CourseCode = courseCode;
            CourseTitle = courseTitle;
        }
    }

    public class Grade
    {
        private Student student;
        private Course course;
        private double score;

        public Grade(Student student, Course course, double score)
        {
            this.student = student;
            this.course = course;
            this.score = score;
        }

        public int StudentNumber
        {
            get { return student.StudentNumber; }
        }

        public int CourseNumber
        {
            get { return course.CourseNumber; }
        }

        public double Score
        {
            get { return score; }
            set { score = value; }
        }
    }

    public static class Data
    {
        private static List<Student> students = new List<Student>();
        private static List<Course> courses = new List<Course>();
        private static List<Grade> grades = new List<Grade>();

        public static Student GetStudent(int studentNumber)
        {
            foreach (Student student in students)
            {
                if (student.StudentNumber == studentNumber)
                {
                    return student;
                }
            }
            return null;
        }

        public static Course GetCourse(int courseNumber)
        {
            foreach (Course course in courses)
            {
                if (course.CourseNumber == courseNumber)
                {
                    return course;
                }
            }
            return null;
        }

        public static Grade GetGrade(int studentNumber, int courseNumber)
        {
            foreach (Grade grade in grades)
            {
                if (grade.StudentNumber == studentNumber && grade.CourseNumber == courseNumber)
                {
                    return grade;
                }
            }
            return null;
        }


        public static void AddStudent(Student student)
        {

            students.Add(student);
        }

        public static void AddCourse(Course course)
        {
            courses.Add(course);
        }

        public static void AddGrade(Grade grade)
        {
            grades.Add(grade);
        }

        public static List<Grade> GetGradesForStudent(int studentNumber)
        {
            List<Grade> studentGrades = new List<Grade>();
            foreach (Grade grade in grades)
            {
                if (grade.StudentNumber == studentNumber)
                {
                    studentGrades.Add(grade);
                }
            }
            return studentGrades;
        }

        public static void SaveDataToFile()
        {
            foreach (Student student in students)
            {
                string studentData = student.StudentNumber + "," + student.FirstName + "," + student.LastName + Environment.NewLine;
                string gradeData = "";
                foreach (Grade grade in grades)
                {
                    if (grade.StudentNumber == student.StudentNumber)
                    {
                        gradeData += grade.CourseNumber + "," + grade.Score + Environment.NewLine;
                    }
                }
                string fileName = student.StudentNumber + ".txt";
                File.WriteAllText(fileName, studentData + gradeData);
            }
        }
    }

    class Program
    {
        static void AjouterEtudiant()
        {
            Console.WriteLine("\nAjout d'un nouvel étudiant");

            Console.WriteLine("Entrez le numéro d'étudiant :");
            int studentNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrez le prénom :");
            string firstName = Console.ReadLine();

            Console.WriteLine("Entrez le nom de famille :");
            string lastName = Console.ReadLine();

            Student student = new Student(studentNumber, firstName, lastName);
            Data.AddStudent(student);

            Console.WriteLine("Étudiant ajouté avec succès !");
        }

        static void AjouterCours()
        {
            Console.WriteLine("\nAjout d'un nouveau cours");

            Console.WriteLine("Entrez le numéro de cours :");
            int courseNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrez le code de cours :");
            string courseCode = Console.ReadLine();

            Console.WriteLine("Entrez le titre de cours :");
            string courseTitle = Console.ReadLine();

            Course course = new Course(courseNumber, courseCode, courseTitle);
            Data.AddCourse(course);

            Console.WriteLine("Cours ajouté avec succès !");
        }

        static void AjouterNote()
        {
            Console.WriteLine("\nAjout d'une nouvelle note");

            Console.WriteLine("Entrez le numéro d'étudiant :");
            int studentNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrez le numéro de cours :");
            int courseNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrez la note :");
            double score = double.Parse(Console.ReadLine());

            // Vérification si l'étudiant et le cours existent
            Student student = Data.GetStudent(studentNumber);
            Course course = Data.GetCourse(courseNumber);
            if (student == null || course == null)
            {
                Console.WriteLine("Erreur : L'étudiant ou le cours n'existe pas.");
                return;
            }

            // Vérification si une note pour cet étudiant et ce cours existe déjà
            Grade existingGrade = Data.GetGrade(studentNumber, courseNumber);
            if (existingGrade != null)
            {
                Console.WriteLine("Erreur : Une note pour cet étudiant et ce cours existe déjà.");
                return;
            }

            // Création de la note et ajout à la liste des notes
            Grade grade = new Grade(student, course, score);
            Data.AddGrade(grade);

            Console.WriteLine("Note ajoutée avec succès !");
        }

        static void AfficherNotesEtudiant()
        {
            Console.WriteLine("\nAffichage des notes d'un étudiant");

            Console.WriteLine("Entrez le numéro d'étudiant :");
            int studentNumber = int.Parse(Console.ReadLine());

            List<Grade> grades = Data.GetGradesForStudent(studentNumber);

            if (grades.Count == 0)
            {
                Console.WriteLine("Aucune note pour cet étudiant.");
            }
            else
            {
                Console.WriteLine("Notes de l'étudiant :");
                foreach (Grade grade in grades)
                {
                    Console.WriteLine($"- {grade.StudentNumber} : {grade.Score}");
                }
            }

        }
        private static List<Student> students = new List<Student>();
        private static List<Course> courses = new List<Course>();
        private static List<Grade> grades = new List<Grade>();

        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans l'application de gestion des étudiants!");

            while (true)
            {
                Console.WriteLine("\nQue voulez-vous faire?");
                Console.WriteLine("1- Ajouter un étudiant");
                Console.WriteLine("2- Ajouter un cours");
                Console.WriteLine("3- Ajouter une note");
                Console.WriteLine("4- Afficher les notes d'un étudiant");
                Console.WriteLine("5- Quitter");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        AjouterEtudiant();
                        break;
                    case "2":
                        AjouterCours();
                        break;
                    case "3":
                        AjouterNote();
                        break;
                    case "4":
                        AfficherNotesEtudiant();
                        break;
                    case "5":
                        Console.WriteLine("Merci d'avoir utilisé notre application!");
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
               
            }
        }
    }


}
