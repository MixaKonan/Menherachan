using System.Collections.Generic;
using Menherachan.Domain.Entities.DBOs;

namespace Menherachan.Domain.Entities.ViewModels
{
    public class ThreadsPreviewViewModel
    {
        public IList<Post> PostsToShow { get; set; }
    }
}