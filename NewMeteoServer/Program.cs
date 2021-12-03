using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static NewMeteoServer.ServerRequestForms;

namespace NewMeteoServer
{
    class Program
    {

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

                var seg = request.Url.Segments;
                if (seg[1] == "auth")
                {
                    var reqData = JsonConvert.DeserializeObject<AuthRequestForm>(json);

                    DBContext db = new DBContext();
                    User user = FindUser(db, reqData.Name);

                    if (reqData.Type == "Sign in")
                        responseString = SignIn(db, reqData, user);
                    else
                        responseString = SignUp(db, reqData, user);
                }
                if (seg[1] == "sendmap")
                {
                    var reqData = JsonConvert.DeserializeObject<MapRequestForm>(json);
                    DBContext db = new DBContext();
                    responseString = AddMap(db, reqData);

                }
                if (seg[1] == "getmap/")
                {
                    if (seg.Length < 3)
                        responseString = "Not found";
                    else
                    {
                        DBContext db = new DBContext();
                        MapDB found = GetMap(db, seg[2]);
                        if (found == null)
                            responseString = "Not found";
                        else
                        {
                            var u = new MapRequestForm { Name = found.Name, Bytes = found.Bytes, Values = found.Values };
                            responseString = JsonConvert.SerializeObject(u);
                        }
                    }
                }

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
                SessionInfo.ActiveUsers.Add(user);
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
                    SessionInfo.ActiveUsers.Add(user);
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

        static string AddMap(DBContext db, MapRequestForm map)
        {
            db.Maps.Add(new MapDB { Name = map.Name, Bytes = map.Bytes, Values = map.Values });
            db.SaveChanges();

            return "ok";
        }

        static MapDB GetMap(DBContext db, string param)
        {
            var list = db.Maps.ToList();
            foreach (var map in list)
            {
                if (map.Name == param)
                    return map;
            }
            return null;
        }
    }
}
