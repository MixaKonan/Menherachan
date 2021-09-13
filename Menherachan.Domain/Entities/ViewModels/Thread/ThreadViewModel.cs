using System.Collections.Generic;
using Menherachan.Domain.Entities.ViewModels.Common;

namespace Menherachan.Domain.Entities.ViewModels.Thread
{
    public class ThreadViewModel
    {
        public int ThreadId { get; set; }

        public List<PostViewModel> Posts { get; set; }
    }
}