namespace DevJobs.Api.Entities
{
    public class JobApplication
    {
        public JobApplication(string applicationName, string applicationEmail, int idVacancy)
        {
            ApplicationName = applicationName;
            ApplicationEmail = applicationEmail;
            IdVacancy = idVacancy;
        }

        public int Id { get; private set; }

        public string ApplicationName { get; private set; }

        public string ApplicationEmail { get; private set; }

        public int IdVacancy { get; private set; }
    }
}
