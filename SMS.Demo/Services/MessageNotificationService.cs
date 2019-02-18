using SMS.Data.Interface;
using SMS.Demo.Common;
using SMS.Demo.Contracts;
using SMS.Entity;
using SMS.Entity.Models;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Demo.Services
{
    public class MessageNotificationService: IMessageNotificationContract
    {
        public IRepository<SMSNotice, int> NoticeRepository { set; get; }
        public IRepository<SMSNoticeParticular, int> SMSNoticeParticularRepository { set; get; }
        public IRepository<SMSNotifyContact, int> SMSNotifyContactRepository { set; get; }
        public IRepository<SMSUser, string> UserRepository { set; get; }
        
        public IRepository<SMSCourseStudentMap, int> CourseStudentMapRepository { set; get; }
        public IRepository<SMSTeacherCourseMap, int> TeacherCourseMapRepository { set; get; }
        public IRepository<SMSCourse, string> CourseRepository { set; get; }
        public IQueryable<SMSNotice> Notices
        {
            get { return NoticeRepository.Entities; }
        }
        public IQueryable<SMSNoticeParticular> SMSNoticeParticulars
        {
            get { return SMSNoticeParticularRepository.Entities; }
        }
        public IQueryable<SMSNotifyContact> SMSNotifyContacts
        {
            get { return SMSNotifyContactRepository.Entities; }
        }
        public IQueryable<SMSUser> Users
        {
            get { return UserRepository.Entities; }
        }
        public IQueryable<SMSCourseStudentMap> CourseStudentMap
        {
            get { return CourseStudentMapRepository.Entities; }
        }
        public IQueryable<SMSTeacherCourseMap> TeacherCourseMap
        {
            get { return TeacherCourseMapRepository.Entities; }
        }
        public IQueryable<SMSCourse> Course
        {
            get { return CourseRepository.Entities; }
        }
        //查找是否有未读消息
        public bool IsExistUnread()
        {
            var userid = IdentityManager.GetIdentFromAll().UserId;
            var list = SMSNotifyContacts.Where(a => a.ReceiverId == userid && a.Marker == 0).ToList();
            if(list.Count > 0)
            {
                return true;
            }
            return false; 
        }
        //查询用户收到的通知
        public DataResult SearchNotice(int page, int limit)
        {
            var userid = IdentityManager.GetIdentFromAll().UserId;
            try
            {
                var list = SMSNotifyContacts.Where(a => a.ReceiverId == userid).Join(Users, d => d.PublisherId, e => e.Id, (d, e) => new
                {
                    PublisherId = d.PublisherId,
                    Marker = d.Marker,
                    NoticeId = d.NoticeId,
                    PublisherName = e.Name
                }).Join(Notices, b => b.NoticeId, c => c.NoticeId, (b, c) => new
                {
                    NoticeId = b.NoticeId,
                    Marker = b.Marker,
                    PublisherName = b.PublisherName,
                    Title =  c.Title,//"<a href='/Notification/SearchNoticeDetailed?id=" + b.NoticeId + "'>" + c.Title + "</a>",
                    PublishDate = c.PublishDate
                }).OrderByDescending(q => q.PublishDate).ToList();
                if (page != 0 && limit != 0)
                {
                    list = list.Skip(limit * (page - 1)).Take(limit).ToList();
                }
                if(list != null)
                {
                    //匿名对象 为只读，所以采用以下方法解决 改写匿名对象的问题
                    List<dynamic> newpayList = new List<dynamic>();
                    list.ForEach(t => {
                        //new 一个ExpandoObject对象
                        dynamic d = new System.Dynamic.ExpandoObject();
                        d.NoticeId = t.NoticeId;
                        d.Marker = t.Marker;
                        d.PublisherName = t.PublisherName;
                        d.Title = t.Title;
                        d.PublishDate = t.PublishDate;
                        newpayList.Add(d);
                    });
                    foreach(var l in newpayList)
                    {
                        if (l.Marker == 0)
                            l.Title = "<a href='/Notification/NoticeDetailedInterface?id=" + l.NoticeId + "'>" + l.Title + "</a>&nbsp<span class='layui-badge-dot'></span>";
                        else
                            l.Title = "<a href='/Notification/NoticeDetailedInterface?id=" + l.NoticeId + "'>" + l.Title + "</a>";
                    }
                    return DataProcess.Success(newpayList);
                }
                return DataProcess.Failure("没有通知消息");
                //if (list != null)
                //{
                //    foreach (var l in list)
                //    {
                //        l.Title = "<a href='/Notification/SearchNoticeDetailed?id=" + l.NoticeId + "'>" + l.Title + "</a>";
                //    }
                //    return DataProcess.Success(list);
                //}
                //else
                //    return DataProcess.Failure("没有数据");
            }
            catch (Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
        //查询通知详情
        public DataResult SearchNoticeDetailed(string id)
        {
            try
            {
                IdentityTicket ident = IdentityManager.GetIdentFromAll();
                var useid = ident.UserId;
                SMSNotifyContact noticecontract = SMSNotifyContacts.Where(a => a.NoticeId == id && a.ReceiverId == useid).FirstOrDefault();
                noticecontract.Marker = 1;
                SMSNotifyContactRepository.Update(noticecontract);
                return DataProcess.Success(
                        Notices.Where(a => a.NoticeId == id).Join(SMSNoticeParticulars,b => b.NoticeId,c => c.NoticeId,(b ,c) => new {
                            Title = b.Title,
                            PublishDate = b.PublishDate,
                            Contents = c.Contents
                        }).FirstOrDefault()
                 );
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
        //更新通知为已读
        public DataResult SetNoticeHasRead(List<string> list)
        {
            try
            {
                foreach (var a in list)
                {
                    SMSNotifyContact noticecontract = SMSNotifyContacts.Where(b => b.NoticeId == a.ToString()).FirstOrDefault();
                    noticecontract.Marker = 1;
                    SMSNotifyContactRepository.Update(noticecontract);
                }
                return DataProcess.Success("Updata success");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure("Have some problem!Come try or contact manager");
            }
        }
        //更新所有通知为已读
        public DataResult SetAllNoticeHasRead(string id)
        {
            try
            {
                List<SMSNotifyContact> list = SMSNotifyContacts.Where(a => a.ReceiverId == id && a.Marker == 0).ToList();
                foreach (var li in list)
                {
                    li.Marker = 1;
                    SMSNotifyContactRepository.Update(li);
                }
                return DataProcess.Success("Updata success");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure("Have some problem!Come try or contact manager");
            }
        }
        //删除所有通知
        public DataResult DeleteNotice(List<string> list)
        {
            try
            {
                foreach (var li in list)
                {
                    var notice = SMSNotifyContacts.Where(a => a.NoticeId == li).FirstOrDefault();
                    SMSNotifyContactRepository.Delete(notice);
                }
                return DataProcess.Success("Delete Success");
            }
            catch(Exception ex)
            {
                return DataProcess.Failure("Have some problem!Come try or contact manager");
            }
        }
        //查询历史发送通知
        public DataResult SearchHasSendNotices(string id)
        {
            try
            {
                //查询ID的通知发送记录
                var list = SMSNotifyContacts.Where(a => a.PublisherId == id).GroupBy(b => b.NoticeId).Select(c => c.FirstOrDefault()).Join(Notices, d => d.NoticeId, e => e.NoticeId, (d, e) => new {
                    NoticeId = d.NoticeId,
                    Marker = d.Marker,
                    Title = e.Title,
                    PublishDate = e.PublishDate
                }).OrderByDescending(f => f.PublishDate).ToList();
                return DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure("The system has problems.");
            }

        }
        //查询历史发送通知详情
        public DataResult SearchHasSendNoticesDetailed(string id)
        {
            try
            {
                return DataProcess.Success(
                        Notices.Where(a => a.NoticeId == id).Join(SMSNoticeParticulars, b => b.NoticeId, c => c.NoticeId, (b, c) => new {
                            Title = b.Title,
                            PublishDate = b.PublishDate,
                            Contents = c.Contents
                        }).FirstOrDefault()
                 );
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult SendNoticeMessage(string title, string content, string courseid, string publishid)
        {
            try
            {
                var studentidlist = CourseStudentMap.Where(a => a.CourseId == courseid).Select(b => new {
                                        StudentId = b.StudentId
                                    }).ToList();
                if(studentidlist.Count == 0)
                {
                    return DataProcess.Failure("No students chose this course");
                }
                string noticeid = RandomNumber.Init();
                SMSNotice notice = new SMSNotice();
                notice.NoticeId = noticeid;
                notice.Title = title;
                notice.PublishDate = DateTime.Now;
                SMSNoticeParticular noticeparticular = new SMSNoticeParticular();
                noticeparticular.Contents = content;
                noticeparticular.NoticeId = noticeid;

                NoticeRepository.Insert(notice,false);
                SMSNoticeParticularRepository.Insert(noticeparticular,false);

                SMSNotifyContact notifycontact;
                foreach (var li in studentidlist) {
                    notifycontact = new SMSNotifyContact();
                    notifycontact.PublisherId = publishid;
                    notifycontact.ReceiverId = li.StudentId;
                    notifycontact.Marker = 0;
                    notifycontact.NoticeId = noticeid;
                    SMSNotifyContactRepository.Insert(notifycontact);
                }
                return DataProcess.Success();
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }

        public DataResult GetCourseOfTeacher(string teacherid)
        {
            try
            {
                var list = TeacherCourseMap.Where(a => a.TeacherId == teacherid).Select(b => new {
                                CourseId = b.CourseId
                           }).Join( Course, c => c.CourseId, d => d.Id, (c, d) => new {
                                Id = c.CourseId,
                                Name = d.Name
                           }).ToList();
                if (list.Count == 0) return DataProcess.Failure("There is no information about the teacher's course");
                return DataProcess.Success(list);
            }
            catch(Exception ex)
            {
                return DataProcess.Failure(ex.Message);
            }
        }
    }
}
