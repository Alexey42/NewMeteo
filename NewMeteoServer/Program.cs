using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewMeteoServer
{
    class Program
    {
        private class AuthRequestForm
        {
            public string Name { get; set; }
            public string Password { get; set; }
            public string Type { get; set; }
        }

        static void Main(string[] args)
        {
            Listen();
        }

        private static Task Listen()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8888/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                string responseString = "";
                string json;
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    json = reader.ReadToEnd();
                }
                var reqData = JsonConvert.DeserializeObject<AuthRequestForm>(json);

                DBContext db = new DBContext();
                User user = FindUser(db, reqData.Name);

                if (reqData.Type == "Sign in")
                    responseString = SignIn(db, reqData, user);
                else
                    responseString = SignUp(db, reqData, user);

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }

        static string SignIn(DBContext db, AuthRequestForm data, User user)
        {
            string result = "";

            if (user == null || user.Password != data.Password)
                result = "Wrong name or password";
            else
            {
                result = "ok";
            }

            return result;
        }

        static string SignUp(DBContext db, AuthRequestForm data, User user)
        {
            string result = "";

            if (user != null)
                result = "Name is already registered";
            else
            {
                try
                {
                    db.Users.Add(new User { Name = data.Name, Password = data.Password, Role = "Developer" });
                    db.SaveChanges();
                    result = "ok";
                }
                catch
                {
                    result = "Database isn't responding";
                }
            }

            return result;
        }

        static User FindUser(DBContext db, string param)
        {
            var list = db.Users.ToList();
            foreach (var user in list)
            {
                if (user.Name == param)
                    return user;
            }
            return null;
        }
    }
}
