using Services.Entity;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Interface
{
    public interface IChapterService
    {
        Task<Chapter> CreateChapter(ChapterModel req);
        Task<bool> UpdateChapter(ChapterModel req, int id);
        Task<bool> DeleteChapter(int id);
        Task<Chapter> GetChapterById(int id);
        Task<List<Chapter>> GetChaptersAsync();
    }
}
