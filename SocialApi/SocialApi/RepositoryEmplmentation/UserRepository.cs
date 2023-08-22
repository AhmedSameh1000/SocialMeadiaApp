using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialApi.Helper;
using SocialApi.Models;
using System.Security.Claims;
using ZwajApp.api.Data;
using ZwajApp.api.IRepository;
using ZwajApp.api.Models;
using ZwajApp.api.ViewModels;

namespace ZwajApp.api.RepositoryEmplmentation;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _Context;


    public UserRepository(AppDbContext Context)
    {
        this._Context = Context;

    }

    public void Add<T>(T entity) where T : class
    {
        _Context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _Context.Remove(entity);
    }

    public async Task<Like> GetLike(string userId, string recipientId)
    {
        return await _Context.Likes.
            FirstOrDefaultAsync(l => l.LikerId == userId && l.LikeeId == recipientId);
    }

    public Task<User> GetUser(string id)
    {

        var User = _Context.Users.Include(f => f.Photos).FirstOrDefaultAsync(c => c.Id == id);
        return User;
    }

    public async Task<PagedList<User>> GetUsers(UserParams userParams)
    {
        var Users = _Context.Users.Include(f => f.Photos).Where(c => c.Id != userParams.UserId).AsQueryable();
        if (userParams.likers)
        {
            var userLikers = await GetUserLikes(userParams.UserId, userParams.likers);
            Users = Users.Where(u => userLikers.Contains(u.Id));
        }
        if (userParams.likees)
        {
            var userLikees = await GetUserLikes(userParams.UserId, userParams.likers);
            Users = Users.Where(u => userLikees.Contains(u.Id));
        }
        return await PagedList<User>.CreateAsync(Users, userParams.pageNumber, userParams.PageSize);
    }

    private async Task<IEnumerable<string>> GetUserLikes(string id, bool likers)
    {
        var User = await _Context.Users.Include(c => c.Likers).Include(c => c.Likeees).FirstOrDefaultAsync(c => c.Id == id);
        if (likers)
        {
            return User.Likers.Where(u => u.LikeeId == id).Select(c => c.LikerId);
        }
        else
        {
            return User.Likeees.Where(u => u.LikerId == id).Select(c => c.LikeeId);
        }
    }

    public async Task<bool> SaveAll()
    {
        return await _Context.SaveChangesAsync() > 0;
    }

    public async Task<bool> IsLikeThisUser(string userId, string recipientId)
    {
        var islikeThisUser = await _Context.Likes.FirstOrDefaultAsync(c => c.LikerId == userId && c.LikeeId == recipientId);
        if (islikeThisUser != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public async Task<bool> dislikeThisUser(string userId, string recipientId)
    {
        var alreadyDislikethisuser = await _Context.Likes.FirstOrDefaultAsync(c => c.LikerId == userId && c.LikeeId == recipientId);
        if (alreadyDislikethisuser != null)
        {
            _Context.Likes.Remove(alreadyDislikethisuser);
            await _Context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<Message> GetMessage(int id)
    {
        var Message = await _Context.Messages.FirstOrDefaultAsync(m => m.id == id);
        return Message;
    }

    public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
    {
        var Messages = _Context.Messages.Include(m => m.sender).ThenInclude(u => u.Photos).
             Include(m => m.recipient).ThenInclude(u => u.Photos).AsQueryable();

        switch (messageParams.MessageType)
        {
            case "Inbox":
                Messages = Messages.Where(m => m.recipientId == messageParams.UserId);
                break;

            case "Outbox":
                Messages = Messages.Where(m => m.senderId == messageParams.UserId);
                break;

            default:
                Messages = Messages.Where(m => m.recipientId == messageParams.UserId && !m.isRead);
                break;
        }
        Messages = Messages.OrderByDescending(m => m.MessageSend);

        return await PagedList<Message>.CreateAsync(Messages, messageParams.pageNumber, messageParams.PageSize);
    }

    public async Task<IEnumerable<Message>> GetConversation(string UserId, string Recipientid)
    {
        var Messages = await _Context.Messages.Include(m => m.sender).ThenInclude(u => u.Photos).
             Include(m => m.recipient).ThenInclude(u => u.Photos)
             .Where(c => c.recipientId == UserId && c.senderId == Recipientid ||
             c.recipientId == Recipientid && c.senderId == UserId
             ).OrderBy(m => m.id).ToListAsync();
        return Messages;
    }

    public async Task<int> GetUnreadMessagesForUser(string userId)
    {
        var Messages = await _Context.Messages.Where(m => m.isRead == false && m.recipientId == userId).ToListAsync();
        var Count = Messages.Count();
        return Count;
    }
}
