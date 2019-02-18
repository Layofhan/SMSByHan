using SMS.Data.Interface;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Demo.Contracts
{
    public interface IMessageNotificationContract : IScopeDependency, IDependency
    {
        bool IsExistUnread();
        DataResult SearchNotice(int page,int limit);
        DataResult SearchNoticeDetailed(string id);
        DataResult SetNoticeHasRead(List<string> list);
        DataResult SetAllNoticeHasRead(string id);
        DataResult DeleteNotice(List<string> list);
        DataResult SearchHasSendNotices(string id);
        DataResult SearchHasSendNoticesDetailed(string id);
        //发送通知消息
        DataResult SendNoticeMessage(string title, string content, string courseid,string publishid);
        //获取教师对应的课程
        DataResult GetCourseOfTeacher(string teacherid);
    }
}
