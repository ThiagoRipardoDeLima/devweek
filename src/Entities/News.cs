using MongoDB.Bson.Serialization.Attributes;

using src.Enum;

namespace src.Entities;

public class News
{
    public News(string hat, string title, string text, string author, string img, string link, Status status)
    {
        Hat     = hat;
        Title   = title;
        Text    = text;
        Author  = author;
        Img     = img;
        Link    = link;
        Status  = status;
        PublishDate = DateTime.Now;

    }

    public Status ChangeStatus(Status status)
    {
        switch (status)
        {
            case Status.Active:
                status = Status.Active;
                break;

            case Status.Inactive:
                status = Status.Inactive;
                break;

            case Status.Draft:
                status = Status.Draft;
            break;
        }

        return status;
    }

   [BsonElement("hat")]
   public string Hat { get; private set; }

   [BsonElement("title")]
   public string Title { get; private set; }

   [BsonElement("text")]
   public string Text { get; private set; }

   [BsonElement("author")]
   public string Author { get; private set; }

   [BsonElement("img")]
   public string Img { get; private set; }

   [BsonElement("link")]
   public string Link { get; private set; }

   public DateTime PublishDate { get; private set; }

   [BsonElement("active")]
   public Status Status { get; private set; }
    
}