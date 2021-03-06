using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThisDevelopersLife.Models;

namespace ThisDevelopersLife.Services
{
    public class InMemoryShowService : IShowService
    {
        private readonly TDLContext _db;
        
        public InMemoryShowService(TDLContext db)
        {
            _db = db;
        }
        
        public Task AddAsync(Episode episode)
        {
            _db.Add(episode);
            _db.SaveChangesAsync();
            return Task.FromResult(0);
        }
        
        public Task AddAsync(string title, string summary, string slug)
        {
            var episode = new Episode(){
                Title = title,
                Summary = summary,
                Slug = slug
            };
            
            return AddAsync(episode);
        }

        public Task<Episode> FindAsync(string slug)
        {
            var episode = LoadAsync().Result.Where(e => e.Slug == slug).Single();
            
            return Task.FromResult(episode);
        }

        public Task<IEnumerable<Episode>> LoadAsync()
        { 
            var episodes = new List<Episode>() {
                new Episode() {
                    Title = "4.0.1 Faith", 
                    Slug ="4-0-1-Faith", 
                    Summary = "How does our Faith drive us? Do you have Faith in yourself and your abilities, and how do you know you're on the right path? ", 
                    LeadImage = "401-Faith.jpg", 
                    MediaFile = "27-Faith.mp3", 
                    PublishedAt = "8/19/2016"
                },
                new Episode() {
                    Title = "3.0.2 Space", 
                    Slug ="3-0-2-Space", 
                    Summary = "In this episode of This Developer's Life we ask the hard questions about space. Why aren't we on Mars? Why haven't we gone back to the moon? We talk to Paul Lutus, Dan Bricklin, Tamar Cohen, and hear from Ray Bradbury.", 
                    LeadImage = "302-Space.jpg", 
                    MediaFile = "26-Space.mp3", 
                    PublishedAt = "8/19/2016"
                },
                new Episode() {
                    Title = "3.0.1 Cancer", 
                    Slug ="3-0-1-Cancer", 
                    Summary = "Mo and Scott share their thoughts and struggle in this cancer diary they started the day after Mo was diagnosed.", 
                    LeadImage = "301-Cancer.jpg", 
                    MediaFile = "25-Cancer.mp3", 
                    PublishedAt = "8/19/2016"
                },
                new Episode() {
                    Title = "1.1.4 Obsession", 
                    Slug ="1-1-4-Obsession", 
                    Summary = "Rob Sullivan talks about his obsessiveness and how it helps him, and hinders him in his job. Rory Blythe and Scott then talk openly about Rory's compulsion to keep learning - which he has embraced completely.", 
                    LeadImage = "114-Obsession.jpg", 
                    MediaFile = "14-Obsession.mp3", 
                    PublishedAt = "8/19/2016"
               }
            };
            
            _db.SaveChanges();           
            return Task.FromResult(episodes.AsEnumerable());
        }
    }
}