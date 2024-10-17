using SQLite;
using System;

namespace WordMate.Models
{
    public class Word
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Translation { get; set; }
        public string Definition { get; set; }
        public bool IsFavorite { get; set; }

        public int CategoryId { get; set; }
        public int SuccessCount { get; set; }

        [Ignore]
        public Category Category { get; set; }
        public Word()
        {
            Id = Guid.NewGuid();
            CategoryId = 1;
            IsFavorite = false;
            SuccessCount = 0;
        }
        public Word(string text, string translation, string definition, 
            int categoryId = 1, bool isFavorite = false, int successCount = 0)
        {
            Id = Guid.NewGuid();
            Text = text;
            Translation = translation;
            Definition = definition;
            CategoryId = categoryId;
            IsFavorite = isFavorite;
            SuccessCount = successCount;
        }
        public Word(Guid id, string text, string translation, string definition, 
            int categoryId, bool isFavorite, int successCount)
        {
            Id = id;
            Text = text;
            Translation = translation;
            Definition = definition;
            CategoryId = categoryId;
            IsFavorite = isFavorite;
            SuccessCount = successCount;
        }
    }
}
