using Microsoft.EntityFrameworkCore;
using SongListApp.Data;

namespace SongListApp.Services
{
    public class SongService
    {
        private readonly AppDbContext _context;
        public SongService(AppDbContext context) => _context = context;

        public async Task<List<Song>> GetSongsAsync()
        {
            return await _context.Songs.ToListAsync();
        }

        public async Task<Song?> GetSongByIdAsync(string id)
        {
            return await _context.Songs.FindAsync(id);
        }

        public async Task<Song> InsertSongAsync(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task<Song?> UpdateSongAsync(string id, Song s)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null) return null;
            song.Title = s.Title;
            song.Artist = s.Artist;
            song.Year = s.Year;
            _context.Songs.Update(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task<Song?> DeleteSongAsync(string id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null) return null;
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return song;
        }

        private bool SongExists(string id) => _context.Songs.Any(e => e.Id == id);
    }
}
