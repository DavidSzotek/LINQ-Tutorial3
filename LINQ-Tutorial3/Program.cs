using System.Linq;

 /*
 * https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/standard-query-operators-overview
 */

class Program
{
    private static void Main(string[] args)
    {
        List<Employee> employeeList = Data.GetEmployees();
        List<Department> departmentList = Data.GetDepartments();

        /**** Method syntax - Inner Join ****/
        var methodInnerResults = departmentList.Join(employeeList,
            department => department.Id,
            employee => employee.DepartmentId,
            (department, employee) => new
            {
                FullName = employee.FirstName + " " + employee.LastName,
                AnnualSalary = employee.AnnualSalary,
                DepartmentName = department.LongName,
            }
            );

        Console.WriteLine($"******* METHOD INNER JOIN *******");
        foreach (var result in methodInnerResults)
        {
            Console.WriteLine($"METHOD INNER - {result.FullName}'s salary from {result.DepartmentName} is {result.AnnualSalary}");
        }

        Console.WriteLine("\n");
        /**** Method syntax - Left Join ****/
        var methodLeftResults = departmentList.GroupJoin(employeeList,
            dept => dept.Id,
            emp => emp.DepartmentId,
            (dept, empGroup) => new
            {
                Employees = empGroup,
                DepartmentName = dept.LongName
            }
            );

        Console.WriteLine($"******* METHOD LEFT JOIN *******");
        foreach (var result in methodLeftResults)
        {
            Console.WriteLine($"Department: {result.DepartmentName}");
            foreach(var employee in result.Employees)
            {
                Console.WriteLine($"\t{employee.FirstName + " " + employee.LastName}");
            }
        }
        Console.WriteLine("\n");

        /**** Query syntax - Inner Join ****/
        var queryResults = from employee in employeeList
                           join department in departmentList
                           on employee.DepartmentId equals department.Id
                           select new
                           {
                               FullName = employee.FirstName + " " + employee.LastName,
                               AnnualSalary = employee.AnnualSalary,
                               DepartmentName = department.LongName,
                           };

        Console.WriteLine($"******* QUERY INNER JOIN *******");

        foreach (var result in queryResults)
        {
            Console.WriteLine($"{result.FullName}'s salary from {result.DepartmentName} is {result.AnnualSalary}");
        }

        Console.WriteLine("\n");

        /**** Query syntax - Left Join with ToList ****/
        var queryLeftResults = (from dept in departmentList
                               join emp in employeeList
                               on dept.Id equals emp.DepartmentId
                               into employeeGroup // into keyword is used to creat group of employees for each department
                               select new
                               {
                                   Employees = employeeGroup,
                                   DepartmentName = dept.LongName
                               }).ToList();

        Console.WriteLine($"******* QUERY LEFT JOIN *******");
        foreach (var result in queryLeftResults)
        {
            Console.WriteLine($"Department: {result.DepartmentName}");
            foreach (var employee in result.Employees)
            {
                Console.WriteLine($"\t{employee.FirstName + " " + employee.LastName}");
            }
        }
        Console.WriteLine("\n");
        Console.ReadKey();
    }
}

public class Department
{
    public int Id { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
}

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal AnnualSalary { get; set; }
    public bool IsManager { get; set; }
    public int DepartmentId { get; set; }
}

public static class Data
{
    public static List<Employee> GetEmployees()
    {
        List<Employee> employees = new List<Employee>();

        Employee employee = new Employee()
        {
            Id = 1,
            FirstName = "Bob",
            LastName = "Jones",
            AnnualSalary = 60000.3m,
            IsManager = true,
            DepartmentId = 1,
        };
        employees.Add(employee);
        employee = new Employee()
        {
            Id = 2,
            FirstName = "Sarah",
            LastName = "Jameson",
            AnnualSalary = 80000.1m,
            IsManager = true,
            DepartmentId = 2,
        };
        employees.Add(employee);
        employee = new Employee()
        {
            Id = 3,
            FirstName = "Douglas",
            LastName = "Roberts",
            AnnualSalary = 40000.2m,
            IsManager = false,
            DepartmentId = 2,
        };
        employees.Add(employee);
        employee = new Employee()
        {
            Id = 4,
            FirstName = "Jane",
            LastName = "Stevens",
            AnnualSalary = 30000.2m,
            IsManager = false,
            DepartmentId = 1,
        };
        employees.Add(employee);
        employee = new Employee()
        {
            Id = 1,
            FirstName = "David",
            LastName = "Szotek",
            AnnualSalary = 75000.3m,
            IsManager = true,
            DepartmentId = 3,
        };
        employees.Add(employee);
        employee = new Employee()
        {
            Id = 1,
            FirstName = "Dominik",
            LastName = "Foniok",
            AnnualSalary = 60000.3m,
            IsManager = true,
            DepartmentId = 1,
        };
        employees.Add(employee);
        employee = new Employee()
        {
            Id = 1,
            FirstName = "Klara",
            LastName = "Mezes",
            AnnualSalary =80000.3m,
            IsManager = true,
            DepartmentId = 3,
        };
        employees.Add(employee);

        return employees;
    }

    public static List<Department> GetDepartments()
    {
        List<Department> departments = new List<Department>();

        Department department = new Department()
        {
            Id = 1,
            ShortName = "HR",
            LongName = "Human Resources"
        };
        departments.Add(department);

        department = new Department()
        {
            Id = 2,
            ShortName = "FN",
            LongName = "Finance"
        };
        departments.Add(department);

        department = new Department()
        {
            Id = 3,
            ShortName = "TE",
            LongName = "Technology"
        };
        departments.Add(department);

        department = new Department()
        {
            Id = 4,
            ShortName = "SC",
            LongName = "Security"
        };
        departments.Add(department);

        return departments;
    }
}