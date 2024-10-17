using System;

namespace WordMate.Models
{
    public class Word
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Translation { get; set; }
        public string Definition { get; set; }
        public string Category { get; set; }
        public bool IsFavorite { get; set; }

        public Word(string text, string translation, string definition, string category = "learning", bool isFavorite = false)
        {
            Id = Guid.NewGuid();
            Text = text;
            Translation = translation;
            Definition = definition;
            Category = category;
            IsFavorite = isFavorite;
        }
        public Word(Guid id, string text, string translation, string definition, string category, bool isFavorite)
        {
            Id = id;
            Text = text;
            Translation = translation;
            Definition = definition;
            Category = category;
            IsFavorite = isFavorite;
        }
    }
}
