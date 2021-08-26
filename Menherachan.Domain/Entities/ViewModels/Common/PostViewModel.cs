using System;
using System.Collections.Generic;

namespace Menherachan.Domain.Entities.ViewModels.Common
{
    public class PostViewModel
    {
        public int ThreadId { get; set; }
        public int PostId { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Comment { get; set; }
        public DateTime PostedAt { get; set; }
        public bool IsPinned { get; set; }
        public bool IsWrittenByOp { get; set; }
        public AdminViewModel? Admin { get; set; }
        public List<FileViewModel> Files { get; set; } = new();

        public PostViewModel()
        {
        }

        public PostViewModel(
            int threadId, int postId,
            string author, string email, string subject, string comment,
            DateTime postedAt,
            bool isPinned, bool isWrittenByOp,
            AdminViewModel? admin, List<FileViewModel> files)
        {
            ThreadId = threadId;
            PostId = postId;
            Author = author;
            Email = email;
            Subject = subject;
            Comment = comment;
            PostedAt = postedAt;
            IsPinned = isPinned;
            IsWrittenByOp = isWrittenByOp;
            Admin = admin;
            Files = files;
        }
    }
}