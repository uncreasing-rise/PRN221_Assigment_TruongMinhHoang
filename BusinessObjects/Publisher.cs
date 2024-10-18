using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string PublisherName { get; set; } = null!;

    public string? ContactEmail { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
