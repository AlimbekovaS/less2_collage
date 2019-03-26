using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace College
{
    class Program
    {
        public static string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = null;
        static void Main(string[] args)
        {
            //new Program().CreateTable();

            //new Program().InsertData();
            //new Program().UpdateData();
            //new Program().GetData();
            //new Program().spInsertData();
            new Program().spGetData();



        }
        public void CreateTable()
        {
            try
            {
                cmd = new SqlCommand("create table student(id int not null, name varchar(100), email varchar(50), join_date date)", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table create Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong."+e);    
            }
            finally
            {
                conn.Close();
            }
        }
        public void InsertData()
        {
            try
            {
                cmd = new SqlCommand("insert into student(id, name, email, join_date)values('101','Ronald Trump', 'fuyguih', '05/10/2019')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record Insert Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong"+e);
                throw;
            }
            finally
            {
                conn.Close();
            }

        }
        public void UpdateData()
        {
            try
            {
                cmd = new SqlCommand("update student set name='Donald Trump' where id=101", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record Updated Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong" + e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public void GetData()
        {
            try
            {
                cmd = new SqlCommand("select * from student", conn);
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
               //cmd.ExecuteNonQuery();
                while (sdr.Read())
                {
                    Console.WriteLine(sdr["id"]+" "+sdr["name"]+" "+sdr["email"]+" "+sdr["join_date"].ToString()); //displaying record
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong" + e);
                
            }
            finally
            {
                conn.Close();
            }


        }
        public void spInsertData()
        {
            int id = 105;
            string name = "Saltant";
            string email = "alimbekovasalta";
            DateTime join_date = DateTime.Now;
            conn.Open();
           
            SqlCommand cmd = new SqlCommand("InsertData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@id",
                Value = id
            };
            cmd.Parameters.Add(idParam);

            SqlParameter nameParam = new SqlParameter
            {
                ParameterName = "@name",
                Value = name
            };
            cmd.Parameters.Add(nameParam);

            SqlParameter emailParam = new SqlParameter
            {
                ParameterName = "@email",
                Value = email
            };
            cmd.Parameters.Add(emailParam);
            SqlParameter join_dateParam = new SqlParameter
            {
                ParameterName = "@join_date",
                Value = join_date
            };
            cmd.Parameters.Add(join_dateParam);
            
        }

        public void spGetData()
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("spGetData", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3));

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string email = reader.GetString(2);
                        DateTime date = reader.GetDateTime(3);
                        Console.WriteLine("{0} \t{1} \t{2} \t{3}", id, name, email, date);
                    }
                }
                reader.Close();
            }
        }
    }
    
}
