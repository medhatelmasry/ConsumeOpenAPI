using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        const string BASE_URL = "https://api4all.azurewebsites.net";

        static async Task Main(string[] args)
        {
            //await getStudentById("A00111111");

            //await addStudent();
            //await updateStudentById("A00123456");

            await deleteStudentById("A00123456");
            await displayStudents();
        }


        private static async Task displayStudents()
        {
            using (var httpClient = new HttpClient())
            {
                var client = new swaggerClient(BASE_URL, httpClient);
                var items = await client.StudentsAllAsync().ConfigureAwait(false);
                foreach (var i in items)
                {
                    Console.WriteLine($"{i.StudentId}\t{i.FirstName}\t{i.LastName}\t{i.School}");
                }
            }
        }

        private static async Task getStudentById(string id)
        {
            using (var httpClient = new HttpClient())
            {
                var client = new swaggerClient(BASE_URL, httpClient);
                var item = await client.Students2Async(id);
                Console.WriteLine($"{item.StudentId}\t{item.FirstName}\t{item.LastName}\t{item.School}");
            }
        }

        private static async Task addStudent()
        {
            using (var httpClient = new HttpClient())
            {
                var client = new swaggerClient(BASE_URL, httpClient);
                var student = new Student()
                {
                    StudentId = "A00123456",
                    FirstName = "Joe",
                    LastName = "Roy",
                    School = "Forestry"
                };

                try
                {
                    var response = await client.StudentsAsync(student);
                }
                catch (ApiException apiEx)
                {
                    // not really error because server returns HTTP Status Code 201
                    Console.WriteLine(apiEx.ToString());
                }
            }
        }

        private static async Task updateStudentById(string id)
        {
            using (var httpClient = new HttpClient())
            {
                var client = new swaggerClient(BASE_URL, httpClient);
                var student = new Student()
                {
                    StudentId = id,
                    FirstName = "Pam",
                    LastName = "Day",
                    School = "Nursing"
                };
                await client.Students3Async(id, student);
            }
        }

        private static async Task deleteStudentById(string id)
        {
            using (var httpClient = new HttpClient())
            {
                var client = new swaggerClient(BASE_URL, httpClient);
                var item = await client.Students4Async(id);
            }
        }
    }
}
