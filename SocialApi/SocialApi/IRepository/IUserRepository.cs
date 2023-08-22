using SocialApi.Helper;
using SocialApi.Models;
using ZwajApp.api.Models;
using ZwajApp.api.ViewModels;

namespace ZwajApp.api.IRepository;

public interface IUserRepository
{
    void Add<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;

    Task<bool> SaveAll();

    Task<PagedList<User>> GetUsers(UserParams userParams);

    Task<User> GetUser(string id);

    Task<Like> GetLike(string userId,string recipientId);
    Task<bool> IsLikeThisUser (string userId,string recipientId);

    Task<bool> dislikeThisUser(string userId,string recipientId);
    Task<Message> GetMessage(int id);
    Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
    Task<IEnumerable<Message>> GetConversation(string UserId, string Recipientid);
    Task<int> GetUnreadMessagesForUser(string userId);


}




