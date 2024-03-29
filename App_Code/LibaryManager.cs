﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rockying.Models
{
    public class LibaryManager
    {
        public static Book GetBook(int id)
        {
            using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
            {
                Book b = dc.Books.FirstOrDefault(t => t.ID == id);
                return b;
            }
        }

        public static MemberBook GetMemberBook(int id)
        {
            using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
            {
                MemberBook b = dc.MemberBooks.FirstOrDefault(t => t.ID == id);
                return b;
            }
        }
        public static void AddBookToLibrary(Book b, Member m, ReadStatusType rs)
        {
            try
            {
                using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
                {
                    MemberBook mb = dc.MemberBooks.FirstOrDefault(t => t.MemberID == m.ID && t.BookID == b.ID);
                    if (mb == null)
                    {
                        mb = new MemberBook()
                        {
                            BookID = b.ID,
                            MemberID = m.ID,
                            Review = string.Empty,
                            ReadStatus = (byte)rs,
                            CurrentPage = 0,
                            Photo = string.Empty,
                            TimesRead = 0
                        };
                        if (rs == ReadStatusType.Reading)
                            mb.ReadingStartDate = DateTime.Now;
                        dc.MemberBooks.InsertOnSubmit(mb);
                    }
                    else
                    {
                        if (mb.ReadStatus != (byte)ReadStatusType.Reading && rs == ReadStatusType.Reading)
                        {
                            mb.ReadingStartDate = DateTime.Now;
                            mb.CurrentPage = 0;
                        }
                        mb.ReadStatus = (byte)rs;
                    }
                    dc.SubmitChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public static bool UpdateReadingProgress(Book b, Member m, int pagecount)
        {
            using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
            {
                MemberBook mb = dc.MemberBooks.FirstOrDefault(t => t.BookID == b.ID && t.MemberID == m.ID && t.ReadStatus == (byte)ReadStatusType.Reading);
                if (mb != null)
                {
                    if (b.PageCount <= pagecount)
                    {
                        mb.ReadStatus = (byte)ReadStatusType.Read;
                        mb.TimesRead += 1;
                        mb.CurrentPage = b.PageCount;
                        mb.ReadingUpdateDate = DateTime.Now;
                    }
                    else
                    {
                        mb.CurrentPage = pagecount;
                        mb.ReadingUpdateDate = DateTime.Now;
                    }
                    dc.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

        public static void SetEmotion(Book b, Member m, BookReviewEmotionType emotion)
        {
            using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
            {
                MemberBook mb = dc.MemberBooks.FirstOrDefault(t => t.BookID == b.ID && t.MemberID == m.ID);
                if (mb != null)
                    mb.Emotion = (byte)emotion;
                else
                {
                    mb = new MemberBook()
                    {
                        BookID = b.ID,
                        Emotion = (byte)emotion,
                        MemberID = m.ID,
                        Photo = b.CoverPage,
                        Review = string.Empty,
                        ReadStatus = (byte)ReadStatusType.Read,
                        TimesRead = 1
                    };
                    dc.MemberBooks.InsertOnSubmit(mb);
                }
                dc.SubmitChanges();
            }
        }

        public static bool PostReview(Book b, Member m, string review)
        {
            using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
            {
                MemberBook mb = dc.MemberBooks.FirstOrDefault(t => t.BookID == b.ID && t.MemberID == m.ID);
                if (mb != null)
                {
                    mb.Review = review.Length > 3000 ? review.Substring(0, 2999) : review;
                    dc.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

        public static void SaveSearchHistory(string keywords, int resultcount, Member m = null)
        {
            using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
            {
                SearchHistory sh = new SearchHistory()
                {
                    Keywords = keywords,
                    ResultCount = resultcount,
                    Member = null,
                    SearchDate = DateTime.Now,
                    SearchType = (int)SearchItemType.Book
                };
                dc.SearchHistories.InsertOnSubmit(sh);
                dc.SubmitChanges();
            }
        }

        public static bool UpdatePhotoMemberBook(Book b, Member m, string photo)
        {
            using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
            {
                MemberBook mb = dc.MemberBooks.FirstOrDefault(t => t.BookID == b.ID && t.MemberID == m.ID);
                if (mb != null)
                {
                    mb.Photo = photo;
                    dc.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

        public static bool RemoveFromLibrary(Book b, Member m)
        {
            using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
            {
                MemberBook mb = dc.MemberBooks.FirstOrDefault(t => t.BookID == b.ID && t.MemberID == m.ID);
                if (mb != null)
                {
                    dc.MemberBooks.DeleteOnSubmit(mb);
                    dc.SubmitChanges();
                    return true;
                }
            }
            return false;
        }
    }
}