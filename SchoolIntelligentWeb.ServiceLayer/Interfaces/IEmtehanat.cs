using SchoolIntelligentWeb.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.ServiceLayer.Interfaces
{
    public interface IEmtehanat
    {
        List<Emtehanat> GetEmtehanat(int classid);
        Boolean AddEmtehanat(int classid, string text, string date);
    }
}
