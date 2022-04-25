namespace WebAPI
{

    public class RepositoryData
    {
        public string? repositoryName { get; set; }
        public string? repositorylanguagesAndBytes { get; set; }
    }
    public class UserData
    {
        public string? userLogin { get; set; }
        public string? userName { get; set; }
        public string? userBio { get; set; }
        public string? languagesAndBytes { get; set; }
    }
    public interface IRequestsService
    {
        public IEnumerable<RepositoryData> GetRepositories(string username);
        public IEnumerable<UserData> GetUserData(string username);
    }
}