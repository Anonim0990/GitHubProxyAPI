namespace WebAPI
{
    public interface IGithubRequestsService
    {
        public IEnumerable<result1> GetRepositories(string username);
        public IEnumerable<result2> GetUserData(string username);
    }
}
