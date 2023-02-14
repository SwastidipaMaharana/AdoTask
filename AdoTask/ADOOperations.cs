using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AdoTask
{
    
    internal class ADOOperations
    {
        //public string connectionString = @"server = DESKTOP-JOLL2HS\SQLEXPRESS ; database = EmployeeDetailDb ; user id = Swasti; password = Subh";
        public string ConnectionString { get; set; }

        public ADOOperations()
        {
            this.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"];
        }
        SqlConnection con;
        public void ShowOptions()
        {
            Console.WriteLine("Please choose one of the option:");
            Console.WriteLine("1.Add Employee");
            Console.WriteLine("2.List Employees");
            Console.WriteLine("3.Update Employee");
            Console.WriteLine("4.Delete Employee");

            int option = Int32.Parse(Console.ReadLine());

            if (option == 1)
            {
                AddEmployee();
            }
            else if (option == 2)
            {
                GetEmployees();
            }
            else if (option == 3)
            {
                UpdateEmployee();
            }
            else if (option == 4)
            {
                DeleteEmployee();
            }
            else
            {
                Console.WriteLine("Invalid Option");
            }
        }

        public void AddEmployee()
        {
            con = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand(@"insert into employee(emp_id,first_name, last_name, email, dob) 
                    Values 
                    (@EmpId,@FirstName, @LastName, @Email, @DOB);", con);
                con.Open();
                Console.WriteLine("Enter the employee id: ");
                command.Parameters.AddWithValue("@EmpId", Console.ReadLine());
                Console.WriteLine("Enter First Name:");
                command.Parameters.AddWithValue("@FirstName", Console.ReadLine());
                Console.WriteLine("Enter Last Name:");
                command.Parameters.AddWithValue("@LastName", Console.ReadLine());
                Console.WriteLine("Enter Email:");
                command.Parameters.AddWithValue("@Email", Console.ReadLine());
                command.Parameters.AddWithValue("@DOB", DateTime.Now);
                command.ExecuteNonQuery();
        
                Console.WriteLine("Item Saved sucessfully:");
        
                GetEmployees();
                ShowOptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void GetEmployees()
        {
            con = new SqlConnection(ConnectionString);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from employee;", con);
        
                DataSet ds = new DataSet();
                da.Fill(ds, "employee");
        
                Console.WriteLine("Emp Id \t First Name \t Last Name \t Email \t\t\t DOB \t");
        
                foreach (DataRow row in ds.Tables["employee"].Rows)
                {
                    Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t | {4} \t",
                                row[0], row[1], row[2], row[3], row[4]));
                }
                ShowOptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void UpdateEmployee()
        {
            con = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand(@"update employee set first_name = @FirstName,
                last_name= @LastName,
                email = @Email,
                dob = @DOB where emp_id = @ID;", con);
                con.Open();
                Console.WriteLine("Enter Employee ID:");
                command.Parameters.AddWithValue("@ID", Console.ReadLine());
                Console.WriteLine("Enter First Name:");
                command.Parameters.AddWithValue("@FirstName", Console.ReadLine());
                Console.WriteLine("Enter Last Name:");
                command.Parameters.AddWithValue("@LastName", Console.ReadLine());
                Console.WriteLine("Enter Email:");
                command.Parameters.AddWithValue("@Email", Console.ReadLine());
                command.Parameters.AddWithValue("@DOB", DateTime.Now);
                command.ExecuteNonQuery();
                Console.WriteLine("Item updated sucessfully:");
                GetEmployees();
                ShowOptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteEmployee()
        {
            con = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand command = new SqlCommand(@"Delete from employee where emp_id = @ID;", con);
                con.Open();
                Console.WriteLine("Enter Employee ID:");
                command.Parameters.AddWithValue("@ID", Console.ReadLine());
                command.ExecuteNonQuery();
                Console.WriteLine("Item deleted sucessfully:");
                GetEmployees();
                ShowOptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

    }
}
