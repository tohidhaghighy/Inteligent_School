using SchoolIntelligentWeb.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface ITadrisshodeha
    {
        List<Tadrisshode> GetTadrisshodeha(int classid,int teacherid,int lessonid);
        Boolean Addtadrisshodeha(int classid,int teacherid,int lessonid, string text, string date);
        List<DomainClasses.Tashvighi> Getalltashvighi(int studentid);
        Boolean addtashvighi(int studentid, string text);

    }
}
