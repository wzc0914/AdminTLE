using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connString = ConfigurationManager.AppSettings["CONN_READ"];
            // IDbConnection conn = new SqlConnection(connString);
            // string query = "SELECT top 10 * FROM t_hotel";
            // //无参数查询，返回列表，带参数查询和之前的参数赋值法相同。
            //var  list= conn.Query<THotel>(query).ToList();
            // Console.Write(list.Count());
            OneToMany(connString);
            Console.ReadLine();
        }

        private static void OneToOne(string sqlConnectionString)
        {
            List<Customer> userList = new List<Customer>();
            using (IDbConnection conn = new SqlConnection(sqlConnectionString))
            {
                string sqlCommandText = @"SELECT c.UserId,c.Username AS UserName,
c.PasswordHash AS [Password],c.Email,c.PhoneNumber,c.IsFirstTimeLogin,c.AccessFailedCount,
c.CreationDate,c.IsActive,r.RoleId,r.RoleName 
    FROM dbo.CICUser c WITH(NOLOCK) 
INNER JOIN CICUserRole cr ON cr.UserId = c.UserId 
INNER JOIN CICRole r ON r.RoleId = cr.RoleId";
                userList = conn.Query<Customer, Role, Customer>(sqlCommandText,
                                                                (user, role) => { user.Role = role; return user; },
                                                                null,
                                                                null,
                                                                true,
                                                                "RoleId",
                                                                null,
                                                                null).ToList();
            }

            if (userList.Count > 0)
            {
                userList.ForEach((item) => Console.WriteLine("UserName:" + item.UserName +
                                                             "----Password:" + item.Password +
                                                             "-----Role:" + item.Role.RoleName +
                                                             "\n"));

                Console.ReadLine();
            }
        }


        private static void OneToMany(string sqlConnectionString)
        {
            Console.WriteLine("One To Many");
            List<User> userList = new List<User>();

            using (IDbConnection connection = new SqlConnection(sqlConnectionString))
            {

                string sqlCommandText3 = @"SELECT c.UserId,
       c.Username      AS UserName,
       c.PasswordHash  AS [Password],
       c.Email,
       c.PhoneNumber,
       c.IsFirstTimeLogin,
       c.AccessFailedCount,
       c.CreationDate,
       c.IsActive,
       r.RoleId,
       r.RoleName,
	   d.id as  DepartId,
	   d.Department  as DepartName
FROM   dbo.CICUser c
      left  JOIN CICUserRole cr
            ON  cr.UserId = c.UserId
       left JOIN CICRole r
            ON  r.RoleId = cr.RoleId
       left join CIDepartInfo d on  d.id=c.departid        ";

                var lookUp = new Dictionary<int, User>();
                userList = connection.Query<User, Role, Department, User>(sqlCommandText3,
                    (user, role, depart) =>
                    {
                        User u;
                        if (!lookUp.TryGetValue(user.UserId, out u))
                        {
                            lookUp.Add(user.UserId, u = user);
                        }
                        u.department = depart;
                        u.Role.Add(role);
                        return user;
                    }, null, null, true, "RoleId,DepartId", null, null).ToList();
                userList = lookUp.Values.ToList();
            }

            if (userList.Count > 0)
            {
                userList.ForEach((item) => Console.WriteLine("UserName:" + item.UserName +
                                             "----Password:" + item.Password +
                                             "-----Role:" + item.Role.First().RoleName +
                                             "\n"));

                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No Data In UserList!");
            }
        }
    }

    public class THotel
    {
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name_en { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string star { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string add_province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string add_city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string add_qu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string add_loc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string add_label { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string themetype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img_big { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img_small { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zb_canyin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zb_gouwu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zb_yulei { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zb_ditie { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zb_jingdian { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ss_tongyong { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ss_fuwu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ss_sheshi { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zc_ruzhu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zc_ertong { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zc_chongwu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zc_pay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal minprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime updatetime { get; set; }
    }
}
