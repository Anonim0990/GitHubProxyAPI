namespace WebAPI
{
    public interface IRequestsService
    {
        public IEnumerable<RepositoryData> GetRepositories(string username);
        public IEnumerable<UserData> GetUserData(string username);
    }
}