﻿namespace Rockying.Models
{
    public enum NotificationType
    {
        NoReadingUpdate = 1
    }
    public enum BookReviewEmotionType
    {
        Like =1,
        Dislike = 2
    }
    public enum SearchItemType
    {
        Story = 1,
        Book = 2
    }
    public enum ReadStatusType
    {
        Read = 1,
        Reading = 2,
        WanttoRead = 3
    }
    public enum PostStatusType
    {
        Draft = 1,
        Publish = 2,
        Inactive = 3
    }

    public enum PageCommentType
    {
        General = 1
    }

    public enum GeneralStatusType
    {
        Active = 0,
        Inactive = 1,
        Deleted = 2,
        Unverified = 3
    }

    public enum MemberTypeType
    {
        Admin = 1,
        Author = 2,
        Member = 3,
        Reader = 4
    }
}

namespace Rockying
{
    public enum AlertType
    {
        Success,
        Warning,
        Error,
        Info
    }
}