using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;
using ApiUnitest.ApiJob.Api.Repository.Repositories;
using MailKit.Security;
using MimeKit;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ApiJobUnitests.ApiJob.Api.Services
{
    public class JobOpportunityService : IJobOpportunityService
    {
        private readonly IJobOpportunityRepository _jobOpportunityRepository;
        private const string pathExternalService = "http://localhost:8081/";
        public JobOpportunityService(IJobOpportunityRepository jobOpportunityRepository)
        {
            _jobOpportunityRepository = jobOpportunityRepository;
        }

        public async Task Post(JobOpportunity request)
        {
            try{
                await _jobOpportunityRepository.PostJobs(request);
                ValidateUserSuscriptions(request.Name);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void ValidateUserSuscriptions(string name)
        {
            var users = _jobOpportunityRepository.GetUsersFilter(name);

            /*if(users.Result.Count() > 0)
            {
                SendEmailSuscription(users.Result, name);
            }*/
        }

        private static void SendEmailSuscription(IEnumerable<User> users, string position)
        {
            var message = new MimeMessage ();

            foreach(var user in users)
            {
                message.From.Add (new MailboxAddress ("Jobs Subscriptions", "hotmailAccount"));
                message.To.Add (new MailboxAddress (user.UserName, user.Email));
                message.Subject = "NEW JOB SUBSCRIPTIONS OF " + position;

                message.Body = new TextPart ("html") {
                    Text = BodyEmail(user, position)
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient()) {
                    client.Connect("smtp.live.com", 25, SecureSocketOptions.StartTls);
                    client.Authenticate("hotmailAccount", "hotmailPassword");
                    client.Send (message);
                    client.Disconnect (true);
                }
            }
        }

        private static string BodyEmail(User user, string position)
        {
            return "<html><body><h2>Hi " + user.UserName + ".</h2><br><h3>New Job about: " + position + ". </h3><br>. <h3>To unsubscribe click here <a href=''>link</a> </h3></body></html>";
        }

        public List<JobOpportunity> JobSearch(JobOpportunity filters)
        {
            var getListInternalService = GetJobInternal(filters);
            //var getListExternalService = GetJobExternal(filters);

            var listCompleted = new List<JobOpportunity>();
            listCompleted.AddRange(getListInternalService);
            //listCompleted.AddRange(getListExternalService);

            return listCompleted;
        }

        public List<JobOpportunity> GetJobInternal(JobOpportunity filters)
        {
            var jobs = _jobOpportunityRepository.GetJobOpportunitiesList();

            if(filters.Name != null)
            {
                jobs = jobs.Where(x => x.Name == filters.Name).ToList();
            }

            if(filters.Salary != 0)
            {
                jobs = jobs.Where(x => x.Salary == filters.Salary).ToList();
            }

            if(filters.Skill != null)
            {
                jobs = jobs.Where(x => x.Skill == filters.Skill).ToList();
            }

            return jobs;
        }

        private List<JobOpportunity> GetJobExternal(JobOpportunity filter)
        {
            var sourceExternalService = "jobs?name=" + filter.Name + "&salary=" + filter.Salary + "&skill=" + filter.Skill;
            string requestUri = pathExternalService + sourceExternalService;

            var client = new RestClient(requestUri);
            var request = new RestRequest { Method = Method.GET };

            IRestResponse response = client.Execute(request);
            return GetListResponseExternalService(response.Content);
        }

        private List<JobOpportunity> GetListResponseExternalService(string jsonResponse)
        {
            var jsonArray = JArray.Parse(jsonResponse);
            List<JobOpportunity> responseList = new List<JobOpportunity>();

            for (int i = 0; i < jsonArray.LongCount(); i++)
            {
                var responseNew = new JobOpportunity
                {
                    Name = jsonArray[i][0].ToString(),
                    Salary = (int)jsonArray[i][1],
                    Country = jsonArray[i][2].ToString(),
                    Skill = jsonArray[i][3].ToString()
                };

                responseList.Add(responseNew);
            }

            return responseList;
        }

        public async Task PostSuscription(User request)
        {
            await _jobOpportunityRepository.PostUser(request);
        }

        private void SendSuscriptionJob()
        {

        }

        public List<User> GetSuscriptionUser()
        {
            return _jobOpportunityRepository.GetUsersList();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _jobOpportunityRepository.GetUserById(id);
        }

        public async Task<Stream> GetExportFile()
        {
            var users = _jobOpportunityRepository.GetUsersList();
            string fileName = "test.txt";
            var byteArray = Encoding.ASCII.GetBytes(fileName);
            var file = new MemoryStream(byteArray);
            //file.Close();

            return file;
        }

        public string GetSuscriptionNow()
        {
            var users = _jobOpportunityRepository.GetUsersList();

            if(users.Count > 0)
            {
                return "Total users: " + users.Count;
            }

            return "There are no users.";
        }

        public Task<int> GetSumValueAndTotal(int value)
        {
            return Task.FromResult(1500 + value);
        }

        public int GetSumValueOneAndValueTwo(int value1, int value2)
        {
            return value1 + value2;
        }

        public int GetSumValue1Value2Value3Value4(int value1, int value2,int value3, int value4)
        {
            return value1 + value2 + value3 + value4;
        }

        public string BodyEmailHtml(User user, string position)
        {
            return "<html><body><h2>Hi " + user.UserName + ".</h2><br><h3>New Job about: " + position + ". </h3><br>. <h3>To unsubscribe click here <a href=''>link</a> </h3></body></html>";
        }

        public async Task<StreamWriter> CreateFile(string fileName)
        {
            string rutaCompleta = @".\Files\" + fileName;
            string text = "test 01 writee....";

            StreamWriter file = new StreamWriter(rutaCompleta, true);
            file.WriteLine(text);

            return file;
        }

        public bool ReturnBool(int age)
        {
            return age > 15;
        }

        public IEnumerable<User> ReturnListUser()
        {
            return GetSuscriptionUser();
        }

        public Task DeleteUser(User request)
        {
            return _jobOpportunityRepository.DeleteUser(request);
        }
    }
}